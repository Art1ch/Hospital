using DoctorAPI.Application.Contracts;
using DoctorAPI.Application.WebDto_s.Doctor.Delete;
using MediatR;

namespace DoctorAPI.Application.Commands.Doctor.Delete;

public class DeleteDoctorCommandHandler<TDoctorId,
    TSpecializationId>
    : IRequestHandler<DeleteDoctorCommand<TDoctorId>,
        DeleteDoctorResponseDto>
{
    private readonly IDoctorRepository<TDoctorId,
        TSpecializationId> _doctorRepository;

    public DeleteDoctorCommandHandler(IDoctorRepository<TDoctorId,
        TSpecializationId> doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task<DeleteDoctorResponseDto> Handle(
        DeleteDoctorCommand<TDoctorId> request,
        CancellationToken ct)
    {
        await _doctorRepository.DeleteAsync(request.Dto.Id, ct);
        return new DeleteDoctorResponseDto();
    }
}
