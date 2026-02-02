using HospitalSystem.Data;
using HospitalSystem.DTOs;
using HospitalSystem.Domain.Entities;
using HospitalSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Services.Implementations;

public class AppointmentService : IAppointmentService
{
    private readonly HospitalDbContext _context;

    public AppointmentService(HospitalDbContext context)
    {
        _context = context;
    }

    public async Task<AppointmentDto> CreateAsync(CreateAppointmentDto dto)
    {
        var appointment = new Appointment
        {
            PatientId = dto.PatientId,
            DoctorId = dto.DoctorId,
            AppointmentDate = dto.AppointmentDate
        };

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();

        var patient = await _context.Patients.FindAsync(dto.PatientId);
        var doctor = await _context.Doctors.FindAsync(dto.DoctorId);

        return new AppointmentDto
        {
            Id = appointment.Id,
            PatientName = patient!.FirstName + " " + patient.LastName,
            DoctorName = doctor!.FirstName + " " + doctor.LastName,
            AppointmentDate = appointment.AppointmentDate
        };
    }

    public async Task<IEnumerable<AppointmentDto>> GetAllAsync()
    {
        return await _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Select(a => new AppointmentDto
            {
                Id = a.Id,
                PatientName = a.Patient.FirstName + " " + a.Patient.LastName,
                DoctorName = a.Doctor.FirstName + " " + a.Doctor.LastName,
                AppointmentDate = a.AppointmentDate
            })
            .ToListAsync();
    }
}