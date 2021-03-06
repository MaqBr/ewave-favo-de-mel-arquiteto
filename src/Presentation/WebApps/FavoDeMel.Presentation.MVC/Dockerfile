#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY ["src/Presentation/WebApps/FavoDeMel.Presentation.MVC/FavoDeMel.Presentation.MVC.csproj", "src/Presentation/WebApps/FavoDeMel.Presentation.MVC/"]
COPY ["src/BuildingBlocks/EventSourcing/EventSourcing.csproj", "src/BuildingBlocks/EventSourcing/"]
COPY ["src/Microservices/Core/FavoDeMel.Domain.Core/FavoDeMel.Domain.Core.csproj", "src/Microservices/Core/FavoDeMel.Domain.Core/"]
COPY ["src/BuildingBlocks/EventBus/EventBus.csproj", "src/BuildingBlocks/EventBus/"]
RUN dotnet restore "src/Presentation/WebApps/FavoDeMel.Presentation.MVC/FavoDeMel.Presentation.MVC.csproj"
COPY . .
WORKDIR "/src/src/Presentation/WebApps/FavoDeMel.Presentation.MVC"
RUN dotnet build "FavoDeMel.Presentation.MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FavoDeMel.Presentation.MVC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FavoDeMel.Presentation.MVC.dll"]