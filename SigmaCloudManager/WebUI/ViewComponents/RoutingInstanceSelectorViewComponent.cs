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
    public class RoutingInstanceSelectorViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoutingInstanceSelectorViewComponent(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(RoutingInstanceSelectorComponentViewModel model)
        {

            await PopulateRoutingInstancesDropDownList(
                    tenantId:model?.TenantId,
                    deviceId:model?.DeviceId,
                    isTenantFacingVrf: model?.IsTenantFacingVrf,
                    isInfrastructureVrf: model?.IsInfrastructureVrf,
                    isDefaultRoutingIntance: model?.IsDefaultRoutingInstance,
                    selectedRoutingInstance: model?.ExistingRoutingInstanceName);

            return View(model);
        }

        private async Task PopulateRoutingInstancesDropDownList(int? tenantId, int? deviceId,
         bool? isTenantFacingVrf = false, bool? isInfrastructureVrf = false, bool? isDefaultRoutingIntance = false, 
            object selectedRoutingInstance = null)
        {
            var query = (from result in await _unitOfWork.RoutingInstanceRepository.GetAsync(
                           q =>
                               q.RoutingInstanceType.IsTenantFacingVrf == isTenantFacingVrf.GetValueOrDefault() &&
                               q.RoutingInstanceType.IsInfrastructureVrf == isInfrastructureVrf.GetValueOrDefault() &&
                               q.RoutingInstanceType.IsDefault == isDefaultRoutingIntance.GetValueOrDefault())
                         select result);

            if (tenantId.HasValue) query = query.Where(x => x.TenantID == tenantId);
            if (deviceId.HasValue) query = query.Where(x => x.DeviceID == deviceId);

            var routingInstances = query.ToList();

            ViewBag.RoutingInstance = new SelectList(_mapper.Map<List<RoutingInstanceViewModel>>(routingInstances),
                "Name", "Name", selectedRoutingInstance);
        }
    }
}
