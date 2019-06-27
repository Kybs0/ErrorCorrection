@echo off
cls

set  jdkPath=JavaSetup8u211.exe
rem  设置jdk安装路径，jre安装路径
set  commonPath=C:\Program Files (x86)
set  jreinstallPath="%commonPath%\jre1.8.0_211"
 
echo.
echo 正在安装jre，需要二、三分钟，请不要执行其他操作
echo.
start /w %jdkPath% /L "%commonPath%\installjava.log" /s 
ADDLOCAL="ToolsFeature,SourceFeature,PublicjreFeature"  
INSTALLDIR=%jreinstallPath%
WEB_JAVA=0 AUTO_UPDATE=0 
echo 安装完成，%jreinstallPath%

pause