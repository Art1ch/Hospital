using DoctorAPI.Core.Entities;
using DoctorAPI.Core.Enums;

namespace DoctorAPI.Application.WebRequests.Doctor.Create;

public record CreateDoctorRequestDto<TId1, TId2>(
    string FirstName,
    string LastName,
    string MiddleName,
    StatusEnum Status,
    DateOnly BirthDate,
    DateOnly CareerStartDay,
    SpecializationEntity<TId1, TId2> Specialization);
