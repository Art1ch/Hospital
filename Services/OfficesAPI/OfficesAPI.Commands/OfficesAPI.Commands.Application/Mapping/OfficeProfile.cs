using AutoMapper;
using OfficesAPI.Commands.Application.Requests.Office;
using OfficesAPI.Commands.Core.Entities;
using OfficesAPI.Shared.Entities;
using OfficesAPI.Shared.Events;

namespace OfficesAPI.Application.Mapping;

internal sealed class OfficeProfile : Profile
{
    public OfficeProfile()
    {
        CreateMap<CreateOfficeRequest, OfficeEntity>()
            .ForMember(destination => destination.Id, options => options.MapFrom(_ => Guid.NewGuid()));
        CreateMap<UpdateOfficeRequest, OfficeEntity>();
        CreateMap<ChangeOfficeStatusRequest, ChangeOfficeStatusEntity>();

        CreateMap<OfficeEntity, CreateOfficeEntity>();
        CreateMap<OfficeEntity, UpdateOfficeEntity>();

        CreateMap<OfficeEntity, OfficeCreatedEvent>()
            .ForMember(destination => destination.Entity, options => options.MapFrom(x => x));
        CreateMap<OfficeEntity, OfficeUpdatedEvent>()
            .ForMember(destination => destination.Entity, options => options.MapFrom(x => x));
        CreateMap<ChangeOfficeStatusRequest, OfficeStatusChangedEvent>();
    }
}
