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
    public class DeviceRoleAndModelViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeviceRoleAndModelViewComponent(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(DeviceRoleAndModelComponentViewModel model)
        {
            await PopulateDeviceRolesDropDownList(model?.IsTenantDomainRole, model?.IsProviderDomainRole, model?.DeviceRole);              
            await PopulateDeviceModelsDropDownList(model?.DeviceModel);

            return View(model);
        }

        private async Task PopulateDeviceRolesDropDownList(bool? isTenantDomainRole = null, bool? isProviderDomainRole = null, string selectedDeviceRole = null)
        {
            var deviceRoles = await _unitOfWork.DeviceRoleRepository.GetAsync(
                    q =>
                    q.IsTenantDomainRole == isTenantDomainRole.GetValueOrDefault() &&
                    q.IsProviderDomainRole == isProviderDomainRole.GetValueOrDefault());
            ViewBag.DeviceRole = new SelectList(_mapper.Map<List<DeviceRoleViewModel>>(deviceRoles), "Name", "Name", selectedDeviceRole);
        }

        private async Task PopulateDeviceModelsDropDownList(object selectedDeviceModel = null)
        {
            var deviceModels = await _unitOfWork.DeviceModelRepository.GetAsync();
            ViewBag.DeviceModel = new SelectList(_mapper.Map<List<DeviceModelViewModel>>(deviceModels), "Name", "Name", selectedDeviceModel);
        }
    }
}
