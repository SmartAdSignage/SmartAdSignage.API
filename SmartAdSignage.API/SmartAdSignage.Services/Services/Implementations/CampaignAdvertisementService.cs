using SmartAdSignage.Core.Models;
using SmartAdSignage.Repository.Repositories.Interfaces;
using SmartAdSignage.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Services.Services.Implementations
{
    public class CampaignAdvertisementService : ICampaignAdvertisementService
    {
        private readonly IGenericRepository<CampaignAdvertisement> _genericRepository;
        public CampaignAdvertisementService(IGenericRepository<CampaignAdvertisement> genericRepository)
        {
            this._genericRepository = genericRepository;
        }
        public Task<CampaignAdvertisement> CreateCampaignAdvertisementAsync(CampaignAdvertisement campaignAdvertisement)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<CampaignAdvertisement>> GetAllCampaignAdvertisementsAsync()
        {
            return await _genericRepository.GetAllAsync();
        }
    }
}
