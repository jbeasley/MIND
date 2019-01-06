using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mind.WebUI.Models;
using SCM.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mind.WebUI.ViewComponents
{
    public class PlaneViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PlaneViewComponent(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(string selectedPlane)
        {
            var planes = await _unitOfWork.PlaneRepository.GetAsync();
            ViewBag.Plane = new SelectList(_mapper.Map<List<PlaneViewModel>>(planes), "Name", "Name", selectedPlane);

            return View(model: null as PlaneViewComponent);
        }
    }
}
