FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/MicroServiceArchitecture.Shared/*.csproj Shared/MicroServiceArchitecture.Shared/
COPY Services/PhotoStock/PhotoStock.Api/*.csproj Services/PhotoStock/PhotoStock.Api/
RUN dotnet restore Services/PhotoStock/PhotoStock.Api/*.csproj
COPY . .
RUN dotnet publish Services/PhotoStock/PhotoStock.Api/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "PhotoStock.Api.dll" ]