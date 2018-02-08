#!/bin/bash

set -e

cd Source/Server/EnvironmentalStressor

dotnet build -c Release
