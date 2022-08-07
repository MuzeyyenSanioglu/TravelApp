#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TravelApp.API/TravelApp.API.csproj", "TravelApp.API/"]
COPY ["TravelApp.Domain/TravelApp.Domain.csproj", "TravelApp.Domain/"]
COPY ["TravelApp.Infrastructure/TravelApp.Infrastructure.csproj", "TravelApp.Infrastructure/"]
RUN dotnet restore "TravelApp.API/TravelApp.API.csproj"
COPY . .
WORKDIR "/src/TravelApp.API"
RUN dotnet build "TravelApp.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TravelApp.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TravelApp.API.dll"]
