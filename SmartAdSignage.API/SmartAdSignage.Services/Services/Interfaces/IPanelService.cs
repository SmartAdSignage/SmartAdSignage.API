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
        /*Task<Panel> CreatePanelAsync(Panel panel);
        Task<Panel> UpdatePanelAsync(Panel panel);
        Task<Panel> DeletePanelAsync(Panel panel);
        Task<Panel> GetByIdAsync(int id);*/
        Task<IEnumerable<Panel>> GetAllPanelsAsync();
    }
}
