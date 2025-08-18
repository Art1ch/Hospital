using DoctorAPI.Application.Contracts.Cache;
using DoctorAPI.Application.Contracts.Repository.Doctor;
using MediatR;

namespace DoctorAPI.Application.Commands.Doctor.Delete;

internal class DeleteDoctorCommandHandler : IRequestHandler<DeleteDoctorCommand>
{
    private readonly IDoctorRepository _doctorRepository;
    private readonly ICacheService _cacheService;

    public DeleteDoctorCommandHandler(IDoctorRepository doctorRepository, ICacheService cacheService)
    {
        _doctorRepository = doctorRepository;
        _cacheService = cacheService;
    }

    public async Task Handle(DeleteDoctorCommand command, CancellationToken cancellationToken)
    {
        await _cacheService.RemoveAsync(command.Id.ToString(), cancellationToken);
        await _doctorRepository.DeleteAsync(command.Id);
    }
}
