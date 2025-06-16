using DoctorAPI.Application.Enums;

namespace DoctorAPI.Application.Requests.Doctor;

public record CreateDoctorRequest(
    string FirstName,
    string LastName,
    string MiddleName,
    DoctorStatus Status,
    DateOnly BirthDate,
    DateOnly CareerStartDay,
    string SpecializationName);
