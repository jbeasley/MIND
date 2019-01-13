using Microsoft.AspNetCore.Mvc;
using Mind.WebUI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mind.WebUI.ViewComponents
{
    public class RoutingInstanceBgpPeersGridDataViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(IEnumerable<BgpPeerRequestViewModel> bgpPeerRequests)
        {
            return Task.FromResult<IViewComponentResult>(View(bgpPeerRequests));
        }
    }
}
