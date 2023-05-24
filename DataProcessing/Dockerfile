FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /App1
COPY ./bin/Release/net6.0/publish ./
ENV ASPNETCORE_ENVIRONMENT=Release
ENV TZ=Europe/Prague
ENV LANG=cs_CZ.UTF-8
ENTRYPOINT ["dotnet", "DataProcessing.dll"]