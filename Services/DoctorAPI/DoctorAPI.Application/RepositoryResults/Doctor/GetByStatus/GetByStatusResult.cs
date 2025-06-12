namespace DoctorAPI.Application.RepositoryResults.Doctor.GetByStatus;

public record GetByStatusResult(
    Guid Id,
    string FirstName,
    string LastName,
    string MiddleName);
