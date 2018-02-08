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

IMAGE_NAME="matheusneder/environmental-stressor"

docker build -t $IMAGE_NAME:$TRAVIS_TAG -t $IMAGE_NAME:latest .
docker login -u "$DOCKER_USERNAME" -p "$DOCKER_PASSWORD"
docker push $IMAGE_NAME:$TRAVIS_TAG
docker push $IMAGE_NAME:latest

rm -Rf $ARTIFACTS_FOLDER
