using Microsoft.AspNetCore.Mvc;
using Mind.WebUI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.WebUI.ViewComponents
{
    public class PortsViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(IEnumerable<PortRequestOrUpdateViewModel> ports)
        {
            return ports != null
                ? Task.FromResult<IViewComponentResult>(View(ports.OrderBy(port => port.PortType).ThenBy(port => port.PortName)))
                : Task.FromResult<IViewComponentResult>(View(model: null as List<PortRequestOrUpdateViewModel>));
        }
    }
}

