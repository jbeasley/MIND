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
            if (model != null)
            {
                if (model.TenantId.HasValue && model.DeviceId.HasValue)
                {
                    await PopulateRoutingInstancesDropDownList(model.TenantId.Value, 
                            model.DeviceId.Value, model.ExistingRoutingInstanceName);
                }
            }

            return View(model);
        }

        private async Task PopulateRoutingInstancesDropDownList(int tenantId, int deviceId, object selectedRoutingInstance = null)
        {
            var routingInstances = await _unitOfWork.RoutingInstanceRepository.GetAsync(
                            q =>
                                q.TenantID == tenantId &&
                                q.DeviceID == deviceId &&
                                q.RoutingInstanceType.IsTenantFacingVrf);

            ViewBag.RoutingInstance = new SelectList(_mapper.Map<List<RoutingInstanceViewModel>>(routingInstances),
                "Name", "Name", selectedRoutingInstance);
        }
    }
}
