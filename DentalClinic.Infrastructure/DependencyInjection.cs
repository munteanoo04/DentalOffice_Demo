using DentalClinic.Application.Contracts.Interfaces;
using DentalClinic.Infrastructure.Data;
using DentalClinic.Infrastructure.Repository;
using DentalClinic.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;  
using Microsoft.Extensions.DependencyInjection;

namespace DentalClinic.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)  
    {
        services.AddDbContext<AppDbContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("Default")));  

        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<IDoctorRepository, DoctorRepository>();
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddScoped<IProcedureRepository, ProcedureRepository>();
        services.AddScoped<JwtTokenService>();  

        return services;
    }
}