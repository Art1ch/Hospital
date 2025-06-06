using DoctorAPI.Core.Enums;

namespace DoctorAPI.Core.Entities;

public class DoctorEntity<TId1, TId2>
{
    public TId1 Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public StatusEnum Status { get; set; }
    public DateOnly BirthDate { get; set; }
    public DateOnly CareerStartDay { get; set; }
    public TId2 SpecializationId { get; set; }
    public SpecializationEntity<TId1, TId2> Specialization { get; set; }
}
