using DoctorAPI.Core.Entities;
using DoctorAPI.Core.Enums;

namespace DoctorAPI.Application.WebRequests.Doctor.Update;

public record UpdateDoctorRequestDto<TDoctorId,
    TSpecializationId>(
    TDoctorId Id,
    string FirstName,
    string LastName,
    string MiddleName,
    StatusEnum Status,
    DateOnly BirthDate,
    DateOnly CareerStartDay,
    SpecializationEntity<TDoctorId,
        TSpecializationId> Specialization);
