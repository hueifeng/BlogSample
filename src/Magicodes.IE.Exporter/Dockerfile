FROM ccr.ccs.tencentyun.com/magicodes/aspnetcore-runtime:3.1 AS base
# 设置默认时区
ENV TZ=Asia/Shanghai
# 安装libgdiplus库，用于Excel导出
RUN apt-get update && apt-get install -y libgdiplus libc6-dev
RUN ln -s /usr/lib/libgdiplus.so /usr/lib/gdiplus.dll

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
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