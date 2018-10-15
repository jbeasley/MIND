using Microsoft.AspNetCore.Mvc;
using SCM.Controllers;
using SCM.Models;
using SCM.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.WebUI.Controllers
{
    public class ProviderDomainRoutingInstanceController : BaseViewController
    {
        [HttpGet]
        [ValidateProviderDomainAttachmentExists]
        public async Task<IActionResult> Details(int? attachmentId, int? routingInstanceId)
        {
            var attachment = await _attachmentService.GetByIDAsync(attachmentId.Value, deep: true);
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);

            return View(Mapper.Map<RoutingInstanceViewModel>(attachment.RoutingInstance));
        }

        protected async Task PopulateRoutingInstancesDropDownList(int deviceID, int? tenantID = null,
            bool? isInfrastructureVrf = null, bool? isTenantFacingVrf = null, object selectedRoutingInstance = null)
        {
            var routingInstances = await RoutingInstanceService.GetAllByDeviceIDAsync(deviceID: deviceID, tenantID: tenantID,
                isTenantFacingVrf: isTenantFacingVrf, isInfrastructureVrf: isInfrastructureVrf);
            ViewBag.RoutingInstanceID = new SelectList(Mapper.Map<List<RoutingInstanceViewModel>>(routingInstances),
                "RoutingInstanceID", "Name", selectedRoutingInstance);
        }
    }
}
