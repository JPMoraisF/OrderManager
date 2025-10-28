# ------------------------
# Etapa 1: Build
# ------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia a solução e os projetos
COPY *.sln .
COPY src/OrderManager/*.csproj ./src/OrderManager/
COPY tests/OrderManager.Tests/*.csproj ./tests/OrderManager.Tests/

# Restaura dependências
RUN dotnet restore

# Copia todo o código e publica em Release
COPY . .
RUN dotnet publish src/OrderManager/OrderManager.csproj -c Release -o /app/publish

# ------------------------
# Etapa 2: Runtime
# ------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

RUN mkdir -p /app/Data/db

# Copia os arquivos do build
COPY --from=build /app/publish .

# Configura HTTP na porta 5000
ENV ASPNETCORE_URLS=http://+:5000

# Expõe a porta do container
EXPOSE 5000

# Comando para iniciar a aplicação
ENTRYPOINT ["dotnet", "OrderManager.dll"]
