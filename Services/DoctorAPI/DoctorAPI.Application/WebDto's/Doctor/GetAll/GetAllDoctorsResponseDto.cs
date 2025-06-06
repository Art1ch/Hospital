using DoctorAPI.Core.Entities;

namespace DoctorAPI.Application.WebDto_s.Doctor.GetAll;

public record GetAllDoctorsResponseDto<TDoctorId, TSpecializationId>(
    List<DoctorEntity<TDoctorId, TSpecializationId>> Doctors);
