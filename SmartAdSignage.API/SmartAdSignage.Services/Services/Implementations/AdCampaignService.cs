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
            if (!ValidateAdCampaign(adCampaign)) 
                return null;
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
            if (existingAdCampaign == null)
                return null;
            if (!ValidateAdCampaign(adCampaign))
                return null;
            //existingAdCampaign.Status = adCampaign.Status;
            existingAdCampaign.EndDate = adCampaign.EndDate;
            existingAdCampaign.TargetedViews = adCampaign.TargetedViews;
            existingAdCampaign.UserId = adCampaign.UserId;
            existingAdCampaign.DateUpdated = DateTime.Now;
            var result = _unitOfWork.AdCampaigns.UpdateAsync(existingAdCampaign);
            await _unitOfWork.AdCampaigns.Commit();
            return result;

        }

        public async Task<IEnumerable<AdCampaign>> GetFinishedAdCampaigns(string userId)
        {
            var adCampaigns = await _unitOfWork.AdCampaigns.GetByConditionAsync(x => x.UserId == userId && x.EndDate <= DateTime.Now);
            return adCampaigns;
        }

        public async Task<int[]> GetStatistics(AdCampaign adCampaign) 
        {
            var views = (await _unitOfWork.CampaignAdvertisements.GetByConditionAsync(x => x.AdCampaignId == adCampaign.Id)).Sum(i => i.Views);
            var overallDisplays = (await _unitOfWork.CampaignAdvertisements.GetByConditionAsync(x => x.AdCampaignId == adCampaign.Id)).Sum(i => i.DisplayedTimes);
            var advertsDisplayed = (await _unitOfWork.CampaignAdvertisements.GetByConditionAsync(x => x.AdCampaignId == adCampaign.Id)).Count();
            /*if (adCampaign.Status != "Finished")
                adCampaign.Status = "Finished";*/
            return new int[] { views, overallDisplays, advertsDisplayed };
        }

        private bool ValidateAdCampaign(AdCampaign adCampaign) 
        {
            if (!CheckDates(adCampaign))
                return false;
            if (adCampaign.StartDate <= DateTime.Now) 
            {
                adCampaign.StartDate = DateTime.Now; 
                if (adCampaign.Status != "Started")
                    adCampaign.Status = "Started";
            }
            return true;
        }

        private bool CheckDates(AdCampaign adCampaign)
        {
            if (adCampaign.StartDate >= adCampaign.EndDate || adCampaign.EndDate <= DateTime.Now)
                return false;
            return true;
        }
    }
}
