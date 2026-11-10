@echo off
REM Compila los dos ejecutables de POO2.sln: la consola y la Web API. Requiere el SDK de .NET 8 o superior.
chcp 65001 >nul
cd /d "%~dp0"
dotnet build POO2.sln -c Release
if errorlevel 1 (
    echo.
    echo *** Hubo errores de compilacion. Revisa los mensajes anteriores. ***
    exit /b 1
)
echo Compilacion exitosa. Ejecuta ejecutar.bat
