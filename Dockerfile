# NuGet restore
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

#Install nodejs
RUN apt-get update
RUN apt-get -y install curl gnupg
RUN curl -sL https://deb.nodesource.com/setup_14.x  | bash -
RUN apt-get -y install nodejs

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out


# Build runtime image
# FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
WORKDIR /app
COPY --from=build /out ./
CMD dotnet MD.DemoWebAppWithDynamoDb.dll
