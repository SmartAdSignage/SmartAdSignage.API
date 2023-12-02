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
        private readonly IUnitOfWork _unitOfWork;
        public AdvertisementService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Advertisement> CreateAdvertisementAsync(Advertisement advertisement)
        {
            var result = await _unitOfWork.Advertisements.AddAsync(advertisement);
            await _unitOfWork.Advertisements.Commit();
            return result;
        }

        public async Task<bool> DeleteAdvertisementByIdAsync(int id)
        {
            var advertisement = await _unitOfWork.Advertisements.GetByIdAsync(id);
            var result = _unitOfWork.Advertisements.DeleteAsync(advertisement);
            await _unitOfWork.Advertisements.Commit();
            return result;
        }

        public Task<Advertisement> GetAdvertisementByIdAsync(int id)
        {
            return _unitOfWork.Advertisements.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Advertisement>> GetAllAdvertisements()
        {
            return await _unitOfWork.Advertisements.GetAllAsync();
        }

        public async Task<Advertisement> UpdateAdvertisementAsync(int id, Advertisement advertisement)
        {
            var existingAdvertisement = await _unitOfWork.Advertisements.GetByIdAsync(id);
            existingAdvertisement.Title = advertisement.Title;
            existingAdvertisement.Type = advertisement.Type;
            existingAdvertisement.File = advertisement.File;
            existingAdvertisement.UserId = advertisement.UserId;
            existingAdvertisement.DateUpdated = DateTime.Now;
            var result = _unitOfWork.Advertisements.UpdateAsync(existingAdvertisement);
            await _unitOfWork.Advertisements.Commit();
            return result;
        }
    }
}
