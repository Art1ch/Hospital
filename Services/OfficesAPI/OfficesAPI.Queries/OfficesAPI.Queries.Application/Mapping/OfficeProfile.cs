using AutoMapper;
using OfficesAPI.Shared.Entities;
using OfficesAPI.Shared.Events;
using OfficesAPI.Shared.RepositoryResults;
using OfficesAPI.Shared.Responses;

namespace OfficesAPI.Queries.Application.Mapping;

internal class OfficeProfile : Profile
{
    public OfficeProfile()
    {
        CreateMap<GetAllOfficesResult, GetAllOfficesResponse>()
            .ConstructUsing(src => new GetAllOfficesResponse(src));
        CreateMap<GetOfficeInfoResult, GetOfficeInfoResponse>()
            .ConstructUsing(src => new GetOfficeInfoResponse(src));

        CreateMap<OfficeCreatedEvent, OfficeEntity>();
        CreateMap<OfficeUpdatedEvent, OfficeEntity>();
    }
}
