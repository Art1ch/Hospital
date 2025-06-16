namespace DoctorAPI.Application.RepositoryResults.Doctor.GetBySpecialization;

public record GetDoctorBySpecializationResult(
    Guid Id,
    string FirstName,
    string LastName,
    string MiddleName);
