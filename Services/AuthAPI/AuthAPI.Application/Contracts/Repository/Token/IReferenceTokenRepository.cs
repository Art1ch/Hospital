using AuthAPI.Core.Entities;

namespace AuthAPI.Application.Contracts.Repository.Account;

public interface IReferenceTokenRepository : IRepository<ReferenceTokenEntity, int>
{
    Task DeleteByAccountId(Guid acconutId, CancellationToken cancellationToken = default);
    Task<ReferenceTokenEntity> GetTokenByValueAsync(string tokenValue, CancellationToken cancellationTOken = default);
}
