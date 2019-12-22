FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["SCA.Service.Auth/SCA.Service.Auth.csproj", "SCA.Service.Auth/"]
RUN dotnet restore "SCA.Service.Auth/SCA.Service.Auth.csproj"
COPY . .
WORKDIR "/src/SCA.Service.Auth"
RUN dotnet build "SCA.Service.Auth.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SCA.Service.Auth.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SCA.Service.Auth.dll"]