@echo off
REM   api.bat  -> arranca la Web API de CunExpress del modulo 7 (G7) en http://localhost:5070
REM   Dejala abierta y, en OTRA terminal, ejecuta:  ejecutar.bat 7
REM   Swagger (probar la API en el navegador): http://localhost:5070/swagger
REM   Para apagarla: Ctrl+C
chcp 65001 >nul
cd /d "%~dp0"
dotnet run -c Release --project CunExpressApi --launch-profile http
