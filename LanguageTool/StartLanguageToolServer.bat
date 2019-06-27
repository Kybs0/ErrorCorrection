@echo off

echo.
echo ÕýÔÚÆô¶¯language-tool-server...
cd LanguageTool-4.5
echo java -cp languagetool-server.jar org.languagetool.server.HTTPServer --port 8081
java -cp languagetool-server.jar org.languagetool.server.HTTPServer --port 8081

pause