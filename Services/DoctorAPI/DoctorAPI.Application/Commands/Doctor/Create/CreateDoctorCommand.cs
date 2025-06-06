using DoctorAPI.Application.WebRequests.Doctor.Create;
using MediatR;

namespace DoctorAPI.Application.Commands.Doctor.Create;

public record CreateDoctorCommand<TId1, TId2>(
    CreateDoctorRequestDto<TId1, TId2> Dto) : IRequest<CreateDoctorResponseDto<TId1>>;

