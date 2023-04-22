FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /App1
COPY ./bin/Release/net6.0/publish ./
ENTRYPOINT ["dotnet", "DataProcessing.dll"]