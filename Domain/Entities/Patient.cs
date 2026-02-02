namespace HospitalSystem.Domain.Entities;

public class Patient
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public string Phone { get; set; } = null!;

    public MedicalRecord MedicalRecord { get; set; } = null!;
}