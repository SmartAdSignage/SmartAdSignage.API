using SmartAdSignage.Core.Extra;
using SmartAdSignage.Core.Models;
using SmartAdSignage.Core.Resources;
using SmartAdSignage.Repository.Repositories.Interfaces;
using SmartAdSignage.Services.Services.Interfaces;

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
            if (adCampaign == null)
                throw new ArgumentException(Resources.Get("Invalid arguments"));
            if (!ValidateAdCampaignDates(adCampaign))
                throw new ArgumentException(Resources.Get("Invalid dates"));
            var result = await _unitOfWork.AdCampaigns.AddAsync(adCampaign);
            await _unitOfWork.AdCampaigns.SaveAsync();
            return result;
        }

        public async Task<bool> DeleteAdCampaignByIdAsync(int id)
        {
            var adCampaign = await GetAdCampaignByIdAsync(id);
            var result = _unitOfWork.AdCampaigns.Delete(adCampaign);
            await _unitOfWork.AdCampaigns.SaveAsync();
            return result;
        }

        public async Task<AdCampaign> GetAdCampaignByIdAsync(int id)
        {
            var result = await _unitOfWork.AdCampaigns.GetByConditionAsync( x => x.Id == id, EntitySelector.AdCampaignSelector);
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<AdCampaign>> GetAllAdCampaignsAsync(PageInfo pageInfo)
        {
            return await _unitOfWork.AdCampaigns.GetPageWithMultiplePredicatesAsync(null, pageInfo, EntitySelector.AdCampaignSelector);
        }

        public async Task<AdCampaign> UpdateAdCampaignAsync(int id, AdCampaign adCampaign)
        {
            var existingAdCampaign = await _unitOfWork.AdCampaigns.GetByIdAsync(id);
            if (existingAdCampaign == null)
                return null;
            if (!ValidateAdCampaignDates(adCampaign))
                throw new ArgumentException(Resources.Get("Invalid dates"));
            existingAdCampaign.Status = adCampaign.Status;
            existingAdCampaign.EndDate = adCampaign.EndDate;
            existingAdCampaign.TargetedViews = adCampaign.TargetedViews;
            existingAdCampaign.UserId = adCampaign.UserId;
            existingAdCampaign.DateUpdated = DateTime.Now;
            var result = _unitOfWork.AdCampaigns.Update(existingAdCampaign);
            await _unitOfWork.AdCampaigns.SaveAsync();
            return result;

        }

        public async Task<IEnumerable<AdCampaign>> GetFinishedAdCampaigns(string userId)
        {
            var adCampaigns = await _unitOfWork.AdCampaigns.GetByConditionAsync(x => x.UserId == userId && x.EndDate <= DateTime.Now, EntitySelector.AdCampaignSelector);
            if (adCampaigns is null)
                return null;
            foreach (var adCampaign in adCampaigns)
            {
                if (adCampaign.Status != "Finished") 
                {
                    adCampaign.Status = "Finished";
                    _unitOfWork.AdCampaigns.Update(adCampaign);
                    await _unitOfWork.AdCampaigns.SaveAsync();
                }
            }
            return adCampaigns;
        }

        public async Task<int[]> GetStatistics(AdCampaign adCampaign) 
        {
            var views = adCampaign.CampaignAdvertisements.Sum(x => x.Views);
            var overallDisplays = adCampaign.CampaignAdvertisements.Sum(x => x.DisplayedTimes); 
            var advertsDisplayed = adCampaign.CampaignAdvertisements.Count(); 
            if (adCampaign.Status != "Finished")
                adCampaign.Status = "Finished";
            return new int[] { views, overallDisplays, advertsDisplayed };
        }

        private bool ValidateAdCampaignDates(AdCampaign adCampaign) 
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

        public async Task<IEnumerable<AdCampaign>> GetAllAdCampaignsByUserIdAsync(string id)
        {
            return await _unitOfWork.AdCampaigns.GetByConditionAsync(x => x.UserId == id, EntitySelector.AdCampaignSelector);
        }
    }
}
