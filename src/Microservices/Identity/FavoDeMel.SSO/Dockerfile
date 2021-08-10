#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Microservices/Identity/FavoDeMel.SSO/FavoDeMel.SSO.csproj", "src/Microservices/Identity/FavoDeMel.SSO/"]
RUN dotnet restore "src/Microservices/Identity/FavoDeMel.SSO/FavoDeMel.SSO.csproj"
COPY . .
WORKDIR "/src/src/Microservices/Identity/FavoDeMel.SSO"
RUN dotnet build "FavoDeMel.SSO.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FavoDeMel.SSO.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FavoDeMel.SSO.dll"]
