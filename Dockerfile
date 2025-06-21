# Build a�amas�
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Proje dosyalar�n� kopyala
COPY Currex/Currex/Currex.csproj ./Currex/
RUN dotnet restore ./Currex/Currex.csproj

# T�m dosyalar� kopyala
COPY Currex/ ./Currex/

# Publish yap
RUN dotnet publish ./Currex/Currex.csproj -c Release -o /app/out

# Runtime a�amas�
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Yay�nlanan dosyalar� kopyala
COPY --from=build /app/out .

# Uygulamay� �al��t�r
ENTRYPOINT ["dotnet", "Currex.dll"]

# HTTP portu i�in expose
EXPOSE 80
