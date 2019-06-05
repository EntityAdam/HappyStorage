FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["HappyStorage.IntegrationTests.csproj", "HappyStorage.IntegrationTests/"]
RUN dotnet restore "HappyStorage.IntegrationTests/HappyStorage.IntegrationTests.csproj"
COPY . .
WORKDIR "/src/HappyStorage.IntegrationTests"
RUN dotnet build
RUN dotnet test