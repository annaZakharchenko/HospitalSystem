namespace HospitalSystem.DTOs.Patient;

public class UpdatePatientDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime? DateOfBirth { get; set; }
    public string? Phone { get; set; }
}