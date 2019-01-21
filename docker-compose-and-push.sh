#!/bin/bash
# The following commands build and run the docker images for staging and production and will update the docker hub repository
# with the production image
#
# The staging app will listen on port 8081
# The production app will listen on port 8082

echo Building the images...
docker-compose up --build -d

echo Pushing to the repository 'jbeasley/mind'
docker login --username=jbeasley --password=trip001
docker push jbeasley/mind:latest
