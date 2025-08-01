using AppointmentAPI.Application.Contracts.Repository.Appointment;
using AppointmentAPI.Application.RepositoryResults.Appointment;
using AppointmentAPI.Core.Entities;
using AppointmentAPI.Core.Enums;
using Dapper;
using System.Data;

namespace AppointmentAPI.Infrastructure.Services.Repository;

internal sealed class AppointmentRepository : IAppointmentRepository
{
    private const string TableName = "Appointments";
    private readonly IDbConnection _connection;

    public AppointmentRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task ChangeAppointmentStatusAsync(Guid id, AppointmentStatus status, CancellationToken cancellationToken = default)
    {
        var sqlQuery = $@"
            UPDATE {TableName} SET
                Status = @Status
            WHERE Id = @Id";
        await _connection.ExecuteAsync(sqlQuery, new { Status = status, Id = id });
    }

    public async Task CreateAsync(AppointmentEntity entity, CancellationToken cancellationToken = default)
    {
        entity.Id = Guid.NewGuid();

        var sql = $@"
            INSERT INTO {TableName} 
                (Id, DoctorId, Date, StartAppointmentTime, EndAppointmentTime, Status)
            VALUES 
                (@Id, @DoctorId, @Date, @StartAppointmentTime, @EndAppointmentTime, @Status)";
        await _connection.ExecuteAsync(sql, entity);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var sqlQuery = $"DELETE FROM {TableName} WHERE Id = @Id";
        await _connection.ExecuteAsync(sqlQuery, new { Id = id });
    }

    public Task<AppointmentEntity> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var sqlQuery = $"SELECT * FROM {TableName} WHERE Id = @Id";
        var appointment = _connection.QuerySingleAsync<AppointmentEntity>(sqlQuery, new { Id = id });
        return appointment;
    }

    public async Task<GetDoctorsAppointmentScheduleResult> GetDoctorsAppointmentScheduleAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var sqlQuery = $@"
                SELECT Date, StartAppointmentTime, EndAppointmentTime
                FROM {TableName}
                WHERE Id = @Id";
        var items = await _connection.QueryAsync<GetDoctorsAppointmentScheduleItem>(sqlQuery, cancellationToken);
        return new GetDoctorsAppointmentScheduleResult(items.ToList());
    }

    public async Task UpdateAsync(AppointmentEntity entity, CancellationToken cancellationToken = default)
    {
        var sqlQuery = $@"
            UPDATE {TableName} SET
                DoctorId = @DoctorId,
                Date = @Date,
                StartAppointmentTime = @StartAppointmentTime,
                EndAppointmentTime = @EndAppointmentTime,
                Status = @Status
            WHERE Id = @Id"; ;
        await _connection.ExecuteAsync(sqlQuery, entity);
    }

    public async Task<IEnumerable<Guid>> GetUpcomingAppointmentsDoctorsIds(
        int minutesBefore,
        int toleranceMinutes,
        CancellationToken cancellationToken = default
    )
    {
        var targetTime = DateTime.Now.AddMinutes(minutesBefore);
        var minTime = targetTime.AddMinutes(-toleranceMinutes);
        var maxTime = targetTime.AddMinutes(toleranceMinutes);

        const string sqlQuery = @"
        SELECT 
            DoctorId, 
        FROM Appointments
        WHERE Status = 3
          AND CAST(Date AS datetime) + CAST(StartAppointmentTime AS datetime) 
              BETWEEN @MinTime AND @MaxTime";

        var doctorIds = await _connection.QueryAsync<Guid>(
            sqlQuery,
            new { MinTime = minTime, MaxTime = maxTime });

        return doctorIds;
    }
}
