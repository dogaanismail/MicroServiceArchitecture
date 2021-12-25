FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/MicroServiceArchitecture.Shared/*.csproj Shared/MicroServiceArchitecture.Shared/
COPY Services/Discount/Discount.Api/*.csproj Services/Discount/Discount.Api/
RUN dotnet restore Services/Discount/Discount.Api/*.csproj
COPY . .
RUN dotnet publish Services/Discount/Discount.Api/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "Discount.Api.dll" ]