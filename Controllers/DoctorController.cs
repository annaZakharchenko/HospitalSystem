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

    [Authorize(Roles = "Doctor")]
    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile(UpdateDoctorDto dto)
    {
        var userId = int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? User.FindFirstValue("sub")
            ?? throw new Exception("User ID not found in token")
        );

        await _doctorService.UpdateProfileAsync(userId, dto);
        return Ok();
    }
}