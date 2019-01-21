#!/bin/bash
# The following commands build docker images for staging and production.
# The staging image will listen on port 8081
# The production image will listen on port 8082

echo Building the image...
docker-compose up --build -d
