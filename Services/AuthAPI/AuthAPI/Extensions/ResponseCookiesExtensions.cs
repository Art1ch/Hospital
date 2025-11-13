namespace AuthAPI.Extensions;

internal static class ResponseCookiesExtensions
{
    public static IResponseCookies AppendSecuredCookies(
        this IResponseCookies cookies,
        IEnumerable<(string Key, string Value, DateTimeOffset Expiry)> cookiesData
    )
    {
        foreach (var (key, value, expiry) in cookiesData)
        {
            var options = new CookieOptions()
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = expiry,
            };
            cookies.Append(key, value, options);
        }
        return cookies;
    }
}

