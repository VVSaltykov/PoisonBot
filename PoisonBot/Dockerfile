#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["PoisonBot/PoisonBot.csproj", "PoisonBot/"]
RUN dotnet restore "PoisonBot/PoisonBot.csproj"
COPY . .
WORKDIR "/src/PoisonBot"
RUN dotnet build "PoisonBot.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "PoisonBot.csproj" -c Debug -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PoisonBot.dll"]

ENV TOKEN = XXX