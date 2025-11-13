using ApiGateway.DelegateHandlers;
using ApiGateway.Middlewares;
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
            builder.Services.AddOfficeGatewayHttpClient();
            builder.Services
                .AddOcelot()
                .AddDelegatingHandler<HeaderOfficeRoutingHandler>(global: true);


            var corsSettings = builder.ConfigureCors();
            builder.ConfigureOfficeServiceAddresses();

            builder.Services.AddCorsPolicy(corsSettings);

            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseCors(corsSettings.PolicyName);
            app.UseMiddleware<AuthorizationHeaderApplierMiddleware>();
            app.MapControllers();
            app.UseOcelot();
            app.Run();
        }
    }    
}
