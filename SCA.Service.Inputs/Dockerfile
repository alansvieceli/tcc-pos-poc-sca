FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["SCA.Service.Inputs/SCA.Service.Inputs.csproj", "SCA.Service.Inputs/"]
RUN dotnet restore "SCA.Service.Inputs/SCA.Service.Inputs.csproj"
COPY . .
WORKDIR "/src/SCA.Service.Inputs"
RUN dotnet build "SCA.Service.Inputs.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SCA.Service.Inputs.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SCA.Service.Inputs.dll"]