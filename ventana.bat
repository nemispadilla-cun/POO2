@echo off
REM   ventana.bat  -> abre la calculadora de Windows Forms del modulo 5 (G5)
REM   Usa el mismo modelo y controlador que la demo de consola (ejecutar.bat 5).
cd /d "%~dp0"
dotnet run -c Release --project Modulo05_GUI\CalculadoraWinForms
