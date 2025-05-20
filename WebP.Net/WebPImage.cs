using System;
using System.Threading;
using WebP.Net.Natives;

namespace WebP.Net;

public ref struct WebPImage(Span<byte> data) : IDisposable
{
	private readonly Span<byte> _data = data;
	private          int        _disposed;

	public void Dispose()
	{
		if (Interlocked.Exchange(ref _disposed, 1) == 1)
			return;
		
		unsafe
		{
			fixed (byte* ptr = &_data.GetPinnableReference())
			{
				Native.WebPFree((IntPtr) ptr);
			}
		}
	}

	public Span<byte> AsSpan()
	{
		if (_disposed != 0)
			throw new ObjectDisposedException(nameof(WebPImage));

		return _data;
	}
}
