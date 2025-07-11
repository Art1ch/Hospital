FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
EXPOSE 80
EXPOSE  433

COPY DoctorAPI/*.csproj DoctorAPI/
COPY DoctorAPI.Application/*.csproj DoctorAPI.Application/
COPY DoctorAPI.Infrastructure/*.csproj DoctorAPI.Infrastructure/
COPY DoctorAPI.Core/*.csproj DoctorAPI.Core/

RUN dotnet restore DoctorAPI/DoctorAPI.csproj

COPY . .

RUN dotnet publish DoctorAPI/DoctorAPI.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "DoctorAPI.dll"]