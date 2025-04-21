using System.Runtime.InteropServices;

namespace WebP.Net.Natives.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct WebPDecoderConfig
{
	public WebPBitstreamFeatures input;
	public WebPDecBuffer         output;
	public WebPDecoderOptions    options;
}
