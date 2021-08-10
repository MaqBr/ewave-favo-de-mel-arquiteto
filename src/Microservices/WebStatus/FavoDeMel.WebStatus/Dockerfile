#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Microservices/WebStatus/FavoDeMel.WebStatus/FavoDeMel.WebStatus.csproj", "src/Microservices/WebStatus/FavoDeMel.WebStatus/"]
COPY ["src/Microservices/Core/FavoDeMel.Domain.Core/FavoDeMel.Domain.Core.csproj", "src/Microservices/Core/FavoDeMel.Domain.Core/"]
RUN dotnet restore "src/Microservices/WebStatus/FavoDeMel.WebStatus/FavoDeMel.WebStatus.csproj"
COPY . .
WORKDIR "/src/src/Microservices/WebStatus/FavoDeMel.WebStatus"
RUN dotnet build "FavoDeMel.WebStatus.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FavoDeMel.WebStatus.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FavoDeMel.WebStatus.dll"]
