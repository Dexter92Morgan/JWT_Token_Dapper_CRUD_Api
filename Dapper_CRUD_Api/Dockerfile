#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Dapper_CRUD_Api/Dapper_CRUD_Api.csproj", "Dapper_CRUD_Api/"]
COPY ["Datas/Datas.csproj", "Datas/"]


RUN dotnet restore "Dapper_CRUD_Api/Dapper_CRUD_Api.csproj"
COPY . .
WORKDIR "/src/Dapper_CRUD_Api"
RUN dotnet build "Dapper_CRUD_Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dapper_CRUD_Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dapper_CRUD_Api.dll"]