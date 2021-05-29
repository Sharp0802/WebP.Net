#WebP.Net
##What is this?
This library provides a simple encoder and decoder for webp.

The version of libwebp used is 1.1.0
##How to use?
```c#
using System.Drawing;
using WebP.Net;

static byte[] EncodeLossy(Bitmap bitmap, float quality)
{
    return WebPEncoder.EncodeLossy(bitmap, quality);
}
static byte[] EncodeLossless(Bitmap bitmap)
{
    return WebPEncoder.EncodeLossless(bitmap);
}
static Bitmap DecodeWebP(byte[] bytes)
{
    return WebPDecoder.Decode(bytes);
}
```