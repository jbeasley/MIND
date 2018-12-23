using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mind.WebUI.Models;
using SCM.Data;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.WebUI.ViewComponents
{
    public class PortBandwidthViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PortBandwidthViewComponent(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task <IViewComponentResult> InvokeAsync(PortBandwidthComponentViewModel model)
        {
            await PopulatePortBandwidthsDropDownList(model?.PortBandwidthGbps);
            return View(model);
        }

        private async Task PopulatePortBandwidthsDropDownList(object selectedPortBandwidth = null)
        {
            var portBandwidths = await _unitOfWork.PortBandwidthRepository.GetAsync(orderBy: q => q.OrderBy(x => x.BandwidthGbps));
            ViewBag.PortBandwidth = new SelectList(_mapper.Map<List<PortBandwidthViewModel>>(portBandwidths), "BandwidthGbps", "BandwidthGbps", selectedPortBandwidth);
        }
    }
}
