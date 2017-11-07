SET BUILDTOOL=c:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe 
%BUILDTOOL% CSMatIO.sln /p:Configuration=Release /p:TargetFrameworkVersion=v2.0
%BUILDTOOL% CSMatIO.sln /p:Configuration=Release /p:TargetFrameworkVersion=v4.0
