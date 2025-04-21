using System.Text;

namespace WebP.Net;

public readonly struct WebPVersion
{
	public WebPVersion(uint major, uint minor, uint revision)
	{
		Major    = major;
		Minor    = minor;
		Revision = revision;
	}

	public uint Major { get; }
	public uint Minor { get; }
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
