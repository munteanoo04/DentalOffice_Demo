namespace DentalClinic.Application.Contracts.Interfaces;

public interface IEmailService
{
    Task SendConfirmationEmailAsync(
        string toEmail,
        string firstName,
        CancellationToken ct = default);

    Task SendReminderEmailAsync(
        string toEmail,
        string patientName,
        string doctorName,
        string procedureName,
        DateTime scheduledAt,
        CancellationToken ct = default);
}