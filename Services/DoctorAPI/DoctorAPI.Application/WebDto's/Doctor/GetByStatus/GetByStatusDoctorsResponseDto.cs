using DoctorAPI.Core.Entities;

namespace DoctorAPI.Application.WebDto_s.Doctor.GetByStatus;

public record GetByStatusDoctorsResponseDto<TDoctorId,
    TSpecializationId>(
    List<DoctorEntity<TDoctorId,
        TSpecializationId>> Doctors);
