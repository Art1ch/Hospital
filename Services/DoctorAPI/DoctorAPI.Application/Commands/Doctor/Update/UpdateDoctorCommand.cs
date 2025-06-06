using DoctorAPI.Application.WebDto_s.Doctor.Update;
using DoctorAPI.Application.WebRequests.Doctor.Update;
using MediatR;

namespace DoctorAPI.Application.Commands.Doctor.Create;

public record UpdateDoctorCommand<TId1, TId2>(
    UpdateDoctorRequestDto<TId1, TId2> Dto) : IRequest<UpdateDoctorResponseDto<TId1>>;
