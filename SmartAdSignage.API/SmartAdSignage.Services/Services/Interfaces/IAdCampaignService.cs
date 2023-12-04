using SmartAdSignage.Core.Extra;
using SmartAdSignage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Services.Services.Interfaces
{
    public interface IAdCampaignService
    {
        Task<AdCampaign> CreateAdCampaignAsync(AdCampaign adCampaign);
        Task<AdCampaign> UpdateAdCampaignAsync(int id, AdCampaign adCampaign);
        Task<bool> DeleteAdCampaignByIdAsync(int id);
        Task<AdCampaign> GetAdCampaignByIdAsync(int id);
        Task<IEnumerable<AdCampaign>> GetAllAdCampaignsAsync(PageInfo pageInfo);
        Task<IEnumerable<AdCampaign>> GetFinishedAdCampaigns(string userId);
        Task<int[]> GetStatistics(AdCampaign adCampaign);
    }
}
