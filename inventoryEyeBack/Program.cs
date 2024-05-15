using System.Text;
using inventoryeyeback;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Session;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

builder.WebHost.UseUrls("https://localhost:5001");

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

var connStr = builder.Configuration.GetConnectionString("MySqlConnectionString");

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("MySqlConnectionString"),
        sqlOptions => sqlOptions.CommandTimeout((int)TimeSpan.FromMinutes(5).TotalSeconds)));

// Configure in-memory cache provider
builder.Services.AddDistributedMemoryCache();

// Configure session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Adjust the timeout as needed
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseAuthorization();

// Use session middleware
app.UseSession();

app.MapControllers();

app.Run();