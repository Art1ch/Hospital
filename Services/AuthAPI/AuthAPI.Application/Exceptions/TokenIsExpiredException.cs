namespace AuthAPI.Application.Exceptions;

public class TokenIsExpiredException : Exception
{
    public TokenIsExpiredException(string? message) : base(message)
    {
        
    }
}
