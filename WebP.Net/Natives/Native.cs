using System;
using System.Runtime.InteropServices;
using System.Security;
using WebP.Net.Natives.Enums;
using WebP.Net.Natives.Structs;

namespace WebP.Net.Natives;

[SuppressUnmanagedCodeSecurity]
internal static class Native
{
	private const string DllPath           = "libwebp.dll";
	private const int    DecoderABIVersion = 0x0209;


	[DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPGetFeaturesInternal")]
	private static extern Vp8StatusCode WebPGetFeaturesInternal(
		[In] IntPtr               rawWebP,
		UIntPtr                   dataSize,
		ref WebPBitstreamFeatures features,
		int                       version);

	public static Vp8StatusCode WebPGetFeatures(
		[In] IntPtr               rawWebP,
		UIntPtr                   dataSize,
		ref WebPBitstreamFeatures features)
	{
		return WebPGetFeaturesInternal(rawWebP, dataSize, ref features, DecoderABIVersion);
	}

	[DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPDecodeBGRInto")]
	public static extern int WebPDecodeBGRInto(
		[In] IntPtr data,
		UIntPtr     dataSize,
		IntPtr      outputBuffer,
		int         outputBufferSize,
		int         outputStride);

	[DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPDecodeBGRAInto")]
	public static extern int WebPDecodeBGRAInto(
		[In] IntPtr data,
		UIntPtr     dataSize,
		IntPtr      outputBuffer,
		int         outputBufferSize,
		int         outputStride);

	[DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPEncodeBGRA")]
	public static extern int WebPEncodeBGRA(
		[In] IntPtr bgra,
		int         width,
		int         height,
		int         stride,
		float       qualityFactor,
		out IntPtr  output);

	[DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPEncodeLosslessBGRA")]
	public static extern int WebPEncodeLosslessBGRA(
		[In] IntPtr bgra,
		int         width,
		int         height,
		int         stride,
		out IntPtr  output);

	[DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPFree")]
	public static extern void WebPFree(IntPtr p);

	[DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPGetDecoderVersion")]
	public static extern int WebPGetDecoderVersion();
}
