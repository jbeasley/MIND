using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mind.WebUI.Models;
using SCM.Data;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.WebUI.ViewComponents
{ 
    public class BgpIpNetworkInboundPolicyViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BgpIpNetworkInboundPolicyViewComponent(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? attachmentSetId, IEnumerable<VpnTenantIpNetworkInRequestViewModel> bgpIpNetworkInboundPolicy)
        {
            // Check if we're gather data for an existing attachment set 
            if (attachmentSetId.HasValue)
            {
                // If we've been passed an existing policy then simply use it to build the view
                if (bgpIpNetworkInboundPolicy != null) return View(bgpIpNetworkInboundPolicy);

                // Get the tenant IP networks which are bound to the inbound policy of the attachment set
                var items = await _unitOfWork.VpnTenantIpNetworkInRepository.GetAsync(
                                    q =>
                                    q.AttachmentSetID == attachmentSetId,
                                    query: q => q.IncludeDeepProperties(),
                                    AsTrackable: false);

                return View(_mapper.Map<List<VpnTenantIpNetworkInRequestViewModel>>(items));
            }

            // The BGP policy is for a new attachment set - simply return whatever we have to build the view
            return View(bgpIpNetworkInboundPolicy);
        }
    }
}
