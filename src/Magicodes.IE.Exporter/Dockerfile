FROM ccr.ccs.tencentyun.com/magicodes/aspnetcore-runtime:latest AS base

#WORKDIR /src
COPY simsun.ttc /usr/share/fonts/simsun.ttc

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:latest AS build
#WORKDIR /src
COPY ["Magicodes.IE.Exporter.csproj", "Magicodes.IE.Exporter/"]
RUN dotnet restore "Magicodes.IE.Exporter/Magicodes.IE.Exporter.csproj"
COPY . .
#WORKDIR "Magicodes.IE.Exporter"
RUN dotnet build "Magicodes.IE.Exporter.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Magicodes.IE.Exporter.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from= publish /app/publish .
ENTRYPOINT ["dotnet", "Magicodes.IE.Exporter.dll"]