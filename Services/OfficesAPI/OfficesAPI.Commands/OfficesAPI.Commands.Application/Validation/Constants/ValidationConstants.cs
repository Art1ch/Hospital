namespace OfficesAPI.Commands.Application.Validation.Constants;

internal static class ValidationConstants
{
    public const string OnFailedRequiredValidation = "Поле обязательно для заполнения.";
    public const string OnFailedNullValidation = "Значение не может быть null.";
    public const string OnFailedRegexValidation = "Данное поле может содержать только буквы, дефисы и пробелы.";
    public const string OnFailedEmailValidation = "Некорректный формат электронной почты";

    public static readonly string OnFailedPhoneNumberValidation = $"""
        Номер телефона должен иметь длину от {OfficeConstants.MinPhoneNumberLength}
         до {OfficeConstants.MaxPhoneNumberLength} цифр
        """;
    public static readonly string OnFailedAddressNameValidation = $"""
        Длина адреса должна составлять от {OfficeConstants.MinAddressNameLength}
         до {OfficeConstants.MaxAddressNameLength} символов.
        """;
}
