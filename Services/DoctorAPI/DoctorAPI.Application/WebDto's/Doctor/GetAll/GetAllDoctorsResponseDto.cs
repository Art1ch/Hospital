using DoctorAPI.Core.Entities;

namespace DoctorAPI.Application.WebDto_s.Doctor.GetAll;

public record GetAllDoctorsResponseDto<TId1, TId2>(
    List<DoctorEntity<TId1, TId2>> Doctors);
