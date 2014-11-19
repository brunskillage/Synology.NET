@echo Off
set config=%1
if "%config%" == "" (
   set config=Release
)

copy version.txt SynologyClient\Properties\AssemblyVersion.cs

%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe Synology.NET.sln /p:Configuration=%config% /m /v:M /fl /nr:true /p:BuildInParallel=true /p:RestorePackages=true /t:Clean,Rebuild

if not "%errorlevel%"=="0" goto failure

rd Download /s /q  REM delete the old stuff
if not exist Download\Net4 mkdir Download\Net4

copy LICENSE.txt Download\
copy SynologyClient\bin\Release\RestSharp.dll Download\net4\
copy SynologyClient\bin\Release\Synology.dll Download\net4\
copy readme.txt Download\

REM rd NugetPackage /s /q  REM delete the old stuff
REM if not exist NugetPackage mkdir NugetPackage\lib
REM copy SynologyClient\bin\Release\Synology.dll NugetPackage\lib
REM cd NugetPackage
REM ..\.NuGet\NuGet pack ..\SynologyClient\SynologyClient.nuspec -IncludeReferencedProjects -Prop REM Configuration=Release
REM pause FINISHED
:success

REM use github status API to indicate commit compile success

exit 0

:failure

REM use github status API to indicate commit compile success

exit -1

pause FINISHED
