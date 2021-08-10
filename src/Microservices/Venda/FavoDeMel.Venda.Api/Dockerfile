#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Microservices/Venda/FavoDeMel.Venda.Api/FavoDeMel.Venda.Api.csproj", "src/Microservices/Venda/FavoDeMel.Venda.Api/"]
COPY ["src/BuildingBlocks/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "src/BuildingBlocks/EventBusRabbitMQ/"]
COPY ["src/BuildingBlocks/EventBus/EventBus.csproj", "src/BuildingBlocks/EventBus/"]
COPY ["src/Microservices/Core/FavoDeMel.Domain.Core/FavoDeMel.Domain.Core.csproj", "src/Microservices/Core/FavoDeMel.Domain.Core/"]
COPY ["src/Microservices/Infrastructure/FavoDeMel.Infra.IoC/FavoDeMel.Infra.IoC.csproj", "src/Microservices/Infrastructure/FavoDeMel.Infra.IoC/"]
COPY ["src/BuildingBlocks/EventSourcing/EventSourcing.csproj", "src/BuildingBlocks/EventSourcing/"]
COPY ["src/Microservices/Catalogo/FavoDeMel.Catalogo.Data.Dapper/FavoDeMel.Catalogo.Data.Dapper.csproj", "src/Microservices/Catalogo/FavoDeMel.Catalogo.Data.Dapper/"]
COPY ["src/Microservices/Catalogo/FavoDeMel.Catalogo.Application/FavoDeMel.Catalogo.Application.csproj", "src/Microservices/Catalogo/FavoDeMel.Catalogo.Application/"]
COPY ["src/Microservices/Catalogo/FavoDeMel.Catalogo.Domain/FavoDeMel.Catalogo.Domain.csproj", "src/Microservices/Catalogo/FavoDeMel.Catalogo.Domain/"]
COPY ["src/Microservices/Venda/FavoDeMel.Venda.Domain/FavoDeMel.Venda.Domain.csproj", "src/Microservices/Venda/FavoDeMel.Venda.Domain/"]
COPY ["src/Microservices/Catalogo/FavoDeMel.Catalogo.Data.EF/FavoDeMel.Catalogo.Data.EF.csproj", "src/Microservices/Catalogo/FavoDeMel.Catalogo.Data.EF/"]
COPY ["src/Microservices/Venda/FavoDeMel.Venda.Data/FavoDeMel.Venda.Data.csproj", "src/Microservices/Venda/FavoDeMel.Venda.Data/"]
COPY ["src/Microservices/Venda/FavoDeMel.Venda.Application/FavoDeMel.Venda.Application.csproj", "src/Microservices/Venda/FavoDeMel.Venda.Application/"]
RUN dotnet restore "src/Microservices/Venda/FavoDeMel.Venda.Api/FavoDeMel.Venda.Api.csproj"
COPY . .
WORKDIR "/src/src/Microservices/Venda/FavoDeMel.Venda.Api"
RUN dotnet build "FavoDeMel.Venda.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FavoDeMel.Venda.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FavoDeMel.Venda.Api.dll"]
