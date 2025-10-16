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

	[DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPConfigInitInternal")]
	public static extern int WebPConfigInitInternal(
		ref WebPConfig config,
		WebPPreset     preset,
		float          quality,
		int            webpDecoderAbiVersion);


	[DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPGetFeaturesInternal")]
	public static extern Vp8StatusCode WebPGetFeaturesInternal(
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

	[DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPConfigLosslessPreset")]
	public static extern int WebPConfigLosslessPreset(ref WebPConfig config, int level);

	[DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPValidateConfig")]
	public static extern int WebPValidateConfig(ref WebPConfig config);

	[DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPPictureInitInternal")]
	public static extern int WebPPictureInitInternal(ref WebPPicture pic, int webpDecoderAbiVersion);

	[DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPPictureImportBGR")]
	public static extern int WebPPictureImportBGR(ref WebPPicture pic, IntPtr bgr, int stride);

	[DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPPictureImportBGRA")]
	public static extern int WebPPictureImportBGRA(ref WebPPicture pic, IntPtr bgra, int stride);

	[DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPPictureImportBGRX")]
	public static extern int WebPPictureImportBGRX(ref WebPPicture pic, IntPtr bgr, int stride);

	[DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPEncode")]
	public static extern int WebPEncode(ref WebPConfig config, ref WebPPicture picture);

	[DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPPictureFree")]
	public static extern void WebPPictureFree(ref WebPPicture pic);

	[DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPGetInfo")]
	public static extern int WebPGetInfo([In] IntPtr data, UIntPtr dataSize, out int width, out int height);

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

	[DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPInitDecoderConfigInternal")]
	public static extern int WebPInitDecoderConfigInternal(
		ref WebPDecoderConfig webPDecoderConfig,
		int                   webpDecoderAbiVersion);

	[DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPDecode")]
	public static extern Vp8StatusCode WebPDecode(IntPtr data, UIntPtr dataSize, ref WebPDecoderConfig config);

	[DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPFreeDecBuffer")]
	public static extern void WebPFreeDecBuffer(ref WebPDecBuffer buffer);

	[DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPEncodeBGR")]
	public static extern int WebPEncodeBGR(
		[In] IntPtr bgr,
		int         width,
		int         height,
		int         stride,
		float       qualityFactor,
		out IntPtr  output);

	[DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPEncodeBGRA")]
	public static extern int WebPEncodeBGRA(
		[In] IntPtr bgra,
		int         width,
		int         height,
		int         stride,
		float       qualityFactor,
		out IntPtr  output);

	[DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPEncodeLosslessBGR")]
	public static extern int WebPEncodeLosslessBGR(
		[In] IntPtr bgr,
		int         width,
		int         height,
		int         stride,
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

	[DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPPictureDistortion")]
	public static extern int WebPPictureDistortion(
		ref WebPPicture srcPicture,
		ref WebPPicture refPicture,
		int             metricType,
		IntPtr          pResult);
}
