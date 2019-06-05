FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["HappyStorage.Web/HappyStorage.Web.csproj", "HappyStorage.Web/"]
COPY ["HappyStorage.FileStorage/HappyStorage.FileStorage.csproj", "HappyStorage.FileStorage/"]
COPY ["HappyStorage.Core/HappyStorage.Core.csproj", "HappyStorage.Core/"]
COPY ["HappyStorage.SqlStorage/HappyStorage.SqlStorage.csproj", "HappyStorage.SqlStorage/"]
COPY ["HappyStorage.Common.Ui/HappyStorage.Common.Ui.csproj", "HappyStorage.Common.Ui/"]
COPY ["HappyStorage.MemoryStorage/HappyStorage.MemoryStorage.csproj", "HappyStorage.MemoryStorage/"]
RUN dotnet restore "HappyStorage.Web/HappyStorage.Web.csproj"
COPY . .
WORKDIR "/src/HappyStorage.Web"
RUN dotnet build "HappyStorage.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "HappyStorage.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HappyStorage.Web.dll"]