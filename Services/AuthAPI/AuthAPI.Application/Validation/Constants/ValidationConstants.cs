namespace AuthAPI.Application.Validation.Constants;

internal static class ValidationConstants
{
    public const string OnFailedRequiredValidation = "Поле обязательно для заполнения.";
    public const string OnFailedNullValidation = "Значение не может быть null.";
    public const string OnFailedRegexValidation = "Данное поле может содержать только буквы, дефисы и пробелы.";
    public const string OnFailedEmailValidation = "Некорректный формат электронной почты";

    public static readonly string OnFailedPhoneNumberValidation =
        $"Номер телефона должен иметь длину от {AccountConstants.MinPhoneNumberLength}" +
        $" до {AccountConstants.MaxPhoneNumberLength} цифр";
    public static readonly string OnFailedPasswordValidation =
        $"Длина пароля должна составлять от {AccountConstants.MinPasswordLength}" +
        $" до {AccountConstants.MaxPasswordLength} символов." +
        $" Также пароль должен содержать символы в верхнем и нижнем регистрах," +
        $" не должен содержать пробела, должен содержать хотя бы одну цифру";
}
