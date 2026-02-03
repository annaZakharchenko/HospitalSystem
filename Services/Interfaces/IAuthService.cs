using HospitalSystem.DTOs.Auth;
using HospitalSystem.DTOs.Patient;

namespace HospitalSystem.Services.Interfaces;

public interface IAuthService
{
   Task<AuthResponseDto> RegisterPatientAsync(RegisterPatientDto dto);
    Task<AuthResponseDto> LoginAsync(LoginDto dto);
}