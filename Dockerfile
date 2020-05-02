# NuGet restore
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /

#Install nodejs
RUN apt-get update
RUN apt-get -y install curl gnupg
RUN curl -sL https://deb.nodesource.com/setup_14.x  | bash -
RUN apt-get -y install nodejs

#Install Make
RUN apk add g++ make python

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out


# Build runtime image
FROM microsoft/dotnet:3.1-aspnetcore-runtime
WORKDIR /
COPY --from=build-env /out .
CMD dotnet MD.DemoWebAppWithDynamoDb.dll
