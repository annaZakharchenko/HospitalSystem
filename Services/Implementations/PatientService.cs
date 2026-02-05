using HospitalSystem.Data;
using HospitalSystem.DTOs.Patient;
using HospitalSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Services.Implementations;

public class PatientService : IPatientService
{
    private readonly HospitalDbContext _context;

    public PatientService(HospitalDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PatientDto>> GetAllAsync()
    {
        return await _context.Patients
            .Include(p => p.User)
            .Select(p => new PatientDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Email = p.User.Email,
                DateOfBirth = p.DateOfBirth,
                Phone = p.Phone
            })
            .ToListAsync();
    }

    public async Task<PatientDto?> GetByIdAsync(int id)
    {
        var patient = await _context.Patients
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == id);
            
        if (patient == null) return null;

        return new PatientDto
        {
            Id = patient.Id,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            Email = patient.User.Email,
            DateOfBirth = patient.DateOfBirth,
            Phone = patient.Phone
        };
    }
    
    public async Task<PatientDto?> GetProfileAsync(int userId)
    {
        var patient = await _context.Patients
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.UserId == userId);
            
        if (patient == null) return null;

        return new PatientDto
        {
            Id = patient.Id,
            Email = patient.User.Email,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            DateOfBirth = patient.DateOfBirth,
            Phone = patient.Phone
        };
    }

    public async Task<PatientDto> UpdateProfileAsync(int userId, UpdatePatientDto dto)
    {
        var patient = await _context.Patients
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.UserId == userId);

        if (patient == null)
            throw new Exception("Patient not found");

        patient.FirstName = dto.FirstName;
        patient.LastName = dto.LastName;
        patient.DateOfBirth = dto.DateOfBirth;
        patient.Phone = dto.Phone;

        await _context.SaveChangesAsync();

        return new PatientDto
        {
            Id = patient.Id,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            Email = patient.User.Email,
            DateOfBirth = patient.DateOfBirth,
            Phone = patient.Phone
        };
    }
}