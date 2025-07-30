using AuthAPI.Application;
using AuthAPI.Extensions;
using AuthAPI.Infrastructure;
using AuthAPI.Middlewares;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

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
    
        builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

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

        app.UseRequestLocalization(options =>
        {
            var supportedCultures = new[] { new CultureInfo("en"), new CultureInfo("ru") };
            options.DefaultRequestCulture = new RequestCulture("en");
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
        });

        app.UseMiddleware<ExceptionMiddleware>();
        app.UseHttpsRedirection();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
