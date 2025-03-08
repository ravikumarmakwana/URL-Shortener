using Microsoft.EntityFrameworkCore;
using System.Reflection;
using URLService.AppStartup;
using URLService.Contexts;
using URLService.Core;
using URLService.Profiles;
using URLService.Repositories;
using URLService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IApplicationConfiguration, ApplicationConfiguration>();

AuthenticationConfigurator.Configure(builder.Services);

builder.Services.AddAutoMapper(new Assembly[]
{
    Assembly.GetAssembly(type: typeof(URLProfile))
});

builder.Services.AddScoped<IURLRepository, URLRepository>();
builder.Services.AddScoped<IAnalyticsRepository, AnalyticsRepository>();

builder.Services.AddScoped<IURLService, URLService.Services.URLService>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

builder.Services.AddScoped<IApplicationConfiguration, ApplicationConfiguration>();
builder.Services.AddScoped<IURLShortenerAlgorithm, URLShortenerAlgorithm>();

builder.Services.AddDbContext<ApplicationDbContext>(
    option => option.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
