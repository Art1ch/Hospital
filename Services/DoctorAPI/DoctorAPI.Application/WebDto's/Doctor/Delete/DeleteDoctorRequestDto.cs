namespace DoctorAPI.Application.WebDto_s.Doctor.Delete;

public record DeleteDoctorRequestDto<TDoctorId>(
    TDoctorId Id);
