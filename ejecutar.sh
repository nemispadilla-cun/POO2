#!/usr/bin/env bash
#   ./ejecutar.sh          -> menu interactivo
#   ./ejecutar.sh 3        -> solo el modulo 3
#   ./ejecutar.sh 1 2      -> modulos 1 y 2
#   ./ejecutar.sh todos    -> todos en orden
cd "$(dirname "$0")"
dotnet run -c Release -- "$@"
