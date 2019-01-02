using Microsoft.AspNetCore.Mvc;
using Mind.WebUI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mind.WebUI.ViewComponents
{
    public class PortDetailsViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(IEnumerable<PortViewModel> ports)
        {
            return ports != null
                ? Task.FromResult<IViewComponentResult>(View(ports))
                : Task.FromResult<IViewComponentResult>(View(model: null as List<PortViewModel>));
        }
    }
}

