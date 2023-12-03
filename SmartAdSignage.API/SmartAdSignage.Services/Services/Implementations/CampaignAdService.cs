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
    public class CampaignAdService : ICampaignAdService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CampaignAdService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task<CampaignAdvertisement> CreateCampaignAdvertisementAsync(CampaignAdvertisement campaignAdvertisement)
        {
            var result = await _unitOfWork.CampaignAdvertisements.AddAsync(campaignAdvertisement);
            await _unitOfWork.CampaignAdvertisements.Commit();
            return result;
        }

        public async Task<bool> DeleteCampaignAdvertisementByIdAsync(int id)
        {
            var campaignAdvertisement = await _unitOfWork.CampaignAdvertisements.GetByIdAsync(id);
            var result =_unitOfWork.CampaignAdvertisements.DeleteAsync(campaignAdvertisement);
            await _unitOfWork.CampaignAdvertisements.Commit();
            return result;
        }

        public async Task<IEnumerable<CampaignAdvertisement>> GetAllCampaignAdvertisementsAsync()
        {
            return await _unitOfWork.CampaignAdvertisements.GetAllAsync();
        }

        public async Task<CampaignAdvertisement> GetCampaignAdvertisementByIdAsync(int id)
        {
            return await _unitOfWork.CampaignAdvertisements.GetByIdAsync(id);
        }

        public async Task<CampaignAdvertisement> UpdateCampaignAdvertisementAsync(int id, CampaignAdvertisement campaignAdvertisement)
        {
            var existingCampaignAdvertisement = await _unitOfWork.CampaignAdvertisements.GetByIdAsync(id);
            if (existingCampaignAdvertisement == null)
                return null;
            existingCampaignAdvertisement.Views = campaignAdvertisement.Views;
            existingCampaignAdvertisement.AdvertisementId = campaignAdvertisement.AdvertisementId;
            existingCampaignAdvertisement.AdCampaignId = campaignAdvertisement.AdCampaignId;
            existingCampaignAdvertisement.DisplayedTimes = campaignAdvertisement.DisplayedTimes;
            existingCampaignAdvertisement.DateUpdated = DateTime.Now;
            var result = _unitOfWork.CampaignAdvertisements.UpdateAsync(existingCampaignAdvertisement);
            await _unitOfWork.CampaignAdvertisements.Commit();
            return result;
        }
    }
}
