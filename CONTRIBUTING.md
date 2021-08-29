# Contributing guidelines

## Code style

By default, 
[.editorconfig](./.editorconfig) 
specifies all the code styles to follow.

**Load [.editorconfig](./.editorconfig)
into your IDE** and use it.

Other than that, it is your freedom no matter how you write it.

For code styles that are not specified in 
[.editorconfig](./.editorconfig)
, You can write your own code style.

## Static code analysis

**Keep your code clean** so that static code analysis tools (Rider, 
Intellisense) do not display warnings. 

In case of unavoidable circumstances, it is okay to suppress warnings 
through SuppressMessageAttribute.

## Assembly versioning

In [./WebP.Net.csproj](./WebP.Net.csproj)
, you can find the code that defines assembly version.
```xml
<AssemblyVersion>a.b.*</AssemblyVersion>
```
Version params of AssemblyVersion follow these 
rules:

1. The format of version must be follow (Major).(Minor).*

2. Major version is increased when you change important functions
(OS support, Compatibility etc...)

3. Minor version is increased when you change less important functions
(Add new function overloads, Add new features etc...)

4. Build number and revision are automatically managed by wildcard(*)

## Library version

**Use the most recent stable version** unless there are any vulnerabilities.

If a vulnerability is found in the external libraries, you should send a
pull request modifying the csproj to use a preview version of the 
libraries with the vulnerability fixed as soon as possible.
