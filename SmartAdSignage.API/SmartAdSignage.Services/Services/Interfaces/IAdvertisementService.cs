using SmartAdSignage.Core.Extra;
using SmartAdSignage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Services.Services.Interfaces
{
    public interface IAdvertisementService
    {
        Task<Advertisement> CreateAdvertisementAsync(Advertisement advertisement);

        Task<IEnumerable<Advertisement>> GetAllAdvertisements(PageInfo pageInfo);

        Task<Advertisement> GetAdvertisementByIdAsync(int id);

        Task<Advertisement> UpdateAdvertisementAsync(int id, Advertisement advertisement);

        Task<bool> DeleteAdvertisementByIdAsync(int id);

        Task<IEnumerable<Advertisement>> GetAllAdvertisementsByUserIdAsync(string id);
    }
}
