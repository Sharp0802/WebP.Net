using System;
using WebP.Net.Helpers;
using WebP.Net.Natives;

namespace WebP.Net
{
	public static class WebPLibrary
	{
		public static WebPVersion GetVersion()
		{
			try
			{
				var v = (uint) Native.WebPGetDecoderVersion();
				return new WebPVersion((v >> 16) % 256, (v >> 8) % 256, v % 256);
			}
			catch (Exception ex)
			{
				throw ThrowHelper.Create(ex);
			}
		}
	}
}
