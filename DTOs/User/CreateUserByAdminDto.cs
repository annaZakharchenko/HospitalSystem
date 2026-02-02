using HospitalSystem.Domain.Enums;

namespace HospitalSystem.DTOs.User;

public class CreateUserByAdminDto
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public UserRole Role { get; set; }
}