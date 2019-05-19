dotnet publish -c Release
docker build -t scrappycoco/my-rest-elastic-configurator .
docker push scrappycoco/my-rest-elastic-configurator