using DoctorAPI.Application.Contracts.UnitOfWork;
using DotNet.Testcontainers.Containers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DoctorAPI.IntegrationTests.Abstractions;

internal abstract class BaseFixture<TContainer, TContainerSettings, TContext> : IAsyncLifetime
    where TContainer : DockerContainer
    where TContainerSettings : class
    where TContext : DbContext

{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly TContainerSettings _settings;
    protected readonly TContainer _container;
    internal TContext _dbContext;

    public BaseFixture()
    {
        var config = new ConfigurationBuilder()
           .AddUserSecrets<TContainerSettings>()
           .Build();

        var sectionName = typeof(TContainerSettings).Name;

        _settings = config.GetSection(sectionName).Get<TContainerSettings>()!;

        _container = CreateContainer();
        _dbContext = CreateDbContext();
        _unitOfWork = CreateUnitOfWork();
    }

    protected abstract IUnitOfWork CreateUnitOfWork();
    protected abstract TContainer CreateContainer();
    protected abstract TContext CreateDbContext();

    public async Task InitializeAsync()
    {
        await _container.StartAsync();
        await _dbContext.Database.MigrateAsync();
        await _unitOfWork.BeginTransactionAsync();
    }

    public async Task DisposeAsync()
    {
        await _unitOfWork.RollbackAsync();
        await _dbContext.DisposeAsync();
        await _container.StopAsync();
        await _container.DisposeAsync();
    }
}
