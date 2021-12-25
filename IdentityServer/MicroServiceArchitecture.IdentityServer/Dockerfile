FROM mcr.microsoft.com/dotnet/sdk:3.1 as build
WORKDIR /app
EXPOSE 80
COPY Shared/MicroServiceArchitecture.Shared/*.csproj Shared/MicroServiceArchitecture.Shared/
COPY IdentityServer/MicroServiceArchitecture.IdentityServer/*.csproj IdentityServer/MicroServiceArchitecture.IdentityServer/
RUN dotnet restore IdentityServer/MicroServiceArchitecture.IdentityServer/*.csproj
COPY . .
RUN dotnet publish IdentityServer/MicroServiceArchitecture.IdentityServer/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:3.1 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "MicroServiceArchitecture.IdentityServer.dll" ]