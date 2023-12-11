using Microsoft.EntityFrameworkCore;
using SmartAdSignage.Core.Models;
using SmartAdSignage.Repository.Data;
using SmartAdSignage.Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Repository.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IGenericRepository<Advertisement> _advertisementRepository;
        private readonly IGenericRepository<AdCampaign> _adCampaignRepository;
        private readonly IGenericRepository<Panel> _panelRepository;
        private readonly IGenericRepository<Location> _locationRepository;
        private readonly IGenericRepository<IoTDevice> _ioTdeviceRepository;
        private readonly IGenericRepository<CampaignAdvertisement> _campaignadvertisementRepository;
        private readonly IGenericRepository<Queue> _queueRepository;

        public UnitOfWork(ApplicationDbContext context, 
            IGenericRepository<Advertisement> advertisementRepository,
            IGenericRepository<AdCampaign> adCampaignRepository,
            IGenericRepository<Panel> panelRepository,
            IGenericRepository<Location> locationRepository,
            IGenericRepository<IoTDevice> ioTdeviceRepository,
            IGenericRepository<CampaignAdvertisement> campaignadvertisementRepository,
            IGenericRepository<Queue> queueRepository)
        {
            _context = context;
            _advertisementRepository = advertisementRepository;
            _adCampaignRepository = adCampaignRepository;
            _panelRepository = panelRepository;
            _locationRepository = locationRepository;
            _ioTdeviceRepository = ioTdeviceRepository;
            _campaignadvertisementRepository = campaignadvertisementRepository;
            _queueRepository = queueRepository;
        }

        public IGenericRepository<Advertisement> Advertisements => _advertisementRepository;

        public IGenericRepository<AdCampaign> AdCampaigns => _adCampaignRepository;

        public IGenericRepository<Panel> Panels => _panelRepository;

        public IGenericRepository<Location> Locations => _locationRepository;

        public IGenericRepository<IoTDevice> IoTDevices => _ioTdeviceRepository;

        public IGenericRepository<CampaignAdvertisement> CampaignAdvertisements => _campaignadvertisementRepository;

        public IGenericRepository<Queue> Queues => _queueRepository;

        public async Task CreateDatabaseBackupAsync(string path) 
        {
            await _context.Database.ExecuteSqlRawAsync($"BACKUP DATABASE SmartAdSignage TO DISK = '{path}'");
        }
    }
}
