FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
EXPOSE 80
EXPOSE  433

COPY ApiGateway/*.csproj ApiGateway/

RUN dotnet restore ApiGateway/ApiGateway.csproj

COPY . .

RUN dotnet publish ApiGateway/ApiGateway.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "ApiGateway.dll"]