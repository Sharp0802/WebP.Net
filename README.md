# WebP.Net
## What is this?
This library provides a simple encoder and decoder for webp.
## How to use?
### Encode / Decode
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
static Bitmap DecodeWebP(byte[] webP)
{
    return WebPDecoder.Decode(bytes);
}
```
### Get info
```c#
using WebP.Net;

static WebPInfo GetInfo(byte[] webP)
{
    return WebPInfo.GetFrom(webP);
}
```
### Get version
```c#
using WebP.Net;

static WebPVersion GetVersion()
{
    return WebPLibrary.GetVersion(); // get version of libwebp
}
static string GetVersionAsString()
{
    return WebPLibrary.GetVersion().ToString; // Major.Minor.Revision
}
```