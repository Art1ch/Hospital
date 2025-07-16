FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
EXPOSE 80
EXPOSE  433

COPY OfficesAPI/*.csproj OfficesAPI/
COPY OfficesAPI.Application/*.csproj OfficesAPI.Application/
COPY OfficesAPI.Infrastructure/*.csproj OfficesAPI.Infrastructure/
COPY OfficesAPI.Core/*.csproj OfficesAPI.Core/

RUN dotnet restore OfficesAPI/OfficesAPI.csproj

COPY . .

RUN dotnet publish OfficesAPI/OfficesAPI.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "OfficeAPI.dll"]