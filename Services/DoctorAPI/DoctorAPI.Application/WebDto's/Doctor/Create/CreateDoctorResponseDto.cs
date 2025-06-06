namespace DoctorAPI.Application.WebRequests.Doctor.Create;

public record CreateDoctorResponseDto<TDoctorId>(
    TDoctorId Id);
