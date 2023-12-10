using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SmartAdSignage.Core.Models;
using SmartAdSignage.Repository.Data;
using SmartAdSignage.Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Repository
{
    public class Seeder : ISeeder
    {
        public readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public Seeder(ApplicationDbContext context,
            IUnitOfWork unitOfWork,
            UserManager<User> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task EnsureSeedDataAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var aspNetCoreIdentityDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await aspNetCoreIdentityDbContext.Database.MigrateAsync();
            await SeedUsersAndRolesAsync(serviceProvider);
            await SeedDataAsync(serviceProvider);
        }

        private async Task SeedUsersAndRolesAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var countUsers = _userManager.Users.Count();

            if (countUsers == 0)
            {
                List<string> userNames = new() { "user@gmail.com", "admin@gmail.com" };

                foreach (var username in userNames)
                {
                    if (await _roleManager.FindByNameAsync("User") == null)
                        await _roleManager.CreateAsync(new IdentityRole("User"));
                    else
                        await _roleManager.CreateAsync(new IdentityRole("Admin"));

                    var user = await _userManager.FindByNameAsync(username);

                    if (user != null)
                    {
                        continue;
                    }

                    user = new Faker<User>()
                        .RuleFor(u => u.UserName, username)
                        .RuleFor(u => u.FirstName, f => f.Person.FirstName)
                        .RuleFor(u => u.LastName, f => f.Person.LastName)
                        .RuleFor(u => u.CompanyName, f => f.Company.CompanyName())
                        .RuleFor(u => u.Email, username)
                        .RuleFor(u => u.EmailConfirmed, true).Generate();
                    var result = await _userManager.CreateAsync(user, "Pass123$");

                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    if (user.Email.Contains("user"))
                        result = await _userManager.AddToRoleAsync(user, "User");
                    else
                        result = await _userManager.AddToRoleAsync(user, "Admin");

                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                }
            }
        }

        private async Task SeedDataAsync(IServiceProvider serviceProvider) 
        {
            var userId = _userManager.Users.First().Id;
            var location = await _unitOfWork.Locations.GetAllAsync();
            if (!location.Any())
            {
                var newLocations = (new Faker<Location>()
                    .RuleFor(l => l.Country, f => f.Address.Country())
                    .RuleFor(l => l.City, f => f.Address.City())
                    .RuleFor(l => l.Street, f => f.Address.StreetAddress())
                    .RuleFor(l => l.StreetType, "Street")
                    .RuleFor(l => l.BuildingNumber, f => f.Random.Int(10).ToString())
                    .Generate(5)).ToList();

                await _unitOfWork.Locations.AddManyAsync(newLocations);
                await _context.SaveChangesAsync();
            }   
            var panels = await _unitOfWork.Panels.GetAllAsync();
            if (!panels.Any()) 
            {
                var locationId = (await _unitOfWork.Locations.GetAllAsync()).First().Id;
                var newPanels = new Faker<Panel>()
                    .RuleFor(p => p.Height, f => f.Random.Double(3, 15))
                    .RuleFor(p => p.Width, f => f.Random.Double(2, 8))
                    .RuleFor(p => p.Brightness, f => f.Random.Double(10, 10000))
                    .RuleFor(p => p.Status, "Active")
                    .RuleFor(p => p.Latitude, f => f.Random.Decimal(0, 100))
                    .RuleFor(p => p.Longitude, f => f.Random.Decimal(0, 100))
                    .RuleFor(p => p.LocationId, locationId)
                    .RuleFor(p => p.UserId, userId)
                    .Generate(5).ToList();

                await _unitOfWork.Panels.AddManyAsync(newPanels);
                await _context.SaveChangesAsync();
            }
            var iotDevices = await _unitOfWork.IoTDevices.GetAllAsync();
            if (!iotDevices.Any())
            {
                panels = await _unitOfWork.Panels.GetAllAsync();
                var panelId = (await _unitOfWork.Panels.GetAllAsync()).First().Id;
                var newIoTDevices = panels.Select(panel => new Faker<IoTDevice>()
                    .RuleFor(i => i.Name, "Lux Meter")
                    .RuleFor(i => i.Status, "Active")
                    .RuleFor(i => i.PanelId, panel.Id)
                    .Generate()).ToList();
                await _unitOfWork.IoTDevices.AddManyAsync(newIoTDevices);
                await _context.SaveChangesAsync();
            }
            var advertisements = await _unitOfWork.Advertisements.GetAllAsync();
            if (!advertisements.Any())
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Images", "Food.png");
                var file = File.ReadAllBytes(path);
                var newAdvertisements = new Faker<Advertisement>()
                    .RuleFor(a => a.Title, f => f.Random.String2(10))
                    .RuleFor(a => a.Type, f => f.Random.String2(10))
                    .RuleFor(a => a.File, file)
                    .RuleFor(a => a.UserId, userId)
                    .Generate(5).ToList();
                await _unitOfWork.Advertisements.AddManyAsync(newAdvertisements);
                await _context.SaveChangesAsync();
            }
            var queues = await _unitOfWork.Queues.GetAllAsync();
            if (!queues.Any())
            {
                var panelId = (await _unitOfWork.Panels.GetAllAsync()).First().Id;
                var advertisementId = (await _unitOfWork.Advertisements.GetAllAsync()).First().Id;
                var newQueues = new Faker<Queue>()
                    .RuleFor(q => q.AdvertisementId, advertisementId)
                    .RuleFor(q => q.PanelId, panelId)
                    .RuleFor(q => q.DisplayOrder, f => f.Random.Int(1, 100))
                    .Generate(5).ToList();
                await _unitOfWork.Queues.AddManyAsync(newQueues);
                await _context.SaveChangesAsync();
            }
            var adCampaigns = await _unitOfWork.AdCampaigns.GetAllAsync();
            if (!adCampaigns.Any())
            {
                panels = await _unitOfWork.Panels.GetAllAsync();
                var newAdCampaigns = new Faker<AdCampaign>()
                    .RuleFor(a => a.Status, f => f.Random.String2(10))
                    .RuleFor(a => a.StartDate, f => f.Date.Past())
                    .RuleFor(a => a.EndDate, f => f.Date.Future())
                    .RuleFor(a => a.TargetedViews, f => f.Random.Int(10000, 100000))
                    .RuleFor(a => a.UserId, userId)
                    .RuleFor(a => a.Panels, panels)
                    .Generate(5).ToList();
                await _unitOfWork.AdCampaigns.AddManyAsync(newAdCampaigns);
                await _context.SaveChangesAsync();
            }
            var camapaignAdvertisements = await _unitOfWork.CampaignAdvertisements.GetAllAsync();
            if (!camapaignAdvertisements.Any())
            {
                var adCampaignId = (await _unitOfWork.AdCampaigns.GetAllAsync()).First().Id;
                var advertisementId = (await _unitOfWork.Advertisements.GetAllAsync()).First().Id;
                var newCampaignAdvertisements = new Faker<CampaignAdvertisement>()
                    .RuleFor(c => c.AdvertisementId, advertisementId)
                    .RuleFor(c => c.AdCampaignId, adCampaignId)
                    .RuleFor(c => c.Views, f => f.Random.Int(1000, 10000))
                    .RuleFor(c => c.DisplayedTimes, f => f.Random.Int(2, 20))
                    .Generate(5).ToList();
                await _unitOfWork.CampaignAdvertisements.AddManyAsync(newCampaignAdvertisements);
                await _context.SaveChangesAsync();
            }
        }
    }
}
