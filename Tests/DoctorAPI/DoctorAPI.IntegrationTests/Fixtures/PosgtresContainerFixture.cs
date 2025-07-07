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
    internal DoctorDbContext DbContext;
    public PostgreSqlContainer Container;
    public IUnitOfWork UnitOfWork;
    private PostgresDbSettings Settings;

    public async Task InitializeAsync()
    {
        ConfigureDbSettings();
        CreateContainer();
        CreateDbContext();
        CreateUnitOfWork();

        await Container.StartAsync();
        await DbContext.Database.MigrateAsync();
        await UnitOfWork.BeginTransactionAsync();
    }

    public async Task DisposeAsync()
    {
        await UnitOfWork.RollbackAsync();
        await DbContext.DisposeAsync();
        await Container.StopAsync();
        await Container.DisposeAsync();
    }

    private void ConfigureDbSettings()
    {
        var config = new ConfigurationBuilder()
           .AddUserSecrets<PostgresDbSettings>()
           .Build();
        var sectionName = typeof(PostgresDbSettings).Name;

        Settings = config.GetSection(sectionName).Get<PostgresDbSettings>()!;
    }

    private void CreateContainer()
    {
        Container = new PostgreSqlBuilder()
            .WithImage(Settings.Image)
            .WithDatabase(Settings.Database)
            .WithUsername(Settings.Username)
            .WithPassword(Settings.Password)
            .WithCleanUp(true)
            .Build();
    }

    private void CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<DoctorDbContext>()
            .UseNpgsql(Settings.ConnectionString).Options;
        DbContext = new DoctorDbContext(options);
    }

    private void CreateUnitOfWork()
    {
        UnitOfWork = new UnitOfWork(DbContext);
    }
}
