using DoctorAPI.Application;
using DoctorAPI.Infrastructure;
using DoctorAPI.Middlewares;
using DoctorAPI.Extensions;
using DoctorAPI.Caching;

namespace DoctorAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Configuration.AddUserSecrets<Program>();

        var jwtSettings = builder.ConfigureJwtSettings();
        var connectionString = builder.ConfigureDoctorDbSettings();
        var cacheSettings = builder.ConfigureCacheSettings();

        builder.Services.AddJwtAuthentication(jwtSettings!);
        builder.Services.AddSwaggerWithJwt();
        builder.Services.AddDistributedCache(cacheSettings);

        builder.Services.AddApplicationLayer();
        builder.Services.AddInfrastructureLayer(connectionString);
        builder.Services.AddCaching();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseMiddleware<ExceptionMiddleware>();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
