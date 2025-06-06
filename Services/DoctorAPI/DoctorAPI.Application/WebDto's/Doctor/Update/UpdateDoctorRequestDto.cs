using DoctorAPI.Core.Entities;
using DoctorAPI.Core.Enums;

namespace DoctorAPI.Application.WebRequests.Doctor.Update;

public record UpdateDoctorRequestDto<TId1, TId2>(
    TId1 Id,
    string FirstName,
    string LastName,
    string MiddleName,
    StatusEnum Status,
    DateOnly BirthDate,
    DateOnly CareerStartDay,
    SpecializationEntity<TId1, TId2> Specialization);
