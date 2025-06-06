using DoctorAPI.Application.Contracts;
using DoctorAPI.Application.WebDto_s.Doctor.Delete;
using MediatR;

namespace DoctorAPI.Application.Commands.Doctor.Delete;

public class DeleteDoctorCommandHandler<TId1, TId2>
    : IRequestHandler<DeleteDoctorCommand<TId1>, DeleteDoctorResponseDto>
{
    private readonly IDoctorRepository<TId1, TId2> _doctorRepository;

    public DeleteDoctorCommandHandler(IDoctorRepository<TId1, TId2> doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public async Task<DeleteDoctorResponseDto> Handle(
        DeleteDoctorCommand<TId1> request,
        CancellationToken ct)
    {
        await _doctorRepository.Delete(request.Dto.Id, ct);
        return new DeleteDoctorResponseDto();
    }
}
