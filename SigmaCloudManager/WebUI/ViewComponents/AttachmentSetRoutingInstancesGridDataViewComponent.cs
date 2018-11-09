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
    public class AttachmentSetRoutingInstancesGridDataViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AttachmentSetRoutingInstancesGridDataViewComponent(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? attachmentSetId, IEnumerable<AttachmentSetRoutingInstanceRequestViewModel> attachmentSetRoutingInstanceRequests)
        {
            // Get a list of BGP peers which are associated with the current set of attachment set routing instances
            // These will be used to create drop-downs for the BGP IP network inbound policy grid
            var routingInstanceNames = attachmentSetRoutingInstanceRequests?.Select(q => q.RoutingInstanceName);
            var bgpPeers = (from result in await _unitOfWork.BgpPeerRepository.GetAsync(
                            q =>
                            routingInstanceNames.Contains(q.RoutingInstance.Name),
                            AsTrackable: false)
                            select result)
                            .ToList();

            HttpContext.Items["InboundBgpPeer"] = _mapper.Map<List<ProviderDomainBgpPeerViewModel>>(bgpPeers);

            return View(attachmentSetRoutingInstanceRequests);
        }
    }
}
