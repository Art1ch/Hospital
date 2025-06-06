using DoctorAPI.Core.Entities;

namespace DoctorAPI.Application.WebDto_s.Doctor.GetById;

public record GetByIdDoctorResponseDto<TId1, TId2>(
    DoctorEntity<TId1, TId2> Doctor);
