using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WebP.Net.Helpers;
using WebP.Net.Natives;

namespace WebP.Net
{
	public abstract class WebPObject : IDisposable
	{
		protected abstract Func<BitmapData, (IntPtr Pointer, int Size)> Encoder { get; }
		
		private (IntPtr Pointer, int Size) DynamicArray { get; set; }

		private object CacheLockHandle { get; } = new();

		private byte[] _bytesCache;

		private byte[] BytesCache
		{
			get
			{
				if (DynamicArray.Pointer == IntPtr.Zero)
					return Array.Empty<byte>();

				lock (CacheLockHandle)
				{
					if (_bytesCache is not null)
						return _bytesCache;
					_bytesCache = new byte[DynamicArray.Size];
					Marshal.Copy(DynamicArray.Pointer, _bytesCache, 0, DynamicArray.Size);
					return _bytesCache;
				}
			}
		}

		[method: MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void VerifyImage(Image image)
		{
			switch (image)
			{
				case null:
					throw ThrowHelper.NullReferenced(nameof(image));
				case { Width: 0 } or { Height: 0 }:
					throw ThrowHelper.ContainsNoData();
				case { Width: > WebpMaxDimension } or { Height: > WebpMaxDimension }:
					throw ThrowHelper.SizeTooBig();
			}
		}

		[method: MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static Bitmap ConvertTo32Argb(Image image)
		{
			var bitmap  = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb);
			using var graphic = Graphics.FromImage(bitmap);
			graphic.DrawImage(image, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
			return bitmap;
		}

		[method: MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static BitmapData GetData(Bitmap bitmap)
		{
			return bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
			                       ImageLockMode.ReadOnly,
			                       bitmap.PixelFormat);
		}
		
		[method: MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void Encode(Image image)
		{
			VerifyImage(image);
			using var  bitmap = ConvertTo32Argb(image);
			BitmapData data   = null;
			try
			{
				data         = GetData(bitmap);
				DynamicArray = Encoder(data);
			}
			finally
			{
				if (data is not null) bitmap.UnlockBits(data);
			}
		}

		private const int WebpMaxDimension = 16383;

		public void Dispose()
		{
			if (DynamicArray.Pointer != IntPtr.Zero)
				Native.WebPFree(DynamicArray.Pointer);
		}
	}
}
