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
    public class AdvertisementService : IAdvertisementService
    {
        private readonly IGenericRepository<Advertisement> _genericRepository;
        public AdvertisementService(IGenericRepository<Advertisement> genericRepository) 
        {
            this._genericRepository = genericRepository;
        }

        public Task<Advertisement> AddAdvertisementAsync(Advertisement advertisement)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Advertisement>> GetAllAdvertisements()
        {
            return await _genericRepository.GetAllAsync();
        }
    }
}
