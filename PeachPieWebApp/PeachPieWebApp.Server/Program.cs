using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Peachpie.AspNetCore.Web;
using PeachPieWebApp.Server;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddDistributedMemoryCache();

// Register in Program.cs:
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<PhpExecutor>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
});

// Add services to the container
builder.Services
    .AddPhp(options =>
    {
       // options.ScriptAssemblyName = "PhpLib"; // Must match your PHP project name
    })
    .AddControllers();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<PhpExecutor>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();


app.UseCors("AllowAll"); // Apply CORS policy

app.UseSession();
app.UsePhp(); // Enable PHP middleware
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();

app.Run();