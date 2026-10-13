@echo off
REM Compila el proyecto. Requiere el SDK de .NET 8 o superior.
chcp 65001 >nul
cd /d "%~dp0"
dotnet build -c Release
if errorlevel 1 (
    echo.
    echo *** Hubo errores de compilacion. Revisa los mensajes anteriores. ***
    exit /b 1
)
echo Compilacion exitosa. Ejecuta ejecutar.bat
