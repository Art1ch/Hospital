using DoctorAPI.Application.RepositoryResults.Doctor.GetAll;

namespace DoctorAPI.Application.Responses.Doctor;

public record GetAllDoctorsResponse(
    List<GetAllDoctorsResult> Doctors);
