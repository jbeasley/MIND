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
    public class BgpIpNetworkOutboundPolicyGridDataViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BgpIpNetworkOutboundPolicyGridDataViewComponent(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(BgpIpNetworkOutboundPolicyRequestViewModel bgpIpNetworkOutboundPolicy)
        {
            if (bgpIpNetworkOutboundPolicy?.RoutingInstanceNames != null)
            {
                var bgpPeers = (from result in await _unitOfWork.BgpPeerRepository.GetAsync(
                            q =>
                                bgpIpNetworkOutboundPolicy.RoutingInstanceNames.Contains(q.RoutingInstance.Name),
                                AsTrackable: false)
                                select result)
                                .ToList();

                bgpIpNetworkOutboundPolicy.BgpPeers = _mapper.Map<List<ProviderDomainBgpPeerViewModel>>(bgpPeers);
            }
            
            return View(bgpIpNetworkOutboundPolicy);
        }
    }
}
