namespace HospitalSystem.Domain.Entities;

public class Doctor
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;
    
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    

    public string Specialization { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}