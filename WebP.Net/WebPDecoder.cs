using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using WebP.Net.Helpers;
using WebP.Net.Natives;

namespace WebP.Net
{
	public static class WebPDecoder
	{
		[method: Obsolete("WebPDecoder is obsolete. Use WebPObject instead of this.")]
		public static Bitmap Decode(byte[] webP)
		{
			var bmp    = default(Bitmap);
			var data   = default(BitmapData);
			var handle = GCHandle.Alloc(webP, GCHandleType.Pinned);
			int size;

			try
			{
				var info = WebPInfo.GetFrom(webP);

				bmp = info.HasAlpha
					? new Bitmap(info.Width, info.Height, PixelFormat.Format32bppArgb)
					: new Bitmap(info.Width, info.Height, PixelFormat.Format24bppRgb);
				data = bmp.LockBits(new Rectangle(0, 0, info.Width, info.Height), ImageLockMode.WriteOnly, bmp.PixelFormat);

				var length  = data.Stride * info.Height;
				var ptrData = handle.AddrOfPinnedObject();
				size = bmp.PixelFormat is PixelFormat.Format24bppRgb
					? Native.WebPDecodeBgrInto(ptrData, webP.Length, data.Scan0, length, data.Stride)
					: Native.WebPDecodeBgraInto(ptrData, webP.Length, data.Scan0, length, data.Stride);
			}
			catch (Exception ex)
			{
				throw ThrowHelper.Create(ex);
			}
			finally
			{
				if (data is not null)
					bmp.UnlockBits(data);
				if (handle.IsAllocated)
					handle.Free();
			}

			if (size is 0)
				throw ThrowHelper.CannotEncodeByUnknown();

			return bmp;
		}
	}
}
