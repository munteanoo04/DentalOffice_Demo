using DentalClinic.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DentalClinic.Infrastructure.Data  // ← folderul trebuie să fie "Data"
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>  // ← IdentityDbContext, nu DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<Doctor> Doctors => Set<Doctor>();
        public DbSet<Appointment> Appointments => Set<Appointment>();
        public DbSet<Procedure> Procedures => Set<Procedure>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);  // ← important pentru Identity

            // ... restul configurărilor tale rămân la fel
        }
    }
}