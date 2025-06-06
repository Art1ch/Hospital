using DoctorAPI.Core.Entities;

namespace DoctorAPI.Application.WebDto_s.Doctor.GetBySpecialization;

public record GetBySpecializationDoctorResponseDto<TId1, TId2>(
    DoctorEntity<TId1, TId2> Doctor);
