namespace AuthAPI.Application.Exceptions;

public class WrongCredentialsGivenException : Exception
{
    public WrongCredentialsGivenException(string? message) : base(message)
    {

    }
}
