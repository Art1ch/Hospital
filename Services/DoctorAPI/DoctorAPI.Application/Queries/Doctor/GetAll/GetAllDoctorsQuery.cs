using DoctorAPI.Application.Requests.Doctor;
using DoctorAPI.Application.Responses.Doctor;
using MediatR;

namespace DoctorAPI.Application.Queries.Doctor.GetAll;

public record GetAllDoctorsQuery(int Page, int PageSize) : IRequest<GetAllDoctorsResponse>;
