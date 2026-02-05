using System.Security.Claims;
using HospitalSystem.DTOs.Doctor;
using HospitalSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalSystem.Controllers;

[ApiController]
[Route("api/doctors")]
public class DoctorController : ControllerBase
{
    private readonly IDoctorService _doctorService;

    public DoctorController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var doctors = await _doctorService.GetAllAsync();
        return Ok(doctors);
    }
    [HttpGet("profile")]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> GetProfile()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
                               ?? User.FindFirst("sub")?.Value 
                               ?? throw new Exception("User ID not found in token"));

        var profile = await _doctorService.GetProfileAsync(userId);
        if (profile == null)
            return NotFound();

        return Ok(profile);
    }

    [HttpPut("profile")]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> UpdateProfile(UpdateDoctorDto dto)
    {
        var userId = int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? User.FindFirstValue("sub")
            ?? throw new Exception("User ID not found in token")
        );

        var profile = await _doctorService.UpdateProfileAsync(userId, dto);
        return Ok(profile);
    }
}