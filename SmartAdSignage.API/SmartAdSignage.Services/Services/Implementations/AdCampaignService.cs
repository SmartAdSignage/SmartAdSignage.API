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
        private readonly IUnitOfWork _unitOfWork;
        public AdCampaignService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<AdCampaign> CreateAdCampaignAsync(AdCampaign adCampaign)
        {
            var result = await _unitOfWork.AdCampaigns.AddAsync(adCampaign);
            await _unitOfWork.AdCampaigns.Commit();
            return result;
        }

        public async Task<bool> DeleteAdCampaignByIdAsync(int id)
        {
            var adCampaign = await GetAdCampaignByIdAsync(id);
            var result = _unitOfWork.AdCampaigns.DeleteAsync(adCampaign);
            await _unitOfWork.AdCampaigns.Commit();
            return result;
        }

        public async Task<AdCampaign> GetAdCampaignByIdAsync(int id)
        {
            return await _unitOfWork.AdCampaigns.GetByIdAsync(id);
        }

        public async Task<IEnumerable<AdCampaign>> GetAllAdCampaignsAsync()
        {
            return await _unitOfWork.AdCampaigns.GetAllAsync();
        }

        public async Task<AdCampaign> UpdateAdCampaignAsync(int id, AdCampaign adCampaign)
        {
            var existingAdCampaign = await _unitOfWork.AdCampaigns.GetByIdAsync(id);
            if (adCampaign.Status == "Started") 
                existingAdCampaign.StartDate = DateTime.Now;
            else
                existingAdCampaign.StartDate = adCampaign.StartDate;

            existingAdCampaign.Status = adCampaign.Status;
            existingAdCampaign.EndDate = adCampaign.EndDate;
            existingAdCampaign.TargetedViews = adCampaign.TargetedViews;
            existingAdCampaign.UserId = adCampaign.UserId;
            existingAdCampaign.DateUpdated = DateTime.Now;
            var result = _unitOfWork.AdCampaigns.UpdateAsync(existingAdCampaign);
            await _unitOfWork.AdCampaigns.Commit();
            return result;

        }
    }
}
