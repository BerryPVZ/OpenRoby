@echo off
set "TARGET=C:\Users\mango\Desktop\OpenRoby"

echo Deleting all .meta files from:
echo %TARGET%
echo.

powershell -NoProfile -ExecutionPolicy Bypass -Command ^
"Get-ChildItem -LiteralPath '%TARGET%' -Filter '*.meta' -File -Recurse -Force -ErrorAction SilentlyContinue | Remove-Item -Force"

echo.
echo Finished deleting .meta files.
pause