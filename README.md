# DutWrapper

- For more information, please read in our homepage.

## Version

[![](https://img.shields.io/github/v/release/dutwrapper/dutwrapper-dotnet?label=release)](https://github.com/dutwrapper/dutwrapper-dotnet/releases)

[![](https://img.shields.io/github/v/tag/dutwrapper/dutwrapper-dotnet?label=pre-release)](https://github.com/dutwrapper/dutwrapper-dotnet/releases)

## Building requirements

- One of following application:
  - Visual Studio 2017 or later (for .NET Core/.NET Framework support) (recommend).
  - Visual Studio Code with C# extension (you will still need .NET SDK).
- .NET Core 3.1 SDK.
  - **Remember:** Download .NET SDK (not .NET Runtime) for building this library.

## License and credits

- [MIT](LICENSE) (click to view licenses)
- [Credit (click here for more information)](CREDIT.md)

## FAQ

### Can I port back .NET/.NET Core project to .NET Framework?
- Yes, you can port back, but you need to do that manually. I don't provide .NET Framework version anymore.

### Branch in dutwrapper?
- `stable`: Default branch and main release.
- `draft`: Alpha branch. This code isn't tested and use it at your own risk.

### I received error about login while running AccountTest?
- Did you mean this error: `dut_account environment variable not found. Please, add or modify this environment in format "username|password"`?
- If so, you will need to add environment variable named `dut_account` with syntax `studentid|password`. This will ensure secure when testing project.

### Wiki, or manual for how-to-use?
- In a plan, please be patient.

### Latest change log?
- To view log for all versions, [click here](CHANGELOG.md)

## Copyright?
- This project - dutwrapper - is not affiliated with [Da Nang University of Technology](http://sv.dut.udn.vn).
- DUT, Da Nang University of Technology, web materials and web contents are trademarks and copyrights of [Da Nang University of Technology](http://sv.dut.udn.vn) school.
