@echo OFF
REM chcp 1251

Set CurrentDate=%Date%
Set CurrentTime=%Time: =0%
Set CurrentDateTime=%CurrentDate:~6,4%_%CurrentDate:~3,2%_%CurrentDate:~0,2%_%CurrentTime:~0,2%_%CurrentTime:~3,2%_%CurrentTime:~6,2%
 
powershell.exe -command .\update_docx_props_macro.ps1 ^
-dir . ^
-conf config.xml ^
-callmacro:$true > %CurrentDateTime%_docprops.txt

