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

    public async Task<AppointmentDto> CreateAsync(int patientId, CreateAppointmentDto dto)
    {
        var doctor = await _context.Doctors.FindAsync(dto.DoctorId);
        if (doctor == null)
            throw new Exception("Doctor not found");

        var patient = await _context.Patients.FindAsync(patientId);
        if (patient == null)
            throw new Exception("Patient not found");

        var appointment = new Appointment
        {
            PatientId = patientId,        
            DoctorId = dto.DoctorId,
            AppointmentDate = dto.AppointmentDate
        };

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();

        return new AppointmentDto
        {
            Id = appointment.Id,
            PatientName = patient.FirstName + " " + patient.LastName,
            DoctorName = doctor.FirstName + " " + doctor.LastName,
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
    
    public async Task<IEnumerable<AppointmentDto>> GetByDoctorIdAsync(int doctorId)
    {
        return await _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Where(a => a.DoctorId == doctorId)
            .OrderBy(a => a.AppointmentDate)
            .Select(a => new AppointmentDto
            {
                Id = a.Id,
                PatientName = a.Patient.FirstName + " " + a.Patient.LastName,
                DoctorName = a.Doctor.FirstName + " " + a.Doctor.LastName,
                AppointmentDate = a.AppointmentDate
            })
            .ToListAsync();
    }


    public async Task<IEnumerable<AppointmentDto>> GetByPatientIdAsync(int patientId)
    {
        return await _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Where(a => a.PatientId == patientId)
            .OrderBy(a => a.AppointmentDate)
            .Select(a => new AppointmentDto
            {
                Id = a.Id,
                PatientName = a.Patient.FirstName + " " + a.Patient.LastName,
                DoctorName = a.Doctor.FirstName + " " + a.Doctor.LastName,
                AppointmentDate = a.AppointmentDate
            })
            .ToListAsync();
    }

    
    public async Task<IEnumerable<AppointmentDto>> GetByDateAsync(DateTime date)
    {
        var dayStart = date.Date;
        var dayEnd = dayStart.AddDays(1);

        return await _context.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Where(a => a.AppointmentDate >= dayStart &&
                        a.AppointmentDate < dayEnd)
            .OrderBy(a => a.AppointmentDate)
            .Select(a => new AppointmentDto
            {
                Id = a.Id,
                PatientName = a.Patient.FirstName + " " + a.Patient.LastName,
                DoctorName = a.Doctor.FirstName + " " + a.Doctor.LastName,
                AppointmentDate = a.AppointmentDate
            })
            .ToListAsync();
    }


    private static AppointmentDto MapToDto(Appointment a) => new()
    {
        Id = a.Id,
        PatientName = a.Patient.FirstName + " " + a.Patient.LastName,
        DoctorName = a.Doctor.FirstName + " " + a.Doctor.LastName,
        AppointmentDate = a.AppointmentDate
    };
}