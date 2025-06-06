using AutoMapper;
using DoctorAPI.Application.Contracts;
using DoctorAPI.Application.WebRequests.Doctor.Create;
using DoctorAPI.Core.Entities;
using FluentValidation;
using MediatR;

namespace DoctorAPI.Application.Commands.Doctor.Create;

public class CreateDoctorCommandHandler<TDoctorId, TSpecializationId>
    : IRequestHandler<CreateDoctorCommand<TDoctorId,
        TSpecializationId>,
        CreateDoctorResponseDto<TDoctorId>>
{
    private readonly IDoctorRepository<TDoctorId, TSpecializationId> _doctorRepository;
    private readonly IValidator<DoctorEntity<TDoctorId, TSpecializationId>> _validator;
    private readonly IMapper _mapper;

    public CreateDoctorCommandHandler(
        IDoctorRepository<TDoctorId, TSpecializationId> doctorRepository,
        IValidator<DoctorEntity<TDoctorId, TSpecializationId>> validator,
        IMapper mapper)
    {
        _doctorRepository = doctorRepository;
        _validator = validator;
        _mapper = mapper;
    }

    public async Task<CreateDoctorResponseDto<TDoctorId>> Handle(
        CreateDoctorCommand<TDoctorId,
            TSpecializationId> request, 
        CancellationToken ct)
    {
        var doctorEntity = _mapper.Map<DoctorEntity<TDoctorId, TSpecializationId>>(request.Dto);
        var validationResult = await _validator.ValidateAsync(doctorEntity, ct);
        if (!validationResult.IsValid)
        {
            throw new Exception();
        }
        await _doctorRepository.CreateAsync(doctorEntity, ct);
        return _mapper.Map<CreateDoctorResponseDto<TDoctorId>>(doctorEntity);
    }
}
