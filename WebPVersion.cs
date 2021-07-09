using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace WebP.Net
{
	public readonly struct WebPVersion
	{
		public WebPVersion(uint major, uint minor, uint revision)
		{
			Major    = major;
			Minor    = minor;
			Revision = revision;
		}

		[property: SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
		public uint Major { get; }

		[property: SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
		public uint Minor { get; }

		[property: SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
		public uint Revision { get; }

		public override string ToString()
		{
			return new StringBuilder()
			      .Append(Major)
			      .Append('.')
			      .Append(Minor)
			      .Append('.')
			      .Append(Revision)
			      .ToString();
		}
	}
}
