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
            var result = await _unitOfWork.Panels.AddAsync(panel);
            await _unitOfWork.Panels.Commit();
            return result;
        }

        public async Task<bool> DeletePanelByIdAsync(int id)
        {
            var panel = await _unitOfWork.Panels.GetByIdAsync(id);
            var result = _unitOfWork.Panels.DeleteAsync(panel);
            await _unitOfWork.Panels.Commit();
            return result;
        }

        public async Task<IEnumerable<Panel>> GetAllPanelsAsync()
        {
            return await _unitOfWork.Panels.GetAllAsync();
        }

        public async Task<Panel> GetPanelByIdAsync(int id)
        {
            return await _unitOfWork.Panels.GetByIdAsync(id);
        }

        public async Task<Panel> UpdatePanelAsync(int id, Panel panel)
        {
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
            var result = _unitOfWork.Panels.UpdateAsync(exsistingPanel);
            await _unitOfWork.Panels.Commit();
            return result;
        }
    }
}
