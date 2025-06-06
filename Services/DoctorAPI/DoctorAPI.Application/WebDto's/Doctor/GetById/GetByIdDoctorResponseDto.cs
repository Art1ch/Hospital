using DoctorAPI.Core.Entities;

namespace DoctorAPI.Application.WebDto_s.Doctor.GetById;

public record GetByIdDoctorResponseDto<TDoctorId,
    TSpecializationId>(
    DoctorEntity<TDoctorId,
        TSpecializationId> Doctor);
