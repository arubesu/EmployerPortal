using EmployerPortal.Data;
using EmployerPortal.Extensions;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<EmployerPortalDbContext>(options =>
    options.UseSqlite("Data Source=employerportal.db"));
builder.Services.AddControllers();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/app-.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Logging.AddSerilog();

var app = builder.Build();

app.Services.MigrateDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.Services.SeedDatabase();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();