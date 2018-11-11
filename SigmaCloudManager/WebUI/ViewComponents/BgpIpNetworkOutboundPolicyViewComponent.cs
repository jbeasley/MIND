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
    public class BgpIpNetworkOutboundPolicyViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(BgpIpNetworkOutboundPolicyRequestViewModel bgpIpNetworkOutboundPolicy)
        { 
            return Task.FromResult<IViewComponentResult>(View(bgpIpNetworkOutboundPolicy));
        }
    }
}
