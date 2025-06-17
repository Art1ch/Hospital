using AuthAPI.Application.Contracts.PasswordHasher;
using AuthAPI.Application.Contracts.TokenProvider;
using AuthAPI.Application.Contracts.UnitOfWork;
using AuthAPI.Application.Exceptions;
using AuthAPI.Application.Responses.Account;
using AuthAPI.Core.Entities;
using AutoMapper;
using MediatR;

namespace AuthAPI.Application.Commands.Account.Login;

internal class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ITokenProvider _tokenProvider;
    private readonly IPasswordHasher _passwordHasher;

    public LoginCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ITokenProvider tokenProvider,
        IPasswordHasher passwordHasher)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _tokenProvider = tokenProvider;
        _passwordHasher = passwordHasher;
    }

    public async Task<LoginResponse> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var accountEntity = _mapper.Map<AccountEntity>(command.Request);
        var isExists = await _unitOfWork.AccountRepository.IsExistsAsync(accountEntity.Email);

        if (!isExists)
        {
            throw new WrongCredentialsGivenException("Wrong credentials");
        }

        var existingAccount = await _unitOfWork.AccountRepository.GetByEmailAsync(accountEntity.Email);
        var isVerified = _passwordHasher.VerifyPassword(command.Request.Password, existingAccount.HashPassword);

        if (!isVerified)
        {
            throw new WrongCredentialsGivenException("Wrong credentials");
        }

        var referenceToken = _tokenProvider.GenerateReferenceToken(accountEntity);
        await _unitOfWork.ReferenceTokenRepository.CreateAsync(referenceToken);
        return new LoginResponse(referenceToken.Token);
    }
}
