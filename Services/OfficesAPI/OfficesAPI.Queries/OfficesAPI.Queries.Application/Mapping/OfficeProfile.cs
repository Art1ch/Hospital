using AutoMapper;
using OfficesAPI.Queries.Application.RepositoryResults.Office;
using OfficesAPI.Queries.Application.Responses.Office;

namespace OfficesAPI.Queries.Application.Mapping;

internal class OfficeProfile : Profile
{
    public OfficeProfile()
    {
        CreateMap<GetAllOfficesResult, GetAllOfficesResponse>()
            .ConstructUsing(src => new GetAllOfficesResponse(src));
        CreateMap<GetOfficeInfoResult, GetOfficeInfoResponse>()
            .ConstructUsing(src => new GetOfficeInfoResponse(src));
    }
}
