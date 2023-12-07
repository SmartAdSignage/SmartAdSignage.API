using SmartAdSignage.Core.Extra;
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
        private readonly IUnitOfWork _unitOfWork;
        public PanelService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Panel> CreatePanelAsync(Panel panel)
        {
            if (panel == null)
                throw new ArgumentException("Invalid arguments");
            var result = await _unitOfWork.Panels.AddAsync(panel);
            await _unitOfWork.Panels.SaveAsync();
            return result;
        }

        public async Task<bool> DeletePanelByIdAsync(int id)
        {
            var panel = await _unitOfWork.Panels.GetByIdAsync(id);
            var result = _unitOfWork.Panels.Delete(panel);
            await _unitOfWork.Panels.SaveAsync();
            return result;
        }

        public async Task<IEnumerable<Panel>> GetAllPanelsAsync(PageInfo pageInfo)
        {
            return await _unitOfWork.Panels.GetPageWithMultiplePredicatesAsync(null, pageInfo, EntitySelector.PanelSelector);
        }

        public async Task<IEnumerable<Panel>> GetAllPanelsByUserIdAsync(string id)
        {
            return await _unitOfWork.Panels.GetByConditionAsync(x => x.UserId == id, EntitySelector.PanelSelector);
        }

        public async Task<Panel> GetPanelByIdAsync(int id)
        {
            var result = await _unitOfWork.Panels.GetByConditionAsync(x => x.Id == id);
            return result.FirstOrDefault();
        }

        public async Task<Panel> UpdatePanelAsync(int id, Panel panel)
        {
            if (panel == null)
                throw new ArgumentException("Invalid arguments");
            var exsistingPanel = await _unitOfWork.Panels.GetByIdAsync(id);
            if (exsistingPanel == null)
                return null;
            exsistingPanel.Width = panel.Width;
            exsistingPanel.Height = panel.Height;
            exsistingPanel.Longitude = panel.Longitude;
            exsistingPanel.Latitude = panel.Latitude;
            exsistingPanel.Status = panel.Status;
            exsistingPanel.LocationId = panel.LocationId;
            exsistingPanel.UserId = panel.UserId;
            exsistingPanel.DateUpdated = DateTime.Now;
            var result = _unitOfWork.Panels.Update(exsistingPanel);
            await _unitOfWork.Panels.SaveAsync();
            return result;
        }
    }
}
