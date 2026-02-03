namespace HospitalSystem.DTOs.Admin;

public class CreateStaffDto
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    
    public string? Specialization { get; set; }
}