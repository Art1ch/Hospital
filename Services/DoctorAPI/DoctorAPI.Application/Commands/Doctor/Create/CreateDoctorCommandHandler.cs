using AutoMapper;
using DoctorAPI.Application.Contracts;
using DoctorAPI.Application.WebRequests.Doctor.Create;
using DoctorAPI.Core.Entities;
using FluentValidation;
using MediatR;

namespace DoctorAPI.Application.Commands.Doctor.Create;

public class CreateDoctorCommandHandler<TId1, TId2>
    : IRequestHandler<CreateDoctorCommand<TId1, TId2>, CreateDoctorResponseDto<TId1>>
{
    private readonly IDoctorRepository<TId1, TId2> _doctorRepository;
    private readonly IValidator<DoctorEntity<TId1, TId2>> _validator;
    private readonly IMapper _mapper;

    public CreateDoctorCommandHandler(
        IDoctorRepository<TId1, TId2> doctorRepository,
        IValidator<DoctorEntity<TId1, TId2>> validator)
    {
        _doctorRepository = doctorRepository;
        _validator = validator;
    }

    public async Task<CreateDoctorResponseDto<TId1>> Handle(
        CreateDoctorCommand<TId1, TId2> request, 
        CancellationToken ct)
    {
        var doctorEntity = _mapper.Map<DoctorEntity<TId1, TId2>>(request.Dto);
        var validationResult = await _validator.ValidateAsync(doctorEntity, ct);
        if (!validationResult.IsValid)
        {
            throw new Exception();
        }
        await _doctorRepository.Create(doctorEntity, ct);
        return _mapper.Map<CreateDoctorResponseDto<TId1>>(doctorEntity);
    }
}
