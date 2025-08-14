using Dapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System.Data;

namespace AppointmentAPI.Infrastructure.DataInitializers;

internal sealed class DatabaseInitializer : IStartupFilter
{
    private readonly IDbConnection _connection;

    public DatabaseInitializer(IDbConnection connection)
    {
        _connection = connection;
    }

    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        InitializeDatabase();
        return next;
    }

    private void InitializeDatabase()
    {
        _connection.Execute(@"
            IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Appointments')
            CREATE TABLE Appointments (
                Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
                DoctorId UNIQUEIDENTIFIER NOT NULL,
                Date DATE NOT NULL,
                StartAppointmentTime TIME NOT NULL,
                EndAppointmentTime TIME NOT NULL,
                Status TINYINT NOT NULL,
                
                CONSTRAINT CHK_AppointmentTimes CHECK (EndAppointmentTime > StartAppointmentTime),
                CONSTRAINT CHK_StatusRange CHECK (Status BETWEEN 1 AND 3)
            )");
    }
}
