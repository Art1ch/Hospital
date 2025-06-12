using DoctorAPI.Application.RepositoryResults.Doctor.GetById;

namespace DoctorAPI.Application.Responses.Doctor;

public record GetByIdDoctorResponse(
    GetByIdDoctorResult Doctor);
