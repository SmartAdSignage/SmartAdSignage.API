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
    public class LocationService : ILocationService
    {
        private readonly IGenericRepository<Location> _genericRepository;

        public LocationService(IGenericRepository<Location> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public Task<Location> CreateAddAdvertisementAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Location>> GetAllLocationsAsync()
        {
            return await _genericRepository.GetAllAsync();
        }
    }
}
