using SmartAdSignage.Core.Extra;
using SmartAdSignage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Services.Services.Interfaces
{
    public interface ICampaignAdService
    {
        Task<CampaignAdvertisement> CreateCampaignAdvertisementAsync(CampaignAdvertisement campaignAdvertisement);
        Task<CampaignAdvertisement> UpdateCampaignAdvertisementAsync(int id, CampaignAdvertisement campaignAdvertisement);
        Task<IEnumerable<CampaignAdvertisement>> GetAllCampaignAdvertisementsAsync(PageInfo pageInfo);
        Task<CampaignAdvertisement> GetCampaignAdvertisementByIdAsync(int id);
        Task<bool> DeleteCampaignAdvertisementByIdAsync(int id);

        Task<IEnumerable<CampaignAdvertisement>> GetAllCampaignAdvertisementsByCampaignIdAsync(int id);
    }
}
