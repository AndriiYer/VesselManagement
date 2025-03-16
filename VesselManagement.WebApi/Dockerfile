FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["VesselManagement.WebApi/VesselManagement.WebApi.csproj", "VesselManagement.WebApi/"]
RUN dotnet restore "VesselManagement.WebApi/VesselManagement.WebApi.csproj"
COPY . .
WORKDIR "/src/VesselManagement.WebApi"
RUN dotnet build "VesselManagement.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "VesselManagement.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VesselManagement.WebApi.dll"]
