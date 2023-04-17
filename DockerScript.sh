docker build --platform=linux/amd64 -t weatherschoolapp ./BlazorWeaherSchoolAPP
docker build --platform=linux/amd64 -t dataprocessing ./DataProcessing
docker save -o ./BlazorWeatherSchoolAPP.tar weatherschoolapp
docker save -o ./DataProcessing.tar dataprocessing

scp ./BlazorWeatherSchoolAPP.tar spravce@meteo.vscht.cz:./BlazorWeatherSchoolAPP.tar
scp ./DataProcessing.tar spravce@meteo.vscht.cz:./DataProcessing.tar 
