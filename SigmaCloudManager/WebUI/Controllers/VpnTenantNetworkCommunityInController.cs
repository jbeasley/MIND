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
using SCM.Validators;
using Mind.Services;
using Mind.WebUI.Models;

namespace SCM.Controllers
{
    public class VpnTenantNetworkCommunityInController : BaseViewController
    {

        private readonly IVpnTenantIpNetworkCommunityInService _vpnTenantIpNetworkCommunityInService;
        private readonly IVpnTenantIpNetworkInService _vpnTenantIpNetworkInService;
        private readonly ITenantCommunityService _tenantCommunityService;

        public VpnTenantNetworkCommunityInController(IVpnTenantIpNetworkCommunityInService vpnTenantIpNetworkCommunityInService,
            IVpnTenantIpNetworkInService vpnTenantIpNetworkInService,
            ITenantCommunityService tenantCommunityService,
            IMapper mapper) : base (vpnTenantIpNetworkCommunityInService, mapper)
        {
            _vpnTenantIpNetworkCommunityInService = vpnTenantIpNetworkCommunityInService;
            _vpnTenantIpNetworkInService = vpnTenantIpNetworkInService;
            _tenantCommunityService = tenantCommunityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByVpnTenantIpNetworkInID(int? id, bool? showWarningMessage = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vpnTenantIpNetworkIn = await _vpnTenantIpNetworkInService.GetByIDAsync(id.Value);
            if (vpnTenantIpNetworkIn == null)
            {
                return NotFound();
            }

            var vpnTenantIpNetworkCommunitiesIn = await _vpnTenantIpNetworkCommunityInService.GetAllByVpnTenantIpNetworkInIDAsync(id.Value);
            ViewBag.VpnTenantIpNetworkIn = Mapper.Map<VpnTenantIpNetworkIn>(vpnTenantIpNetworkIn);

            return View(Mapper.Map<List<VpnTenantIpNetworkCommunityInViewModel>>(vpnTenantIpNetworkCommunitiesIn));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _vpnTenantIpNetworkCommunityInService.GetByIDAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            return View(Mapper.Map<VpnTenantIpNetworkCommunityInViewModel>(item));
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vpnTenantIpNetworkIn = await _vpnTenantIpNetworkInService.GetByIDAsync(id.Value);
            ViewBag.VpnTenantIpNetworkIn = vpnTenantIpNetworkIn;
            await PopulateTenantCommunitiesDropDownList(vpnTenantIpNetworkIn.TenantIpNetwork.TenantID);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VpnTenantIpNetworkInID,TenantCommunityID")] VpnTenantIpNetworkCommunityInViewModel vpnTenantIpNetworkCommunityInModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var vpnTenantIpNetworkCommunityIn = Mapper.Map<VpnTenantIpNetworkCommunityIn>(vpnTenantIpNetworkCommunityInModel);
                    await _vpnTenantIpNetworkCommunityInService.AddAsync(vpnTenantIpNetworkCommunityIn);

                    return RedirectToAction("GetAllByVpnTenantIpNetworkInID", new
                    {
                        id = vpnTenantIpNetworkCommunityIn.VpnTenantIpNetworkInID,
                        showWarningMessage = true
                    });
                }
            }

            catch (DbUpdateException /** ex **/ )
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            var vpnTenantIpNetworkIn = await _vpnTenantIpNetworkInService.GetByIDAsync(vpnTenantIpNetworkCommunityInModel.VpnTenantIpNetworkInID);
            ViewBag.VpnTenantNetworkIn = Mapper.Map<VpnTenantIpNetworkIn>(vpnTenantIpNetworkIn);
            await PopulateTenantCommunitiesDropDownList(vpnTenantIpNetworkIn.TenantIpNetwork.TenantID);

            return View(vpnTenantIpNetworkCommunityInModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id, int? vpnTenantIpNetworkInID, bool? concurrencyError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vpnTenantIpNetworkCommunityIn = await _vpnTenantIpNetworkCommunityInService.GetByIDAsync(id.Value);
            if (vpnTenantIpNetworkCommunityIn == null)
            {
                if (concurrencyError.GetValueOrDefault())
                {
                    return RedirectToAction("GetAllByVpnTenantIpNetworkInID", new
                    {
                        id = vpnTenantIpNetworkInID
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

            return View(Mapper.Map<VpnTenantIpNetworkCommunityInViewModel>(vpnTenantIpNetworkCommunityIn));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(VpnTenantIpNetworkCommunityInViewModel vpnTenantIpNetworkCommunityInModel)
        {
            var vpnTenantIpNetworkCommunityIn = await _vpnTenantIpNetworkCommunityInService.GetByIDAsync(vpnTenantIpNetworkCommunityInModel.VpnTenantIpNetworkCommunityInID);
            if (vpnTenantIpNetworkCommunityIn == null)
            {
                return RedirectToAction("GetAllByVpnTenantIpNetworkInID", new
                {
                    id = vpnTenantIpNetworkCommunityInModel.VpnTenantIpNetworkInID
                });
            }

            try
            {
                await _vpnTenantIpNetworkCommunityInService.DeleteAsync(vpnTenantIpNetworkCommunityInModel.VpnTenantIpNetworkCommunityInID);

                return RedirectToAction("GetAllByVpnTenantIpNetworkInID", new
                {
                    id = vpnTenantIpNetworkCommunityIn.VpnTenantIpNetworkInID,
                    showWarningMessage = true
                });
            }

            catch (DbUpdateConcurrencyException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("Delete", new
                {
                    concurrencyError = true,
                    id = vpnTenantIpNetworkCommunityIn.VpnTenantIpNetworkCommunityInID,
                    vpnTenantIpNetworkInID = vpnTenantIpNetworkCommunityIn.VpnTenantIpNetworkInID
                });
            }
        }

        private async Task PopulateTenantCommunitiesDropDownList(int tenantID, object selectedTenantCommunity = null)
        {
            var tenantCommunities = await _tenantCommunityService.GetAllByTenantIDAsync(tenantID);
            ViewBag.TenantCommunityID = new SelectList(Mapper.Map<List<TenantCommunityViewModel>>(tenantCommunities), 
                "TenantCommunityID", "Name", selectedTenantCommunity);
        }
    }
}
