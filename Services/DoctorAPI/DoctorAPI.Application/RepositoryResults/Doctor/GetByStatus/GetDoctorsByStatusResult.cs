namespace DoctorAPI.Application.RepositoryResults.Doctor.GetByStatus;

public record GetDoctorsByStatusResult(
    Guid Id,
    string FirstName,
    string LastName,
    string MiddleName);
