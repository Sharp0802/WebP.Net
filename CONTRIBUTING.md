# Contributing guidelines

## Code style

By default, [.editorconfig](./.editorconfig) specifies all the code styles to follow.

**Load [.editorconfig](./.editorconfig) into your IDE** and use it.

Other than that, it is your freedom no matter how you write it.

For code styles that are not specified in [.editorconfig](./.editorconfig), You can write your own code style.

## Static code analysis

**Keep your code clean** so that static code analysis tools (Rider, Intellisense) do not display warnings. 

In case of unavoidable circumstances, it is okay to suppress warnings through SuppressMessageAttribute.

## Assembly versioning

In [./WebP.Net/WebP.Net.csproj](./WebP.Net/WebP.Net.csproj), you can find the code that defines assembly version.
```xml
<DeclaredVersion>a.b</DeclaredVersion>
```
Version params of AssemblyVersion follow these rules:

1. The format of version must be follow (Major).(Minor)

2. Major version is increased when you change important functions
(OS support, Compatibility etc...)

3. Minor version is increased when you change less important functions(Add new function overloads, Add new features etc...)
