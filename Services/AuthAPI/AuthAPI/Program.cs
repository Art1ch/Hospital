using AuthAPI.Application;
using AuthAPI.Extensions;
using AuthAPI.Infrastructure;
using AuthAPI.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace AuthAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Configuration.AddUserSecrets<Program>();

        builder.WebHost.ConfigureKestrel(options =>
        {
            options.ListenAnyIP(5000, listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http2;
            });
        });


        builder.ConfigureJwtSettings();
        var connectionString = builder.ConfigureAuthDbSettings();

        builder.Services.AddApplicationLayer();
        builder.Services.AddInfrastructureLayer(connectionString);

        builder.Services.AddGrpc();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
        app.MapGrpcService<AuthGrpcService>();

        app.Run();
    }
}
