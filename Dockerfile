FROM mcr.microsoft.com/dotnet/sdk:5.0 as build
WORKDIR /source
COPY BookKeeperBot.csproj BookKeeperBot.sln ./
RUN dotnet restore
COPY . .
RUN dotnet publish -c release -o /build --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:5.0 as release
WORKDIR /app
COPY --from=build /build ./

ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:5000
ENV BotConfiguration:Token=token

ENTRYPOINT [ "dotnet", "BookKeeperBot.dll" ]