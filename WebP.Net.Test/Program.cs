
using System;
using System.Drawing.Imaging;
using System.IO;
using WebP.Net;

Console.WriteLine("Hello, World!");

Console.WriteLine("libwebp version: {0}", WebPLibrary.GetVersion().ToString());

var bitmap = WebPDecoder.Decode(File.ReadAllBytes("sample.webp"));

bitmap.Save("sample.png", ImageFormat.Png);

var bytes = WebPEncoder.EncodeLossless(bitmap);
File.WriteAllBytes("after.webp", bytes);
