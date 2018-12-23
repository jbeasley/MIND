using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mind.WebUI.Models;
using SCM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.WebUI.ViewComponents
{ 
    public class DeviceMtuAndStatusViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeviceMtuAndStatusViewComponent(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(DeviceMtuAndStatusComponentViewModel model)
        {
            await PopulateDeviceStatusDropDownList(model?.DeviceStatus);
            return View(model);
        }

        private async Task PopulateDeviceStatusDropDownList(object selectedDeviceStatus = null)
        {
            var deviceStatuses = await _unitOfWork.DeviceStatusRepository.GetAsync();
            ViewBag.DeviceStatus = new SelectList(_mapper.Map<List<DeviceStatusViewModel>>(deviceStatuses), "Name", "Name", selectedDeviceStatus);
        }
    }
}
