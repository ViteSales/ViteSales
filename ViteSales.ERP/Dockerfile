FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ViteSales.ERP/ViteSales.ERP.csproj", "ViteSales.ERP/"]
RUN dotnet restore "ViteSales.ERP/ViteSales.ERP.csproj"
COPY . .
WORKDIR "/src/ViteSales.ERP"
RUN dotnet build "ViteSales.ERP.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ViteSales.ERP.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ViteSales.ERP.dll"]
