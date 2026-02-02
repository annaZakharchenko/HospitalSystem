using HospitalSystem.Domain.Entities;

namespace HospitalSystem.DTOs.Patient;

public class PatientDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
}