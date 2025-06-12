using DoctorAPI.Core.Constants.Doctor;
using DoctorAPI.Core.Constants.Specialization;

namespace DoctorAPI.Application.Validation.Constants;

internal class ValidationConstants
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
    public const string OnFailedRequiredValidation = "Поле обязательно для заполнения.";
    public const string OnFailedNullValidation = "Значение не может быть null.";
    public const string OnFailedRegexValidation ="Данное поле может содержать только буквы, дефисы и пробелы.";
}
