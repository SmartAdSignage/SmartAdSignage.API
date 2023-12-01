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
        private readonly Lazy<IGenericRepository<Advertisement>> _advertisementRepository;
        private readonly Lazy<IGenericRepository<AdCampaign>> _adCampaignRepository;
        private readonly Lazy<IGenericRepository<Panel>> _panelRepository;
        private readonly Lazy<IGenericRepository<Location>> _locationRepository;
        private readonly Lazy<IGenericRepository<IoTDevice>> _ioTdeviceRepository;
        private readonly Lazy<IGenericRepository<CampaignAdvertisement>> _campaignadvertisementRepository;
        private readonly Lazy<IGenericRepository<Queue>> _queueRepository;

        public UnitOfWork(Lazy<IGenericRepository<Advertisement>> advertisementRepository,
            Lazy<IGenericRepository<AdCampaign>> adCampaignRepository,
            Lazy<IGenericRepository<Panel>> panelRepository,
            Lazy<IGenericRepository<Location>> locationRepository,
            Lazy<IGenericRepository<IoTDevice>> ioTdeviceRepository,
            Lazy<IGenericRepository<CampaignAdvertisement>> campaignadvertisementRepository,
            Lazy<IGenericRepository<Queue>> queueRepository)
        {
            _advertisementRepository = advertisementRepository;
            _adCampaignRepository = adCampaignRepository;
            _panelRepository = panelRepository;
            _locationRepository = locationRepository;
            _ioTdeviceRepository = ioTdeviceRepository;
            _campaignadvertisementRepository = campaignadvertisementRepository;
            _queueRepository = queueRepository;
        }

        public IGenericRepository<Advertisement> Advertisements => _advertisementRepository.Value;

        public IGenericRepository<AdCampaign> AdCampaigns => _adCampaignRepository.Value;

        public IGenericRepository<Panel> Panels => _panelRepository.Value;

        public IGenericRepository<Location> Locations => _locationRepository.Value;

        public IGenericRepository<IoTDevice> IoTDevices => _ioTdeviceRepository.Value;

        public IGenericRepository<CampaignAdvertisement> CampaignAdvertisements => _campaignadvertisementRepository.Value;

        public IGenericRepository<Queue> Queues => _queueRepository.Value;
    }
}
