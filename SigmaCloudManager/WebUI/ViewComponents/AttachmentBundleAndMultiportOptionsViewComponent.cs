using Microsoft.AspNetCore.Mvc;
using Mind.WebUI.Models;
using System.Threading.Tasks;

namespace Mind.WebUI.ViewComponents
{
    public class AttachmentBundleAndMultiportOptionsViewComponent : ViewComponent
    {
        public Task <IViewComponentResult> InvokeAsync(AttachmentBundleAndMultiportOptionsComponentViewModel model)
        {
            if (model == null)
            {
                model = new AttachmentBundleAndMultiportOptionsComponentViewModel();
            }

            return Task.FromResult<IViewComponentResult>(View(model));
        }
    }
}
