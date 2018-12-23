using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mind.WebUI.Models;
using SCM.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mind.WebUI.ViewComponents
{
    public class PortStatusViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PortStatusViewComponent(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task <IViewComponentResult> InvokeAsync(PortStatusComponentViewModel model)
        {
            await PopulatePortStatusesDropDownList(model.PortStatus);
            return View(model);
        }

        private async Task PopulatePortStatusesDropDownList(object selectedPortStatus = null)
        {
            var portStatuses = await _unitOfWork.PortStatusRepository.GetAsync();
            ViewBag.PortStatus = new SelectList(_mapper.Map<List<PortStatusViewModel>>(portStatuses), "Name", "Name", selectedPortStatus);
        }
    }
}
