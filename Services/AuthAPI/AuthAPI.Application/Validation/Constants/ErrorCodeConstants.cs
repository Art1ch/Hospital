namespace AuthAPI.Application.Validation.Constants;

internal static class ErrorCodeConstants
{
    public const string EmailRequired = "Email.Required";
    public const string EmailInvalid = "Email.Invalid";

    public const string PhoneTooShort = "Phone.TooShort";
    public const string PhoneTooLong = "Phone.TooLong";

    public const string PasswordRequired = "Password.Required";
    public const string PasswordTooShort = "Password.TooShort";
    public const string PasswordTooLong = "Password.TooLong";
    public const string PasswordUppercaseRequired = "Password.UppercaseRequired";
    public const string PasswordLowercaseRequired = "Password.LowercaseRequired";
    public const string PasswordNumberRequired = "Password.NumberRequired";
    public const string PasswordNoWhitespace = "Password.NoWhitespace";

    public const string TokenRequired = "Token.Required";
}
