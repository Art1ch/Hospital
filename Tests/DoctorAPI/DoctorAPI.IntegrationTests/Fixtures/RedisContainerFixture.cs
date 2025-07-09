using DoctorAPI.Application.Contracts.Cache;
using DoctorAPI.Infrastructure.Services;
using DoctorAPI.IntegrationTests.Settings;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Testcontainers.Redis;


namespace DoctorAPI.IntegrationTests.Fixtures;

public sealed class RedisContainerFixture : IAsyncLifetime
{
    public RedisContainer Container;
    public ICacheService CacheService;
    public IDistributedCache CacheMemory;
    private RedisSettings Settings;
    private IDatabase Database;
    private IConnectionMultiplexer Connection; 

    public async Task InitializeAsync()
    {
        ConfigureRedisSettings();
        CreateContainer();
        await Container.StartAsync();
        await CreateConnection();
        CreateDatabase();
        CreateCacheMemory();
        CreateCacheService();

    }


    public async Task DisposeAsync()
    {
        await ClearCacheStorage();
        await Connection.CloseAsync();
        await Connection.DisposeAsync();
        await Container.StopAsync();
        await Container.DisposeAsync();
    }

    private void ConfigureRedisSettings()
    {
        var config = new ConfigurationBuilder()
          .AddUserSecrets<RedisSettings>()
          .Build();
        var sectionName = typeof(RedisSettings).Name;

        Settings = config
            .GetSection(sectionName)
            .Get<RedisSettings>()!;
    }

    private void CreateContainer()
    {
        Container = new RedisBuilder()
            .WithImage(Settings.Image)
            .Build();
    }

    private async Task CreateConnection()
    {
        Connection = await ConnectionMultiplexer.ConnectAsync(Settings.ConnectionString);
    }

    private void CreateDatabase()
    {
        Database = Connection.GetDatabase();
    }

    private void CreateCacheMemory()
    {
        CacheMemory = new RedisCache(Options.Create(new RedisCacheOptions
        {
            Configuration = Settings.ConnectionString,
            InstanceName = Settings.InstanceName,
        }));
    }

    private void CreateCacheService()
    {
        CacheService = new CacheService(CacheMemory);
    }

    private async Task ClearCacheStorage()
    {
        if (Database != null)
        {
            var server = Connection.GetServer(Connection.GetEndPoints().First());
            await server.FlushDatabaseAsync(Database.Database);
        }
    }
}
