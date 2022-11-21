#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebApi_AzureSQL/WebApi_AzureSQL.csproj", "WebApi_AzureSQL/"]
RUN dotnet restore "WebApi_AzureSQL/WebApi_AzureSQL.csproj"
COPY . .
WORKDIR "/src/WebApi_AzureSQL"
RUN dotnet build "WebApi_AzureSQL.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApi_AzureSQL.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi_AzureSQL.dll"]