FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ./AkinBook.sln ./
COPY ./src ./src

RUN dotnet restore ./AkinBook.sln
RUN dotnet publish ./src/AkinBook.Api/AkinBook.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "AkinBook.Api.dll"]