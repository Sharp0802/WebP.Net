using System;
using System.Runtime.InteropServices;
using WebP.Net.Helpers;
using WebP.Net.Natives;
using WebP.Net.Natives.Enums;
using WebP.Net.Natives.Structs;

namespace WebP.Net;

public readonly struct WebPInfo
{
	public static WebPInfo GetFrom(byte[] webP)
	{
		var handle = GCHandle.Alloc(webP, GCHandleType.Pinned);

		try
		{
			var features = new WebPBitstreamFeatures();
			var status   = Native.WebPGetFeatures(handle.AddrOfPinnedObject(), (UIntPtr)webP.Length, ref features);
			if (status is not Vp8StatusCode.Ok)
				throw new ExternalException(status.ToString());
			return new WebPInfo(features);
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

	internal WebPInfo(WebPBitstreamFeatures features)
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
