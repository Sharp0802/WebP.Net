using System;
using System.Runtime.InteropServices;

namespace WebP.Net.Natives.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct WebPRgbaBuffer
{
	public IntPtr  rgba;
	public int     stride;
	public UIntPtr size;
}
