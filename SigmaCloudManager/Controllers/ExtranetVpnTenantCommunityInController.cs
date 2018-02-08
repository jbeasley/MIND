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
    public class ExtranetVpnTenantCommunityInController : BaseViewController
    {
        public ExtranetVpnTenantCommunityInController(IExtranetVpnTenantCommunityInService extranetVpnTenantCommunityInService,
            IExtranetVpnMemberService extranetVpnMemberService,
            IVpnTenantCommunityInService vpnTenantCommunityInService,
            IExtranetVpnTenantCommunityInValidator extranetVpnTenantCommunityInValidator,
            IMapper mapper)
        {
            ExtranetVpnTenantCommunityInService = extranetVpnTenantCommunityInService;
            VpnTenantCommunityInService = vpnTenantCommunityInService;
            ExtranetVpnMemberService = extranetVpnMemberService;
            ExtranetVpnTenantCommunityInValidator = extranetVpnTenantCommunityInValidator;
            this.Validator = extranetVpnTenantCommunityInValidator;
            Mapper = mapper;
        }

        private IExtranetVpnTenantCommunityInService ExtranetVpnTenantCommunityInService { get; }
        private IExtranetVpnMemberService ExtranetVpnMemberService { get; }
        private IVpnTenantCommunityInService VpnTenantCommunityInService { get; }
        private IMapper Mapper { get; }

        private IExtranetVpnTenantCommunityInValidator ExtranetVpnTenantCommunityInValidator { get; }


        [HttpGet]
        public async Task<PartialViewResult> VpnTenantCommunities(int vpnID, int tenantID)
        {
            var vpnTenantCommunities = await VpnTenantCommunityInService.GetAllByVpnIDAsync(vpnID, tenantID, 
                extranet: true, includeProperties: false);
            return PartialView(Mapper.Map<List<VpnTenantCommunityInViewModel>>(vpnTenantCommunities));
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

            var extranetVpnTenantCommunitiesIn = await ExtranetVpnTenantCommunityInService.GetAllByExtranetVpnMemberIDAsync(id.Value);
            ViewBag.ExtranetVpnMember = Mapper.Map<ExtranetVpnMemberViewModel>(extranetVpnMember);

            return View(Mapper.Map<List<ExtranetVpnTenantCommunityInViewModel>>(extranetVpnTenantCommunitiesIn));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await ExtranetVpnTenantCommunityInService.GetByIDAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            return View(Mapper.Map<ExtranetVpnTenantCommunityInViewModel>(item));
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
        public async Task<IActionResult> Create([Bind("TenantID,ExtranetVpnMemberID,VpnTenantCommunityInID")]
            ExtranetVpnTenantCommunityInViewModel extranetVpnTenantCommunityInModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var extranetVpnTenantCommunityIn = Mapper.Map<ExtranetVpnTenantCommunityIn>(extranetVpnTenantCommunityInModel);
                    await ExtranetVpnTenantCommunityInValidator.ValidateNewAsync(extranetVpnTenantCommunityIn);

                    if (ExtranetVpnTenantCommunityInValidator.ValidationDictionary.IsValid)
                    {
                        await ExtranetVpnTenantCommunityInService.AddAsync(extranetVpnTenantCommunityIn);

                        return RedirectToAction("GetAllByExtranetVpnMemberID", new
                        {
                            id = extranetVpnTenantCommunityIn.ExtranetVpnMemberID,
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

            var extranetVpnMember = await ExtranetVpnMemberService.GetByIDAsync(extranetVpnTenantCommunityInModel.ExtranetVpnMemberID);
            ViewBag.ExtranetVpnMember = Mapper.Map<ExtranetVpnMemberViewModel>(extranetVpnMember);
            await PopulateTenantsDropDownList(extranetVpnMember.MemberVpnID, extranetVpnTenantCommunityInModel.TenantID);
            await PopulateVpnTenantCommunitiesDropDownList(extranetVpnMember.MemberVpn.VpnID,
                extranetVpnMember.MemberVpn.TenantID, extranetVpnTenantCommunityInModel.ExtranetVpnTenantCommunityInID);

            return View(extranetVpnTenantCommunityInModel);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var extranetVpnTenantCommunityIn = await ExtranetVpnTenantCommunityInService.GetByIDAsync(id.Value);
            if (extranetVpnTenantCommunityIn == null)
            {
                return NotFound();
            }

            var extranetVpnMember = await ExtranetVpnMemberService.GetByIDAsync(extranetVpnTenantCommunityIn.ExtranetVpnMemberID);
            ViewBag.ExtranetVpnMember = Mapper.Map<ExtranetVpnMemberViewModel>(extranetVpnMember);

            return View(Mapper.Map<ExtranetVpnTenantCommunityInViewModel>(extranetVpnTenantCommunityIn));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id, int? extranetVpnMemberID, bool? concurrencyError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var extranetVpnTenantCommunityIn = await ExtranetVpnTenantCommunityInService.GetByIDAsync(id.Value);
            if (extranetVpnTenantCommunityIn == null)
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

            return View(Mapper.Map<ExtranetVpnTenantCommunityInViewModel>(extranetVpnTenantCommunityIn));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ExtranetVpnTenantCommunityInViewModel extranetVpnTenantCommunityInModel)
        {
            var extranetVpnTenantCommunityIn = await ExtranetVpnTenantCommunityInService.GetByIDAsync(extranetVpnTenantCommunityInModel.ExtranetVpnTenantCommunityInID);
            if (extranetVpnTenantCommunityIn == null)
            {
                return RedirectToAction("GetAllByExtranetVpnMemberID", new
                {
                    id = extranetVpnTenantCommunityInModel.ExtranetVpnMemberID
                });
            }

            try
            {
                await ExtranetVpnTenantCommunityInService.DeleteAsync(Mapper.Map<ExtranetVpnTenantCommunityIn>(extranetVpnTenantCommunityIn));

                return RedirectToAction("GetAllByExtranetVpnMemberID", new
                {
                    id = extranetVpnTenantCommunityIn.ExtranetVpnMemberID,
                    showWarningMessage = true
                });
            }

            catch (DbUpdateConcurrencyException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("Delete", new
                {
                    concurrencyError = true,
                    id = extranetVpnTenantCommunityInModel.ExtranetVpnTenantCommunityInID,
                    extranetVpnMemberID = extranetVpnTenantCommunityIn.ExtranetVpnMemberID
                });
            }
        }

        private async Task PopulateTenantsDropDownList(int vpnID, object selectedTenant = null)
        {
            var query = from communities in await VpnTenantCommunityInService.GetAllByVpnIDAsync(vpnID)
                        select communities.TenantCommunity.Tenant;

            var tenants = query.ToList().GroupBy(q => q.TenantID).Select(r => r.First());

            ViewBag.TenantID = new SelectList(Mapper.Map<List<TenantViewModel>>(tenants),
                "TenantID", "Name", selectedTenant);
        }

        private async Task PopulateVpnTenantCommunitiesDropDownList(int vpnID, int tenantID, object selectedVpnTenantCommunity = null)
        {
            var tenantCommunities = await VpnTenantCommunityInService.GetAllByVpnIDAsync(vpnID, tenantID, 
                extranet: true, includeProperties: false);

            var result = from communities in tenantCommunities.Select(x => 
                new { x.VpnTenantCommunityInID,  x.TenantCommunity.Name })
                select communities;

            ViewBag.VpnTenantCommunityInID = new SelectList(result,
                "VpnTenantCommunityInID", "Name", selectedVpnTenantCommunity);
        }
    }
}