FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["HappyStorage.BlazorWeb/HappyStorage.BlazorWeb.csproj", "HappyStorage.BlazorWeb/"]
COPY ["HappyStorage.FileStorage/HappyStorage.FileStorage.csproj", "HappyStorage.FileStorage/"]
COPY ["HappyStorage.Core/HappyStorage.Core.csproj", "HappyStorage.Core/"]
COPY ["HappyStorage.SqlStorage/HappyStorage.SqlStorage.csproj", "HappyStorage.SqlStorage/"]
COPY ["HappyStorage.Common.Ui/HappyStorage.Common.Ui.csproj", "HappyStorage.Common.Ui/"]
COPY ["HappyStorage.MemoryStorage/HappyStorage.MemoryStorage.csproj", "HappyStorage.MemoryStorage/"]
RUN dotnet restore "HappyStorage.BlazorWeb/HappyStorage.BlazorWeb.csproj"
COPY . .
WORKDIR "/src/HappyStorage.BlazorWeb"
RUN dotnet build "HappyStorage.BlazorWeb.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "HappyStorage.BlazorWeb.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HappyStorage.BlazorWeb.dll"]