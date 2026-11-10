#!/usr/bin/env bash
#   ./api.sh  -> arranca la Web API de CunExpress del modulo 7 (G7) en http://localhost:5070
#   Dejala abierta y, en OTRA terminal, ejecuta:  ./ejecutar.sh 7
#   Swagger (probar la API en el navegador): http://localhost:5070/swagger
#   Para apagarla: Ctrl+C
cd "$(dirname "$0")"
dotnet run -c Release --project CunExpressApi --launch-profile http
