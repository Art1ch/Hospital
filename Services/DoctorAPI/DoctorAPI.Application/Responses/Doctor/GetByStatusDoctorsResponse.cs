using DoctorAPI.Application.RepositoryResults.Doctor.GetByStatus;

namespace DoctorAPI.Application.Responses.Doctor;

public record GetByStatusDoctorsResponse(
    List<GetByStatusResult> Doctors);
