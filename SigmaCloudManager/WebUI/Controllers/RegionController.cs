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
using Mind.WebUI.Models;

namespace SCM.Controllers
{
    public class RegionController : BaseViewController
    {
        public RegionController(IRegionService regionService, IMapper mapper)
        {
            RegionService = regionService;
            Mapper = mapper;
        }

        private IRegionService RegionService { get; set; }
        private IMapper Mapper { get; set; }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regions = await RegionService.GetAllAsync();
            return View(Mapper.Map<List<RegionViewModel>>(regions));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await RegionService.GetByIDAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            return View(Mapper.Map<RegionViewModel>(item));
        }
    }
}
