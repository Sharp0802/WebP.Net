using System;
using System.Runtime.InteropServices;
using WebP.Net.Helpers;
using WebP.Net.Natives;
using WebP.Net.Natives.Enums;
using WebP.Net.Natives.Structs;

namespace WebP.Net;

public readonly struct WebPInfo
{
	public static WebPInfo GetFrom(Span<byte> webp)
	{
		var features = new WebPBitstreamFeatures();

		Vp8StatusCode status;
		unsafe
		{
			fixed (byte* p = webp)
			{
				status = Native.WebPGetFeatures((IntPtr)p, (UIntPtr)webp.Length, ref features);
			}
		}
		if (status is not Vp8StatusCode.Ok)
			throw new ExternalException(status.ToString());
		
		return new WebPInfo(features);
	}

	private WebPInfo(WebPBitstreamFeatures features)
	{
		Width      = features.Width;
		Height     = features.Height;
		HasAlpha   = features.HasAlpha is not 0;
		IsAnimated = features.HasAnimation is not 0;
	}

	public int  Width      { get; }
	public int  Height     { get; }
	public bool HasAlpha   { get; }
	public bool IsAnimated { get; }
}
