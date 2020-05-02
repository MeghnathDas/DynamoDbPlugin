# NuGet restore
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /

#Install nodejs
RUN apt-get update \
    && apt-get upgrade -y \
    && curl -sL https://deb.nodesource.com/setup_8.x | bash - \
    && apt-get install -y nodejs \
    
# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out


# Build runtime image
FROM microsoft/dotnet:3.1-aspnetcore-runtime
WORKDIR /
COPY --from=build-env /app/out .
CMD dotnet MD.DemoWebAppWithDynamoDb.dll
