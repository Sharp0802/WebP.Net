using System.Diagnostics.CodeAnalysis;

namespace WebP.Net.Enums
{
	[SuppressMessage("ReSharper", "InconsistentNaming")]
	public enum WebpCspMode
	{
		ModeRGB,
		ModeRgba,
		ModeBgr,
		ModeBgra,
		ModeARGB,
		ModeRGBA4444,
		ModeRGB565,
		ModeRgbA,
		ModeBgrA,
		ModeArgb,
		ModeRgbA4444,
		ModeYuv,
		ModeYuva,
		ModeLast,
	}
}