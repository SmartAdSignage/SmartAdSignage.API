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
        private readonly IUnitOfWork _unitOfWork;
        public LocationService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Location> CreateLocationAsync(Location location)
        {
            var result = await _unitOfWork.Locations.AddAsync(location);
            await _unitOfWork.Locations.Commit();
            return result;
        }

        public async Task<bool> DeleteLocationByIdAsync(int id)
        {
            var location = await _unitOfWork.Locations.GetByIdAsync(id);
            var result = _unitOfWork.Locations.DeleteAsync(location);
            await _unitOfWork.Locations.Commit();
            return result;
        }

        public async Task<IEnumerable<Location>> GetAllLocationsAsync()
        {
            return await _unitOfWork.Locations.GetAllAsync();
        }

        public Task<Location> GetLocationByIdAsync(int id)
        {
            return _unitOfWork.Locations.GetByIdAsync(id);
        }

        public async Task<Location> UpdateLocationAsync(int id, Location location)
        {
            var existingLocation = await _unitOfWork.Locations.GetByIdAsync(id);
            existingLocation.Street = location.Street;
            existingLocation.StreetType = location.StreetType;
            existingLocation.City = location.City;
            existingLocation.BuildingNumber = location.BuildingNumber;
            existingLocation.Country = location.Country;
            existingLocation.DateUpdated = DateTime.Now;
            var result = _unitOfWork.Locations.UpdateAsync(existingLocation);
            await _unitOfWork.Locations.Commit();
            return result;
        }
    }
}
