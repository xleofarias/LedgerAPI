using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using LedgerAPI.API;

var currentDirectory = Environment.CurrentDirectory;
var env = ".env";

// Traverse up the directory tree to find the .env file
while(currentDirectory != null)
{
    string fullPath = Path.Combine(currentDirectory, env);
    if(File.Exists(fullPath))
    {
        // Load environment variables from .env file
        Env.Load(fullPath);
        break;
    }
    currentDirectory = Directory.GetParent(currentDirectory)?.FullName;
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add services to the container.
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

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/openapi/v1.json", "LedgerAPI API V1");
    });
}

app.MapControllers();

app.UseHttpsRedirection();

app.Run();
