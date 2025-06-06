namespace DoctorAPI.Core.Constants.Specialization;

public static class SpecializationConstants
{
    public const int MinSpecializationNameLength = 4;
    public const int MaxSpecializationNameLength = 20;

    public static readonly string OnFailedSpecializationNameValidation =
        $"Имя специализации должно содержать от {MinSpecializationNameLength} " +
        $"до {MaxSpecializationNameLength} символов.";
}
