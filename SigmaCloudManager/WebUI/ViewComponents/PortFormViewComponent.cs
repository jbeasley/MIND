using Microsoft.AspNetCore.Mvc;
using Mind.WebUI.Models;
using System.Threading.Tasks;

namespace Mind.WebUI.ViewComponents
{
    public class PortFormViewComponent : ViewComponent
    {
        public Task <IViewComponentResult> InvokeAsync()
        {    
            return Task.FromResult<IViewComponentResult>(View(new PortRequestOrUpdateViewModel()));
        }
    }
}
