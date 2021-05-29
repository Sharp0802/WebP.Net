using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace WebP.Net.Structs
{
	[StructLayout(LayoutKind.Explicit),
	 SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global"),
	 SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
	public struct RgbaYuvaBuffer
	{
		[FieldOffset(0)] public WebPRgbaBuffer Rgba;

		[FieldOffset(0)] public WebPYuvaBuffer Yuva;
	}
}
