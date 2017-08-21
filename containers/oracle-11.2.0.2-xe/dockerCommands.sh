#Creates docker image and tag
sudo docker build --force-rm=true --no-cache=true --shm-size=1G -t fnietoga/oracledatabase:11.2.0.2-xe -f dockerfile.xe .
#sudo docker push fnietoga/oracledatabase:11.2.0.2-xe

#Run docker with custom scripts
#sudo docker events
sudo docker run --name oracle -p 1521:1521 -p 8089:8080 --shm-size=1G -t -d fnietoga/oracledatabase:11.2.0.2-xe
#Container logs
sudo docker logs --details linux
#Interactive bash to docker container
#sudo docker exec -i -t oracle /bin/bash
sudo docker container stop oracle
sudo docker container rm oracle

##Attach to a image fault in build
docker run --rm -it 87b3a94b9960 /bin/bash