using AppointmentAPI.Application;
using AppointmentAPI.Infrastructure;
using AppointmentAPI.Endpoints;
using AppointmentAPI.Extensions;
using AppointmentAPI.Workers.AppointmentReminderWorker;

namespace AppointmentAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.AddUserSecrets<Program>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var connectionString = builder.ConfigureAppointmentDbSetting();
        builder.Services.AddApplicationLayer();
        builder.Services.AddInfrastructureLayer(connectionString);
        builder.Services.AddHostedService<AppointmentReminderWorker>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapAppointmentEndpoints();

        app.Run();
    }
}
