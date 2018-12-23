using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mind.WebUI.Models;
using SCM.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.WebUI.ViewComponents
{
    public class PortProfileViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PortProfileViewComponent(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task <IViewComponentResult> InvokeAsync(PortProfileComponentViewModel model)
        {
            var tasks = new List<Task>
            {
                PopulatePortRolesDropDownList(model?.IsTenantDomainRole,model?.IsProviderDomainRole, model?.PortRole),
                PopulatePortPoolsDropDownList(model?.IsTenantDomainRole, model?.IsProviderDomainRole, model?.PortPool)
            };

            await Task.WhenAll(tasks);

            return View(model);
        }

        private async Task PopulatePortRolesDropDownList(bool? isTenantDomainRole, bool? isProviderDomainRole, object selectedPortRole = null)
        {
            var portRoles = await _unitOfWork.PortRoleRepository.GetAsync(
                q => 
                q.DeviceRolePortRoles
                .Any(
                    x => 
                    x.DeviceRole.IsTenantDomainRole == isTenantDomainRole.GetValueOrDefault() && 
                    x.DeviceRole.IsProviderDomainRole == isProviderDomainRole.GetValueOrDefault())
                );

            ViewBag.PortRole = new SelectList(_mapper.Map<List<PortRoleViewModel>>(portRoles), "Name", "Name", selectedPortRole);
        }

        private async Task PopulatePortPoolsDropDownList(bool? isTenantDomainRole, bool? isProviderDomainRole, object selectedPortPool = null)
        {
            var portPools = await _unitOfWork.PortPoolRepository.GetAsync(
                q =>
                q.PortRole.DeviceRolePortRoles
                .Any(
                    x =>
                    x.DeviceRole.IsTenantDomainRole == isTenantDomainRole.GetValueOrDefault() &&
                    x.DeviceRole.IsProviderDomainRole == isProviderDomainRole.GetValueOrDefault())
                );

            ViewBag.PortPool = new SelectList(_mapper.Map<List<PortPoolViewModel>>(portPools), "Name", "Name", selectedPortPool);
        }
    }
}
