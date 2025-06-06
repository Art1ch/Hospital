using DoctorAPI.Core.Entities;

namespace DoctorAPI.Application.WebDto_s.Doctor.GetBySpecialization;

public record GetBySpecializationDoctorResponseDto<TDoctorId,
    TSpecializationId>(
    DoctorEntity<TDoctorId,
        TSpecializationId> Doctor);
