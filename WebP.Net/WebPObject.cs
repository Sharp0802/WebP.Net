using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WebP.Net.Helpers;
using WebP.Net.Natives;
using WebP.Net.Natives.Enums;
using WebP.Net.Natives.Structs;

namespace WebP.Net
{
	public abstract class WebPObject : IDisposable
	{
		private const int WebpMaxDimension = 16383;

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
				case {Width: 0} or {Height: 0}:
					throw ThrowHelper.ContainsNoData();
				case {Width: > WebpMaxDimension} or {Height: > WebpMaxDimension}:
					throw ThrowHelper.SizeTooBig();
			}
		}

		[method: MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static Bitmap ConvertTo32Argb(Image image)
		{
			var       bitmap  = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb);
			using var graphic = Graphics.FromImage(bitmap);
			graphic.DrawImage(image, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
			return bitmap;
		}

		[method: MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static BitmapData GetData(Bitmap bitmap, ImageLockMode mode)
		{
			return bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), mode, bitmap.PixelFormat);
		}

		[method: MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void Encode(Image image)
		{
			VerifyImage(image);
			using var  bitmap = ConvertTo32Argb(image);
			BitmapData data   = null;
			try
			{
				data         = GetData(bitmap, ImageLockMode.ReadOnly);
				DynamicArray = Encoder(data);
			}
			finally
			{
				if (data is not null) bitmap.UnlockBits(data);
			}
		}

		[method: MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static WebPInfo GetFrom(IntPtr pointer, int size)
		{
			var features = new WebPBitstreamFeatures();
			var status   = Native.WebPGetFeatures(pointer, size, ref features);
			if (status is not Vp8StatusCode.Ok)
				throw new ExternalException(status.ToString());
			return new WebPInfo(features);
		}

		private delegate int DecodeInto(IntPtr ptr, int size, IntPtr output, int outputSize, int outputStride);
		
		[method: MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static Bitmap Decode(IntPtr pointer, int size)
		{
			Bitmap     bmp    = null;
			BitmapData data   = null;
			int        length;

			try
			{
				var info = GetFrom(pointer, size);
				bmp = new Bitmap(info.Width, info.Height, info.HasAlpha 
					                 ? PixelFormat.Format32bppArgb 
					                 : PixelFormat.Format24bppRgb); 
				data = GetData(bmp, ImageLockMode.WriteOnly);
				length = ((DecodeInto) (info.HasAlpha 
					? Native.WebPDecodeBgraInto
					: Native.WebPDecodeBgrInto))
				   .Invoke(pointer, size, data.Scan0, data.Stride * info.Height, data.Stride);
				
			}
			finally
			{
				if (data is not null) bmp.UnlockBits(data);
			}

			if (length is 0)
				throw ThrowHelper.CannotEncodeByUnknown();

			return bmp;
		}

		public abstract WebPObject Create(byte[] webp);

		public abstract WebPObject Create(Image image);

		public void Dispose()
		{
			if (DynamicArray.Pointer != IntPtr.Zero)
				Native.WebPFree(DynamicArray.Pointer);
		}
	}
}
