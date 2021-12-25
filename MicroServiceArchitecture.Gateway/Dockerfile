FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim as build
WORKDIR /app
EXPOSE 80
COPY MicroServiceArchitecture.Gateway/*.csproj MicroServiceArchitecture.Gateway/
RUN dotnet restore MicroServiceArchitecture.Gateway/*.csproj
COPY . .
RUN dotnet publish MicroServiceArchitecture.Gateway/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "MicroServiceArchitecture.Gateway.dll" ]