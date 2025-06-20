using AuthAPI.Core.Entities;

namespace AuthAPI.Application.Contracts.Repository.Token;

public interface IRefreshTokenRepository : IRepository<RefreshTokenEntity, int>
{

}
