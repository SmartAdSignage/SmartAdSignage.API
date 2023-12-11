using SmartAdSignage.Core.Extra;
using SmartAdSignage.Core.Models;
using SmartAdSignage.Core.Resources;
using SmartAdSignage.Repository.Repositories.Interfaces;
using SmartAdSignage.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Services.Services.Implementations
{
    public class CampaignAdService : ICampaignAdService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CampaignAdService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<CampaignAdvertisement> CreateCampaignAdvertisementAsync(CampaignAdvertisement campaignAdvertisement)
        {
            if (campaignAdvertisement == null)
                throw new ArgumentException(Resources.Get("Invalid arguments"));
            var result = await _unitOfWork.CampaignAdvertisements.AddAsync(campaignAdvertisement);
            await _unitOfWork.CampaignAdvertisements.SaveAsync();
            return result;
        }

        public async Task<bool> DeleteCampaignAdvertisementByIdAsync(int id)
        {
            var campaignAdvertisement = await _unitOfWork.CampaignAdvertisements.GetByIdAsync(id);
            var result =_unitOfWork.CampaignAdvertisements.Delete(campaignAdvertisement);
            await _unitOfWork.CampaignAdvertisements.SaveAsync();
            return result;
        }

        public async Task<IEnumerable<CampaignAdvertisement>> GetAllCampaignAdvertisementsAsync(PageInfo pageInfo)
        {
            return await _unitOfWork.CampaignAdvertisements.GetPageWithMultiplePredicatesAsync(null, pageInfo, EntitySelector.CampaignAdvertisementSelector);
        }

        public async Task<IEnumerable<CampaignAdvertisement>> GetAllCampaignAdvertisementsByCampaignIdAsync(int id)
        {
            return await _unitOfWork.CampaignAdvertisements.GetByConditionAsync(x => x.AdCampaignId == id, EntitySelector.CampaignAdvertisementSelector);
        }

        public async Task<CampaignAdvertisement> GetCampaignAdvertisementByIdAsync(int id)
        {
            var result = await _unitOfWork.CampaignAdvertisements.GetByConditionAsync(x => x.Id == id, EntitySelector.CampaignAdvertisementSelector);
            return result.FirstOrDefault();
        }

        public async Task<CampaignAdvertisement> UpdateCampaignAdvertisementAsync(int id, CampaignAdvertisement campaignAdvertisement)
        {
            if (campaignAdvertisement == null)
                throw new ArgumentException(Resources.Get("Invalid arguments"));
            var existingCampaignAdvertisement = await _unitOfWork.CampaignAdvertisements.GetByIdAsync(id);
            if (existingCampaignAdvertisement == null)
                return null;
            existingCampaignAdvertisement.Views = campaignAdvertisement.Views;
            existingCampaignAdvertisement.AdvertisementId = campaignAdvertisement.AdvertisementId;
            existingCampaignAdvertisement.AdCampaignId = campaignAdvertisement.AdCampaignId;
            existingCampaignAdvertisement.DisplayedTimes = campaignAdvertisement.DisplayedTimes;
            existingCampaignAdvertisement.DateUpdated = DateTime.Now;
            var result = _unitOfWork.CampaignAdvertisements.Update(existingCampaignAdvertisement);
            await _unitOfWork.CampaignAdvertisements.SaveAsync();
            return result;
        }
    }
}
