# DUTWRAPPER CHANGE LOG

This file will list all version log for modified, added or removed functions of dutwrapper.

## 1.8.0-alpha4
- Added news fetching options:
  - Student affairs news (`News.NewsInstance.GetNewsStudentAffairs()`);
  - Examination news (`News.NewsInstance.GetNewsExamination()`);
  - Tuition fee news (`News.NewsInstance.GetNewsTuitionFee()`).
- `GetCurrentSchoolYear()` has been moved from `DutSchoolYear` to `Utils` (now is `Utils.GetCurrentSchoolYear()`).
- Renamed `Account` to `AccountInstance`.
- Moved `NewsType`, `SearchMethod` and `SubjectStatus` to `News.NewsParameters`.
- Added network checker (in `Utils.Connections`):
  - Check if network available (`IsNetworkAvailable()`);
  - Check if connected to internet (`IsConnectedToInternet()`);
  - Check if DUT website is online (`IsWebsiteOnline()`).

## 1.8.0-alpha3
- Removed DutWrapper.WinUI. You can use DutWrapper instead because of same libraries.
- Add `ToMarkdown()` for displaying news global and news subject.
- Updated dependencies to latest version.

## 1.8.0-alpha2
- Sync json serializer to match in libraries with java and python language.

## 1.8.0-alpha1
- Sync json serializer to match in java language.
- Updated dependencies to latest.
- Changed some properties name (you'll need to change your code to working again).
- Fixed a issue cause `SubjectResult` in `TrainingStatus` display error about `PointT10`, `PointT4` and `PointByChar`.

## 1.7.1
- Fixed a issue prevent logging in to sv.dut.udn.vn.

## 1.7.0
- Moved all extensions and utils function to `Utils` class.

## 1.7.0-draft3
- Utils.GetCurrentSchoolWeek(): Get current school year and week.
- Merge extension between StringExtension, NumberExtension and Account.
- Adjust AccountTest for better result view.

## 1.7.0-draft2
- New news subject item with regex.
- New account training result: View your training result on school.

## 1.7.0-draft1
- Return project to .NET Core 3.1 for support older projects (you can change back to 6.0 if you like).
- Change HTML parse to AngleSharp.
- Add more properties in AccountInformation.
- Removed DutWrapper.TestRunnerConsole and DutWrapper.UWP.
  - DutWrapper.UWP wasn't updated at a loong time and needs to be deleted to focus to main library.

## 1.6.1
- Fixed crash library when system date format is not dd/MM/yyyy.

## 1.6
- Upgrade .NET used for this project is 6.0.
- Changed namespace (~~DUTAPI~~ to DutWrapper).
- GetNews() now separated to two functions for news global and news subject (this will develop news subject easier).
- Account now switched to session id (so, Session class has been dropped).

## 1.5
- Changed namespace (~~ZoeMeow.DUTAPI~~ to DUTAPI).

## 1.4.2
- Optimize code line.

## 1.4.1
- Merge GetNewsGeneral() and GetNewsSubject() together.
  - To GetNews().

## 1.4
- Inital commit for Java.

## 1.3

- Inital commit for Python.
- Delete unneed files for .NET.
- Optimize code :)

## 1.2
- Added feature: Get all subjects fee list.
- Change file and function name: ~~GetScheduleSubject~~ to **GetSubjectsSchedule**.
- Add [CHANGELOG.md](CHANGELOG.md) for all old logs.
- Optimize code.

## 1.1
- Added feature: Get schedule about subjects and examimation.

## 1.0
Inital commit with features:
- Get news general (Nhận thông báo chung).
- Get news subjects (Nhận thông báo lớp học phần).
- Sesion (Phiên, dùng để đăng nhập/đăng xuất/lấy thông tin tài khoản).
