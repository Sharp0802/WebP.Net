using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using WebP.Net.Helpers;
using WebP.Net.Natives;

namespace WebP.Net
{
	public static class WebPEncoder
	{
		public static byte[] EncodeLossy(Bitmap image, float quality)
		{
			if (quality is < 0f or > 100f)
				throw ThrowHelper.QualityOutOfRange();

			return EncodeBase(image, data =>
			{
				var size = Native.WebPEncodeBgra(
				                                 data.Scan0,
				                                 data.Width,
				                                 data.Height,
				                                 data.Stride,
				                                 quality,
				                                 out var ptr);
				if (size is 0)
					throw ThrowHelper.CannotEncodeByUnknown();

				return (ptr, size);
			});
		}

		public static byte[] EncodeLossless(Bitmap image)
		{
			return EncodeBase(image, data =>
			{
				var size = Native.WebPEncodeLosslessBgra(
				                                         data.Scan0,
				                                         data.Width,
				                                         data.Height,
				                                         data.Stride,
				                                         out var ptr);
				if (size is 0)
					throw ThrowHelper.CannotEncodeByUnknown();

				return (ptr, size);
			});
		}

		internal const int WebpMaxDimension = 16383;

		private static byte[] EncodeBase(Image image, Func<BitmapData, (IntPtr Ptr, int Size)> encoder)
		{
			static void ValidateImage(Image image)
			{
				if (image is null)
					throw ThrowHelper.NullReferenced(nameof(image));
				if (image.Width is 0 || image.Height is 0)
					throw ThrowHelper.ContainsNoData();
				if (image.Width > WebpMaxDimension || image.Height > WebpMaxDimension)
					throw ThrowHelper.SizeTooBig();
			}

			static BitmapData LockBitsAsReadonly(Bitmap bmp)
			{
				return bmp.LockBits(
				                    new Rectangle(0, 0, bmp.Width, bmp.Height),
				                    ImageLockMode.ReadOnly,
				                    bmp.PixelFormat);
			}

			static byte[] PointerToBytes(IntPtr ptr, int size)
			{
				var bytes = new byte[size];
				Marshal.Copy(ptr, bytes, 0, size);
				return bytes;
			}

			static Bitmap ConvertFormat(Image origin, PixelFormat format)
			{
				var clone = new Bitmap(origin.Width, origin.Height, format);

				using var graphic = Graphics.FromImage(clone);
				graphic.DrawImage(origin, new Rectangle(0, 0, clone.Width, clone.Height));

				return clone;
			}

			ValidateImage(image);

			using var bmp = ConvertFormat(image, PixelFormat.Format32bppArgb);

			var data = default(BitmapData);
			var ptr  = IntPtr.Zero;

			try
			{
				data = LockBitsAsReadonly(bmp);

				int size;
				(ptr, size) = encoder(data);

				var bytes = PointerToBytes(ptr, size);

				return bytes;
			}
			catch (Exception ex)
			{
				throw ThrowHelper.Create(ex);
			}
			finally
			{
				if (data is not null)
					bmp.UnlockBits(data);
				if (ptr != IntPtr.Zero)
					Native.WebPFree(ptr);
			}
		}
	}
}