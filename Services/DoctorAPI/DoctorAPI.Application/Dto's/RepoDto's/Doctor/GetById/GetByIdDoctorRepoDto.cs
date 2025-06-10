using DoctorAPI.Core.Entities;
using DoctorAPI.Core.Enums;

namespace DoctorAPI.Application.WebDto_s.Doctor.GetById;

public record GetByIdDoctorRepoDto(
    Guid Id,
    string FirstName,
    string LastName,
    string MiddleName,
    StatusEnum Status,
    DateOnly BirthDate,
    DateOnly CareerStartDay,
    SpecializationEntity Specialization);
