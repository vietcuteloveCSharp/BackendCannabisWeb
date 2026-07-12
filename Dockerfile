# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY ["WebCannabisAccessorries.sln", "./"]
COPY ["Cannabis.Server/Cannabis.Server.csproj", "Cannabis.Server/"]
COPY ["DAL/DAL.csproj", "DAL/"]
COPY ["Service/Service.csproj", "Service/"]
COPY ["Shared/Shared.csproj", "Shared/"]
RUN dotnet restore Cannabis.Server/Cannabis.Server.csproj
COPY . .
WORKDIR "/src/Cannabis.Server"
RUN dotnet publish -c Release -o /app/publish --no-restore


# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
#ENV ASPNETCORE_ENVIRONMENT=Development

EXPOSE 8080

ENTRYPOINT ["dotnet","Cannabis.Server.dll"]