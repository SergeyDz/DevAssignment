REM Check MsBuild is available
SET MSBUILD=%WINDIR%\Microsoft.NET\Framework\v4.0.30319\MsBuild.exe
IF NOT EXIST "%MSBUILD%" GOTO NOMSB

"%MSBUILD%" ../Source/SD.CodeProblem.DevAssignment.sln /t:rebuild /p:configuration=Debug 
GOTO :EOF

:NOMSB
echo. 
echo MSBUILD not found 
echo. 
GOTO :EOF 