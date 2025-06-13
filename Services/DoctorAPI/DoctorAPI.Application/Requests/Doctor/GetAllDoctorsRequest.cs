namespace DoctorAPI.Application.Requests.Doctor;

public record GetAllDoctorsRequest(
    int Page,
    int PageSize);
