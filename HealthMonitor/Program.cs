using Radzen;
using WeatherApi;
using Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddRadzenComponents();

var weatherApiBaseUrl = builder.Configuration["WeatherApiBaseUrl"];

if (!string.IsNullOrEmpty(weatherApiBaseUrl))
{
    builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(weatherApiBaseUrl) });
}

builder.Services.AddScoped<WeatherService>();
builder.Services.AddScoped<TimeLineController>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
