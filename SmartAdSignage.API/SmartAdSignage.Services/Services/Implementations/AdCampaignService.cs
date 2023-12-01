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
            var result = _genericRepository.AddAsync(adCampaign);
            _genericRepository.Commit();
            return result;
        }

        public bool DeleteAdCampaignByIdAsync(int id)
        {
            var adCampaign = GetAdCampaignByIdAsync(id);
            var result = _genericRepository.DeleteAsync(adCampaign.Result);
            _genericRepository.Commit();
            return result;
        }

        public async Task<AdCampaign> GetAdCampaignByIdAsync(int id)
        {
            return await _genericRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<AdCampaign>> GetAllAdCampaignsAsync()
        {
            return await _genericRepository.GetAllAsync();
        }

        public AdCampaign UpdateAdCampaignAsync(int id, AdCampaign adCampaign)
        {
            var existingAdCampaign = GetAdCampaignByIdAsync(id).Result;
            if (adCampaign.Status == "Started") 
            {
                existingAdCampaign.Status = adCampaign.Status;
                existingAdCampaign.StartDate = DateTime.Now;
                existingAdCampaign.EndDate = adCampaign.EndDate;
                existingAdCampaign.TargetedViews = adCampaign.TargetedViews;
                existingAdCampaign.UserId = adCampaign.UserId;
            }
            else
            {
                existingAdCampaign.Status = adCampaign.Status;
                existingAdCampaign.StartDate = adCampaign.StartDate;
                existingAdCampaign.EndDate = adCampaign.EndDate;
                existingAdCampaign.TargetedViews = adCampaign.TargetedViews;
                existingAdCampaign.UserId = adCampaign.UserId;
            }
            var result = _genericRepository.UpdateAsync(existingAdCampaign);
            _genericRepository.Commit();
            return result;

        }
    }
}
