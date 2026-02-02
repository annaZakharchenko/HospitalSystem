using HospitalSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HospitalSystem.Data;

public class HospitalDbContext : DbContext
{
    public HospitalDbContext(DbContextOptions<HospitalDbContext> options)
        : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<LabTechnician> LabTechnicians => Set<LabTechnician>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<MedicalRecord> MedicalRecords => Set<MedicalRecord>();
    public DbSet<MedicalRecordEntry> MedicalRecordEntries => Set<MedicalRecordEntry>();
    public DbSet<LabTest> LabTests => Set<LabTest>();
    public DbSet<LabTestResult> LabTestResults => Set<LabTestResult>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<Patient>()
            .HasOne(p => p.MedicalRecord)
            .WithOne(m => m.Patient)
            .HasForeignKey<MedicalRecord>(m => m.PatientId);

        modelBuilder.Entity<LabTest>()
            .HasOne(l => l.Result)
            .WithOne(r => r.LabTest)
            .HasForeignKey<LabTestResult>(r => r.LabTestId);
    }
}