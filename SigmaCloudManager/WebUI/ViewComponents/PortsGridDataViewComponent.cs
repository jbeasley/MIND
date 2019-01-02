using Microsoft.AspNetCore.Mvc;
using Mind.WebUI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using SCM.Data;
using AutoMapper;
using SCM.Models;

namespace Mind.WebUI.ViewComponents
{
    public class PortsGridDataViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PortsGridDataViewComponent(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(PortsGridDataViewModel portsGridData)
        {
            if (portsGridData?.Ports != null)
            {
                return View(
                       portsGridData.Ports.OrderBy(
                                port =>
                                port.PortType)
                            .ThenBy(
                                port =>
                                port.PortName)
                            .ThenBy(
                                port =>
                                port.IsNew));
            }
            else if (portsGridData?.DeviceId != null)
            {
                var p = await GetPortsData(portsGridData.DeviceId.Value);
                return View(p);
            }
            else
            {
                return View(model: null as List<PortRequestOrUpdateViewModel>);
            }
        }

        private async Task<List<PortRequestOrUpdateViewModel>> GetPortsData(int deviceId)
        {
            var ports = (from result in await _unitOfWork.PortRepository.GetAsync(
                      q =>
                         q.DeviceID == deviceId,
                         query: q => q.IncludeDeepProperties(),
                         AsTrackable: false)
                         select result)
                         .ToList();

            return _mapper.Map<List<PortRequestOrUpdateViewModel>>(ports);
        }
    }
}
