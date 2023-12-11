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
    public class AdvertisementService : IAdvertisementService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AdvertisementService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Advertisement> CreateAdvertisementAsync(Advertisement advertisement)
        {
            if (advertisement == null)
                throw new ArgumentException(Resources.Get("Invalid arguments"));
            var result = await _unitOfWork.Advertisements.AddAsync(advertisement);
            await _unitOfWork.Advertisements.SaveAsync();
            return result;
        }

        public async Task<bool> DeleteAdvertisementByIdAsync(int id)
        {
            var advertisement = await _unitOfWork.Advertisements.GetByIdAsync(id);
            var result = _unitOfWork.Advertisements.Delete(advertisement);
            await _unitOfWork.Advertisements.SaveAsync();
            return result;
        }

        public async Task<Advertisement> GetAdvertisementByIdAsync(int id)
        {
            var result = await _unitOfWork.Advertisements.GetByConditionAsync(x => x.Id == id, EntitySelector.AdvertisementSelector);
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<Advertisement>> GetAllAdvertisements(PageInfo pageInfo)
        {
            return await _unitOfWork.Advertisements.GetPageWithMultiplePredicatesAsync(null, pageInfo, EntitySelector.AdvertisementSelector);
        }

        public async Task<IEnumerable<Advertisement>> GetAllAdvertisementsByUserIdAsync(string id)
        {
            return await _unitOfWork.Advertisements.GetByConditionAsync(x => x.UserId == id, EntitySelector.AdvertisementSelector);
        }

        public async Task<Advertisement> UpdateAdvertisementAsync(int id, Advertisement advertisement)
        {
            if (advertisement == null)
                throw new ArgumentException(Resources.Get("Invalid arguments"));
            var existingAdvertisement = await _unitOfWork.Advertisements.GetByIdAsync(id);
            if (existingAdvertisement == null)
                return null;
            existingAdvertisement.Title = advertisement.Title;
            existingAdvertisement.Type = advertisement.Type;
            existingAdvertisement.File = advertisement.File;
            existingAdvertisement.UserId = advertisement.UserId;
            existingAdvertisement.DateUpdated = DateTime.Now;
            var result = _unitOfWork.Advertisements.Update(existingAdvertisement);
            await _unitOfWork.Advertisements.SaveAsync();
            return result;
        }
    }
}
