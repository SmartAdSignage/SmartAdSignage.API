using SmartAdSignage.Core.Models;
using SmartAdSignage.Repository.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Repository.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<Advertisement> Advertisements { get; }

        IGenericRepository<AdCampaign> AdCampaigns { get; }

        IGenericRepository<Panel> Panels { get; }

        IGenericRepository<Location> Locations { get; }

        IGenericRepository<IoTDevice> IoTDevices { get; }

        IGenericRepository<CampaignAdvertisement> CampaignAdvertisements { get; }

        IGenericRepository<Queue> Queues { get; }
    }
}
