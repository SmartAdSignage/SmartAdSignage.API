using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SmartAdSignage.API.Extensions;
using SmartAdSignage.Core.Models;
using SmartAdSignage.Repository;
using SmartAdSignage.Repository.Data;
using SmartAdSignage.Repository.Repositories.Implementations;
using SmartAdSignage.Repository.Repositories.Interfaces;
using SmartAdSignage.Services.Services.Implementations;
using SmartAdSignage.Services.Services.Interfaces;
using System.Text;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<ISeeder, Seeder>();
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddAuthorization();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IGenericRepository<Advertisement>, GenericRepository<Advertisement>>();
builder.Services.AddTransient<IGenericRepository<AdCampaign>, GenericRepository<AdCampaign>>();
builder.Services.AddTransient<IGenericRepository<Panel>, GenericRepository<Panel>>();
builder.Services.AddTransient<IGenericRepository<Location>, GenericRepository<Location>>();
builder.Services.AddTransient<IGenericRepository<IoTDevice>, GenericRepository<IoTDevice>>();
builder.Services.AddTransient<IGenericRepository<CampaignAdvertisement>, GenericRepository<CampaignAdvertisement>>();
builder.Services.AddTransient<IGenericRepository<Queue>, GenericRepository<Queue>>();

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAdvertisementService, AdvertisementService>();
builder.Services.AddScoped<IPanelService, PanelService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IIoTDeviceService, IoTDeviceService>();
builder.Services.AddScoped<IAdCampaignService, AdCampaignService>();
builder.Services.AddScoped<ICampaignAdService, CampaignAdService>();
builder.Services.AddScoped<IQueueService, QueueService>();

builder.Services.ConfigureSwagger();
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.ConfigureMapping();
builder.Services.ConfigureCors();
builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();
app.UseCors("EnableCORS");
app.ConfigureExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<ISeeder>();
    await seeder.EnsureSeedDataAsync(scope.ServiceProvider);
}

app.Run();

await Log.CloseAndFlushAsync();