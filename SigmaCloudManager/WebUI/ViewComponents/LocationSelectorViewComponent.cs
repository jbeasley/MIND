using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mind.WebUI.Models;
using SCM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.WebUI.ViewComponents
{ 
    public class LocationSelectorViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LocationSelectorViewComponent(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(LocationSelectorViewModel model)
        {
            await PopulateRegionsDropDownList(model?.RegionId);
            if (model != null) {
                if (model.RegionId.HasValue) {
                    await PopulateSubRegionsDropDownList(model.RegionId.Value, model.SubRegionId);
                }
                if (model.SubRegionId.HasValue)
                {
                    await PopulateLocationsDropDownList(model.SubRegionId.Value, model.LocationName);
                }
            }

            return View(model);
        }

        private async Task PopulateRegionsDropDownList(object selectedRegion = null)
        {
            var regions = await _unitOfWork.RegionRepository.GetAsync();
            ViewBag.Region = new SelectList(_mapper.Map<List<RegionViewModel>>(regions), "RegionId", "Name", selectedRegion);
        }

        private async Task PopulateSubRegionsDropDownList(int regionId, object selectedSubRegion = null)
        {
            var subRegions = await _unitOfWork.SubRegionRepository.GetAsync(
                                q =>
                                q.RegionID == regionId);
            ViewBag.SubRegion = new SelectList(_mapper.Map<List<SubRegionViewModel>>(subRegions), "SubRegionId", "Name", selectedSubRegion);
        }

        private async Task PopulateLocationsDropDownList(int subRegionId, object selectedLocation = null)
        {
            var locations = await _unitOfWork.LocationRepository.GetAsync(
                                q =>
                                q.SubRegionID == subRegionId);
            ViewBag.Location = new SelectList(_mapper.Map<List<LocationViewModel>>(locations), "LocationName", "LocationName", selectedLocation);
        }
    }
}
