using DentalClinic.Application.Contracts.Interfaces;
using DentalClinic.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DentalClinic.Infrastructure.Services;

public class AppointmentReminderService
{
    private readonly AppDbContext _ctx;
    private readonly IEmailService _email;
    private readonly ILogger<AppointmentReminderService> _logger;

    public AppointmentReminderService(
        AppDbContext ctx,
        IEmailService email,
        ILogger<AppointmentReminderService> logger)
    {
        _ctx = ctx;
        _email = email;
        _logger = logger;
    }

    public async Task SendRemindersAsync()
    {
        var tomorrow = DateTime.UtcNow.Date.AddDays(1);
        var dayAfter = tomorrow.AddDays(1);

        var appointments = await _ctx.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .Include(a => a.Procedure)
            .Where(a =>
                a.ScheduledAt >= tomorrow &&
                a.ScheduledAt < dayAfter &&
                a.Status == DentalClinic.Domain.Enums.AppointmentStatus.Scheduled)
            .ToListAsync();

        foreach (var appt in appointments)
        {
            try
            {
                await _email.SendReminderEmailAsync(
                    appt.Patient.Email,
                    $"{appt.Patient.FirstName} {appt.Patient.LastName}",
                    $"{appt.Doctor.FirstName}  {appt.Doctor.LastName}",
                    appt.Procedure.Name,
                    appt.ScheduledAt);

                _logger.LogInformation(
                    "Reminder sent for appointment {Id}", appt.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "Failed to send reminder for appointment {Id}", appt.Id);
            }
        }
    }
}