using HospitalSystem.DTOs.Auth;
using HospitalSystem.DTOs.Patient;
using HospitalSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HospitalSystem.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    // [HttpPost("register")]
    // public async Task<ActionResult<AuthResponseDto>> Register(RegisterDto dto)
    // {
    //     return Ok(await _authService.RegisterAsync(dto));
    // }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterPatient(RegisterPatientDto dto)
    {
        var result = await _authService.RegisterPatientAsync(dto);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login(LoginDto dto)
    {
        return Ok(await _authService.LoginAsync(dto));
    }
}