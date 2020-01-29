FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
ENV TZ=America/Sao_Paulo
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["SCA.Service.Monitoring/SCA.Service.Monitoring.csproj", "SCA.Service.Monitoring/"]
RUN dotnet restore "SCA.Service.Monitoring/SCA.Service.Monitoring.csproj"
COPY . .
WORKDIR "/src/SCA.Service.Monitoring"
RUN dotnet build "SCA.Service.Monitoring.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SCA.Service.Monitoring.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SCA.Service.Monitoring.dll"]