REM Run unit tests through OpenCover
REM This allows OpenCover to gather code coverage results
.\..\Source\packages\OpenCover.4.5.3522\OpenCover.Console.exe^
 -register:user^
 -target:.\..\Source\packages\NUnit.Runners.2.6.4\tools\nunit-console.exe^
 -targetargs:"/nologo .\..\Source\Test\SD.CodeProblem.DevAssignment.DomainModel.Test\bin\Debug\SD.CodeProblem.DevAssignment.DomainModel.Test.dll /noshadow"^
 -output:.\..\Reports\output.xml

REM Generate the report
.\..\Source\packages\ReportGenerator.2.0.4.0\ReportGenerator.exe^
 -reports:.\..\Reports\output.xml^
 -targetdir:.\..\Reports^
 -reporttypes:Html^
 -filters:-Test*

REM Open the report
start .\..\Reports\index.htm