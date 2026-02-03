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

    // Доктор создаёт тест
    [HttpPost]
    public async Task<IActionResult> CreateLabTest(CreateLabTestDto dto)
    {
        var result = await _labTestService.CreateAsync(dto);
        return Ok(result);
    }

    [HttpPost("result")]
    public async Task<IActionResult> AddResult(AddLabTestResultDto dto)
    {
        var result = await _labTestService.AddResultAsync(dto);
        return Ok(result);
    }

    [HttpGet("patient/{patientId}")]
    public async Task<IActionResult> GetByPatient(int patientId)
    {
        var tests = await _labTestService.GetByPatientIdAsync(patientId);
        return Ok(tests);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchByPatientName([FromQuery] string name)
    {
        var tests = await _labTestService.SearchByPatientNameAsync(name);
        return Ok(tests);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var test = await _labTestService.GetByIdAsync(id);
        return Ok(test);
    }
}
