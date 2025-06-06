using DoctorAPI.Core.Enums;

namespace DoctorAPI.Application.WebDto_s.Doctor.GetByStatus;

public record GetByStatusDoctorsRequestDto(
    StatusEnum Status);