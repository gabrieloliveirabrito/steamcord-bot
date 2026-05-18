# build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY SteamCord.slnx .
COPY SteamCord.Bot/ SteamCord.Bot/
COPY SteamCord.Application/ SteamCord.Application/
COPY SteamCord.Infrastructure/ SteamCord.Infrastructure/

RUN dotnet restore SteamCord.Bot/SteamCord.Bot.csproj

RUN dotnet publish \
    SteamCord.Bot/SteamCord.Bot.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

# runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

COPY --from=build /app/publish .

RUN apt-get update && apt-get install -y libgssapi-krb5-2

ENTRYPOINT ["dotnet", "SteamCord.Bot.dll"]