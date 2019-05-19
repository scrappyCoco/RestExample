dotnet publish -c Release
docker build -t scrappycoco/my-rest-web-api .
docker push scrappycoco/my-rest-web-api