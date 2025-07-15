FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
EXPOSE 80
EXPOSE  433

COPY AppointmentAPI/*.csproj AppointmentAPI/
COPY AppointmentAPI.Application/*.csproj AppointmentAPI.Application/
COPY AppointmentAPI.Infrastructure/*.csproj AppointmentAPI.Infrastructure/
COPY AppointmentAPI.Core/*.csproj AppointmentAPI.Core/

RUN dotnet restore AppointmentAPI/AppointmentAPI.csproj

COPY . .

RUN dotnet publish AppointmentAPI/AppointmentAPI.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "AppointmentAPI.dll"]