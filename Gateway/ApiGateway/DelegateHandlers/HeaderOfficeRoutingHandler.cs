using ApiGateway.Settings;
using Microsoft.Extensions.Options;

namespace ApiGateway.DelegateHandlers;

public class HeaderOfficeRoutingHandler : DelegatingHandler
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly OfficeServiceAddressesSettings _officeAddressesSettings;

    public HeaderOfficeRoutingHandler(IHttpClientFactory httpClientFactory, IOptions<OfficeServiceAddressesSettings> options)
    {
        _httpClientFactory = httpClientFactory;
        _officeAddressesSettings = options.Value;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (!IsOfficeRequest(request))
            return await base.SendAsync(request, cancellationToken);

        var targetHost = GetTargetHost(request);
        var downstreamUrl = $"{targetHost}{request.RequestUri!.PathAndQuery}";

        using var newRequest = CloneRequest(request, new Uri(downstreamUrl));

        var client = _httpClientFactory.CreateClient("OfficeGateway");
        return await client.SendAsync(newRequest, cancellationToken);
    }

    private static bool IsOfficeRequest(HttpRequestMessage request)
        => request.Method == HttpMethod.Get &&
           request.RequestUri?.AbsolutePath.StartsWith("/office", StringComparison.OrdinalIgnoreCase) == true;

    private string GetTargetHost(HttpRequestMessage request)
    {
        if (request.Headers.TryGetValues("X-Need-Fresh-Data", out var values) &&
            values.FirstOrDefault()?.Equals("true", StringComparison.OrdinalIgnoreCase) == true)
        {
            return _officeAddressesSettings.OfficeWriteSideAddress;
        }

        return _officeAddressesSettings.OfficeReadSideAddress;
    }

    private static HttpRequestMessage CloneRequest(HttpRequestMessage original, Uri newUri)
    {
        var clone = new HttpRequestMessage(original.Method, newUri)
        {
            Content = original.Content
        };

        foreach (var header in original.Headers)
            clone.Headers.TryAddWithoutValidation(header.Key, header.Value);

        return clone;
    }
}