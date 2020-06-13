@echo off
SET RELEASEVERSION="1.0.0.0"
SET CONFIGURATION="Release"
SET FRAMEWORK="netcoreapp3.1"
SET DROPLOCATION="%~dp0bin\Deploy"
SET PACKAGENAME="OpenMinesweeper"
SET ZIPNAME=%PACKAGENAME%_%RELEASEVERSION%
SET SEVENZIP="%~dp0External\Packages\7Zip\7za.exe"
call :sub >%~dp0bin\%CONFIGURATION%\PackRelease.log
exit /b

:sub
if not exist "%~dp0bin\%CONFIGURATION%\%ZIPNAME%" mkdir "%~dp0bin\%CONFIGURATION%\%ZIPNAME%"
xcopy /y /f "%~dp0bin\%CONFIGURATION%\%FRAMEWORK%\*.exe" "%~dp0bin\%CONFIGURATION%\%ZIPNAME%"
xcopy /y /f "%~dp0bin\%CONFIGURATION%\%FRAMEWORK%\*.dll" "%~dp0bin\%CONFIGURATION%\%ZIPNAME%"
xcopy /y /f "%~dp0bin\%CONFIGURATION%\%FRAMEWORK%\OpenMinesweeper.NET.runtimeconfig.json" "%~dp0bin\%CONFIGURATION%\%ZIPNAME%"
xcopy /y /f "%~dp0Doc\READMEFIRST.txt" "%~dp0bin\%CONFIGURATION%\%ZIPNAME%"

if not exist "%DROPLOCATION%" mkdir "%DROPLOCATION%"
start "" /WAIT %SEVENZIP% a -t7z "%DROPLOCATION%\%ZIPNAME%.7z" "%~dp0bin\%CONFIGURATION%\%ZIPNAME%\*.*"
if exist "%~dp0bin\%CONFIGURATION%\%ZIPNAME%" rmdir /s /q "%~dp0bin\%CONFIGURATION%\%ZIPNAME%"