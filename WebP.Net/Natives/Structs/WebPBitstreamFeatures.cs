using System.Runtime.InteropServices;

namespace WebP.Net.Natives.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct WebPBitstreamFeatures
{
	public int Width;
	public int Height;
	public int HasAlpha;
	public int HasAnimation;
	public int Format;
}
