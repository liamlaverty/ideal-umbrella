#!/bin/bash
set -e

# Name of the target container to check
container_name="$1"
# Timeout in seconds. Default: 60
timeout=$((${2:-60}));

echo "docker ps result:"
DOCKER_PS_OUTPUT=$(docker ps)
echo "${DOCKER_PS_OUTPUT}"

if [ -z $container_name ]; then
  echo "No container name specified";
  exit 1;
fi

echo "Container: $container_name";
echo "Timeout: $timeout sec";

# test the container exists before checking its health
CID=$(docker ps -q -f status=running -f name=^/${container_name}$)
if [ ! "${CID}" ]; then
  echo "Container  $container_name doesn't exist"
  exit 1;
fi

try=0;
is_healthy="false";
while [ $is_healthy != "true" ];
do
  try=$(($try + 1));
  printf "■";
  is_healthy=$(docker inspect --format='{{json .State.Health}}' $container_name | jq '.Status == "healthy"');
  sleep 1;
  if [[ $try -eq $timeout ]]; then
    echo " Container did not boot within timeout";
    exit 1;
  fi
done


