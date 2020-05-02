# NuGet restore
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln .
COPY MD.Core.DynamoDbPlugin/*.csproj MD.Core.DynamoDbPlugin/
COPY MD.DemoWebAppWithDynamoDb/*.csproj MD.DemoWebAppWithDynamoDb/
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out


# Build runtime image
FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/out .
CMD dotnet MD.DemoWebAppWithDynamoDb.dll
