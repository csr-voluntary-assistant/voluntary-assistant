#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Volutnariat/Volutnariat.csproj", "Volutnariat/"]
RUN dotnet restore "Volutnariat/Volutnariat.csproj"
COPY . .
WORKDIR "/src/Volutnariat"
RUN dotnet build "Volutnariat.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Volutnariat.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Volutnariat.dll"]