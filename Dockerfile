# NuGet restore
FROM microsoft/aspnetcore-build AS ms_dotnet_core_build
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
FROM microsoft/aspnetcore
WORKDIR /app
COPY --from=ms_dotnet_core_build /app/out ./
CMD dotnet MD.DemoWebAppWithDynamoDb.dll
