@echo off
setlocal
powershell -NoProfile -ExecutionPolicy Bypass -File "%~dp0Install-PTBR.ps1" -RestoreLatest %*
endlocal
