using System.Runtime.InteropServices;

namespace WebP.Net.Natives.Structs;

[StructLayout(LayoutKind.Explicit)]
public struct RgbaYuvaBuffer
{
	[FieldOffset(0)] public WebPRgbaBuffer Rgba;

	[FieldOffset(0)] public WebPYuvaBuffer Yuva;
}
