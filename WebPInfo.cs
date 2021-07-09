using System;
using System.Runtime.InteropServices;
using WebP.Net.Helpers;
using WebP.Net.Natives;
using WebP.Net.Natives.Enums;
using WebP.Net.Structs;

namespace WebP.Net
{
	public readonly struct WebPInfo
	{
		public static WebPInfo GetFrom(byte[] webP)
		{
			var handle = GCHandle.Alloc(webP, GCHandleType.Pinned);

			try
			{
				var features = new WebPBitstreamFeatures();
				var status   = Native.WebPGetFeatures(handle.AddrOfPinnedObject(), webP.Length, ref features);

				if (status is not Vp8StatusCode.Ok)
					throw new ExternalException(status.ToString());

				return new WebPInfo(features.Width,
				                    features.Height,
				                    features.Has_alpha is 1,
				                    features.Has_animation is 1);
			}
			catch (Exception ex)
			{
				throw ThrowHelper.Create(ex);
			}
			finally
			{
				if (handle.IsAllocated)
					handle.Free();
			}
		}

		public WebPInfo(int width, int height, bool hasAlpha, bool isAnimated)
		{
			Width      = width;
			Height     = height;
			HasAlpha   = hasAlpha;
			IsAnimated = isAnimated;
		}

		public int  Width      { get; }
		public int  Height     { get; }
		public bool HasAlpha   { get; }
		public bool IsAnimated { get; }
	}
}
