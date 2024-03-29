﻿using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace WebP.Net.Helpers;

internal static class ThrowHelper
{
	[DoesNotReturn, Obsolete]
	public static Exception Create(
		[DisallowNull]     Exception inner,
		[CallerMemberName] string    caller = "Unknown")
	{
		return new Exception($"{inner.Message}\nIn {caller}", inner);
	}

	[DoesNotReturn]
	public static Exception UnknownPlatform()
	{
		return new PlatformNotSupportedException("Unknown platform detected. Platform must be x86 or x64");
	}

	[DoesNotReturn, Obsolete]
	public static Exception ContainsNoData([CallerMemberName] string caller = "Unknown")
	{
		return Create(new DataException("Bitmap contains no data"), caller);
	}

	[DoesNotReturn, Obsolete]
	public static Exception SizeTooBig([CallerMemberName] string caller = "Unknown")
	{
		return
			Create(new DataException($"Dimension of bitmap is too large. Max is {WebPObject.WebpMaxDimension}x{WebPObject.WebpMaxDimension} pixels"),
			       caller);
	}

	[DoesNotReturn, Obsolete]
	public static Exception CannotEncodeByUnknown([CallerMemberName] string caller = "Unknown")
	{
		return Create(new Exception("Cannot encode by unknown cause"), caller);
	}

	[DoesNotReturn, Obsolete]
	public static Exception NullReferenced(string var, [CallerMemberName] string caller = "Unknown")
	{
		return Create(new NullReferenceException($"{var} is null"), caller);
	}

	[DoesNotReturn, Obsolete]
	public static Exception QualityOutOfRange([CallerMemberName] string caller = "Unknown")
	{
		return Create(new NullReferenceException("Quality must be between"), caller);
	}
}
