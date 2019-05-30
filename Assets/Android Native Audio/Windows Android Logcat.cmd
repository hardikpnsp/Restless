@echo off
C:
cd "C:\Users\hardi\AppData\Local\Android\sdk\platform-tools"
adb.exe kill-server
adb.exe logcat -s Unity