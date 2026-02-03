using System.Security.Claims;
using HospitalSystem.DTOs.Patient;
using HospitalSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalSystem.Controllers;

[ApiController]
[Route("api/patients")]
[Authorize]
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var patients = await _patientService.GetAllAsync();
        return Ok(patients);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var patient = await _patientService.GetByIdAsync(id);
        if (patient == null)
            return NotFound();
        
        return Ok(patient);
    }

    [HttpGet("profile")]
    [Authorize(Roles = "Patient")]
    public async Task<IActionResult> GetProfile()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
                               ?? User.FindFirst("sub")?.Value 
                               ?? throw new Exception("User ID not found in token"));

        var profile = await _patientService.GetProfileAsync(userId);
        if (profile == null)
            return NotFound();

        return Ok(profile);
    }

    [HttpPut("profile")]
    [Authorize(Roles = "Patient")]
    public async Task<IActionResult> UpdateProfile(UpdatePatientDto dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
                               ?? User.FindFirst("sub")?.Value 
                               ?? throw new Exception("User ID not found in token"));

        var profile = await _patientService.UpdateProfileAsync(userId, dto);
        return Ok(profile);
    }
}