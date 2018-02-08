using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper;
using SCM.Models;
using SCM.Models.ViewModels;
using SCM.Services;
using SCM.Models.RequestModels;
using SCM.Validators;
using SCM.Factories;

namespace SCM.Controllers
{
    public class RouteTargetController : BaseViewController
    {
        public RouteTargetController(IRouteTargetService routeTargetService,
            IRouteTargetRangeService routeTargetRangeService,
            IRouteTargetValidator routeTargetValidator, 
            IVpnService vpnService, IMapper mapper)
        {
            RouteTargetService = routeTargetService;
            RouteTargetRangeService = routeTargetRangeService;
            VpnService = vpnService;
            Mapper = mapper;

            RouteTargetValidator = routeTargetValidator;
            this.Validator = routeTargetValidator;
        }

        private IRouteTargetService RouteTargetService { get; }
        private IRouteTargetRangeService RouteTargetRangeService { get; }
        private IVpnService VpnService { get; }
        private IMapper Mapper { get; }
        private IRouteTargetValidator RouteTargetValidator { get; }

        [HttpGet]
        public async Task<IActionResult> GetAllByVpnID(int? id, bool showWarning = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vpn = await VpnService.GetByIDAsync(id.Value);
            if (vpn == null)
            {
                return NotFound();
            }

            RouteTargetValidator.ValidateExisting(vpn);
            if (RouteTargetValidator.ValidationDictionary.IsValid)
            {
                ViewData["SuccessMessage"] = "The Route Targets for this VPN are configured correctly!";
            }

            if (showWarning)
            {
                ViewData["NetworkWarningMessage"] = "The VPN requires synchronisation with the network as a result of this update. "
                            + "Follow this <a href = '/Vpn/GetAll'>link</a> to go to the VPNs page.";
            }

            var routeTargets = await RouteTargetService.GetAllByVpnIDAsync(id.Value);
            ViewBag.Vpn = Mapper.Map<VpnViewModel>(vpn);

            return View(Mapper.Map<List<RouteTargetViewModel>>(routeTargets));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await RouteTargetService.GetByIDAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            var vpn = await VpnService.GetByIDAsync(item.VpnID);
            ViewBag.Vpn = Mapper.Map<VpnViewModel>(vpn);

            return View(Mapper.Map<RouteTargetViewModel>(item));
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await PopulateRouteTargetRangesDropDownList();
            var vpn = await VpnService.GetByIDAsync(id.Value);
            ViewBag.Vpn = Mapper.Map<VpnViewModel>(vpn);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VpnID,RouteTargetRangeID,AdministratorSubField,RequestedAssignedNumberSubField,"
            + "AutoAllocateAssignedNumberSubField,IsHubExport")] RouteTargetRequestViewModel request)
        {
            var vpn = await VpnService.GetByIDAsync(request.VpnID);

            if (ModelState.IsValid)
            {
                await RouteTargetValidator.ValidateRouteTargetsChangeableAsync(vpn);
                var mappedRequest = Mapper.Map<RouteTargetRequest>(request);
                await RouteTargetValidator.ValidateNewAsync(mappedRequest);

                if (RouteTargetValidator.ValidationDictionary.IsValid)
                {
                    try
                    {
                        await RouteTargetService.AddAsync(Mapper.Map<RouteTargetRequest>(request));
                        return RedirectToAction("GetAllByVpnID", new
                        {
                            id = request.VpnID,
                            showWarning = true
                        });
                    }

                    catch (DbUpdateException /** ex **/ )
                    {
                        //Log the error (uncomment ex variable name and write a log.
                        ModelState.AddModelError("", "Unable to save changes. " +
                            "Try again, and if the problem persists " +
                            "see your system administrator.");
                    }

                    catch (FactoryFailureException ex)
                    {
                        ModelState.AddModelError("", ex.Message);
                    }
                }
            }

            ViewBag.Vpn = Mapper.Map<VpnViewModel>(vpn);
            await PopulateRouteTargetRangesDropDownList(request.RouteTargetRangeID);

            return View(request);
        }
      
        [HttpGet]
        public async Task<IActionResult> Delete(int? id, int? vpnID, bool? concurrencyError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var routeTarget = await RouteTargetService.GetByIDAsync(id.Value);
            if (routeTarget == null)
            {
                if (concurrencyError.GetValueOrDefault())
                {
                    return RedirectToAction("GetAllByVpnID", new
                    {
                        id = vpnID
                    });
                }

                return NotFound();
            }

            if (concurrencyError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] = "The record you attempted to delete "
                    + "was modified by another user after you got the original values. "
                    + "The delete operation was cancelled and the current values in the "
                    + "database have been displayed. If you still want to delete this "
                    + "record, click the Delete button again. Otherwise "
                    + "click the Back to List hyperlink.";
            }

            var vpn = await VpnService.GetByIDAsync(routeTarget.VpnID);
            ViewBag.Vpn = Mapper.Map<VpnViewModel>(vpn);

            return View(Mapper.Map<RouteTargetViewModel>(routeTarget));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(RouteTargetViewModel routeTargetModel)
        {
            try
            {
                var routeTarget = await RouteTargetService.GetByIDAsync(routeTargetModel.RouteTargetID);
                if (routeTarget == null)
                {
                    return RedirectToAction("GetAllByVpnID", new
                    {
                        id = routeTargetModel.VpnID
                    });
                }

                var vpn = await VpnService.GetByIDAsync(routeTargetModel.VpnID);
                this.Validator.ValidationDictionary.Clear();
                await RouteTargetValidator.ValidateRouteTargetsChangeableAsync(vpn);

                if (!RouteTargetValidator.ValidationDictionary.IsValid)
                {
                    ViewBag.Vpn = vpn;
                    return View(Mapper.Map<RouteTargetViewModel>(routeTarget));
                }

                await RouteTargetService.DeleteAsync(routeTarget);            
                return RedirectToAction("GetAllByVpnID", new
                {
                    id = routeTarget.VpnID,
                    showWarning = true
                });
            }

            catch (DbUpdateConcurrencyException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("Delete", new
                {
                    concurrencyError = true,
                    id = routeTargetModel.RouteTargetID,
                    vpnID = routeTargetModel.VpnID
                });
            }
        }

        /// <summary>
        /// Helper to populate a list of Route Target Ranges
        /// </summary>
        /// <param name="selectedRouteTargetRange"></param>
        /// <returns></returns>
        private async Task PopulateRouteTargetRangesDropDownList(object selectedRouteTargetRange = null)
        {
            var routeTargetRanges = await RouteTargetRangeService.GetAllAsync();
            ViewBag.RouteTargetRangeID = new SelectList(Mapper.Map<List<RouteTargetRangeViewModel>>(routeTargetRanges),
                "RouteTargetRangeID", "Name", selectedRouteTargetRange);
        }
    }
}
