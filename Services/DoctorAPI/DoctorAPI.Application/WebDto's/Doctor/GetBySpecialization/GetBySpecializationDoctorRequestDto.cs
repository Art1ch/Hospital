namespace DoctorAPI.Application.WebDto_s.Doctor.GetBySpecialization;

public record GetBySpecializationDoctorRequestDto<T>(
    T SpecializationId);
