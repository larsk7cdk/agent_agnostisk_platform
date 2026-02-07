FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/AgentAgnostiskPlatform.API/AgentAgnostiskPlatform.API.csproj", "src/AgentAgnostiskPlatform.API/"]
COPY ["src/AgentAgnostiskPlatform.Application/AgentAgnostiskPlatform.Application.csproj", "src/AgentAgnostiskPlatform.Application/"]
COPY ["src/AgentAgnostiskPlatform.Domain/AgentAgnostiskPlatform.Domain.csproj", "src/AgentAgnostiskPlatform.Domain/"]
COPY ["src/AgentAgnostiskPlatform.Infrastructure/AgentAgnostiskPlatform.Infrastructure.csproj", "src/AgentAgnostiskPlatform.Infrastructure/"]
COPY ["src/AgentAgnostiskPlatform.Persistence/AgentAgnostiskPlatform.Persistence.csproj", "src/AgentAgnostiskPlatform.Persistence/"]
RUN dotnet restore "src/AgentAgnostiskPlatform.API/AgentAgnostiskPlatform.API.csproj"
COPY . .
WORKDIR "/src/src/AgentAgnostiskPlatform.API"
RUN dotnet build "AgentAgnostiskPlatform.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AgentAgnostiskPlatform.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AgentAgnostiskPlatform.API.dll"]
