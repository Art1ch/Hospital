namespace AppointmentAPI.Application.Validation.Constants;

internal static class ValidationConstants
{
    public const string OnFailedRequiredValidation = "Поле обязательно для заполнения.";
    public const string OnFailedNullValidation = "Значение не может быть null.";
    public const string OnFailedRegexValidation = "Данное поле может содержать только буквы, дефисы и пробелы.";
    public const string OnFailedDateValidation = "Дата встречи не может быть в прошлом";
    public const string OnFailedTimeValidation = $"""
        Время встречи может быть от {AppointmentConstants.StartAppointmentTimeString}
        до {AppointmentConstants.EndAppointmentTimeString}
        """;
}
