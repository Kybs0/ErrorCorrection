@echo off
cls

set  jdkPath=JavaSetup8u211.exe
rem  ����jdk��װ·����jre��װ·��
set  commonPath=C:\Program Files (x86)
set  jreinstallPath="%commonPath%\jre1.8.0_211"
 
echo.
echo ���ڰ�װjre����Ҫ���������ӣ��벻Ҫִ����������
echo.
start /w %jdkPath% /L "%commonPath%\installjava.log" /s 
ADDLOCAL="ToolsFeature,SourceFeature,PublicjreFeature"  
INSTALLDIR=%jreinstallPath%
WEB_JAVA=0 AUTO_UPDATE=0 
echo ��װ��ɣ�%jreinstallPath%

pause