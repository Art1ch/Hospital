using DoctorAPI.Application;
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

        //builder.Services.Configure<JwtSettings>(
        //    builder.Configuration.GetSection(nameof(JwtSettings)));

        //var dbConfig = new DoctorDbConfiguration(
        //    builder.Configuration["ConnectionStrings:DoctorDbString"]!);

        //var jwtSettings = builder.Configuration
        //    .GetSection(nameof(JwtSettings))
        //    .Get<JwtSettings>();

        //builder.Services.AddJwtAuthentication(jwtSettings!);
        //builder.Services.AddSwaggerWithJwt();

        //builder.Services.AddApplicationLayer();
        //builder.Services.AddInfrastructureLayer(dbConfig.DbConnectionString);

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
