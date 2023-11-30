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
        /*Task<AdCampaign> CreateAdCampaignAsync(AdCampaign adCampaign);
        Task<AdCampaign> UpdateAdCampaignAsync(AdCampaign adCampaign);
        Task<AdCampaign> DeleteAdCampaignAsync(AdCampaign adCampaign);
        Task<AdCampaign> GetByIdAsync(int id);*/
        Task<IEnumerable<AdCampaign>> GetAllAdCampaignsAsync();
    }
}
