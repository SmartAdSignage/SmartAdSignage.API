using Autofac;
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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<ISeeder, Seeder>();
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddAuthorization();

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
builder.Services.AddScoped<ICampaignAdvertisementService, CampaignAdvertisementService>();
builder.Services.AddScoped<IQueueService, QueueService>();
builder.Services.AddScoped<ILoggerService, LoggerService>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.ConfigureSwagger();
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.ConfigureMapping();
builder.Services.ConfigureCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseHttpsRedirection();

app.UseCors("EnableCORS");
app.ConfigureExceptionHandler(app.Services.CreateScope().ServiceProvider.GetRequiredService<ILoggerService>());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<ISeeder>();
    await seeder.EnsureSeedDataAsync(scope.ServiceProvider);
}

app.Run();
