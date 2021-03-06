FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /source

COPY Chakra.Web/*.csproj .
RUN dotnet restore

COPY ./Chakra.Web/ .
RUN dotnet publish -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app

COPY --from=build /app .

EXPOSE 80
ENTRYPOINT ["dotnet", "Chakra.Web.dll"]