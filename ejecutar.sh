#!/usr/bin/env bash
# Ejecuta el programa. Sin argumentos abre el menú; con números ejecuta esos módulos.
#   ./ejecutar.sh          -> menú interactivo
#   ./ejecutar.sh 3        -> solo el módulo 3
#   ./ejecutar.sh 1 4 7    -> módulos 1, 4 y 7
#   ./ejecutar.sh todos    -> todos en orden
cd "$(dirname "$0")"
if [ ! -f out/co/edu/cun/poo/Main.class ]; then
    echo "No se encontró la compilación. Ejecutando compilar.sh..."
    ./compilar.sh
fi
java -Dstdout.encoding=UTF-8 -Dfile.encoding=UTF-8 -cp out co.edu.cun.poo.Main "$@"
