using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mind.WebUI.Models;
using SCM.Data;
using SCM.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.WebUI.ViewComponents
{
    public class MtuOptionsViewComponent : ViewComponent
    { 
        public Task <IViewComponentResult> InvokeAsync(MtuOptionsComponentViewModel model)
        {
            return Task.FromResult<IViewComponentResult>(View(model));
        }
    }
}
