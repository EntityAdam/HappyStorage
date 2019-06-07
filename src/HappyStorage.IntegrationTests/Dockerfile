FROM mcr.microsoft.com/dotnet/core/sdk:2.2
WORKDIR /src
ENV ConnectionStrings__Postgres ""
ENV MigratorDll "/src/HappyStorage.Migrations/bin/Debug/netstandard2.0/HappyStorage.Migrations.dll"
RUN dotnet tool install -g FluentMigrator.DotNet.Cli
ENV HOME="/root"
ENV PATH="${HOME}/.dotnet/tools:${PATH}"
COPY ["HappyStorage.IntegrationTests.csproj", "HappyStorage.IntegrationTests/"]
RUN dotnet restore "HappyStorage.IntegrationTests/HappyStorage.IntegrationTests.csproj"
COPY . .
WORKDIR "/src/HappyStorage.IntegrationTests"
RUN dotnet build
RUN dotnet fm migrate -p postgres -c $ConnectionStrings__Postgres -a $MigratorDll
RUN dotnet test