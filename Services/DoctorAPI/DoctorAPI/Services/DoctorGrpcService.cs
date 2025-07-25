using DoctorAPI.Application.Queries.Doctor.GetEntitiesQuery;
using Grpc.Core;
using MediatR;
using Shared.Protos.Doctor;

namespace Doctor.API.Services;

public class DoctorGrpcService(
    ISender sender
) : DoctorService.DoctorServiceBase
{
    public override async Task<GetDoctorsAccountsIdsResponse> GetDoctorsAccountsIds(
        GetDoctorsAccountsIdsRequest request,
        ServerCallContext context
    )
    {
        var doctorIds = request.DoctorsIds.Select(Guid.Parse);
        var doctorsQuery = await sender.Send(new GetDoctorEntitiesQuery());
        var doctorsAccountsIds = doctorsQuery
            .Where(d => doctorIds.Contains(d.Id))
            .Select(x => x.AccountId)
            .Select(x => x.ToString());
        var response = new GetDoctorsAccountsIdsResponse();
        response.AccountsIds.AddRange(doctorsAccountsIds);
        return response;
    }
}

