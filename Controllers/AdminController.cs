using HospitalSystem.DTOs.Admin;
using HospitalSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/admin")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpPost("create-doctor")]
    public async Task<IActionResult> CreateDoctor(CreateStaffDto dto)
    {
        await _adminService.CreateDoctorAsync(dto);
        return Ok("Doctor created");
    }

    [HttpPost("create-lab")]
    public async Task<IActionResult> CreateLab(CreateStaffDto dto)
    {
        await _adminService.CreateLabTechnicianAsync(dto);
        return Ok("Lab technician created");
    }
}