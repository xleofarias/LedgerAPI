using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using LedgerAPI.API;


var directory = Directory.GetCurrentDirectory();
var directoryFather = Path.GetDirectoryName(directory) 
    ?? throw new InvalidOperationException("Could not determine the parent directory.");
var directoryEnv = Path.Combine(directoryFather, ".env");

// Load environment variables from .env file
Env.Load(directoryEnv);

Console.WriteLine(directoryEnv);

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add DbContext
var connectionString = Environment.GetEnvironmentVariable("ConnectionSqlServerDev");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString ?? throw new InvalidOperationException("Connection string not found.")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
