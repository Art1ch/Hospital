using AuthAPI.Application.Contracts.PasswordHasher;
using AuthAPI.Application.Contracts.TokenProvider;
using AuthAPI.Application.Contracts.UnitOfWork;
using AuthAPI.Application.Exceptions;
using AuthAPI.Application.Responses.Account;
using AuthAPI.Core.Entities;
using AutoMapper;
using MediatR;

namespace AuthAPI.Application.Commands.Account.Register;

internal class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, RegistrationResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenProvider _tokenProvider;
    private readonly IMapper _mapper;

    public RegistrationCommandHandler(
        IUnitOfWork unitOfWork,
        IPasswordHasher passwordHasher,
        ITokenProvider tokenProvider,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
        _tokenProvider = tokenProvider;
        _mapper = mapper;
    }

    public async Task<RegistrationResponse> Handle(RegistrationCommand command, CancellationToken cancellationToken)
    {
        var accountEntity = _mapper.Map<AccountEntity>(command.Request);
        var isExists = await _unitOfWork.AccountRepository.IsExistsAsync(accountEntity.Email);
        if (isExists)
        {
            throw new AccountAlreadyExistsException("Account already exists");
        }
        var hashPassword = _passwordHasher.GeneratePassword(command.Request.Password!);
        accountEntity.HashPassword = hashPassword;
        await _unitOfWork.AccountRepository.CreateAsync(accountEntity);
        var referenceToken = _tokenProvider.GenerateReferenceToken(accountEntity);
        await _unitOfWork.ReferenceTokenRepository.CreateAsync(referenceToken);
        return new RegistrationResponse(referenceToken.Token);
    }
}