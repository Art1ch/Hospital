using DoctorAPI.Application;
using DoctorAPI.Configuration;
using DoctorAPI.Infrastructure;
using DoctorAPI.Middlewares;

namespace DoctorAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var dbConfig = new DoctorDbConfiguration(builder.Configuration);

        builder.Services.AddApplicationLayer();
        builder.Services.AddInfrastructureLayer(dbConfig.ConnectionString);

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseMiddleware<ExceptionMiddleware>();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
