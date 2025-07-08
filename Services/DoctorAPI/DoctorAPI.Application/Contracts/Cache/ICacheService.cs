namespace DoctorAPI.Application.Contracts.Cache;

public interface ICacheService
{
    Task<TValue?> GetAsync<TValue>(string key, CancellationToken cancellationToken = default);
    Task SetAsync<TValue>(string key, TValue value, CancellationToken cancellationToken = default);
    Task RefreshAsync(string key, CancellationToken cancellationToken = default);
    Task RemoveAsync(string key, CancellationToken cancellationToken = default);
}
