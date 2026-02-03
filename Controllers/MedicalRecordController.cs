using System.Security.Claims;
using HospitalSystem.DTOs.MedicalRecord;
using HospitalSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalSystem.Controllers;

[ApiController]
[Route("api/medical-records")]
public class MedicalRecordController : ControllerBase
{
    private readonly IMedicalRecordService _service;

    public MedicalRecordController(IMedicalRecordService service)
    {
        _service = service;
    }

    [Authorize(Roles = "Doctor")]
    [HttpPost("entry")]
    public async Task<IActionResult> AddEntry(CreateMedicalRecordEntryDto dto)
    {
        var doctorId = int.Parse(User.FindFirstValue("sub")!);
        await _service.AddEntryAsync(doctorId, dto);
        return Ok();
    }

    [Authorize(Roles = "Patient")]
    [HttpGet("my")]
    public async Task<IActionResult> GetMy()
    {
        var patientId = int.Parse(User.FindFirstValue("sub")!);
        return Ok(await _service.GetByPatientIdAsync(patientId));
    }
}
