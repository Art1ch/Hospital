using DoctorAPI.Application.Constants.Doctor;
using DoctorAPI.Application.Constants.Specialization;

namespace DoctorAPI.Application.Validation.Constants;

internal static class ValidationConstants
{
    public static readonly string OnFailedFirstNameValidation =
       $"Имя должно содержать от {DoctorConstants.MinFirstNameLength}" +
        $" до {DoctorConstants.MaxLastNameLength} символов.";
    public static readonly string OnFailedLastNameValidation =
        $"Фамилия должна содержать от {DoctorConstants.MinLastNameLength}" +
        $" до {DoctorConstants.MaxLastNameLength} символов.";
    public static readonly string OnFailedMiddleNameValidation =
        $"Отчество должно содержать от {DoctorConstants.MinMiddleNameLength}" +
        $" до {DoctorConstants.MaxMiddleNameLength} символов.";
    public static readonly string OnFailedSpecializationNameValidation = 
        $"Имя специальности должно содержать от {SpecializationConstants.MinSpecializationNameLength}" +
        $" до {SpecializationConstants.MaxSpecializationNameLength} символов.";
    public static readonly string OnFailedPageNumberValidation =
        $"Минимальная страница должна быть {PaginationConstants.MinPageNumber}";
    public static readonly string OnFailedPageSizeValidation =
        $"Количество данных на странице должно варьироваться от {PaginationConstants.MinPageSize}" +
        $" до {PaginationConstants.MaxPageSize} единиц";
    public static readonly string OnFailedRequiredValidation = "Поле обязательно для заполнения.";
    public static readonly string OnFailedNullValidation = "Значение не может быть null.";
    public static readonly string OnFailedRegexValidation ="Данное поле может содержать только буквы, дефисы и пробелы.";
}
