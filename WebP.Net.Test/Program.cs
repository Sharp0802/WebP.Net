using System;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Versioning;

namespace WebP.Net.Test;

internal static class Program
{
	[SupportedOSPlatform("windows")]
	public static void Main(string[] args)
	{
		Console.WriteLine("libwebp version: {0}", WebPLibrary.GetVersion().ToString());

		var bitmap = WebPDecoder.Decode(File.ReadAllBytes("sample.webp"));

		bitmap.Save("sample.png", ImageFormat.Png);

		using var webp = WebPEncoder.EncodeLossless(bitmap);
		File.WriteAllBytes("after.webp", webp.AsSpan().ToArray());
	}
}
