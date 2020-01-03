#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["SCA.Monitoring/SCA.Monitoring.csproj", "SCA.Monitoring/"]
RUN dotnet restore "SCA.Monitoring/SCA.Monitoring.csproj"
COPY . .
WORKDIR "/src/SCA.Monitoring"
RUN dotnet build "SCA.Monitoring.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SCA.Monitoring.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SCA.Monitoring.dll"]