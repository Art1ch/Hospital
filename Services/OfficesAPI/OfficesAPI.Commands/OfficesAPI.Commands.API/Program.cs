using OfficeAPI.Commands.API.Middlewares;
using OfficesAPI.Commands.API.Extensions;
using OfficesAPI.Commands.Application;
using OfficesAPI.Infrastructure;

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
        var corsSettings = builder.ConfigureCors();

        builder.Services.AddApplicationLayer()
            .AddInfrastructureLayer(eventStoreSettings, messageBrokerSettings);

        builder.Services.AddCorsPolicy(corsSettings);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors(corsSettings.PolicyName);
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
