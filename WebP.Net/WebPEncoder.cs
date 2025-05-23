﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using WebP.Net.Helpers;
using WebP.Net.Natives;

namespace WebP.Net;

public static class WebPEncoder
{
	public const int MaxSize = 16383;


	public static WebPImage EncodeLossy(Image image, int quality)
	{
		return quality is <= 0 or > 100 
			? throw new ArgumentOutOfRangeException(nameof(quality)) 
			: EncodeBase(image, quality);
	}

	public static WebPImage EncodeLossless(Image image)
	{
		return EncodeBase(image, 100);
	}

	private static WebPImage EncodeBase(Image image, int quality)
	{
		if (image is null)
			throw ThrowHelper.NullReferenced(nameof(image));
		if (image.Width is 0 || image.Height is 0)
			throw ThrowHelper.ContainsNoData();
		if (image.Width > MaxSize || image.Height > MaxSize)
			throw ThrowHelper.SizeTooBig();

		using var bmp = new Bitmap(image.Width, image.Height, image.PixelFormat);
		using var graphic = Graphics.FromImage(bmp);
		graphic.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height));
		
		var data = default(BitmapData);

		try
		{
			data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, bmp.PixelFormat);

			var size = quality >= 100
				? Native.WebPEncodeLosslessBGRA(data.Scan0, data.Width, data.Height, data.Stride, out var ptr)
				: Native.WebPEncodeBGRA(data.Scan0, data.Width, data.Height, data.Stride, quality, out ptr);
			if (size is 0)
				throw ThrowHelper.CannotEncodeByUnknown();

			unsafe
			{
				return new WebPImage(new Span<byte>((void*) ptr, size));
			}
		}
		catch (Exception ex)
		{
			throw ThrowHelper.Create(ex);
		}
		finally
		{
			if (data is not null)
				bmp.UnlockBits(data);
		}
	}
}
