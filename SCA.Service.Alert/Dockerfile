FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["SCA.Service.Alert/SCA.Service.Alert.csproj", "SCA.Service.Alert/"]
RUN dotnet restore "SCA.Service.Alert/SCA.Service.Alert.csproj"
COPY . .
WORKDIR "/src/SCA.Service.Alert"
RUN dotnet build "SCA.Service.Alert.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SCA.Service.Alert.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SCA.Service.Alert.dll"]