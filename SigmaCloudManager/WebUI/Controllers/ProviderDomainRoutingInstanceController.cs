using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mind.WebUI.Attributes;
using SCM.Controllers;
using SCM.Data;
using SCM.Models;
using SCM.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.WebUI.Controllers
{
    public class ProviderDomainRoutingInstanceController : BaseViewController
    {
        public ProviderDomainRoutingInstanceController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet]
        [ValidateProviderDomainRoutingInstanceExists]
        public async Task<IActionResult> Details(int? routingInstanceId)
        {
            var routingInstance = await _unitOfWork.RoutingInstanceRepository.GetAsync(
                q =>
                    q.RoutingInstanceID == routingInstanceId &&
                    q.RoutingInstanceType.IsTenantFacingVrf,
                    AsTrackable: false);                                     

            return View(Mapper.Map<RoutingInstanceViewModel>(routingInstance));
        }
    }
}
