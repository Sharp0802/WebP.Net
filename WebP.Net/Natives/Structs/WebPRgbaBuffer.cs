using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace WebP.Net.Natives.Structs
{
	[StructLayout(LayoutKind.Sequential),
	 SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global"),
	 SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
	public struct WebPRgbaBuffer
	{
		public IntPtr  rgba;
		public int     stride;
		public UIntPtr size;
	}
}
