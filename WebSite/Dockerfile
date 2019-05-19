FROM mcr.microsoft.com/dotnet/core/aspnet:2.1
COPY ./bin/Release/netcoreapp2.1/publish/ WebSite/
COPY ./bin/Release/netcoreapp2.1/publish/appsettings.Docker.json appsettings.Docker.json
ENTRYPOINT ["dotnet", "WebSite/WebSite.dll"]