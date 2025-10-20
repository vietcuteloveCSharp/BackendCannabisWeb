# ===============================
# BUILD STAGE
# ===============================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy toàn bộ source code vào container
COPY . .

# Publish project Web API
RUN dotnet publish "CannabisServer/Cannabis.Server/Cannabis.Server.csproj" -c Release -o /app/publish

# ===============================
# RUNTIME STAGE
# ===============================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy kết quả build từ stage trước
COPY --from=build /app/publish .

# Expose port (Render sẽ map PORT env tự động)
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# Entry point cho ứng dụng
ENTRYPOINT ["dotnet", "Cannabis.Server.dll"]
