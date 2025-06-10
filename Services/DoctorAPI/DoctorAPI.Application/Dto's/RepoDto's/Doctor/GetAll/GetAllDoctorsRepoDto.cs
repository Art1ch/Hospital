
namespace DoctorAPI.Application.WebDto_s.Doctor.GetAll;

public record GetAllDoctorsRepoDto(
    Guid Id,
    string FirstName,
    string LastName,
    string MiddleName);
