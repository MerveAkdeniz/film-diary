FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# SADECE PROJEYÝ KOPYALA
COPY backend/FilmDiary.API/FilmDiary.API/FilmDiary.API.csproj ./ 
RUN dotnet restore

# TÜM DOSYALARI KOPYALA
COPY backend/FilmDiary.API/FilmDiary.API/. ./
RUN dotnet publish -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 80
ENTRYPOINT ["dotnet", "FilmDiary.API.dll"]