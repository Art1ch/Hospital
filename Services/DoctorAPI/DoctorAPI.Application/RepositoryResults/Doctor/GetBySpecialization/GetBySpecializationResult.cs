namespace DoctorAPI.Application.RepositoryResults.Doctor.GetBySpecialization;

public record GetBySpecializationResult(
    Guid Id,
    string FirstName,
    string LastName,
    string MiddleName);
