#!/usr/bin/env bash
# Compila los dos ejecutables de POO2.sln: la consola y la Web API. Requiere el SDK de .NET 8 o superior.
set -e
cd "$(dirname "$0")"
dotnet build POO2.sln -c Release
echo "Compilacion exitosa. Ejecuta ./ejecutar.sh"
