using SmartAdSignage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Services.Services.Interfaces
{
    public interface ILocationService
    {
        Task<Location> CreateLocationAsync(Location location);
        Task<Location> UpdateLocationAsync(int id, Location location);
        Task<Location> GetLocationByIdAsync(int id);
        Task<IEnumerable<Location>> GetAllLocationsAsync();
        Task<bool> DeleteLocationByIdAsync(int id);
    }
}
