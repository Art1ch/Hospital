using DoctorAPI.Application.RepositoryResults.Doctor.GetBySpecialization;

namespace DoctorAPI.Application.Responses.Doctor;

public record GetBySpecializationDoctorResponse(
    GetBySpecializationResult Doctor);
