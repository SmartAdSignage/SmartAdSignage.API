using SmartAdSignage.Core.Extra;
using SmartAdSignage.Core.Models;
using SmartAdSignage.Repository.Repositories.Interfaces;
using SmartAdSignage.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public async Task<Panel> ChangePanelBrightness(int id)
        {
            var panel = await _unitOfWork.Panels.GetByIdAsync(id);
            if (panel == null)
                return null;
            var lightMeter = (panel.IoTDevices?.Where(x => x.Name == "Light Meter").FirstOrDefault()) ?? throw new LightMeterException("No light meter was found to perform operation");
            if (lightMeter.Status != "Active")
                throw new LightMeterException("Light meter for this panel is not active");
            double luxValue;
            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromSeconds(60);
                httpClient.BaseAddress = new Uri("http://localhost:9080/");

                string requestUrl = $"get-lux-data";
                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);
                string content = await response.Content.ReadAsStringAsync();
                CultureInfo culture = CultureInfo.InvariantCulture;
                luxValue = Convert.ToDouble(content, culture);
            }
            if (luxValue > 100)
            {
                if (panel.Brightness == luxValue / 10)
                    return panel;
                else
                { 
                    panel.Brightness = luxValue / 10;
                    panel = _unitOfWork.Panels.Update(panel);
                    await _unitOfWork.Panels.SaveAsync();
                }
            }
            else 
            {
                if (panel.Brightness == 10.0)
                    return panel;
                else
                {
                    panel.Brightness = 10.0;
                    panel = _unitOfWork.Panels.Update(panel);
                    await _unitOfWork.Panels.SaveAsync();
                }
            }
            return panel;
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
