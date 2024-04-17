@echo off

set DESTDIR=.\TestResults
if not exist %DESTDIR% mkdir %DESTDIR%

rem dotnet test --nologo -v q --collect "Code Coverage"
dotnet test --nologo -v m >%DESTDIR%\output.txt
tail -2 %DESTDIR%\output.txt
