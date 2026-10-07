#!/usr/bin/env bash
# Compila todo el proyecto en la carpeta "out". Requiere JDK 17 o superior.
set -e
cd "$(dirname "$0")"
rm -rf out
mkdir -p out
find src -name '*.java' > out/fuentes.txt
javac --release 17 -encoding UTF-8 -d out @out/fuentes.txt
echo "Compilación exitosa. Ejecuta ./ejecutar.sh"
