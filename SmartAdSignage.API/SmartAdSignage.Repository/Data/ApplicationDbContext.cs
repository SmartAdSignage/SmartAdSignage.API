using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartAdSignage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Advertisement>()
                .HasMany(a => a.Queues)
                .WithOne(q => q.Advertisement)
                .HasForeignKey(q => q.AdvertisementId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<AdCampaign>()
                .HasMany(a => a.CampaignAdvertisements)
                .WithOne(q => q.AdCampaign)
                .HasForeignKey(q => q.AdCampaignId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Panel>()
                .HasMany(a => a.Queues)
                .WithOne(q => q.Panel)
                .HasForeignKey(q => q.PanelId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Panel>()
                .HasMany(a => a.IoTDevices)
                .WithOne(q => q.Panel)
                .HasForeignKey(q => q.PanelId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Location>()
                .HasMany(a => a.Panels)
                .WithOne(q => q.Location)
                .HasForeignKey(q => q.LocationId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<User>()
                .HasMany(u => u.Panels)
                .WithOne(l => l.User)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<User>()
                .HasMany(u => u.AdCampaigns)
                .WithOne(l => l.User)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<User>()
                .HasMany(u => u.Advertisements)
                .WithOne(l => l.User)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Advertisement>()
                .HasMany(u => u.CampaignAdvertisements)
                .WithOne(l => l.Advertisement)
                .HasForeignKey(l => l.AdvertisementId)
                .OnDelete(DeleteBehavior.Cascade);

            /*builder.Entity<AdCampaign>()
                .HasMany(u => u.AdCampaignPanels)
                .WithOne(l => l.AdCampaign)
                .HasForeignKey(l => l.AdCampaignId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Panel>()
                .HasMany(u => u.AdCampaignPanels)
                .WithOne(l => l.Panel)
                .HasForeignKey(l => l.PanelId)
                .OnDelete(DeleteBehavior.Cascade);*/

            /*builder.Entity<AdCampaign>()
            .HasMany(opt => opt.Panels)
            .WithMany(x => x.AdCampaigns)
            .UsingEntity<Dictionary<string, object>>(
                "AdCampaignPanels",
                opt => opt.HasOne<AdCampaign>().WithMany().HasForeignKey("AccessActionId"),
                opt => opt.HasOne<Panel>().WithMany().HasForeignKey("RoleId"),
                opt =>
                {
                    opt.Property<int>("Id").UseIdentityColumn();
                    opt.HasKey("Id");
                    opt.ToTable("RoleAccessActions");
                }
            );*/

            /*builder.Entity<AdCampaign>()
                .HasMany(u => u.Panels)
                .WithMany(l => l.AdCampaigns);
            
            builder.Entity<Advertisement>()
                .HasOne(u => u.User)
                .WithMany(l => l.Advertisements)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<AdCampaign>()
                .HasOne(u => u.User)
                .WithMany(l => l.AdCampaigns)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CampaignAdvertisement>()
                .HasOne(u => u.Advertisement)
                .WithMany(l => l.CampaignAdvertisements)
                .HasForeignKey(l => l.AdvertisementId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CampaignAdvertisement>()
                .HasOne(u => u.AdCampaign)
                .WithMany(l => l.CampaignAdvertisements)
                .HasForeignKey(l => l.AdCampaignId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Queue>()
                .HasOne(u => u.Advertisement)
                .WithMany(l => l.Queues)
                .HasForeignKey(l => l.AdvertisementId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Queue>()
                .HasOne(u => u.Panel)
                .WithMany(l => l.Queues)
                .HasForeignKey(l => l.PanelId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Panel>()
                .HasOne(u => u.Location)
                .WithMany(l => l.Panels)
                .HasForeignKey(l => l.LocationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Panel>()
                .HasOne(u => u.User)
                .WithMany(l => l.Panels)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<IoTDevice>()
                .HasOne(u => u.Panel)
                .WithMany(l => l.IoTDevices)
                .HasForeignKey(l => l.PanelId)
                .OnDelete(DeleteBehavior.Cascade);



            builder.Entity<User>().HasMany(u => u.Advertisements).WithOne(a => a.User).HasForeignKey(a => a.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<User>().HasMany(u => u.AdCampaigns).WithOne(a => a.User).HasForeignKey(a => a.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<AdCampaign>().HasMany(a => a.CampaignAdvertisements).WithOne(c => c.AdCampaign).HasForeignKey(c => c.AdCampaignId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<AdCampaign>().HasMany(a => a.Panels).WithOne(p => p.AdCampaign).HasForeignKey(p => p.AdCampaignId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Advertisement>().HasMany(a => a.CampaignAdvertisements).WithOne(c => c.Advertisement).HasForeignKey(c => c.AdvertisementId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Advertisement>().HasMany(a => a.Queues).WithOne(q => q.Advertisement).HasForeignKey(q => q.AdvertisementId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Panel>().HasMany(p => p.Queues).WithOne(q => q.Panel).HasForeignKey(q => q.PanelId).OnDelete(DeleteBehavior.Cascade);*/
        }
    }
}
