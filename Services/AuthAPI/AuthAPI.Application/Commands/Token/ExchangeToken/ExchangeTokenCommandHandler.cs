using AuthAPI.Application.Contracts.Repository.Account;
using AuthAPI.Application.Contracts.Repository.Token;
using AuthAPI.Application.Contracts.TokenProvider;
using MediatR;

namespace AuthAPI.Application.Commands.Token.ExchangeToken;

internal sealed class ExchangeTokenCommandHandler : IRequestHandler<ExchangeTokenCommand, ExchangeTokenResult>
{
    private readonly ITokenProvider _tokenProvider;
    private readonly IReferenceTokenRepository _referenceTokenRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public ExchangeTokenCommandHandler(
        ITokenProvider tokenProvider,
        IReferenceTokenRepository referenceTokenRepository,
        IAccountRepository accountRepository,
        IRefreshTokenRepository refreshTokenRepository)
    {
        _tokenProvider = tokenProvider;
        _referenceTokenRepository = referenceTokenRepository;
        _accountRepository = accountRepository;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<ExchangeTokenResult> Handle(ExchangeTokenCommand command, CancellationToken cancellationToken)
    {
        var tokenValue = command.Request.ReferenceToken;
        var referenceToken = await _referenceTokenRepository.GetTokenByValueAsync(tokenValue);

        var isExpired = _tokenProvider.IsTokenExpired(referenceToken.ExpiresAt);
        if (isExpired)
        {
            return new ExchangeTokenResult(false, null, null, null, "Token is expired");
        }
        var account = await _accountRepository.GetAsync(referenceToken.AccountId);

        var idToken = _tokenProvider.GenerateIdToken(account);
        var accessToken = _tokenProvider.GenerateAccessToken(account);
        var refreshToken = _tokenProvider.GenerateRefreshToken(account);

        await _refreshTokenRepository.CreateAsync(refreshToken, cancellationToken);
        await _referenceTokenRepository.DeleteAsync(referenceToken.Id, cancellationToken);

        return new ExchangeTokenResult(true, idToken, accessToken, refreshToken, null);
    }
}
