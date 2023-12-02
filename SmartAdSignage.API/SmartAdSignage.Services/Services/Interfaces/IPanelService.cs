using SmartAdSignage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAdSignage.Services.Services.Interfaces
{
    public interface IPanelService
    {
        Task<Panel> CreatePanelAsync(Panel panel);
        Task<Panel> UpdatePanelAsync(int id, Panel panel);
        Task<bool> DeletePanelByIdAsync(int id);
        Task<Panel> GetPanelByIdAsync(int id);
        Task<IEnumerable<Panel>> GetAllPanelsAsync();
    }
}
