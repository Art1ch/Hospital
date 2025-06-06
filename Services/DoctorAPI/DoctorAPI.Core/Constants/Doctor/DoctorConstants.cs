namespace DoctorAPI.Core.Constants.Doctor;

public static class DoctorConstants
{
    public const int MinFirstNameLength = 2;
    public const int MaxFirstNameLength = 30;

    public const int MinLastNameLength = 2;
    public const int MaxLastNameLength = 30;

    public const int MinMiddleNameLength = 2;
    public const int MaxMiddleNameLength = 30;

    public static readonly string OnFailedFirstNameValidation =
        $"Имя должно содержать от {MinFirstNameLength} до {MaxFirstNameLength} символов.";

    public static readonly string OnFailedLastNameValidation =
        $"Фамилия должна содержать от {MinLastNameLength} до {MaxLastNameLength} символов.";

    public static readonly string OnFailedMiddleNameValidation =
        $"Отчество должно содержать от {MinMiddleNameLength} до {MaxMiddleNameLength} символов.";
}
