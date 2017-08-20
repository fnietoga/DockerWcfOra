#Creates docker image and tag
sudo docker build --force-rm=true --no-cache=true --shm-size=1G -t fnietoga/oraclelinux:7-slim -f dockerfile .


#Run docker with custom scripts
sudo docker events
sudo docker run --name linux --shm-size=1G -t -d fnietoga/oraclelinux:7-slim
#Container logs
sudo docker logs --details linux
#Interactive bash to docker container
#sudo docker exec -i -t linux /bin/bash
sudo docker container stop linux
sudo docker container rm linux


