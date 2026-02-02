using HospitalSystem.Domain.Enums;

namespace HospitalSystem.DTOs.Auth;

public class AuthResponseDto
{
    public string Token { get; set; } = null!;
    public string Email { get; set; } = null!;
    public UserRole Role { get; set; }
}