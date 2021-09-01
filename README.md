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
    using var webp = new WebPObject(bitmap);
    return webp.GetWebPLossy(quality);
}
static byte[] EncodeLossless(Bitmap bitmap)
{
    using var webp = new WebPObject(bitmap);
    return webp.GetLossless();
}
static Image DecodeWebP(byte[] webP)
{
    using var webp = new WebPObject(webP);
    return webp.GetImage();
}
```

### Get info

```c#
using WebP.Net;

static WebPInfo GetInfo(byte[] webP)
{
    using var webp = new WebPObject(webP);
    return webP.GetInfo();
}
```

### Get version

```c#
using WebP.Net;

static WebPVersion GetVersion()
{
    return WebPObject.GetVersion(); // get version of libwebp
}
static string GetVersionAsString()
{
    return WebPObject.GetVersion().ToString(); // Major.Minor.Revision
}
```
