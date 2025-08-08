using AutoMapper;
using OfficesAPI.Commands.Application.Requests.Office;
using OfficesAPI.Commands.Core.Entities;
using OfficesAPI.Shared.Events;

namespace OfficesAPI.Application.Mapping;

internal sealed class OfficeProfile : Profile
{
    public OfficeProfile()
    {
        CreateMap<CreateOfficeRequest, CreateOfficeEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));
        CreateMap<UpdateOfficeRequest, UpdateOfficeEntity>();
        CreateMap<ChangeOfficeStatusRequest, ChangeOfficeStatusEntity>();

        CreateMap<CreateOfficeRequest, OfficeCreatedEvent>();
        CreateMap<UpdateOfficeRequest, OfficeUpdatedEvent>();
        CreateMap<ChangeOfficeStatusRequest, OfficeStatusChangedEvent>();
    }
}
