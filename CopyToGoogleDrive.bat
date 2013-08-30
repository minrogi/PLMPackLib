@ECHO OFF
REM first, erase existing file
if exist "K:\Google Drive\Packages\PLMPackLib\PLMPackLibMSM.msm" (
	erase "K:\Google Drive\Packages\PLMPackLib\PLMPackLibMSM.msm"
)
if exist "K:\Google Drive\Packages\PLMPackLib\PLMPackLibSetup.exe" (
	erase "K:\Google Drive\Packages\PLMPackLib\PLMPackLibSetup.exe"
)
REM copy new version
copy "K:\Codeplex\PLMPackLib\PicParamMSM\bin\Release\PLMPackLibMSM.msm" "K:\Google Drive\Packages\PLMPackLib\PLMPackLibMSM.msm"
copy "K:\Codeplex\PLMPackLib\PicParamMSI\bin\Release\PLMPackLibSetup.exe" "K:\Google Drive\Packages\PLMPackLib\PLMPackLibSetup.exe"
pause