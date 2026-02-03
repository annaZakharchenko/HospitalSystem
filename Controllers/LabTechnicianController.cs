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

    [Authorize(Roles = "LabTechnician")]
    [HttpPut("profile")]
    public async Task<IActionResult> UpdateProfile(UpdateLabTechnicianDto dto)
    {
        var userId = int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? User.FindFirstValue("sub")
            ?? throw new Exception("User ID not found")
        );

        await _labtechnician.UpdateProfileAsync(userId, dto);
        return Ok();
    }
}