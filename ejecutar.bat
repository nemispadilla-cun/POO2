@echo off
REM   ejecutar.bat          -> menu interactivo
REM   ejecutar.bat 3        -> solo el modulo 3
REM   ejecutar.bat 1 2      -> modulos 1 y 2
REM   ejecutar.bat todos    -> todos en orden
chcp 65001 >nul
cd /d "%~dp0"
dotnet run -c Release -- %*
