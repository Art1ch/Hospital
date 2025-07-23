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

        builder.Services.AddApplicationLayer();
        builder.Services.AddInfrastructureLayer(eventStoreSettings, messageBrokerSettings);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.UseMiddleware<ExceptionMiddleware>();

        app.Run();
    }
}
