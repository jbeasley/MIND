using Microsoft.AspNetCore.Mvc;
using Mind.WebUI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mind.WebUI.ViewComponents
{
    public class PortsGridDataViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(IEnumerable<PortRequestOrUpdateViewModel> ports)
        {
            return Task.FromResult<IViewComponentResult>(View(ports));
        }
    }
}
