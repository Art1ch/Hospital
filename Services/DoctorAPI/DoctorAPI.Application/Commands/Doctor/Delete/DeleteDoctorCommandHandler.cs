using AutoMapper;
using DoctorAPI.Application.Contracts;
using DoctorAPI.Application.WebDto_s.Doctor.Delete;
using MediatR;

namespace DoctorAPI.Application.Commands.Doctor.Delete;

public class DeleteDoctorCommandHandler
    : IRequestHandler<DeleteDoctorCommand,
        DeleteDoctorResponseDto>
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly IMapper _mapper;

    public DeleteDoctorCommandHandler(
        IDoctorRepository doctorRepository,
        IMapper mapper)
    {
        _doctorRepository = doctorRepository;
        _mapper = mapper;
    }

    public async Task<DeleteDoctorResponseDto> Handle(
        DeleteDoctorCommand request,
        CancellationToken ct)
    {
        var response = await _doctorRepository.DeleteAsync(request.Dto.Id, ct);
        var result = _mapper.Map<DeleteDoctorResponseDto>(response);
        return result;
    }
}
