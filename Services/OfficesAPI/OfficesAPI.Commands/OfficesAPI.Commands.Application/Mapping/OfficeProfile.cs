using AutoMapper;
using OfficesAPI.Commands.Application.Requests;
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
            .ForMember(x => x.Id, x => x.MapFrom(_ => Guid.NewGuid()))
            .ForMember(x => x.ImageUrl, x => x.Ignore());

        CreateMap<UpdateOfficeRequest, OfficeEntity>()
            .ForMember(x => x.ImageUrl, x => x.Ignore());

        CreateMap<ChangeOfficeStatusRequest, ChangeOfficeStatusEntity>();

        CreateMap<ChangeOfficeStatusRequest, OfficeStatusChangedEvent>();

        CreateMap<DeleteOfficeRequest, DeleteOfficeEntity>();

        CreateMap<DeleteOfficeRequest, OfficeDeletedEvent>();

        CreateMap<OfficeEntity, CreateOfficeEntity>();

        CreateMap<OfficeEntity, UpdateOfficeEntity>();

        CreateMap<OfficeEntity, OfficeCreatedEvent>()
            .ConstructUsing(x => new OfficeCreatedEvent(x));

        CreateMap<OfficeEntity, OfficeUpdatedEvent>()
            .ConstructUsing(x => new OfficeUpdatedEvent(x));
    }
}
