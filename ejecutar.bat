@echo off
REM Ejecuta el programa. Sin argumentos abre el menu; con numeros ejecuta esos modulos.
REM   ejecutar.bat          -> menu interactivo
REM   ejecutar.bat 3        -> solo el modulo 3
REM   ejecutar.bat 1 4 7    -> modulos 1, 4 y 7
REM   ejecutar.bat todos    -> todos en orden
chcp 65001 >nul
cd /d "%~dp0"
if not exist out\co\edu\cun\poo\Main.class (
    echo No se encontro la compilacion. Ejecutando compilar.bat...
    call "%~dp0compilar.bat"
    if errorlevel 1 exit /b 1
)
java -Dstdout.encoding=UTF-8 -Dfile.encoding=UTF-8 -cp out co.edu.cun.poo.Main %*
