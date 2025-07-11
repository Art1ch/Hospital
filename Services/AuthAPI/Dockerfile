FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
EXPOSE 80
EXPOSE  433

COPY AuthAPI/*.csproj AuthAPI/
COPY AuthAPI.Application/*.csproj AuthAPI.Application/
COPY AuthAPI.Infrastructure/*.csproj AuthAPI.Infrastructure/
COPY AuthAPI.Core/*.csproj AuthAPI.Core/

RUN dotnet restore AuthAPI/AuthAPI.csproj

COPY . .

RUN dotnet publish AuthAPI/AuthAPI.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "AuthAPI.dll"]