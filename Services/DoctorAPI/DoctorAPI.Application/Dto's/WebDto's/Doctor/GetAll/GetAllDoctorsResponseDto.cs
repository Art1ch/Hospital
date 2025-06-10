using DoctorAPI.Core.Entities;

namespace DoctorAPI.Application.WebDto_s.Doctor.GetAll;

public record GetAllDoctorsResponseDto(
    List<GetAllDoctorsRepoDto> Doctors);
