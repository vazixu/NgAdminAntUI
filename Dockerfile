#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-bionic AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


#สนำร
#FROM mcr.microsoft.com/dotnet/core/sdk:3.1-bionic AS build
FROM registry.cn-shenzhen.aliyuncs.com/vazi/aspnetcoresdk3.1-node10-ng10 AS build
WORKDIR /src
COPY ["NgAdminAntUI.csproj", ""]
RUN dotnet restore "./NgAdminAntUI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "NgAdminAntUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NgAdminAntUI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NgAdminAntUI.dll"]