# Utilise l'image de base pour ASP.NET Core
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Utilise l'image de base pour le SDK .NET (pour construire l'application)
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["infolog_asp_fab/infolog_asp_fab.csproj", "./"]
RUN dotnet restore "./infolog_asp_fab.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "infolog_asp_fab.csproj" -c Release -o /app/build

# Étape de publication
FROM build AS publish
RUN dotnet publish "infolog_asp_fab.csproj" -c Release -o /app/publish

# Étape finale : copie de la publication dans l'image finale
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "infolog_asp_fab.dll"]
