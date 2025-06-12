namespace DoctorAPI.Application.RepositoryResults.Doctor.GetAll;

public record GetAllDoctorsResult(
    Guid Id,
    string FirstName,
    string LastName,
    string MiddleName);
