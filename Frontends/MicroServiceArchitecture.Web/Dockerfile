FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/MicroServiceArchitecture.Shared/*.csproj Shared/MicroServiceArchitecture.Shared/
COPY Frontends/MicroServiceArchitecture.Web/*.csproj Frontends/MicroServiceArchitecture.Web/
RUN dotnet restore Frontends/MicroServiceArchitecture.Web/*.csproj
COPY . .
RUN dotnet publish Frontends/MicroServiceArchitecture.Web/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","MicroServiceArchitecture.Web.dll" ]