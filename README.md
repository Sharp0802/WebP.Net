# WebP.Net

![](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
[![](https://img.shields.io/badge/NuGet-004880?style=for-the-badge&logo=nuget&logoColor=white)](https://www.nuget.org/packages/WebP_Net/)

## What is this?

This library provides a simple encoder and decoder for webp.

## How to use?

### Install

In your csproj :

```xml
<PackageReference Include="WebP_Net" Version="2.0.0" />
```

Or, if you using .net cli :

```
dotnet add package WebP_Net --version 2.0.0
```

### Encode / Decode

```c#
using System.Drawing;
using WebP.Net;

var bitmap = WebPDecoder.Decode(File.ReadAllBytes("..."));

using var webp = WebPEncoder.EncodeLossless(bitmap);
File.WriteAllBytes("...", webp.AsSpan().ToArray());
```

### Get version

```c#
using WebP.Net;

var version = WebPLibrary.GetVersion();
```
