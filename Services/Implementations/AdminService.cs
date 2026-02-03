using HospitalSystem.Data;
using HospitalSystem.Domain.Entities;
using HospitalSystem.Domain.Enums;
using HospitalSystem.DTOs.Admin;
using HospitalSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

public class AdminService : IAdminService
{
    private readonly HospitalDbContext _context;

    public AdminService(HospitalDbContext context)
    {
        _context = context;
    }

    public async Task CreateDoctorAsync(CreateStaffDto dto)
    {
        if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            throw new Exception("User already exists");

        var user = new User
        {
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role = UserRole.Doctor
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var doctor = new Doctor
        {
            UserId = user.Id,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Specialization = dto.Specialization!
        };

        _context.Doctors.Add(doctor);
        await _context.SaveChangesAsync();
    }

    public async Task CreateLabTechnicianAsync(CreateStaffDto dto)
    {
        if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            throw new Exception("User already exists");

        var user = new User
        {
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role = UserRole.LabTechnician
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var lab = new LabTechnician
        {
            UserId = user.Id,
            FirstName = dto.FirstName,
            LastName = dto.LastName
        };

        _context.LabTechnicians.Add(lab);
        await _context.SaveChangesAsync();
    }
}