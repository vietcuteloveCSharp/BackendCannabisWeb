# ===============================
# BUILD STAGE
# ===============================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution + csproj
COPY WebCannabisAccessorries.sln .
COPY ./Cannabis.Server/*.csproj   ./Cannabis.Server/
COPY ./DAL/*.csproj   ./DAL/
COPY ./DTO/*.csproj   ./DTO/
COPY ./Exceptions/*.csproj  ./Exceptions/
COPY ./Service/*.csproj  ./Service/
COPY ./Repository/*.csproj  ./Repository/
COPY ./Enum/*.csproj ./Enum/
COPY ./Middleware/*.csproj ./Middleware/

# Restore
RUN dotnet restore ./Cannabis.Server/Cannabis.Server.csproj

# Copy source
COPY . .

# Publish
WORKDIR /src/Cannabis.Server
RUN dotnet publish -c Release -o /app/publish

# ===============================
# RUNTIME STAGE
# ===============================
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://0.0.0.0:8080

ENTRYPOINT ["dotnet", "Cannabis.Server.dll"]