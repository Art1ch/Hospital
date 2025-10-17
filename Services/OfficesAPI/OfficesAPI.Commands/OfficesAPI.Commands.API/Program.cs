using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using OfficeAPI.Commands.API.Middlewares;
using OfficesAPI.Commands.API.Extensions;
using OfficesAPI.Commands.API.Healthchecks;
using OfficesAPI.Commands.Application;
using OfficesAPI.Infrastructure;
using OfficesAPI.Shared.Infrastructure;

namespace OfficesAPI.Commands.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.AddUserSecrets<Program>();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var messageBrokerSettings = builder.ConfigureMessageBroker();
        var eventStoreSettings = builder.ConfigureEventStore();
        var dbSettings = builder.ConfigureOfficeDb();
        builder.ConfigureCloudinary();

        builder.Services
            .AddApplicationLayer()
            .AddInfrastructureLayer(eventStoreSettings, messageBrokerSettings)
            .AddWriteRepository(dbSettings);

        builder.Services.AddAllHealthChecks();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseHealthChecks("/commands/health", new HealthCheckOptions
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
