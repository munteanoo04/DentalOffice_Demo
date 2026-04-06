using DentalClinic.Web.Auth;
using DentalClinic.Web.Components;
using DentalClinic.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Named client "api" — matches AuthService.CreateClient("api")
builder.Services.AddHttpClient("api", client =>
    client.BaseAddress = new Uri("https://localhost:7173/"));

builder.Services.AddSingleton<AuthService>();
builder.Services.AddScoped<PatientService>();
builder.Services.AddScoped<DoctorService>();
builder.Services.AddScoped<AppointmentService>();
builder.Services.AddScoped<ProcedureService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();