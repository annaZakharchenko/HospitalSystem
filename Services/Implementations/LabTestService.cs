using HospitalSystem.Data;
using HospitalSystem.DTOs.LabTest;
using HospitalSystem.Domain.Entities;
using HospitalSystem.Domain.Enums;
using HospitalSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Services.Implementations;

public class LabTestService : ILabTestService
{
    private readonly HospitalDbContext _context;

    public LabTestService(HospitalDbContext context)
    {
        _context = context;
    }

    public async Task<LabTestDto> CreateAsync(CreateLabTestDto dto)
    {
        var test = new LabTest
        {
            PatientId = dto.PatientId,
            DoctorId = dto.DoctorId,
            MedicalRecordId = dto.MedicalRecordId,
            TestType = dto.TestType,
            Status = LabTestStatus.Ordered
        };

        _context.LabTests.Add(test);
        await _context.SaveChangesAsync();

        return await GetByIdAsync(test.Id);
    }

    public async Task<LabTestDto> AddResultAsync(AddLabTestResultDto dto)
    {
        var test = await _context.LabTests
            .FirstOrDefaultAsync(t => t.Id == dto.LabTestId);

        if (test == null)
            throw new Exception("Lab test not found");

        if (test.Status == LabTestStatus.Completed)
            throw new Exception("Test already completed");

        var result = new LabTestResult
        {
            LabTestId = dto.LabTestId,
            LabTechnicianId = dto.LabTechnicianId,
            Result = dto.Result
        };

        test.Status = LabTestStatus.Completed;

        _context.LabTestResults.Add(result);
        await _context.SaveChangesAsync();

        return await GetByIdAsync(test.Id);
    }

    public async Task<LabTestDto> GetByIdAsync(int id)
    {
        var test = await _context.LabTests
            .Include(t => t.Doctor)
            .Include(t => t.MedicalRecord).ThenInclude(m => m.Patient)
            .Include(t => t.Result).ThenInclude(r => r!.LabTechnician)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (test == null)
            throw new Exception("Lab test not found");

        return MapToDto(test);
    }

    public async Task<IEnumerable<LabTestDto>> GetByPatientIdAsync(int patientId)
    {
        var tests = await _context.LabTests
            .Include(t => t.Doctor)
            .Include(t => t.MedicalRecord).ThenInclude(m => m.Patient)
            .Include(t => t.Result).ThenInclude(r => r!.LabTechnician)
            .Where(t => t.PatientId == patientId)
            .ToListAsync();

        return tests.Select(MapToDto);
    }

    public async Task<IEnumerable<LabTestDto>> SearchByPatientNameAsync(string patientName)
    {
        var tests = await _context.LabTests
            .Include(t => t.Doctor)
            .Include(t => t.MedicalRecord).ThenInclude(m => m.Patient)
            .Include(t => t.Result).ThenInclude(r => r!.LabTechnician)
            .Where(t => 
                (t.MedicalRecord.Patient.FirstName + " " + t.MedicalRecord.Patient.LastName)
                .Contains(patientName))
            .ToListAsync();

        return tests.Select(MapToDto);
    }

    private LabTestDto MapToDto(LabTest test)
    {
        return new LabTestDto
        {
            Id = test.Id,
            
            PatientId = test.PatientId,
            PatientFullName = $"{test.MedicalRecord.Patient.FirstName} {test.MedicalRecord.Patient.LastName}",
            
            DoctorId = test.DoctorId,
            DoctorFullName = $"{test.Doctor.FirstName} {test.Doctor.LastName}",
            
            TestType = test.TestType,
            Status = test.Status,
            CreatedAt = test.CreatedAt,
            
            LabTechnicianId = test.Result?.LabTechnicianId,
            LabTechnicianFullName = test.Result != null 
                ? $"{test.Result.LabTechnician.FirstName} {test.Result.LabTechnician.LastName}"
                : null,
            ResultValue = test.Result?.Result,
            ResultDate = test.Result?.CreatedAt
        };
    }
}