using HospitalSystem.Data;
using HospitalSystem.DTOs.Doctor;
using HospitalSystem.Domain.Entities;
using HospitalSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Services.Implementations;

public class DoctorService : IDoctorService
{
    private readonly HospitalDbContext _context;

    public DoctorService(HospitalDbContext context)
    {
        _context = context;
    }

    public async Task<DoctorDto> CreateAsync(CreateDoctorDto dto)
    {
        var doctor = new Doctor
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Specialization = dto.Specialization
        };

        _context.Doctors.Add(doctor);
        await _context.SaveChangesAsync();

        return new DoctorDto
        {
            Id = doctor.Id,
            FullName = $"{doctor.FirstName} {doctor.LastName}",
            Specialization = doctor.Specialization
        };
    }

    public async Task<IEnumerable<DoctorDto>> GetAllAsync()
    {
        return await _context.Doctors
            .Select(d => new DoctorDto
            {
                Id = d.Id,
                FullName = d.FirstName + " " + d.LastName,
                Specialization = d.Specialization
            })
            .ToListAsync();
    }
}