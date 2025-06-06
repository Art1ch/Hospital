using DoctorAPI.Core.Entities;

namespace DoctorAPI.Application.WebDto_s.Doctor.GetById;

public record GetByIdDoctorRequestDto<TDoctorId>(
    TDoctorId Id);
