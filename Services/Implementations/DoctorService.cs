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