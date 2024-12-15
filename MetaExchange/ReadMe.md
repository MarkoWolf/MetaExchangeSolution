# MetaExchange Web API - Docker Deployment Guide

This README provides detailed steps to build, configure, and run the MetaExchange Web API in a Docker container

## Prerequisites
1. **Docker** installed on your system.
2. **Docker Compose** installed (often included with Docker Desktop).
3. Access to the project repository containing the `MetaExchange` solution.

## Steps to Build and Run the Container

### 3. Build and Run the Container
Navigate to the directory containing the `docker-compose.yml` file and run the following commands:

```
docker-compose up --build
```

### Swagger Support

Swagger is available in the Development environment to explore and test the API.

If ASPNETCORE_ENVIRONMENT=Development is set in the docker-compose.yml, Swagger will be exposed at:

http://localhost:4523/index.html

For any other environment (e.g., Production), Swagger will not be available.

### API Endpoints

You can call the following API endpoints directly for execution plans:

Buy Execution Plan:

GET http://localhost:4523/api/OrderBook/executionplans/buy?amount=1.5

Sell Execution Plan:

GET http://localhost:4523/api/OrderBook/executionplans/sell?amount=1.5

## Additional Notes
1. **Environment Switching**: Use the `ASPNETCORE_ENVIRONMENT` variable in `docker-compose.yml` to toggle between `Development` and `Production` environments.
2. **Ports**: The application listens on HTTP port `4523` by default. Adjust the `ports` section in `docker-compose.yml` if needed.


