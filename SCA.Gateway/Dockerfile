FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["SCA.Gateway/SCA.Gateway.csproj", "SCA.Gateway/"]
RUN dotnet restore "SCA.Gateway/SCA.Gateway.csproj"
COPY . .
WORKDIR "/src/SCA.Gateway"
RUN dotnet build "SCA.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SCA.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SCA.Gateway.dll"]