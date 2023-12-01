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
        private readonly IGenericRepository<Panel> _genericRepositoryPanels;
        private readonly IGenericRepository<Location> _genericRepositoryLocations;
        private readonly IGenericRepository<Advertisement> _genericRepositoryAdvertisements;
        private readonly IGenericRepository<AdCampaign> _genericRepositoryAdCampaigns;
        private readonly IGenericRepository<CampaignAdvertisement> _genericRepositoryCampaignAdvertisements;
        private readonly IGenericRepository<IoTDevice> _genericRepositoryIoTDevices;
        private readonly IGenericRepository<Queue> _genericRepositoryQueues;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public Seeder(ApplicationDbContext context,
            IGenericRepository<Panel> genericRepositoryPanels, 
            IGenericRepository<Location> genericRepositoryLocations, 
            IGenericRepository<Advertisement> genericRepositoryAdvertisements, 
            IGenericRepository<AdCampaign> genericRepositoryAdCampaigns, 
            IGenericRepository<CampaignAdvertisement> genericRepositoryCampaignAdvertisements, 
            IGenericRepository<IoTDevice> genericRepositoryIoTDevices, 
            IGenericRepository<Queue> genericRepositoryQueues, 
            UserManager<User> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _genericRepositoryPanels = genericRepositoryPanels;
            _genericRepositoryLocations = genericRepositoryLocations;
            _genericRepositoryAdvertisements = genericRepositoryAdvertisements;
            _genericRepositoryAdCampaigns = genericRepositoryAdCampaigns;
            _genericRepositoryCampaignAdvertisements = genericRepositoryCampaignAdvertisements;
            _genericRepositoryIoTDevices = genericRepositoryIoTDevices;
            _genericRepositoryQueues = genericRepositoryQueues;
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

                    /*result = await _userManager.AddClaimsAsync(
                        user,
                        new Claim[]
                        {
                            new Claim(JwtClaimTypes.GivenName, user.FirstName),
                            new Claim(JwtClaimTypes.FamilyName, user.LastName),
                            new Claim(JwtClaimTypes.Email, user.Email)
                        });*/

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
            var location = await _genericRepositoryLocations.GetAllAsync();
            if (location.Count() == 0)
            {
                //var newLocations = new List<Location>();
                var newLocations = (new Faker<Location>()
                    .RuleFor(l => l.Country, f => f.Random.String2(10))
                    .RuleFor(l => l.City, f => f.Random.String2(10))
                    .RuleFor(l => l.Street, f => f.Random.String2(10))
                    .RuleFor(l => l.StreetType, f => f.Random.String2(10))
                    .RuleFor(l => l.BuildingNumber, f => f.Random.String2(10))
                    .Generate(5)).ToList();

                await _genericRepositoryLocations.AddManyAsync(newLocations);
                await _context.SaveChangesAsync();
            }   
            var panels = await _genericRepositoryPanels.GetAllAsync();
            if (panels.Count() == 0) 
            {
                //var newPanels = new List<Panel>();
                var locationId = (await _genericRepositoryLocations.GetAllAsync()).First().Id;
                var newPanels = new Faker<Panel>()
                    .RuleFor(p => p.Height, f => f.Random.Double(0, 100))
                    .RuleFor(p => p.Width, f => f.Random.Double(0, 100))
                    .RuleFor(p => p.Status, f => f.Random.String2(10))
                    .RuleFor(p => p.Latitude, f => f.Random.Decimal(0, 100))
                    .RuleFor(p => p.Longitude, f => f.Random.Decimal(0, 100))
                    .RuleFor(p => p.LocationId, locationId)
                    .RuleFor(p => p.UserId, userId)
                    .Generate(5).ToList();

                await _genericRepositoryPanels.AddManyAsync(newPanels);
                await _context.SaveChangesAsync();
            }
            var iotDevices = await _genericRepositoryIoTDevices.GetAllAsync();
            if (iotDevices.Count() == 0)
            {
                var panelId = (await _genericRepositoryPanels.GetAllAsync()).First().Id;
                var newIoTDevices = new Faker<IoTDevice>()
                    .RuleFor(i => i.Name, f => f.Random.String2(10))
                    .RuleFor(i => i.Status, f => f.Random.String2(10))
                    .RuleFor(i => i.PanelId, panelId)
                    .Generate(5).ToList();
                await _genericRepositoryIoTDevices.AddManyAsync(newIoTDevices);
                await _context.SaveChangesAsync();
            }
            var advertisements = await _genericRepositoryAdvertisements.GetAllAsync();
            if (advertisements.Count() == 0)
            {

                var newAdvertisements = new Faker<Advertisement>()
                    .RuleFor(a => a.Title, f => f.Random.String2(10))
                    .RuleFor(a => a.Type, f => f.Random.String2(10))
                    .RuleFor(a => a.File, File.ReadAllBytes(@"C:\Users\Admin\OneDrive\Рабочий стол\Скріни\Wow.jpg"))
                    .RuleFor(a => a.UserId, userId)
                    .Generate(5).ToList();
                await _genericRepositoryAdvertisements.AddManyAsync(newAdvertisements);
                await _context.SaveChangesAsync();
            }
            var queues = await _genericRepositoryQueues.GetAllAsync();
            if (queues.Count() == 0)
            {
                var panelId = (await _genericRepositoryPanels.GetAllAsync()).First().Id;
                var advertisementId = (await _genericRepositoryAdvertisements.GetAllAsync()).First().Id;
                var newQueues = new Faker<Queue>()
                    .RuleFor(q => q.AdvertisementId, advertisementId)
                    .RuleFor(q => q.PanelId, panelId)
                    .RuleFor(q => q.DisplayOrder, f => f.Random.Int(10))
                    .Generate(5).ToList();
                await _genericRepositoryQueues.AddManyAsync(newQueues);
                await _context.SaveChangesAsync();
            }
            var adCampaigns = await _genericRepositoryAdCampaigns.GetAllAsync();
            if (adCampaigns.Count() == 0)
            {
                var newAdCampaigns = new Faker<AdCampaign>()
                    .RuleFor(a => a.Status, f => f.Random.String2(10))
                    .RuleFor(a => a.StartDate, f => f.Date.Past())
                    .RuleFor(a => a.EndDate, f => f.Date.Future())
                    .RuleFor(a => a.TargetedViews, f => f.Random.Int(10000, 100000))
                    .RuleFor(a => a.UserId, userId)
                    .Generate(5).ToList();
                await _genericRepositoryAdCampaigns.AddManyAsync(newAdCampaigns);
                await _context.SaveChangesAsync();
            }
            var camapaignAdvertisements = await _genericRepositoryCampaignAdvertisements.GetAllAsync();
            if (camapaignAdvertisements.Count() == 0)
            {
                var adCampaignId = (await _genericRepositoryAdCampaigns.GetAllAsync()).First().Id;
                var advertisementId = (await _genericRepositoryAdvertisements.GetAllAsync()).First().Id;
                var newCampaignAdvertisements = new Faker<CampaignAdvertisement>()
                    .RuleFor(c => c.AdvertisementId, advertisementId)
                    .RuleFor(c => c.AdCampaignId, adCampaignId)
                    .RuleFor(c => c.Views, f => f.Random.Int(10000, 100000))
                    .RuleFor(c => c.DisplayedTimes, f => f.Random.Int(2, 20))
                    .Generate(5).ToList();
                await _genericRepositoryCampaignAdvertisements.AddManyAsync(newCampaignAdvertisements);
                await _context.SaveChangesAsync();
            }
        }
    }
}
