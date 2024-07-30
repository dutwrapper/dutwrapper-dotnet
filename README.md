# DutWrapper
An unofficial wrapper at [sv.dut.udn.vn - Da Nang University of Science and Technology student page](http://sv.dut.udn.vn).

## Version
- Release version [![https://github.com/dutwrapper/dutwrapper-dotnet](https://img.shields.io/github/v/release/dutwrapper/dutwrapper-dotnet)](https://github.com/dutwrapper/dutwrapper-dotnet/releases)
- Pre-release version [![https://github.com/dutwrapper/dutwrapper-dotnet/tree/draft](https://img.shields.io/github/v/tag/dutwrapper/dutwrapper-dotnet?label=pre-release%20tag)](https://github.com/dutwrapper/dutwrapper-dotnet/tree/draft)
- [Summary change log](CHANGELOG.md) / [Entire source code changes](https://github.com/dutwrapper/dutwrapper-dotnet/commits)
- Badge provided by [shields.io](https://shields.io/)

## Building requirements
- One of following application:
  - Visual Studio 2017 or later (for .NET Core/.NET Framework support) (recommend).
  - Visual Studio Code with C# extension (you will still need .NET SDK).
- .NET Core 3.1 SDK.
  - **Remember:** Download .NET SDK (not .NET Runtime) for building this library.
  - This project will focus to .NET Core 3.1 SDK, so if you need library for newer .NET (5, 6, 7, 8,...), you can change framework in Project Properties.

## FAQ

### Can I port back .NET/.NET Core project to .NET Framework?
- Yes, you can port back, but you need to do that manually. I don't provide .NET Framework version anymore.

### Branch in dutwrapper?
- `stable`/`main`: Default branch and main release. This is **my recommend branch**.
- `draft`: This branch will used for update my progress and it is unstable. Use it at your own risk.

### I received error about login while running AccountTest?
- Make sure you have `dut_account` variable set with syntax `studentid|password`. This will ensure secure when testing project.

### I'm got issue or a feature request about this library. How should I do?
- Navigate to [issue tab](https://github.com/dutwrapper/dutwrapper-dotnet/issues) on this repository to create a issue or feature request.

## Credit and license?
- License: [**MIT**](LICENSE)
- DISCLAIMER:
  - This project - dutwrapper - is not affiliated with [Da Nang University of Science and Technology school](http://dut.udn.vn).
  - DUT, Da Nang University of Technology, web materials and web contents are trademarks and copyrights of [Da Nang University of Science and Technology school](http://dut.udn.vn).
- Used third-party dependencies:
  - [AngleSharp](https://github.com/AngleSharp/AngleSharp): Licensed under [the MIT license](https://github.com/AngleSharp/AngleSharp/blob/devel/LICENSE).
