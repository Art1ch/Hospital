using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;
using Microsoft.Extensions.Configuration;
using DoctorAPI.Infrastructure.Context;
using DoctorAPI.IntegrationTests.Settings;

namespace DoctorAPI.IntegrationTests.Fixtures;

public sealed class PostgreSqlTestContainerFixture : IAsyncLifetime
{
    private readonly PostgreSqlContainer _container;
    internal DoctorDbContext DbContext { get; private set; }
    private readonly IConfiguration _config;

    public PostgreSqlTestContainerFixture()
    {
        var config = new ConfigurationBuilder()
            .AddUserSecrets<PostgreSqlTestContainerFixture>()
            .Build();

        var settings = config.GetSection("DbSettings").Get<DbSettings>();

        _container = new PostgreSqlBuilder()
            .WithImage(settings.Image)
            .WithDatabase(settings.Database)
            .WithUsername(settings.Username)
            .WithPassword(settings.Password)
            .WithCleanUp(true)
            .Build();
    }

    public async Task InitializeAsync()
    {
        await _container.StartAsync();

        var options = new DbContextOptionsBuilder<DoctorDbContext>()
            .UseNpgsql(_container.GetConnectionString()).Options;

        DbContext = new DoctorDbContext(options); 
        await DbContext.Database.MigrateAsync();
    }

    public async Task DisposeAsync()
    {
        await DbContext.DisposeAsync();
        await _container.StopAsync();
        await _container.DisposeAsync();
    }
}
