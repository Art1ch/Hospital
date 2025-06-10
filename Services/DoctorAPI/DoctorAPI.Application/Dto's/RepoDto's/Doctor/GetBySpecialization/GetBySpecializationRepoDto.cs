namespace DoctorAPI.Application.WebDto_s.Doctor.GetBySpecialization;

public record GetBySpecializationRepoDto(
    Guid Id,
    string FirstName,
    string LastName,
    string MiddleName);
