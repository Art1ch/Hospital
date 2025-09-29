using Ocelot.DependencyInjection;
using Ocelot.Extensions;
using Ocelot.Middleware;

namespace ApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
            builder.Services.AddOcelot();

            var corsSettings = builder.ConfigureCors();
            builder.Services.AddCorsPolicy(corsSettings);

            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseCors(corsSettings.PolicyName);
            app.UseAuthorization();
            app.MapControllers();
            app.UseOcelot().Wait();
            app.Run();
        }
    }
}
