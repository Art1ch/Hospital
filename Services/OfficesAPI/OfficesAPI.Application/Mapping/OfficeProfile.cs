using AutoMapper;
using OfficesAPI.Application.RepositoryResults.Office;
using OfficesAPI.Application.Requests.Office;
using OfficesAPI.Application.Responses.Office;
using OfficesAPI.Core.Entities;

namespace OfficesAPI.Application.Mapping;

internal class OfficeProfile : Profile
{
    public OfficeProfile()
    {
        CreateMap<CreateOfficeRequest, OfficeEntity>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()));
        CreateMap<UpdateOfficeRequest, OfficeEntity>();

        CreateMap<GetAllOfficesResult, GetAllOfficesResponse>()
            .ConstructUsing(src => new GetAllOfficesResponse(src));
        CreateMap<GetOfficeInfoResult, GetOfficeInfoResponse>()
            .ConstructUsing(src => new GetOfficeInfoResponse(src));
    }
}
