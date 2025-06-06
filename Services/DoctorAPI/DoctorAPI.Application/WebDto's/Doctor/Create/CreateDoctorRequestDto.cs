using DoctorAPI.Core.Entities;
using DoctorAPI.Core.Enums;

namespace DoctorAPI.Application.WebRequests.Doctor.Create;

public record CreateDoctorRequestDto<TDoctorId, TSpecializationId>(
    string FirstName,
    string LastName,
    string MiddleName,
    StatusEnum Status,
    DateOnly BirthDate,
    DateOnly CareerStartDay,
    SpecializationEntity<TDoctorId,
        TSpecializationId> Specialization);
