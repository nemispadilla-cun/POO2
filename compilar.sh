#!/usr/bin/env bash
# Compila el proyecto. Requiere el SDK de .NET 8 o superior.
set -e
cd "$(dirname "$0")"
dotnet build -c Release
echo "Compilacion exitosa. Ejecuta ./ejecutar.sh"
