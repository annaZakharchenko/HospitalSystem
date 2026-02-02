using HospitalSystem.Data;
using HospitalSystem.DTOs.MedicalRecord;
using HospitalSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Services.Implementations;

public class MedicalRecordService : IMedicalRecordService
{
    private readonly HospitalDbContext _context;

    public MedicalRecordService(HospitalDbContext context)
    {
        _context = context;
    }

    public async Task<MedicalRecordDto> GetByPatientIdAsync(int patientId)
    {
        var record = await _context.MedicalRecords
            .Include(r => r.Entries)
            .FirstAsync(r => r.PatientId == patientId);

        return new MedicalRecordDto
        {
            Id = record.Id,
            PatientId = record.PatientId,
            Entries = record.Entries.Select(e => new MedicalRecordEntryDto
            {
                Date = e.CreatedAt,
                Description = e.Description
            }).ToList()
        };
    }
}