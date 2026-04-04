using DentalClinic.Web.Auth;
using DentalClinic.Web.Components;
using DentalClinic.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient("auth", client =>
    client.BaseAddress = new Uri("https://localhost:7173/"));

builder.Services.AddHttpClient<PatientService>(client =>
    client.BaseAddress = new Uri("https://localhost:7173/"));

builder.Services.AddHttpClient<DoctorService>(client =>
    client.BaseAddress = new Uri("https://localhost:7173/"));

builder.Services.AddHttpClient<AppointmentService>(client =>
    client.BaseAddress = new Uri("https://localhost:7173/"));

builder.Services.AddSingleton<AuthService>();

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