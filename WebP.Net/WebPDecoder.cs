using System;
using System.Drawing;
using System.Drawing.Imaging;
using WebP.Net.Helpers;
using WebP.Net.Natives;

namespace WebP.Net;

public static class WebPDecoder
{
	public static Bitmap Decode(Span<byte> webP)
	{
		var bmp    = default(Bitmap);
		var data   = default(BitmapData);
		int size;

		try
		{
			var info = WebPInfo.GetFrom(webP);

			bmp = info.HasAlpha
				? new Bitmap(info.Width, info.Height, PixelFormat.Format32bppArgb)
				: new Bitmap(info.Width, info.Height, PixelFormat.Format24bppRgb);
			data = bmp.LockBits(new Rectangle(0, 0, info.Width, info.Height), ImageLockMode.WriteOnly, bmp.PixelFormat);

			var length  = data.Stride * info.Height;
			
			unsafe
			{
				fixed (byte* ptr = webP)
				{
					size = bmp.PixelFormat is PixelFormat.Format24bppRgb
						? Native.WebPDecodeBGRInto((IntPtr)ptr, (UIntPtr)webP.Length, data.Scan0, length, data.Stride)
						: Native.WebPDecodeBGRAInto((IntPtr)ptr, (UIntPtr)webP.Length, data.Scan0, length, data.Stride);
				}
			}
		}
		catch (Exception ex)
		{
			throw ThrowHelper.Create(ex);
		}
		finally
		{
			if (bmp is not null && data is not null)
				bmp.UnlockBits(data);
		}

		if (size is 0)
			throw ThrowHelper.CannotEncodeByUnknown();

		return bmp;
	}
}
