using DoctorAPI.Application.WebDto_s.Doctor.Delete;
using MediatR;

namespace DoctorAPI.Application.Commands.Doctor.Delete;

public record DeleteDoctorCommand<TId1>(
    DeleteDoctorRequestDto<TId1> Dto) : IRequest<DeleteDoctorResponseDto>;

