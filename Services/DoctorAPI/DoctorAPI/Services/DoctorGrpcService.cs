using DoctorAPI.Application.Queries.Doctor.GetById;
using Grpc.Core;
using MediatR;
using Shared.Protos.Doctor;

namespace Doctor.API.Services;

public class DoctorGrpcService(
    ISender sender
) : DoctorService.DoctorServiceBase
{
    public override async Task<GetDoctorsAccountIdResponse> GetDoctorsAccountId(
        GetDoctorsAccountIdRequest request,
        ServerCallContext context
    )
    {
        var id = Guid.Parse(request.DoctorId);
        var query = new GetDoctorByIdQuery(id);
        var result = await sender.Send(query);
        var accountId = result.Doctor.AccountId;
        var response = new GetDoctorsAccountIdResponse()
        {
            AccountId = accountId.ToString()
        };
        return response;
    }
}

