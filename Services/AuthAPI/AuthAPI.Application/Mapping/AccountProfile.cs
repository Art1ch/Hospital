using AuthAPI.Application.Requests.Account;
using AuthAPI.Core.Entities;
using AuthAPI.Core.Enums;
using AutoMapper;

namespace AuthAPI.Application.Mapping;

internal class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<RegistrationRequest, AccountEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(_ => Role.Patient))
            .ForMember(dest => dest.IsEmailVerified, opt => opt.MapFrom(_ => false));
        CreateMap<LoginRequest, AccountEntity>();
    }
}
