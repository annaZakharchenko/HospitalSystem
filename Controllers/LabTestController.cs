using System.Security.Claims;
using HospitalSystem.DTOs.LabTest;
using HospitalSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalSystem.Controllers;

[ApiController]
[Route("api/labtests")]
[Authorize]
public class LabTestController : ControllerBase
{
    private readonly ILabTestService _labTestService;

    public LabTestController(ILabTestService labTestService)
    {
        _labTestService = labTestService;
    }

    [Authorize(Roles = "Doctor")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateLabTestDto dto)
    {
        var doctorId = int.Parse(User.FindFirstValue("sub")!);
        dto.DoctorId = doctorId;

        return Ok(await _labTestService.CreateAsync(dto));
    }

    [Authorize(Roles = "LabTechnician")]
    [HttpPut("{id}/result")]
    public async Task<IActionResult> AddResult(int id, AddLabTestResultDto dto)
    {
        dto.LabTestId = id;
        return Ok(await _labTestService.AddResultAsync(dto));
    }

    [Authorize(Roles = "Patient")]
    [HttpGet("patient")]
    public async Task<IActionResult> GetByPatientId()
    {
        var patientId = int.Parse(User.FindFirstValue("sub")!);
        return Ok(await _labTestService.GetByPatientIdAsync(patientId));
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchByPatientName([FromQuery] string name)
    {
        var tests = await _labTestService.SearchByPatientNameAsync(name);
        return Ok(tests);
    }

    [Authorize(Roles = "Doctor,Admin")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
        => Ok(await _labTestService.GetByIdAsync(id));
}
