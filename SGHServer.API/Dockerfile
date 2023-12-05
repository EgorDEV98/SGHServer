FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["SGHServer.API/SGHServer.API.csproj", "SGHServer.API/"]
COPY ["SGHServer.Domain/SGHServer.Domain.csproj", "SGHServer.Domain/"]
COPY ["SGHServer.Persistence/SGHServer.Persistence.csproj", "SGHServer.Persistence/"]
COPY ["SGHServer.Application/SGHServer.Application.csproj", "SGHServer.Application/"]
RUN dotnet restore "SGHServer.API/SGHServer.API.csproj"
COPY . .
WORKDIR "/src/SGHServer.API"
RUN dotnet build "SGHServer.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SGHServer.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SGHServer.API.dll"]
