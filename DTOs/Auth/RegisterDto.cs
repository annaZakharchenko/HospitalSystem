using HospitalSystem.Domain.Enums;

namespace HospitalSystem.DTOs.Auth;

public class RegisterDto
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public UserRole Role { get; set; } 
}