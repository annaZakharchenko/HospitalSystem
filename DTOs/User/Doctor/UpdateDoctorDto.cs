namespace HospitalSystem.DTOs.Doctor;

public class UpdateDoctorDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Specialization { get; set; } = null!;
}