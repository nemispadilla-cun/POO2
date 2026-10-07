@echo off
setlocal enabledelayedexpansion
REM Compila todo el proyecto en la carpeta "out". Requiere JDK 17 o superior.
chcp 65001 >nul
cd /d "%~dp0"
if exist out rmdir /s /q out
mkdir out
REM Lista de fuentes: cada ruta entre comillas y con barras "/" para que
REM funcione aunque la carpeta tenga espacios (javac trata "\" como escape).
for /f "delims=" %%f in ('dir /s /b src\*.java') do (
    set "ruta=%%f"
    echo "!ruta:\=/!">> out\fuentes.txt
)
javac --release 17 -encoding UTF-8 -d out @out\fuentes.txt
if errorlevel 1 (
    echo.
    echo *** Hubo errores de compilacion. Revisa los mensajes anteriores. ***
    exit /b 1
)
echo Compilacion exitosa. Ejecuta ejecutar.bat
