# Build
FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY . .
RUN dotnet restore "./JobsApi.AuthedGateway.csproj" --disable-parallel
RUN dotnet publish "./JobsApi.AuthedGateway.csproj" -c release -o /app --no-restore

# Serve
FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal
WORKDIR /app
COPY --from=build /app ./

EXPOSE 80
ENTRYPOINT ["dotnet", "JobsApi.AuthedGateway.dll"]