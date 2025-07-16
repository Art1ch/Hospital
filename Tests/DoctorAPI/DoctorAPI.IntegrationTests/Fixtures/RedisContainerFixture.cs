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
    public IDistributedCache CacheMemory;
    internal RedisSettings _settings;
    private IDatabase _database;
    private IConnectionMultiplexer _connection; 

    public async Task InitializeAsync()
    {
        ConfigureRedisSettings();
        CreateCacheMemory();
        CreateContainer();
        await Container.StartAsync();
        await CreateConnection();
        CreateDatabase();
    }


    public async Task DisposeAsync()
    {
        await ClearCacheStorage();
        await _connection.CloseAsync();
        await _connection.DisposeAsync();
        await Container.StopAsync();
        await Container.DisposeAsync();
    }

    private void ConfigureRedisSettings()
    {
        var config = new ConfigurationBuilder()
          .AddUserSecrets<RedisSettings>()
          .Build();
        var sectionName = typeof(RedisSettings).Name;

        _settings = config
            .GetSection(sectionName)
            .Get<RedisSettings>()!;
    }

    private void CreateCacheMemory()
    {
        var cacheOptions = new RedisCacheOptions()
        {
            Configuration = _settings.ConnectionString,
            InstanceName = _settings.InstanceName,
        };
        var optionAccessor = Options.Create(cacheOptions);
        CacheMemory = new RedisCache(optionAccessor);
    }

    private void CreateContainer()
    {
        Container = new RedisBuilder()
            .WithImage(_settings.Image)
            .Build();
    }

    private async Task CreateConnection()
    {
        _connection = await ConnectionMultiplexer.ConnectAsync(_settings.ConnectionString);
    }

    private void CreateDatabase()
    {
        _database = _connection.GetDatabase();
    }

    private async Task ClearCacheStorage()
    {
        if (_database != null)
        {
            var endpoints = _connection.GetEndPoints();
            var firstEndpoint = endpoints.First();
            var server = _connection.GetServer(firstEndpoint);
            await server.FlushDatabaseAsync(_database.Database);
        }
    }
}
