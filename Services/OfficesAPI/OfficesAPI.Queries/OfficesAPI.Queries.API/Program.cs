using OfficeAPI.Queries.API.Extensions;
using OfficeAPI.Queries.API.Middlewares;
using OfficesAPI.Queries.API.Extensions;
using OfficesAPI.Queries.Application;
using OfficesAPI.Queries.Infrastructure;

namespace OfficesAPI.Queries.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Configuration.AddUserSecrets<Program>();

        builder.ConfigureDbSettings();
        var messageBrokerSettings = builder.ConfigureMessageBroker();
        var corsSettings = builder.ConfigureCors();

        builder.Services.AddApplicationLayer(messageBrokerSettings)
            .AddInfrastructureLayer();

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
