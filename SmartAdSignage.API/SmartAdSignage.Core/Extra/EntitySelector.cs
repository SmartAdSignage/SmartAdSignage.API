using SmartAdSignage.Core.DTOs.AdCampaign.Reponses;
using SmartAdSignage.Core.DTOs.Advertisement.Responses;
using SmartAdSignage.Core.DTOs.CampaignAdvertisement.Responses;
using SmartAdSignage.Core.DTOs.IoTDevice.Responses;
using SmartAdSignage.Core.DTOs.Location.Responses;
using SmartAdSignage.Core.DTOs.Panel.Responses;
using SmartAdSignage.Core.DTOs.Queue.Responses;
using SmartAdSignage.Core.DTOs.User.Responses;
using SmartAdSignage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Core.Extra
{
    public static class EntitySelector
    {
        public static Expression<Func<AdCampaign, AdCampaign>> AdCampaignSelector => q => new AdCampaign
        {
            Id = q.Id,
            StartDate = q.StartDate,
            EndDate = q.EndDate,
            TargetedViews = q.TargetedViews,
            Status = q.Status,
            User = q.User,
            Panels = q.Panels.Select(panel => new Panel
            {
                Id = panel.Id,
                Width = panel.Width,
                Height = panel.Height,
                LocationId = panel.LocationId,
                UserId = panel.UserId
            }).ToList(),
            CampaignAdvertisements = q.CampaignAdvertisements.Select(campaignAdvertisement => new CampaignAdvertisement
            {
                Id = campaignAdvertisement.Id,
                AdCampaignId = campaignAdvertisement.AdCampaignId,
                Views = campaignAdvertisement.Views,
                DisplayedTimes = campaignAdvertisement.DisplayedTimes,
                AdvertisementId = campaignAdvertisement.AdvertisementId,
            }).ToList()
        };

        public static Expression<Func<Advertisement, Advertisement>> AdvertisementSelector => q => new Advertisement
        {
            Id = q.Id,
            Title = q.Title,
            Type = q.Type,
            File = q.File,
            User = q.User,
            CampaignAdvertisements = q.CampaignAdvertisements.Select(campaignAdvertisement => new CampaignAdvertisement
            {
                Id = campaignAdvertisement.Id,
                AdCampaignId = campaignAdvertisement.AdCampaignId,
                Views = campaignAdvertisement.Views,
                DisplayedTimes = campaignAdvertisement.DisplayedTimes,
            }).ToList(),
            Queues = q.Queues.Select(queue => new Queue
            {
                Id = queue.Id,
                PanelId = queue.PanelId,
                DisplayOrder = queue.DisplayOrder
            }).ToList()
        };

        public static Expression<Func<CampaignAdvertisement, CampaignAdvertisement>> CampaignAdvertisementSelector => q => new CampaignAdvertisement
        {
            Id = q.Id,
            Views = q.Views,
            DisplayedTimes = q.DisplayedTimes,
            AdCampaign = new AdCampaign
            {
                Id = q.AdCampaign.Id,
                StartDate = q.AdCampaign.StartDate,
                EndDate = q.AdCampaign.EndDate,
                TargetedViews = q.AdCampaign.TargetedViews,
                Status = q.AdCampaign.Status,
                UserId = q.AdCampaign.UserId
            },
            Advertisement = new Advertisement
            {
                Id = q.Advertisement.Id,
                Title = q.Advertisement.Title,
                Type = q.Advertisement.Type,
                File = q.Advertisement.File,
            }
        };

        public static Expression<Func<IoTDevice, IoTDevice>> IoTDeviceSelector => q => new IoTDevice
        {
            Id = q.Id,
            Name = q.Name,
            Status = q.Status,
            Panel = new Panel
            {
                Id = q.Panel.Id,
                Width = q.Panel.Width,
                Height = q.Panel.Height,
                LocationId = q.Panel.LocationId,
                UserId = q.Panel.UserId,
                Latitude = q.Panel.Latitude,
                Longitude = q.Panel.Longitude,
            }
        };

        public static Expression<Func<Location, Location>> LocationSelector => q => new Location
        {
            Id = q.Id,
            Country = q.Country,
            City = q.City,
            Street = q.Street,
            StreetType = q.StreetType,
            BuildingNumber = q.BuildingNumber,
            Panels = q.Panels.Select(panel => new Panel
            {
                Id = panel.Id,
                Width = panel.Width,
                Height = panel.Height,
                UserId = panel.UserId,
                Latitude = panel.Latitude,
                Longitude = panel.Longitude,
            }).ToList()
        };

        public static Expression<Func<Panel, Panel>> PanelSelector => q => new Panel
        {
            Id = q.Id,
            Height = q.Height,
            Width = q.Width,
            Status = q.Status,
            Latitude = q.Latitude,
            Longitude = q.Longitude,
            Location = new Location
            {
                Id = q.Location.Id,
                Country = q.Location.Country,
                City = q.Location.City,
                Street = q.Location.Street,
                StreetType = q.Location.StreetType,
                BuildingNumber = q.Location.BuildingNumber,
            },
            User = q.User,
            AdCampaigns = q.AdCampaigns.Select(adCampaign => new AdCampaign
            {
                Id = adCampaign.Id,
                StartDate = adCampaign.StartDate,
                EndDate = adCampaign.EndDate,
                TargetedViews = adCampaign.TargetedViews,
                Status = adCampaign.Status,
            }).ToList(),
            IoTDevices = q.IoTDevices.Select(ioTDevice => new IoTDevice
            {
                Id = ioTDevice.Id,
                Name = ioTDevice.Name,
                Status = ioTDevice.Status
            }).ToList(),
            Queues = q.Queues.Select(queue => new Queue 
            {
                Id = queue.Id,
                DisplayOrder = queue.DisplayOrder,
                AdvertisementId = queue.AdvertisementId
            }).ToList()
        };

        public static Expression<Func<Queue, Queue>> QueueSelector => q => new Queue
        {
            Id = q.Id,
            DisplayOrder = q.DisplayOrder,
            Panel = new Panel 
            {
                Id = q.Panel.Id,
                Width = q.Panel.Width,
                Height = q.Panel.Height,
                LocationId = q.Panel.LocationId,
                UserId = q.Panel.UserId,
                Latitude = q.Panel.Latitude,
                Longitude = q.Panel.Longitude,
            },
            Advertisement = new Advertisement
            {
                Id = q.Advertisement.Id,
                Title = q.Advertisement.Title,
                Type = q.Advertisement.Type,
                File = q.Advertisement.File,
            }
        };
    }
}
