using System.Security.Claims;
using HospitalSystem.Data;
using HospitalSystem.Services.Interfaces;
using HospitalSystem.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/appointments")]
public class AppointmentController : ControllerBase
{
    private readonly IAppointmentService _service;
    private readonly HospitalDbContext _context;
    
    public AppointmentController(IAppointmentService service, HospitalDbContext context)
    {
        _service = service;
        _context = context;
    }
    
    [Authorize(Roles = "Doctor, Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var appointments = await _service.GetAllAsync();
        return Ok(appointments);
    }
    
    [Authorize(Roles = "Patient")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateAppointmentDto dto)
    {
        var userId = int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier) 
            ?? User.FindFirstValue("sub") 
            ?? throw new Exception("User ID not found in token")
        );

        var patient = await _context.Patients.FirstOrDefaultAsync(p => p.UserId == userId);
        if (patient == null)
            return NotFound("Patient not found");

        var result = await _service.CreateAsync(patient.Id, dto);
        return Ok(result);
    }
    
    [Authorize(Roles = "Doctor")]
    [HttpGet("doctor")]
    public async Task<IActionResult> GetForDoctor()
    {
        var userId = int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier) 
            ?? User.FindFirstValue("sub") 
            ?? throw new Exception("User ID not found in token")
        );

        var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.UserId == userId);
        if (doctor == null)
            return NotFound("Doctor not found");

        return Ok(await _service.GetByDoctorIdAsync(doctor.Id));
    }

    [Authorize(Roles = "Patient")]
    [HttpGet("patient")]
    public async Task<IActionResult> GetForPatient()
    {
        var userId = int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier) 
            ?? User.FindFirstValue("sub") 
            ?? throw new Exception("User ID not found in token")
        );

        var patient = await _context.Patients.FirstOrDefaultAsync(p => p.UserId == userId);
        if (patient == null)
            return NotFound("Patient not found");

        return Ok(await _service.GetByPatientIdAsync(patient.Id));
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet("by-date")]
    public async Task<IActionResult> GetByDate([FromQuery] DateTime date)
        => Ok(await _service.GetByDateAsync(date));
}