using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UserService.Contexts;
using UserService.Core;
using UserService.Entities;
using UserService.Profiles;
using UserService.Repositories;
using UserService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add AutoMapper
builder.Services.AddAutoMapper(new Assembly[]
{
    Assembly.GetAssembly(type: typeof(UserProfile))
});

// Register Services and Repositories to IOC Container
builder.Services.AddScoped<IApplicationConfiguration, ApplicationConfiguration>();
builder.Services.AddScoped<IUserService, UserService.Services.UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Add Db Context
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

// Add Identify Configuration
builder.Services.AddIdentity<User, IdentityRole<int>>(option =>
    {
        option.Password.RequireDigit = true;
        option.Lockout.MaxFailedAccessAttempts = 5;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();