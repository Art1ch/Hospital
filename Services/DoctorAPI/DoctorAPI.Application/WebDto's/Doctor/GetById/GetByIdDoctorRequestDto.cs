using DoctorAPI.Core.Entities;

namespace DoctorAPI.Application.WebDto_s.Doctor.GetById;

public record GetByIdDoctorRequestDto<T>(
    T Id);
