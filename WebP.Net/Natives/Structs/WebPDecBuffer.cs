﻿using System;
using System.Runtime.InteropServices;
using WebP.Net.Natives.Enums;

namespace WebP.Net.Natives.Structs;

[StructLayout(LayoutKind.Sequential)]
public struct WebPDecBuffer
{
	public           WebpCspMode    colorSpace;
	public           int            width;
	public           int            height;
	public           int            isExternalMemory;
	public           RgbaYuvaBuffer u;
	private readonly uint           pad1;
	private readonly uint           pad2;
	private readonly uint           pad3;
	private readonly uint           pad4;
	public           IntPtr         private_memory;
}
