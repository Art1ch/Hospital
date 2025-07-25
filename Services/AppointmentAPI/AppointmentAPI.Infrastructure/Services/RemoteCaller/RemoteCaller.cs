using AppointmentAPI.Application.Contracts.RemoteCaller;
using AppointmentAPI.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Shared.Protos.Auth;
using Shared.Protos.Doctor;

namespace AppointmentAPI.Infrastructure.Services.RemoteCaller;

internal class RemoteCaller(
    DoctorService.DoctorServiceClient doctorServiceClient,
    AuthService.AuthServiceClient authServiceClient
) : IRemoteCaller
{
    public async Task<string> GetDoctorsEmailAsync(Guid doctorId)
    {
        var getDoctorsAccountIdRequest = new GetDoctorsAccountIdRequest()
        {
            DoctorId = doctorId.ToString()
        };
        var getDoctorsAccountIdResponse = await doctorServiceClient.GetDoctorsAccountIdAsync(getDoctorsAccountIdRequest);
        var getAccountsEmailRequest = new GetAccountsEmailRequest()
        {
            AccountId = getDoctorsAccountIdResponse.AccountId.ToString()
        };
        var getAccountsEmailResponse = await authServiceClient.GetAccountsEmailAsync(getAccountsEmailRequest);
        return getAccountsEmailResponse.Email;
    }
}
