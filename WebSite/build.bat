dotnet publish -c Release
docker build -t scrappycoco/my-rest-web-site .
docker push scrappycoco/my-rest-web-site