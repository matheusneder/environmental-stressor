#!/bin/bash

set -e

cd Source/Server/EnvironmentalStressor

ARTIFACTS_FOLDER=./artifacts

if [ ! -d $ARTIFACTS_FOLDER ]
then
	mkdir $ARTIFACTS_FOLDER
fi

dotnet publish -r linux-x64 -c Release -o $ARTIFACTS_FOLDER

cd $ARTIFACTS_FOLDER

docker build -t matheusneder/environmental-stressor:v$TRAVIS_BUILD_NUMBER -t matheusneder/environmental-stressor:latest .
docker login -u "$DOCKER_USERNAME" -p "$DOCKER_PASSWORD"
docker push matheusneder/environmental-stressor:v$TRAVIS_BUILD_NUMBER
docker push matheusneder/environmental-stressor:latest

rm -Rf $ARTIFACTS_FOLDER
