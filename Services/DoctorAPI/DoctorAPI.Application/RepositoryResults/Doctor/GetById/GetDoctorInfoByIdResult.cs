using DoctorAPI.Application.Entities;
using DoctorAPI.Application.Enums;

namespace DoctorAPI.Application.RepositoryResults.Doctor.GetById;

public record GetDoctorInfoByIdResult(
    Guid Id,
    Guid AccountId,
    string FirstName,
    string LastName,
    string MiddleName,
    DoctorStatus Status,
    DateOnly BirthDate,
    DateOnly CareerStartDay,
    SpecializationEntity Specialization);
