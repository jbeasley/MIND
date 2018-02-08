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

namespace SCM.Controllers
{
    public class TenantCommunitySetCommunityController : BaseViewController
    {
        public TenantCommunitySetCommunityController(ITenantCommunitySetCommunityService tenantCommunitySetCommunityService,
            ITenantCommunitySetService tenantCommunitySetService,
            ITenantCommunityService tenantCommunityService,
            IVpnService vpnService,
            IMapper mapper)
        {
            TenantCommunitySetCommunityService = tenantCommunitySetCommunityService;
            TenantCommunitySetService = tenantCommunitySetService;
            TenantCommunityService = tenantCommunityService;
            VpnService = vpnService;
            Mapper = mapper;
        }

        private ITenantCommunitySetCommunityService TenantCommunitySetCommunityService { get; set; }
        private ITenantCommunitySetService TenantCommunitySetService { get; set; }
        private ITenantCommunityService TenantCommunityService { get; set; }
        private IVpnService VpnService { get; set; }
        private IMapper Mapper { get; set; }

        [HttpGet]
        public async Task<IActionResult> GetAllByTenantCommunitySetID(int? id, bool showWarningMessage = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenantCommunities = await TenantCommunitySetCommunityService.GetAllByTenantCommunitySetIDAsync(id.Value);
            if (showWarningMessage)
            {
                ViewData["NetworkWarningMessage"] = $"VPNs require synchronisation with the network as a result of this update. "
                            + "Follow this <a href = '/Vpn/GetAll'>link</a> to the VPNs page.";
            }

            ViewBag.TenantCommunitySet = await TenantCommunitySetService.GetByIDAsync(id.Value);
            return View(Mapper.Map<List<TenantCommunitySetCommunityViewModel>>(tenantCommunities));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await TenantCommunitySetCommunityService.GetByIDAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            ViewBag.TenantCommunitySet = await TenantCommunitySetService.GetByIDAsync(item.TenantCommunitySetID);
            return View(Mapper.Map<TenantCommunitySetCommunityViewModel>(item));
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenantCommunitySet = await TenantCommunitySetService.GetByIDAsync(id.Value);
            ViewBag.TenantCommunitySet = tenantCommunitySet;
            await PopulateTenantCommunitiesDropDownList(tenantCommunitySet.TenantID);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenantCommunitySetID,TenantCommunityID")] TenantCommunitySetCommunityViewModel tenantCommunitySetCommunityModel)
        {
            if (ModelState.IsValid)
            {
                var tenantCommunitySetCommunity = Mapper.Map<TenantCommunitySetCommunity>(tenantCommunitySetCommunityModel);

                try
                {
                    await TenantCommunitySetCommunityService.AddAsync(Mapper.Map<TenantCommunitySetCommunity>(tenantCommunitySetCommunityModel));

                    // Check if VPNs need re-sync to network as a result of the creation of a Tenant Community in the set
                    // and generate a message for the view if so

                    var vpns = await VpnService.GetAllByTenantCommunitySetIDAsync(tenantCommunitySetCommunityModel.TenantCommunitySetID);

                    return RedirectToAction("GetAllByTenantCommunitySetID", new
                    {
                        id = tenantCommunitySetCommunityModel.TenantCommunitySetID,
                        showWarningMessage = vpns.Any()
                    });
                }

                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists " +
                        "see your system administrator.");
                }
            }
        
            var tenantCommunitySet = await TenantCommunitySetService.GetByIDAsync(tenantCommunitySetCommunityModel.TenantCommunitySetID);
            ViewBag.TenantCommunitySet = tenantCommunitySet;
            await PopulateTenantCommunitiesDropDownList(tenantCommunitySet.TenantID);

            return View(Mapper.Map<TenantCommunitySetCommunityViewModel>(tenantCommunitySetCommunityModel));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id, int? tenantCommunitySetID, bool? concurrencyError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenantCommunitySetCommunity = await TenantCommunitySetCommunityService.GetByIDAsync(id.Value);
            if (tenantCommunitySetCommunity == null)
            {
                if (concurrencyError.GetValueOrDefault())
                {
                    return RedirectToAction("GetAllByTenantCommunitySetID", new
                    {
                        id = tenantCommunitySetID
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

            var tenantCommunitySet = await TenantCommunitySetService.GetByIDAsync(tenantCommunitySetCommunity.TenantCommunitySetID);
            ViewBag.TenantCommunitySet = Mapper.Map<TenantCommunitySetViewModel>(tenantCommunitySet);

            return View(Mapper.Map<TenantCommunitySetCommunityViewModel>(tenantCommunitySetCommunity));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(TenantCommunitySetCommunityViewModel tenantCommunitySetCommunityModel)
        {
            var tenantCommunitySetCommunity = await TenantCommunitySetCommunityService.GetByIDAsync(tenantCommunitySetCommunityModel.TenantCommunitySetCommunityID);
            if (tenantCommunitySetCommunity == null)
            {
                return RedirectToAction("GetAllByTenantCommunitySetID", new
                {
                    id = tenantCommunitySetCommunityModel.TenantCommunitySetID
                });
            }

            try
            {
                await TenantCommunitySetCommunityService.DeleteAsync(Mapper.Map<TenantCommunitySetCommunity>(tenantCommunitySetCommunityModel));

                // Check if VPNs need re-sync to network as a result of the removeal of the Tenant Community from the set
                // and generate a message for the view if so

                var vpns = await VpnService.GetAllByTenantCommunitySetIDAsync(tenantCommunitySetCommunityModel.TenantCommunitySetID);

                return RedirectToAction("GetAllByTenantCommunitySetID", new
                {
                    id = tenantCommunitySetCommunityModel.TenantCommunitySetID,
                    showWarningMessage = vpns.Any()
                });
            }

            catch (DbUpdateConcurrencyException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("Delete", new
                {
                    concurrencyError = true,
                    id = tenantCommunitySetCommunityModel.TenantCommunitySetCommunityID,
                    tenantCommunitySetID = tenantCommunitySetCommunityModel.TenantCommunitySetID
                });
            }
        }

        private async Task PopulateTenantCommunitiesDropDownList(int tenantID, object selectedTenantCommunity = null)
        {
            var tenantCommunities = await TenantCommunityService.GetAllByTenantIDAsync(tenantID);
            ViewBag.TenantCommunityID = new SelectList(Mapper.Map<List<TenantCommunity>>(tenantCommunities),
                "TenantCommunityID", "Name", selectedTenantCommunity);
        }
    }
}