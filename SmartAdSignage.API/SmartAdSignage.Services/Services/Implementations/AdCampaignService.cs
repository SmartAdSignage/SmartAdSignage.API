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
    public class AdCampaignService : IAdCampaignService
    {
        private readonly IGenericRepository<AdCampaign> _genericRepository;
        public AdCampaignService(IGenericRepository<AdCampaign> genericRepository)
        {
            this._genericRepository = genericRepository;
        }
        public Task<AdCampaign> CreateAdCampaignAsync(AdCampaign adCampaign)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<AdCampaign>> GetAllAdCampaignsAsync()
        {
            return await _genericRepository.GetAllAsync();
        }
    }
}
