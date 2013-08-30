@ECHO OFF
echo Call WixCop to make a cleaner wxs code
"C:\Program Files (x86)\WiX Toolset v3.7\bin\WixCop.exe" /f /s ..\PicParamMSI\*.wxs
"C:\Program Files (x86)\WiX Toolset v3.7\bin\WixCop.exe" /f /s ..\PicParamMSM\*.wxs
pause