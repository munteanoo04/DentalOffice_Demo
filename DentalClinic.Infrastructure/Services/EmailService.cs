using DentalClinic.Application.Contracts.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace DentalClinic.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _config;
    public EmailService(IConfiguration config) => _config = config;

    private async Task SendAsync(
        string toEmail,
        string toName,
        string subject,
        string htmlBody,
        CancellationToken ct = default)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(
            _config["Email:FromName"],
            _config["Email:Username"]));
        message.To.Add(new MailboxAddress(toName, toEmail));
        message.Subject = subject;
        message.Body = new TextPart("html") { Text = htmlBody };

        using var client = new SmtpClient();
        await client.ConnectAsync(
            _config["Email:SmtpHost"],
            int.Parse(_config["Email:SmtpPort"]!),
            SecureSocketOptions.StartTls, ct);
        await client.AuthenticateAsync(
            _config["Email:Username"],
            _config["Email:Password"], ct);
        await client.SendAsync(message, ct);
        await client.DisconnectAsync(true, ct);
    }

    public async Task SendConfirmationEmailAsync(
        string toEmail,
        string firstName,
        CancellationToken ct = default)
    {
        var html = $"""
            <h2>Welcome to Dental Clinic, {firstName}! 🦷</h2>
            <p>Your account has been successfully created.</p>
            <p>You can now login and book appointments at our clinic.</p>
            <p>Thank you for choosing us!</p>
            <hr/>
            <small>Dental Clinic Team</small>
            """;

        await SendAsync(
            toEmail, firstName,
            "Welcome to Dental Clinic — Account Created",
            html, ct);
    }

    public async Task SendReminderEmailAsync(
        string toEmail,
        string patientName,
        string doctorName,
        string procedureName,
        DateTime scheduledAt,
        CancellationToken ct = default)
    {
        var html = $"""
            <h2>Appointment Reminder 🦷</h2>
            <p>Dear <strong>{patientName}</strong>,</p>
            <p>This is a reminder that you have an appointment tomorrow:</p>
            <ul>
                <li><strong>Procedure:</strong> {procedureName}</li>
                <li><strong>Doctor:</strong> {doctorName}</li>
                <li><strong>Date & Time:</strong>
                    {scheduledAt:dd MMM yyyy HH:mm}</li>
            </ul>
            <p>Please arrive 10 minutes early.</p>
            <hr/>
            <small>Dental Clinic Team</small>
            """;

        await SendAsync(
            toEmail, patientName,
            "Reminder: Your Appointment Tomorrow",
            html, ct);
    }
}