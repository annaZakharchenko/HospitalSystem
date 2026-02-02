namespace HospitalSystem.DTOs.Doctor;

public class CreateDoctorDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Specialization { get; set; }
}