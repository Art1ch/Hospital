using DoctorAPI.Core.Enums;

namespace DoctorAPI.Core.Entities;

public class DoctorEntity<TDoctorId, TSpecializationId>
{
    public TDoctorId Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public StatusEnum Status { get; set; }
    public DateOnly BirthDate { get; set; }
    public DateOnly CareerStartDay { get; set; }
    public TSpecializationId SpecializationId { get; set; }
    public SpecializationEntity<TDoctorId, TSpecializationId> Specialization { get; set; }
}
