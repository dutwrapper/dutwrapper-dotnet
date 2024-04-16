# DutWrapper - dotNET

An unofficial wrapper for easier to use at [sv.dut.udn.vn - Da Nang University of Technology student page](http://sv.dut.udn.vn).

## Version

[![](https://img.shields.io/github/v/release/dutwrapper/dutwrapper-dotnet?label=release)](https://github.com/dutwrapper/dutwrapper-dotnet/releases)

[![](https://img.shields.io/github/v/tag/dutwrapper/dutwrapper-dotnet?label=pre-release)](https://github.com/dutwrapper/dutwrapper-dotnet/releases)

## Building requirements

- One of following application:
  - Visual Studio 2017 or later (for .NET Core/.NET Framework support) (recommend).
  - Visual Studio Code with C# extension (you will still need .NET SDK).
- .NET Core 3.1 SDK.
  - **Remember:** Download .NET SDK (not .NET Runtime) for building this library.

## FAQ

### Can I port back .NET/.NET Core project to .NET Framework?
- Yes, you can port back, but you need to do that manually. I don't provide .NET Framework version anymore.

### Which branch should I use?
- `stable`/`main`: Default branch and main release. This is **my recommend branch**.
- `draft`: Alpha branch. This branch is used for update my progress and it's very unstable. Use it at your own risk.

### I received error about login while running AccountTest?
- Did you mean this error: `dut_account environment variable not found. Please, add or modify this environment in format "username|password"`?
- If so, you will need to add environment variable named `dut_account` with syntax `studentid|password`.

### Wiki, or manual for how-to-use?
- In a plan, please be patient.

## Changelog

### v1.7.1
- Fixed a issue prevent logging in to sv.dut.udn.vn.

### Older version
- To view log for all versions, [click here](CHANGELOG.md)

## License
- [MIT](LICENSE) (click to view licenses)

## Credits?
- [Click here](CREDIT.md) to view which this repository used.

## Disclaimer
- This project - dutwrapper - is not affiliated with [Da Nang University of Technology](http://sv.dut.udn.vn).
- DUT, Da Nang University of Technology, web materials and web contents are trademarks and copyrights of [Da Nang University of Technology](http://sv.dut.udn.vn) school.
