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
    public class PanelService : IPanelService
    {
        private readonly IGenericRepository<Panel> _genericRepository;

        public PanelService(IGenericRepository<Panel> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<IEnumerable<Panel>> GetAllPanelsAsync()
        {
            return await _genericRepository.GetAllAsync();
        }
    }
}
