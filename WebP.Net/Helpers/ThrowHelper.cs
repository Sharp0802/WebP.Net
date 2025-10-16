using System;
using System.Data;
using System.Runtime.CompilerServices;

namespace WebP.Net.Helpers;

internal static class ThrowHelper
{
	public static Exception Create(
		Exception                 inner,
		[CallerMemberName] string caller = "Unknown")
	{
		return new Exception($"[from {caller}] {inner.Message}", inner);
	}

	public static Exception ContainsNoData([CallerMemberName] string caller = "Unknown")
	{
		return Create(new DataException("Bitmap contains no data"), caller);
	}

	public static Exception SizeTooBig([CallerMemberName] string caller = "Unknown")
	{
		return
			Create(new DataException($"Dimension of bitmap is too large. Max is {WebPEncoder.MaxSize}x{WebPEncoder.MaxSize} pixels"),
			       caller);
	}

	public static Exception CannotEncodeByUnknown([CallerMemberName] string caller = "Unknown")
	{
		return Create(new Exception("Cannot encode by unknown cause"), caller);
	}

	public static Exception NullReferenced(string var, [CallerMemberName] string caller = "Unknown")
	{
		return Create(new NullReferenceException($"{var} is null"), caller);
	}

	public static Exception QualityOutOfRange([CallerMemberName] string caller = "Unknown")
	{
		return Create(new NullReferenceException("Quality must be between"), caller);
	}
}
