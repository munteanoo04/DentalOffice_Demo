using DentalClinic.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Infrastructure.DbContext;

public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<Procedure> Procedures => Set<Procedure>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Patient
        builder.Entity<Patient>(e =>
        {
            e.HasKey(p => p.Id);
            e.Property(p => p.Id).ValueGeneratedOnAdd();
            e.Property(p => p.FirstName).IsRequired().HasMaxLength(100);
            e.Property(p => p.LastName).IsRequired().HasMaxLength(100);
            e.Property(p => p.Email).IsRequired().HasMaxLength(150);
            e.Property(p => p.PhoneNumber).HasMaxLength(20);
            e.Ignore(p => p.Age);
        });

        // Doctor
        builder.Entity<Doctor>(e =>
        {
            e.HasKey(d => d.Id);
            e.Property(d => d.Id).ValueGeneratedOnAdd();
            e.Property(d => d.FirstName).IsRequired().HasMaxLength(100);
            e.Property(d => d.LastName).IsRequired().HasMaxLength(100);
            e.Property(d => d.Email).IsRequired().HasMaxLength(150);
            e.Property(d => d.PhoneNumber).HasMaxLength(20);
            e.Property(d => d.Specialization).IsRequired().HasMaxLength(100);
        });

        // Procedure
        builder.Entity<Procedure>(e =>
        {
            e.HasKey(p => p.Id);
            e.Property(p => p.Id).ValueGeneratedOnAdd();
            e.Property(p => p.Name).IsRequired().HasMaxLength(100);
            e.Property(p => p.Description).HasMaxLength(500);
            e.Property(p => p.Price).HasColumnType("decimal(18,2)");
            e.Property(p => p.Category).HasConversion<string>();
        });

        // Appointment
        builder.Entity<Appointment>(e =>
        {
            e.HasKey(a => a.Id);
            e.Property(a => a.Id).ValueGeneratedOnAdd();
            e.Property(a => a.TreatmentType).IsRequired().HasMaxLength(200);
            e.Property(a => a.Notes).HasMaxLength(1000);
            e.Property(a => a.Status).HasConversion<string>();

            e.HasOne(a => a.Patient)
             .WithMany(p => p.Appointments)
             .HasForeignKey(a => a.PatientId)
             .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(a => a.Doctor)
             .WithMany(d => d.Appointments)
             .HasForeignKey(a => a.DoctorId)
             .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(a => a.Procedure)
             .WithMany(p => p.Appointments)
             .HasForeignKey(a => a.ProcedureId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        // User
        builder.Entity<User>(e =>
        {
            e.HasKey(u => u.Id);
            e.Property(u => u.Id).ValueGeneratedOnAdd();
            e.Property(u => u.FirstName).IsRequired().HasMaxLength(100);
            e.Property(u => u.LastName).IsRequired().HasMaxLength(100);
            e.Property(u => u.Email).IsRequired().HasMaxLength(150);
            e.Property(u => u.PhoneNumber).HasMaxLength(20);
            e.Property(u => u.PasswordHash).IsRequired();
            e.Property(u => u.Role).IsRequired().HasMaxLength(20);
            e.HasIndex(u => u.Email).IsUnique();
        });
    }
}