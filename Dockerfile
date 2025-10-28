# ===============================
# BUILD STAGE
# ===============================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy toàn bộ source code vào  /src
COPY ./Cannabis.Server/*.csproj   ./Cannabis.Server/
COPY ./DAL/*.csproj   ./DAL/
COPY ./DTO/*.csproj   ./DTO/
COPY ./Exceptions/*.csproj  ./Exceptions/
COPY ./Service/*.csproj  ./Service/
COPY ./Repository/*.csproj  ./Repository/
COPY ./Enum/*.csproj ./Enum/
COPY ./Middleware/*.csproj ./Middleware/

RUN apt-get update && apt-get install -y ca-certificates

# Restore dependencies
RUN dotnet restore ./Cannabis.Server/Cannabis.Server.csproj
COPY ./Cannabis.Server   ./Cannabis.Server
COPY ./DAL   ./DAL
COPY ./DTO   ./DTO
COPY ./Exceptions  ./Exceptions
COPY ./Service  ./Service
COPY ./Repository  ./Repository
COPY ./Enum ./Enum
COPY ./Middleware ./Middleware
# Publish project Web API
RUN dotnet publish "./Cannabis.Server/Cannabis.Server.csproj" -c Release -o /app/publish

# ===============================
# RUNTIME STAGE
# ===============================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy kết quả build từ stage trước
COPY --from=build /app/publish .

# Expose port (Render sẽ map PORT env tự động)
EXPOSE 8080
ENV ASPNETCORE_ENVIRONMENT=Development

# Entry point cho ứng dụng
ENTRYPOINT ["dotnet", "Cannabis.Server.dll"]
