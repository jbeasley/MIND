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
    public class ExtranetVpnTenantNetworkInController : BaseViewController
    {
        public ExtranetVpnTenantNetworkInController(IExtranetVpnTenantNetworkInService extranetVpnTenantNetworkInService,
            IExtranetVpnMemberService extranetVpnMemberService,
            IVpnTenantIpNetworkInService vpnTenantNetworkInService,
            IExtranetVpnTenantNetworkInValidator extranetVpnTenantNetworkInValidator,
            IMapper mapper)
        {
            ExtranetVpnTenantNetworkInService = extranetVpnTenantNetworkInService;
            VpnTenantNetworkInService = vpnTenantNetworkInService;
            ExtranetVpnMemberService = extranetVpnMemberService;
            ExtranetVpnTenantNetworkInValidator = extranetVpnTenantNetworkInValidator;
            this.Validator = extranetVpnTenantNetworkInValidator;
            Mapper = mapper;
        }

        private IExtranetVpnTenantNetworkInService ExtranetVpnTenantNetworkInService { get; }
        private IExtranetVpnMemberService ExtranetVpnMemberService { get; }
        private IVpnTenantIpNetworkInService VpnTenantNetworkInService { get; }
        private IExtranetVpnTenantNetworkInValidator ExtranetVpnTenantNetworkInValidator { get; }
        private IMapper Mapper { get; }


        [HttpGet]
        public async Task<PartialViewResult> VpnTenantNetworks(int vpnID, int tenantID)
        {
            var vpnTenantNetworks = await VpnTenantNetworkInService.GetAllByVpnIDAsync(vpnID, tenantID, 
                extranet: true);
            return PartialView(Mapper.Map<List<VpnTenantIpNetworkInViewModel>>(vpnTenantNetworks));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByExtranetVpnMemberID(int? id, bool? showWarningMessage = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var extranetVpnMember = await ExtranetVpnMemberService.GetByIDAsync(id.Value);
            if (extranetVpnMember == null)
            {
                return NotFound();
            }

            if (showWarningMessage.GetValueOrDefault())
            {
                ViewData["NetworkWarningMessage"] = "The Extranet VPN requires synchronisation with the network as a result of this update. "
                        + "Follow this <a href = '/Vpn/GetAll'>link</a> to go to the VPNs page.";
            }

            var extranetVpnTenantNetworksIn = await ExtranetVpnTenantNetworkInService.GetAllByExtranetVpnMemberIDAsync(id.Value);
            ViewBag.ExtranetVpnMember = Mapper.Map<ExtranetVpnMemberViewModel>(extranetVpnMember);

            return View(Mapper.Map<List<ExtranetVpnTenantNetworkInViewModel>>(extranetVpnTenantNetworksIn));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await ExtranetVpnTenantNetworkInService.GetByIDAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            return View(Mapper.Map<ExtranetVpnTenantNetworkInViewModel>(item));
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var extranetVpnMember = await ExtranetVpnMemberService.GetByIDAsync(id.Value);
            if (extranetVpnMember == null)
            {
                return NotFound();
            }

            ViewBag.ExtranetVpnMember = Mapper.Map<ExtranetVpnMemberViewModel>(extranetVpnMember);
            await PopulateTenantsDropDownList(extranetVpnMember.MemberVpn.VpnID);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenantID,ExtranetVpnMemberID,VpnTenantNetworkInID")]
            ExtranetVpnTenantNetworkInViewModel extranetVpnTenantNetworkInModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var extranetVpnTenantNetworkIn = Mapper.Map<ExtranetVpnTenantNetworkIn>(extranetVpnTenantNetworkInModel);
                    await ExtranetVpnTenantNetworkInValidator.ValidateNewAsync(extranetVpnTenantNetworkIn);

                    if (ExtranetVpnTenantNetworkInValidator.ValidationDictionary.IsValid)
                    {
                        await ExtranetVpnTenantNetworkInService.AddAsync(extranetVpnTenantNetworkIn);

                        return RedirectToAction("GetAllByExtranetVpnMemberID", new
                        {
                            id = extranetVpnTenantNetworkIn.ExtranetVpnMemberID,
                            showWarningMessage = true
                        });
                    }
                }
            }

            catch (DbUpdateException /** ex **/ )
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            var extranetVpnMember = await ExtranetVpnMemberService.GetByIDAsync(extranetVpnTenantNetworkInModel.ExtranetVpnMemberID);
            ViewBag.ExtranetVpnMember = Mapper.Map<ExtranetVpnMemberViewModel>(extranetVpnMember);
            await PopulateTenantsDropDownList(extranetVpnMember.MemberVpnID, extranetVpnTenantNetworkInModel.TenantID);
            await PopulateVpnTenantNetworksDropDownList(extranetVpnMember.MemberVpn.VpnID,
                extranetVpnMember.MemberVpn.TenantID, extranetVpnTenantNetworkInModel.ExtranetVpnTenantNetworkInID);

            return View(extranetVpnTenantNetworkInModel);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var extranetVpnTenantNetworkIn = await ExtranetVpnTenantNetworkInService.GetByIDAsync(id.Value);
            if (extranetVpnTenantNetworkIn == null)
            {
                return NotFound();
            }

            var extranetVpnMember = await ExtranetVpnMemberService.GetByIDAsync(extranetVpnTenantNetworkIn.ExtranetVpnMemberID);
            ViewBag.ExtranetVpnMember = Mapper.Map<ExtranetVpnMemberViewModel>(extranetVpnMember);

            return View(Mapper.Map<ExtranetVpnTenantNetworkInViewModel>(extranetVpnTenantNetworkIn));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id, int? extranetVpnMemberID, bool? concurrencyError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var extranetVpnTenantNetworkIn = await ExtranetVpnTenantNetworkInService.GetByIDAsync(id.Value);
            if (extranetVpnTenantNetworkIn == null)
            {
                if (concurrencyError.GetValueOrDefault())
                {
                    return RedirectToAction("GetAllByExtranetVpnMemberID", new { id = extranetVpnMemberID });
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

            return View(Mapper.Map<ExtranetVpnTenantNetworkInViewModel>(extranetVpnTenantNetworkIn));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ExtranetVpnTenantNetworkInViewModel extranetVpnTenantNetworkInModel)
        {
            var extranetVpnTenantNetworkIn = await ExtranetVpnTenantNetworkInService.GetByIDAsync(extranetVpnTenantNetworkInModel.ExtranetVpnTenantNetworkInID);
            if (extranetVpnTenantNetworkIn == null)
            {
                return RedirectToAction("GetAllByExtranetVpnMemberID", new
                {
                    id = extranetVpnTenantNetworkInModel.ExtranetVpnMemberID
                });
            }

            try
            {
                await ExtranetVpnTenantNetworkInService.DeleteAsync(Mapper.Map<ExtranetVpnTenantNetworkIn>(extranetVpnTenantNetworkIn));

                return RedirectToAction("GetAllByExtranetVpnMemberID", new
                {
                    id = extranetVpnTenantNetworkIn.ExtranetVpnMemberID,
                    showWarningMessage = true
                });
            }

            catch (DbUpdateConcurrencyException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("Delete", new
                {
                    concurrencyError = true,
                    id = extranetVpnTenantNetworkInModel.ExtranetVpnTenantNetworkInID,
                    extranetVpnMemberID = extranetVpnTenantNetworkIn.ExtranetVpnMemberID
                });
            }
        }

        private async Task PopulateTenantsDropDownList(int vpnID, object selectedTenant = null)
        {
            var query = from networks in await VpnTenantNetworkInService.GetAllByVpnIDAsync(vpnID)
                        select networks.TenantIpNetwork.Tenant;

            var tenants = query.ToList().GroupBy(q => q.TenantID).Select(r => r.First());

            ViewBag.TenantID = new SelectList(Mapper.Map<List<TenantViewModel>>(tenants),
                "TenantID", "Name", selectedTenant);
        }

        private async Task PopulateVpnTenantNetworksDropDownList(int vpnID, int tenantID, object selectedVpnTenantNetwork = null)
        {
            var tenantIpNetworks = (from result in await VpnTenantNetworkInService.GetAllByVpnIDAsync(vpnID, tenantID, extranet: true)
                                    select new { result.VpnTenantIpNetworkInID, result.TenantIpNetwork.CidrName });

            ViewBag.VpnTenantNetworkInID = new SelectList(tenantIpNetworks,
                "VpnTenantIpNetworkInID", "Name", selectedVpnTenantNetwork);
        }
    }
}