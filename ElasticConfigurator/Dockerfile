FROM mcr.microsoft.com/dotnet/core/runtime:2.1
COPY ./bin/Release/netcoreapp2.1/publish/ ElasticConfigurator/
ENTRYPOINT ["dotnet", "ElasticConfigurator/ElasticConfigurator.dll"]