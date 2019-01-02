using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mind.WebUI.Models;
using SCM.Data;
using SCM.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.WebUI.ViewComponents
{
    public class PortFormViewComponent : ViewComponent
    { 
        public Task <IViewComponentResult> InvokeAsync(PortRequestOrUpdateViewModel portModel)
        {
            return Task.FromResult<IViewComponentResult>(View(portModel));
        }
    }
}
