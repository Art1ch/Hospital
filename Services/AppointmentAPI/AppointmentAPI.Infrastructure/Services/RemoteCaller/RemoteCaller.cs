using AppointmentAPI.Application.Contracts.RemoteCaller;
using Shared.Protos.Auth;
using Shared.Protos.Doctor;

namespace AppointmentAPI.Infrastructure.Services.RemoteCaller;

internal sealed class RemoteCaller(
    DoctorService.DoctorServiceClient doctorServiceClient,
    AuthService.AuthServiceClient authServiceClient
) : IRemoteCaller
{
    public async Task<IEnumerable<string>> GetDoctorsEmailsAsync(IEnumerable<Guid> doctorsIds)
    {
        var stringDoctorsIds = doctorsIds.Select(x => x.ToString());

        var getDoctorsAccountsIdsRequest = new GetDoctorsAccountsIdsRequest();
        getDoctorsAccountsIdsRequest.DoctorsIds.AddRange(stringDoctorsIds);

        var getDoctorsAccountsIdsResponse = await doctorServiceClient.GetDoctorsAccountsIdsAsync(getDoctorsAccountsIdsRequest);

        var getAccountsEmailRequest = new GetAccountsEmailsRequest();
        getAccountsEmailRequest.AccountsIds.AddRange(getDoctorsAccountsIdsResponse.AccountsIds);

        var getAccountsEmailResponse = await authServiceClient.GetAccountsEmailsAsync(getAccountsEmailRequest);

        var emails = getAccountsEmailResponse.Emails.ToList();

        return emails;
    }
}
