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
    private const string EnvFilePath = "../../../../../../.env";
    private IDatabase _database;
    private IConnectionMultiplexer _connection; 

    public async Task InitializeAsync()
    {
        ConfigureRedisSettings();
        CreateContainer();
        await Container.StartAsync();   

        await CreateConnection();      

        CreateDatabase();
        CreateCacheMemory();
    }

    public async Task DisposeAsync()
    {
        await ClearCacheStorage();

        if (_connection != null)
        {
            await _connection.CloseAsync();
            await _connection.DisposeAsync();
        }
        
        await Container.StopAsync();
        await Container.DisposeAsync();
    }

    private void ConfigureRedisSettings()
    {
        DotNetEnv.Env.Load(EnvFilePath);

        var config = new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .Build();

        var configRoot = config.GetSection(nameof(RedisSettings));
        _settings = configRoot.Get<RedisSettings>()!;
    }

    private void CreateCacheMemory()
    {
        var cacheOptions = new RedisCacheOptions()
        {
            Configuration = Container.GetConnectionString(),
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
        _connection = await ConnectionMultiplexer.ConnectAsync(Container.GetConnectionString());
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
            var server = _connection.GetServer(endpoints.First());
            foreach (var key in server.Keys())
            {
                await _database.KeyDeleteAsync(key);
            }
        }
    }
}
