using System.Security.Claims;
using HospitalSystem.DTOs.User.LabTechnician;
using HospitalSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalSystem.Controllers;

[ApiController]
[Route("api/lab-technicians")]
public class LabTechnicianController : ControllerBase
{
    private readonly ILabTechnician _labtechnician;

    public LabTechnicianController(ILabTechnician labtechnician)
    {
        _labtechnician = labtechnician;
    }
    
    [HttpGet("profile")]
    [Authorize(Roles = "LabTechnician")]
    public async Task<IActionResult> GetProfile()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
                               ?? User.FindFirst("sub")?.Value 
                               ?? throw new Exception("User ID not found in token"));

        var profile = await _labtechnician.GetProfileAsync(userId);
        if (profile == null)
            return NotFound();

        return Ok(profile);
    }

    [HttpPut("profile")]
    [Authorize(Roles = "LabTechnician")]
    public async Task<IActionResult> UpdateProfile(UpdateLabTechnicianDto dto)
    {
        var userId = int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? User.FindFirstValue("sub")
            ?? throw new Exception("User ID not found")
        );

        var profile= await _labtechnician.UpdateProfileAsync(userId, dto);
        return Ok(profile);
    }
}