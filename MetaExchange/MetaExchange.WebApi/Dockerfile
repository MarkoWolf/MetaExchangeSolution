# Multi-Stage Build
# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /app

# Copy everything and publish
COPY ./ ./MetaExchange/
WORKDIR /app/MetaExchange/MetaExchange.WebApi

# Restore and publish
WORKDIR /app/MetaExchange
RUN dotnet restore /app/MetaExchange/MetaExchange.sln
RUN dotnet publish -c Release -o /app/out

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Copy the published output from build-env
COPY --from=build-env /app/out .

# Create a directory for logs
ARG LOG_DIR=/app/logs
RUN mkdir -p $LOG_DIR

# Expose ports
EXPOSE 80

# Set environment variables
ENV ASPNETCORE_URLS=http://+:80

# Run the application
ENTRYPOINT ["dotnet", "MetaExchange.WebApi.dll"]