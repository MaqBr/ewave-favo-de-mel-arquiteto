FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY ["src/Microservices/Identity/FavoDeMel.Identity/FavoDeMel.Identity.csproj", "src/Microservices/Identity/FavoDeMel.Identity/"]
RUN dotnet restore "src/Microservices/Identity/FavoDeMel.Identity/FavoDeMel.Identity.csproj"
COPY . .
WORKDIR "/src/src/Microservices/Identity/FavoDeMel.Identity"
RUN dotnet build "FavoDeMel.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FavoDeMel.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FavoDeMel.Identity.dll"]