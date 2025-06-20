using AuthAPI.Application.Contracts.Repository.Account;
using AuthAPI.Application.Contracts.Repository.Token;
using AuthAPI.Application.Contracts.TokenProvider;
using AuthAPI.Application.Contracts.UnitOfWork;
using AuthAPI.Application.Exceptions;
using AuthAPI.Application.Responses.Token;
using MediatR;

namespace AuthAPI.Application.Commands.Token.ExchangeToken;

internal sealed class ExchangeTokenCommandHandler : IRequestHandler<ExchangeTokenCommand, ExchangeTokenResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenProvider _tokenProvider;

    public ExchangeTokenCommandHandler(
        IUnitOfWork unitOfWork,
        ITokenProvider tokenProvider)
    {
        _unitOfWork = unitOfWork;
        _tokenProvider = tokenProvider;
    }

    public async Task<ExchangeTokenResponse> Handle(ExchangeTokenCommand command, CancellationToken cancellationToken)
    {
        var referenceTokenRepository = _unitOfWork.GetRepository<IReferenceTokenRepository>();
        var accountTokenRepository = _unitOfWork.GetRepository<IAccountRepository>();
        var refreshTokenRepository = _unitOfWork.GetRepository<IRefreshTokenRepository>();

        var tokenValue = command.Request.ReferenceToken;
        var referenceToken = await referenceTokenRepository.GetTokenByValueAsync(tokenValue);

        var isExpired = _tokenProvider.IsTokenExpired(referenceToken.ExpiresAt);
        if (isExpired)
        {
            throw new TokenIsExpiredException("Token is expired");
        }
        var account = await accountTokenRepository.GetAsync(referenceToken.AccountId);

        var idToken = _tokenProvider.GenerateIdToken(account);
        var accessToken = _tokenProvider.GenerateAccessToken(account);
        var refreshToken = _tokenProvider.GenerateRefreshToken(account);

        await refreshTokenRepository.CreateAsync(refreshToken, cancellationToken);
        await referenceTokenRepository.DeleteAsync(referenceToken.Id, cancellationToken);

        return new ExchangeTokenResponse(idToken, accessToken, refreshToken.Token);
    }
}
