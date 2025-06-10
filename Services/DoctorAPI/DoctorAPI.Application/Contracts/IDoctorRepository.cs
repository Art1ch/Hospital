using DoctorAPI.Application.Dto_s.RepoDto_s.Doctor.Create;
using DoctorAPI.Application.Dto_s.RepoDto_s.Doctor.Delete;
using DoctorAPI.Application.Dto_s.RepoDto_s.Doctor.Update;
using DoctorAPI.Application.WebDto_s.Doctor.GetAll;
using DoctorAPI.Application.WebDto_s.Doctor.GetById;
using DoctorAPI.Application.WebDto_s.Doctor.GetBySpecialization;
using DoctorAPI.Application.WebDto_s.Doctor.GetByStatus;
using DoctorAPI.Core.Entities;
using DoctorAPI.Core.Enums;

namespace DoctorAPI.Application.Contracts;

public interface IDoctorRepository
{
    Task<List<GetAllDoctorsRepoDto>> GetAllAsync(
        int page, int pageSize, CancellationToken ct);
    Task<GetByIdDoctorRepoDto> GetByIdAsync(
        Guid id, CancellationToken ct);
    Task<GetBySpecializationRepoDto> GetBySpecializationAsync(
        int specializationId, CancellationToken ct);
    Task<List<GetByStatusRepoDto>> GetByStatusAsync(
        StatusEnum status, CancellationToken ct);
    Task<CreateDoctorRepoDto> CreateAsync(
        DoctorEntity doctor, CancellationToken ct);
    Task<UpdateDoctorRepoDto> UpdateAsync(
        DoctorEntity doctor, CancellationToken ct);
    Task<DeleteDoctorRepoDto> DeleteAsync(Guid id, CancellationToken ct);
}