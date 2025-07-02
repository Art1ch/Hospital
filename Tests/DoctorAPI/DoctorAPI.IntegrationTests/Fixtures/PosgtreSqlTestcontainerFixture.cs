using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;
using DoctorAPI.Infrastructure.Context;
using DoctorAPI.IntegrationTests.Settings;
using DoctorAPI.IntegrationTests.Abstractions;
using DoctorAPI.Application.Contracts.UnitOfWork;
using DoctorAPI.Infrastructure.UnitOfWorkImplementation;
using DotNet.Testcontainers.Builders;

namespace DoctorAPI.IntegrationTests.Fixtures;

internal sealed class PostgreSqlTestContainerFixture : BaseFixture<PostgreSqlContainer, PostgreDbSettings, DoctorDbContext>
{
    public PostgreSqlTestContainerFixture() : base()
    {
    }

    protected override PostgreSqlContainer CreateContainer()
    {
        var container = new PostgreSqlBuilder()
            .WithImage(base._settings.Image)
            .WithDatabase(base._settings.Database)
            .WithUsername(base._settings.Username)
            .WithPassword(base._settings.Password)
            .WithCleanUp(true)
            .Build();
        return container;
    }

    protected override DoctorDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<DoctorDbContext>()
            .UseNpgsql(base._settings.ConnectionString).Options;

        var dbContext = new DoctorDbContext(options);
        return dbContext;
    }

    protected override IUnitOfWork CreateUnitOfWork()
    {
        var unitOfWork = new UnitOfWork(base._dbContext);
        return unitOfWork;
    }
}
