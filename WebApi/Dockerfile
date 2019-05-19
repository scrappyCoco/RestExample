FROM mcr.microsoft.com/dotnet/core/aspnet:2.1
COPY ./bin/Release/netcoreapp2.1/publish/ WebApi/
COPY ./bin/Release/netcoreapp2.1/publish/appsettings.Docker.json appsettings.Docker.json
ENTRYPOINT ["dotnet", "WebApi/WebApi.dll"]