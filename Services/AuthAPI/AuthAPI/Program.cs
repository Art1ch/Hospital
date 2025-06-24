using AuthAPI.Application;
using AuthAPI.Configuration.DbSettings;
using AuthAPI.Configuration.JwtSettings;
using AuthAPI.Extensions;
using AuthAPI.Infrastructure;

namespace AuthAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.ConfigureJwtSettings();
        var connectionString = builder.ConfigureAuthDbSettings();

        builder.Services.AddApplicationLayer();
        builder.Services.AddInfrastructureLayer(connectionString);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
