using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartAdSignage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Repository.Data
{
    public class ApplicationDbContext: IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<AdCampaign> AdCampaigns { get; set; }

        public DbSet<Advertisement> Advertisements { get; set; }

        public DbSet<CampaignAdvertisement> CampaignAdvertisements { get; set; }

        public DbSet<Panel> Panels { get; set; }

        public DbSet<Queue> Queues { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<IoTDevice> IoTDevices { get; set; }

        /*protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().HasMany(u => u.Advertisements).WithOne(a => a.User).HasForeignKey(a => a.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<User>().HasMany(u => u.AdCampaigns).WithOne(a => a.User).HasForeignKey(a => a.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<AdCampaign>().HasMany(a => a.CampaignAdvertisements).WithOne(c => c.AdCampaign).HasForeignKey(c => c.AdCampaignId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<AdCampaign>().HasMany(a => a.Panels).WithOne(p => p.AdCampaign).HasForeignKey(p => p.AdCampaignId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Advertisement>().HasMany(a => a.CampaignAdvertisements).WithOne(c => c.Advertisement).HasForeignKey(c => c.AdvertisementId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Advertisement>().HasMany(a => a.Queues).WithOne(q => q.Advertisement).HasForeignKey(q => q.AdvertisementId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Panel>().HasMany(p => p.Queues).WithOne(q => q.Panel).HasForeignKey(q => q.PanelId).OnDelete(DeleteBehavior.Cascade);
        }*/
    }
}
