#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
ENV DB_CONNECTION_STRING="Data Source=host.docker.internal,1433;Initial Catalog=starbucksdemo;TrustServerCertificate=True;User ID=sa;Password=dL*vYDmxr@j"
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["StarbucksDemo/StarbucksDemo.csproj", "StarbucksDemo/"]
COPY ["StarbucksDemo.Application/StarbucksDemo.Application.csproj", "StarbucksDemo.Application/"]
COPY ["StarbucksDemo.Core/StarbucksDemo.Core.csproj", "StarbucksDemo.Core/"]
COPY ["StarbucksDemo.Infraestructure/StarbucksDemo.Infraestructure.csproj", "StarbucksDemo.Infraestructure/"]
RUN dotnet restore "./StarbucksDemo/StarbucksDemo.csproj"
COPY . .
WORKDIR "/src/StarbucksDemo"
RUN dotnet build "./StarbucksDemo.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./StarbucksDemo.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StarbucksDemo.dll"]