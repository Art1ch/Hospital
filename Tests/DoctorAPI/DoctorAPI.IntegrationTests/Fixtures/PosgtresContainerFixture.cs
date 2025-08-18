using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;
using DoctorAPI.Application.Contracts.UnitOfWork;
using DoctorAPI.Infrastructure.UnitOfWorkImplementation;
using DoctorAPI.IntegrationTests.Settings;
using Microsoft.Extensions.Configuration;
using DoctorAPI.Infrastructure.Context;

namespace DoctorAPI.IntegrationTests.Fixtures;

public sealed class PostgresContainerFixture : IAsyncLifetime
{
    public IUnitOfWork UnitOfWork;
    public PostgreSqlContainer Container;
    internal DoctorDbContext _dbContext;
    private const string EnvFilePath = "../../../../../../.env";
    private PostgresDbSettings _settings;

    public async Task InitializeAsync()
    {
        ConfigureDbSettings();
        CreateContainer();
        CreateDbContext();
        CreateUnitOfWork();

        await Container.StartAsync();
        await _dbContext.Database.MigrateAsync();
        await UnitOfWork.BeginTransactionAsync();
    }

    public async Task DisposeAsync()
    {
        await UnitOfWork.RollbackAsync();
        await _dbContext.DisposeAsync();
        await Container.StopAsync();
        await Container.DisposeAsync();
    }

    private void ConfigureDbSettings()
    {
        DotNetEnv.Env.Load(EnvFilePath);

        var config = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .Build();

        var configRoot = config.GetSection(nameof(PostgresDbSettings));
        _settings = configRoot.Get<PostgresDbSettings>()!;
    }

    private void CreateContainer()
    {
        Container = new PostgreSqlBuilder()
            .WithImage(_settings.Image)
            .WithDatabase(_settings.Database)
            .WithUsername(_settings.Username)
            .WithPassword(_settings.Password)
            .WithCleanUp(true)
            .Build();
    }

    private void CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<DoctorDbContext>()
            .UseNpgsql(_settings.ConnectionString).Options;
        _dbContext = new DoctorDbContext(options);
    }

    private void CreateUnitOfWork()
    {
        UnitOfWork = new UnitOfWork(_dbContext);
    }
}
