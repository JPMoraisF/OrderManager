# ------------------------
# Step 1: Build
# ------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY *.sln .
COPY src/*.csproj ./src/OrderManager/
COPY tests/OrderManager.Tests/*.csproj ./tests/OrderManager.Tests/

RUN dotnet restore

COPY . .
RUN dotnet publish src/OrderManager.csproj -c Release -o /app/publish

# ------------------------
# Step 2: Runtime
# ------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

RUN mkdir -p /app/Data/db

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:5000

EXPOSE 5000

ENTRYPOINT ["dotnet", "OrderManager.dll"]
