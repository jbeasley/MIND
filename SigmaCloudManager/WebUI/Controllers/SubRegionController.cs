using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using SCM.Models;
using SCM.Models.ViewModels;
using SCM.Services;
using SCM.Validators;

namespace SCM.Controllers
{
    public class SubRegionController : BaseViewController
    {
        public SubRegionController(ISubRegionService subRegionService, IRegionService regionService, IMapper mapper)
        {
            SubRegionService = subRegionService;
            RegionService = regionService;
            Mapper = mapper;
        }

        private ISubRegionService SubRegionService { get; set; }
        private IRegionService RegionService { get; set; }
        private IMapper Mapper { get; set; }

        [HttpGet]
        public async Task<IActionResult> GetAllByRegionID(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var region = await RegionService.GetByIDAsync(id.Value);
            if (region == null)
            {
                return NotFound();
            }

            ViewBag.Region = region;
            var subRegions = await SubRegionService.GetAllByRegionIDAsync(id.Value);
            return View(Mapper.Map<List<SubRegionViewModel>>(subRegions));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await SubRegionService.GetByIDAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            return View(Mapper.Map<SubRegionViewModel>(item));
        }
    }
}
