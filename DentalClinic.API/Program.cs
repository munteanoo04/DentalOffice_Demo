using DentalClinic.Application;
using DentalClinic.Domain.Entities;
using DentalClinic.Domain.Enums;
using DentalClinic.Infrastructure;
using DentalClinic.Infrastructure.DbContext;
using DentalClinic.Infrastructure.Services;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opts =>
    {
        opts.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DentalClinic API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter: Bearer {your token}"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id   = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddCors(opts =>
    opts.AddPolicy("BlazorPolicy", policy =>
        policy.WithOrigins(
            "https://localhost:7261",
            "http://localhost:7261")
              .AllowAnyHeader()
              .AllowAnyMethod()));

// Hangfire
builder.Services.AddHangfire(config =>
    config.UseSqlServerStorage(
        builder.Configuration.GetConnectionString("Default")));
builder.Services.AddHangfireServer();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();

    if (!db.Users.Any(u => u.Email == "admin@clinic.com"))
    {
        db.Users.Add(User.Create(
            "Admin", "Clinic",
            "admin@clinic.com", "000000000",
            BCrypt.Net.BCrypt.HashPassword("secret"),
            "Admin"));
        db.SaveChanges();
    }

    if (!db.Procedures.Any())
    {
        db.Procedures.AddRange(
            Procedure.Create("Procedure A", "Routine dental checkup",
                100, 30, ProcedureCategory.Checkup),
            Procedure.Create("Procedure B", "Tooth filling",
                200, 45, ProcedureCategory.Filling),
            Procedure.Create("Procedure C", "Tooth extraction",
                300, 60, ProcedureCategory.Extraction),
            Procedure.Create("Procedure D", "Root canal treatment",
                500, 90, ProcedureCategory.RootCanal),
            Procedure.Create("Procedure E", "Teeth whitening",
                400, 60, ProcedureCategory.Whitening)
        );
        db.SaveChanges();
    }

    if (!db.Doctors.Any())
    {
        db.Doctors.AddRange(
            Doctor.Create("Ion", "Popescu", "ion@clinic.com", "069100001", "General Dentist"),
            Doctor.Create("Maria", "Ionescu", "maria@clinic.com", "069100002", "Orthodontist"),
            Doctor.Create("Andrei", "Constantin", "andrei@clinic.com", "069100003", "Oral Surgeon"),
            Doctor.Create("Elena", "Gheorghe", "elena@clinic.com", "069100004", "Endodontist"),
            Doctor.Create("Mihai", "Stoica", "mihai@clinic.com", "069100005", "Cosmetic Dentist")
        );
        db.SaveChanges();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DentalClinic API v1"));
}

app.UseCors("BlazorPolicy");
app.UseAuthentication();
app.UseAuthorization();

// Hangfire dashboard (admin only in production)
app.UseHangfireDashboard("/hangfire");

// Schedule daily reminder at 9am
RecurringJob.AddOrUpdate<AppointmentReminderService>(
    "send-appointment-reminders",
    service => service.SendRemindersAsync(),
    "0 9 * * *"); // every day at 9:00 AM

app.MapControllers();
app.Run();