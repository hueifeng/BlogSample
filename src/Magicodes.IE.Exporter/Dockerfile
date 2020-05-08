#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM ccr.ccs.tencentyun.com/magicodes/aspnetcore-runtime:latest AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY /src/Magicodes.IE.Exporter/simsun.ttc /usr/share/fonts/simsun.ttc

FROM mcr.microsoft.com/dotnet/core/sdk:latest AS build
WORKDIR /src
COPY ["src/Magicodes.IE.Exporter/Magicodes.IE.Exporter.csproj", "src/Magicodes.IE.Exporter/"]
RUN dotnet restore "src/Magicodes.IE.Exporter/Magicodes.IE.Exporter.csproj"
COPY . .
WORKDIR "/src/src/Magicodes.IE.Exporter"
RUN dotnet build "Magicodes.IE.Exporter.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Magicodes.IE.Exporter.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Magicodes.IE.Exporter.dll"]