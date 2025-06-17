using AuthAPI.Application.Contracts.PasswordHasher;
using AuthAPI.Application.Contracts.TokenProvider;
using AuthAPI.Application.Contracts.UnitOfWork;
using AuthAPI.Application.Exceptions;
using AuthAPI.Application.Responses.Token;
using MediatR;

namespace AuthAPI.Application.Commands.Token.ExchangeToken;

internal class ExchangeTokenCommandHandler : IRequestHandler<ExchangeTokenCommand, ExchangeTokenResponse>
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
        var tokenValue = command.Request.ReferenceToken;
        var referenceToken = await _unitOfWork.ReferenceTokenRepository.GetTokenByValueAsync(tokenValue);
        var isExpired = _tokenProvider.IsTokenExpired(referenceToken.ExpiresAt);
        if (isExpired)
        {
            throw new TokenIsExpiredException("Token is expired");
        }
        var account = await _unitOfWork.AccountRepository.GetAsync(referenceToken.AccountId);

        var idToken = _tokenProvider.GenerateIdToken(account);
        var accessToken = _tokenProvider.GenerateAccessToken(account);
        var refreshToken = _tokenProvider.GenerateRefreshToken(account);

        await _unitOfWork.RefreshTokenRepository.CreateAsync(refreshToken);
        await _unitOfWork.ReferenceTokenRepository.DeleteAsync(referenceToken.Id);

        return new ExchangeTokenResponse(idToken, accessToken, refreshToken.Token);
    }
}
