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

namespace SCM.Controllers
{
    public class VpnTenantNetworkCommunityInController : BaseViewController
    {
        public VpnTenantNetworkCommunityInController(IVpnTenantNetworkCommunityInService vpnTenantNetworkCommunityInService,
            IVpnTenantNetworkInService vpnTenantNetworkInService,
            ITenantCommunityService tenantCommunityService,
            IMapper mapper)
        {
            VpnTenantNetworkCommunityInService = vpnTenantNetworkCommunityInService;
            VpnTenantNetworkInService = vpnTenantNetworkInService;
            TenantCommunityService = tenantCommunityService;
            Mapper = mapper;
        }

        private IVpnTenantNetworkCommunityInService VpnTenantNetworkCommunityInService { get; }
        private IVpnTenantNetworkInService VpnTenantNetworkInService { get; }
        private ITenantCommunityService TenantCommunityService { get; }
        private IMapper Mapper { get; set; }

        [HttpGet]
        public async Task<IActionResult> GetAllByVpnTenantNetworkInID(int? id, bool? showWarningMessage = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vpnTenantNetworkIn = await VpnTenantNetworkInService.GetByIDAsync(id.Value);
            if (vpnTenantNetworkIn == null)
            {
                return NotFound();
            }

            if (showWarningMessage.GetValueOrDefault())
            {
                ViewData["NetworkWarningMessage"] = "The VPN requires synchronisation with the network as a result of this update. "
                  + "Follow this <a href = '/Vpn/GetAll'>link</a> to go to the VPNs page."; ;
            }


            var vpnTenantNetworkCommunitiesIn = await VpnTenantNetworkCommunityInService.GetAllByVpnTenantNetworkInIDAsync(id.Value);
            ViewBag.VpnTenantNetworkIn = Mapper.Map<VpnTenantNetworkIn>(vpnTenantNetworkIn);

            return View(Mapper.Map<List<VpnTenantNetworkCommunityInViewModel>>(vpnTenantNetworkCommunitiesIn));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await VpnTenantNetworkCommunityInService.GetByIDAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            return View(Mapper.Map<VpnTenantNetworkCommunityInViewModel>(item));
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vpnTenantNetworkIn = await VpnTenantNetworkInService.GetByIDAsync(id.Value);
            ViewBag.VpnTenantNetworkIn = vpnTenantNetworkIn;
            await PopulateTenantCommunitiesDropDownList(vpnTenantNetworkIn.TenantNetwork.TenantID);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VpnTenantNetworkInID,TenantCommunityID")] VpnTenantNetworkCommunityInViewModel vpnTenantNetworkCommunityInModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var vpnTenantNetworkCommunityIn = Mapper.Map<VpnTenantNetworkCommunityIn>(vpnTenantNetworkCommunityInModel);
                    await VpnTenantNetworkCommunityInService.AddAsync(vpnTenantNetworkCommunityIn);

                    return RedirectToAction("GetAllByVpnTenantNetworkInID", new
                    {
                        id = vpnTenantNetworkCommunityIn.VpnTenantNetworkInID,
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

            var vpnTenantNetworkIn = await VpnTenantNetworkInService.GetByIDAsync(vpnTenantNetworkCommunityInModel.VpnTenantNetworkInID);
            ViewBag.VpnTenantNetworkIn = Mapper.Map<VpnTenantNetworkIn>(vpnTenantNetworkIn);
            await PopulateTenantCommunitiesDropDownList(vpnTenantNetworkIn.TenantNetwork.TenantID);

            return View(vpnTenantNetworkCommunityInModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id, int? vpnTenantNetworkInID, bool? concurrencyError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vpnTenantNetworkCommunityIn = await VpnTenantNetworkCommunityInService.GetByIDAsync(id.Value);
            if (vpnTenantNetworkCommunityIn == null)
            {
                if (concurrencyError.GetValueOrDefault())
                {
                    return RedirectToAction("GetAllByVpnTenantNetworkInID", new
                    {
                        id = vpnTenantNetworkInID
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

            return View(Mapper.Map<VpnTenantNetworkCommunityInViewModel>(vpnTenantNetworkCommunityIn));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(VpnTenantNetworkCommunityInViewModel vpnTenantNetworkCommunityInModel)
        {
            var vpnTenantNetworkCommunityIn = await VpnTenantNetworkCommunityInService.GetByIDAsync(vpnTenantNetworkCommunityInModel.VpnTenantNetworkCommunityInID);
            if (vpnTenantNetworkCommunityIn == null)
            {
                return RedirectToAction("GetAllByVpnTenantNetworkInID", new
                {
                    id = vpnTenantNetworkCommunityInModel.VpnTenantNetworkInID
                });
            }

            try
            {
                await VpnTenantNetworkCommunityInService.DeleteAsync(Mapper.Map<VpnTenantNetworkCommunityIn>(vpnTenantNetworkCommunityInModel));

                return RedirectToAction("GetAllByVpnTenantNetworkInID", new
                {
                    id = vpnTenantNetworkCommunityIn.VpnTenantNetworkInID,
                    showWarningMessage = true
                });
            }

            catch (DbUpdateConcurrencyException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("Delete", new
                {
                    concurrencyError = true,
                    id = vpnTenantNetworkCommunityIn.VpnTenantNetworkCommunityInID,
                    vpnTenantNetworkInID = vpnTenantNetworkCommunityIn.VpnTenantNetworkInID
                });
            }
        }

        private async Task PopulateTenantCommunitiesDropDownList(int tenantID, object selectedTenantCommunity = null)
        {
            var tenantCommunities = await TenantCommunityService.GetAllByTenantIDAsync(tenantID);
            ViewBag.TenantCommunityID = new SelectList(Mapper.Map<List<TenantCommunityViewModel>>(tenantCommunities), 
                "TenantCommunityID", "Name", selectedTenantCommunity);
        }
    }
}
