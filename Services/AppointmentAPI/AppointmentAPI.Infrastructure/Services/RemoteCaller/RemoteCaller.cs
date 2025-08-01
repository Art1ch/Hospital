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
        if (!doctorsIds.Any() || doctorsIds == null)
        {
            return new List<string>();
        }

        var stringDoctorsIds = doctorsIds.Select(x => x.ToString());

        var getDoctorsAccountsIdsRequest = GetDoctorsAccountsIdsRequest(stringDoctorsIds);
        var getDoctorsAccountsIdsResponse = await doctorServiceClient.GetDoctorsAccountsIdsAsync(getDoctorsAccountsIdsRequest);

        var getAccountsEmailRequest = GetAccountsEmailsRequest(getDoctorsAccountsIdsResponse.AccountsIds);
        var getAccountsEmailResponse = await authServiceClient.GetAccountsEmailsAsync(getAccountsEmailRequest);

        var emails = getAccountsEmailResponse.Emails.ToList();
        return emails;
    }

    private GetDoctorsAccountsIdsRequest GetDoctorsAccountsIdsRequest(IEnumerable<string> stringDoctorsIds)
    {
        var getDoctorsAccountsIdsRequest = new GetDoctorsAccountsIdsRequest();
        getDoctorsAccountsIdsRequest.DoctorsIds.AddRange(stringDoctorsIds);

        return getDoctorsAccountsIdsRequest;
    }

    private GetAccountsEmailsRequest GetAccountsEmailsRequest(IEnumerable<string> accountsIds)
    {
        var getAccountsEmailRequest = new GetAccountsEmailsRequest();
        getAccountsEmailRequest.AccountsIds.AddRange(accountsIds);

        return getAccountsEmailRequest;
    }
}
