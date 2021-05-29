using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;

namespace WebP.Net.Helpers
{
	internal static class ThrowHelper
	{
		[Pure, DoesNotReturn]
		public static Exception Create(
			Exception                 inner,
			[CallerMemberName] string caller = "Unknown")
			=> new($"{inner.Message}\nIn {caller}", inner);

		[Pure, DoesNotReturn]
		public static Exception UnknownPlatform([CallerMemberName] string caller = "Unknown")
			=> Create(new PlatformNotSupportedException("Unknown platform detected. Platform must be x86 or x64"), caller);

		[Pure, DoesNotReturn]
		public static Exception ContainsNoData([CallerMemberName] string caller = "Unknown")
			=> Create(new DataException("Bitmap contains no data"));

		[Pure, DoesNotReturn]
		public static Exception SizeTooBig([CallerMemberName] string caller = "Unknown")
			=> Create(new DataException($"Dimension of bitmap is too large. Max is {WebPEncoder.WebpMaxDimension}x{WebPEncoder.WebpMaxDimension} pixels"), caller);

		[Pure, DoesNotReturn]
		public static Exception CannotEncodeByUnknown([CallerMemberName] string caller = "Unknown")
			=> Create(new Exception("Cannot encode by unknown cause"), caller);

		[Pure, DoesNotReturn]
		public static Exception NullReferenced(string var, [CallerMemberName] string caller = "Unknown")
			=> Create(new NullReferenceException($"{var} is null"), caller);

		[Pure, DoesNotReturn]
		public static Exception QualityOutOfRange([CallerMemberName] string caller = "Unknown")
			=> Create(new NullReferenceException("Quality must be between"), caller);
	}
}