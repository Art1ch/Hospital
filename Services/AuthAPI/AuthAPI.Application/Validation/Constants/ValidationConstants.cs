namespace AuthAPI.Application.Validation.Constants;

internal static class ValidationConstants
{
    public static readonly string OnFailedRequiredValidation = "Поле обязательно для заполнения.";
    public static readonly string OnFailedNullValidation = "Значение не может быть null.";
    public static readonly string OnFailedRegexValidation = "Данное поле может содержать только буквы, дефисы и пробелы.";
    public static readonly string OnFailedEmailValidation = "Некорректный формат электронной почты";

    public static readonly string OnFailedPhoneNumberValidation =
        $"Номер телефона должен иметь длину от {AccountConstants.MinPhoneNumberLength}" +
        $" до {AccountConstants.MaxPhoneNumberLength} цифр";
    public static readonly string OnFailedPasswordValidation =
        $"Длина пароля должна составлять от {AccountConstants.MinPasswordLength}" +
        $" до {AccountConstants.MaxPasswordLength} символов." +
        $" Также пароль должен содержать символы в верхнем и нижнем регистрах," +
        $" не должен содержать пробела, должен содержать хотя бы одну цифру";
}
