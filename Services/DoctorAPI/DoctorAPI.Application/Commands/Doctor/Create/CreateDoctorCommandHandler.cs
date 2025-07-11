using AutoMapper;
using DoctorAPI.Application.Contracts.Repository.Doctor;
using DoctorAPI.Application.Entities;
using MediatR;

namespace DoctorAPI.Application.Commands.Doctor.Create;

internal class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand>
{
    private readonly IMapper _mapper;
    private readonly IDoctorRepository _doctorRepository;

    public CreateDoctorCommandHandler(IMapper mapper, IDoctorRepository doctorRepository)
    {
        _mapper = mapper;
        _doctorRepository = doctorRepository;
    }

    public async Task Handle(CreateDoctorCommand command, CancellationToken cancellationToken)
    {
        var doctorEntity = _mapper.Map<DoctorEntity>(command.Request);
        await _doctorRepository.CreateAsync(doctorEntity, cancellationToken);   
    }
}
