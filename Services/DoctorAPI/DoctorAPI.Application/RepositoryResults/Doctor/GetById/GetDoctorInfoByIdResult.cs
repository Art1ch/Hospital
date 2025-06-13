using DoctorAPI.Core.Entities;
using DoctorAPI.Core.Enums;

namespace DoctorAPI.Application.RepositoryResults.Doctor.GetById;

public record GetDoctorInfoByIdResult(
    Guid Id,
    string FirstName,
    string LastName,
    string MiddleName,
    DoctorStatus Status,
    DateOnly BirthDate,
    DateOnly CareerStartDay,
    SpecializationEntity Specialization);
