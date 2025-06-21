# Build aþamasý
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Proje dosyalarýný kopyala
COPY Currex/Currex/Currex.csproj ./Currex/
RUN dotnet restore ./Currex/Currex.csproj

# Tüm dosyalarý kopyala
COPY Currex/ ./Currex/

# Publish yap
RUN dotnet publish ./Currex/Currex.csproj -c Release -o /app/out

# Runtime aþamasý
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Yayýnlanan dosyalarý kopyala
COPY --from=build /app/out .

# Uygulamayý çalýþtýr
ENTRYPOINT ["dotnet", "Currex.dll"]

# HTTP portu için expose
EXPOSE 80
