using DoctorAPI.Core.Entities;

namespace DoctorAPI.Application.WebDto_s.Doctor.GetByStatus;

public record GetByStatusDoctorsResponseDto<TId1, TId2>(
    List<DoctorEntity<TId1, TId2>> Doctors);
