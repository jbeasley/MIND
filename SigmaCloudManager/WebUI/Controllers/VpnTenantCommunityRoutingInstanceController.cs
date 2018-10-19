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
    public class VpnTenantCommunityRoutingInstanceController : BaseViewController
    {
        public VpnTenantCommunityRoutingInstanceController(ITenantService tenantService,
            IVpnTenantCommunityRoutingInstanceService vpnTenantCommunityRoutingInstanceService,
            IAttachmentSetService attachmentSetService,
            ITenantCommunityService tenantCommunityService,
            ITenantCommunitySetService tenantCommunitySetService,
            IVpnService vpnService,
            IAttachmentSetRoutingInstanceService attachmentSetRoutingInstanceService,
            IRoutingInstanceService vrfService,
            IMapper mapper)
        {
            TenantService = tenantService;
            VpnTenantCommunityRoutingInstanceService = vpnTenantCommunityRoutingInstanceService;
            AttachmentSetService = attachmentSetService;
            TenantCommunityService = tenantCommunityService;
            TenantCommunitySetService = tenantCommunitySetService;
            VpnService = vpnService;
            AttachmentSetRoutingInstanceService = attachmentSetRoutingInstanceService;
            RoutingInstanceService = vrfService;
            Mapper = mapper;
        }

        private ITenantService TenantService { get; }
        private IVpnTenantCommunityRoutingInstanceService VpnTenantCommunityRoutingInstanceService { get; }
        private IAttachmentSetService AttachmentSetService { get; }
        private ITenantCommunityService TenantCommunityService { get; }
        private ITenantCommunitySetService TenantCommunitySetService { get; }
        private IAttachmentSetRoutingInstanceService AttachmentSetRoutingInstanceService { get; }
        private IRoutingInstanceService RoutingInstanceService { get; }
        private IVpnService VpnService { get; }
        private IMapper Mapper { get; }

        [HttpGet]
        public async Task<PartialViewResult> TenantCommunities(int tenantID)
        {
            var tenantCommunities = await TenantCommunityService.GetAllByTenantIDAsync(tenantID);
            return PartialView(Mapper.Map<List<TenantCommunityViewModel>>(tenantCommunities));
        }

        [HttpGet]
        public async Task<PartialViewResult> TenantCommunitySets(int tenantID)
        {
            var tenantCommunitySets = await TenantCommunitySetService.GetAllByTenantIDAsync(tenantID);
            return PartialView(Mapper.Map<List<TenantCommunitySetViewModel>>(tenantCommunitySets));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByAttachmentSetID(int? id, bool? showWarningMessage = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attachmentSet = await AttachmentSetService.GetByIDAsync(id.Value);
            if (attachmentSet == null)
            {
                return NotFound();
            }

            if (showWarningMessage.GetValueOrDefault())
            {
                ViewData["NetworkWarningMessage"] = "The VPN requires synchronisation with the network as a result of this update. "
                        + "Follow this <a href = '/Vpn/GetAll'>link</a> to go to the VPNs page.";
            }

            var vpnTenantCommunities = await VpnTenantCommunityRoutingInstanceService.GetAllByAttachmentSetIDAsync(id.Value);
            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);

            return View(Mapper.Map<List<VpnTenantCommunityRoutingInstanceViewModel>>(vpnTenantCommunities));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await VpnTenantCommunityRoutingInstanceService.GetByIDAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            return View(Mapper.Map<VpnTenantCommunityRoutingInstanceViewModel>(item));
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attachmentSet = await AttachmentSetService.GetByIDAsync(id.Value);
            if (attachmentSet == null)
            {
                return NotFound();
            }

            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            await PopulateTenantsDropDownList();
            await PopulateRoutingInstancesDropDownList(attachmentSet.AttachmentSetID);

            return View(new VpnTenantCommunityRoutingInstanceViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenantID,TenantCommunityID,TenantCommunitySetID,AttachmentSetID,RoutingInstanceID," +
            "LocalIpRoutingPreference")]
            VpnTenantCommunityRoutingInstanceViewModel vpnTenantCommunityRoutingInstanceModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var vpnTenantCommunityRoutingInstance = Mapper.Map<VpnTenantCommunityRoutingInstance>(vpnTenantCommunityRoutingInstanceModel);
                    await VpnTenantCommunityRoutingInstanceService.AddAsync(vpnTenantCommunityRoutingInstance);

                    return RedirectToAction("GetAllByAttachmentSetID", new
                    {
                        id = vpnTenantCommunityRoutingInstance.AttachmentSetID,
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

            var attachmentSet = await AttachmentSetService.GetByIDAsync(vpnTenantCommunityRoutingInstanceModel.AttachmentSetID);
            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            await PopulateTenantsDropDownList();
            await PopulateTenantCommunitiesDropDownList(vpnTenantCommunityRoutingInstanceModel.TenantID, vpnTenantCommunityRoutingInstanceModel.TenantCommunityID);
            await PopulateTenantCommunitySetsDropDownList(vpnTenantCommunityRoutingInstanceModel.TenantID, vpnTenantCommunityRoutingInstanceModel.TenantCommunitySetID);
            await PopulateRoutingInstancesDropDownList(attachmentSet.AttachmentSetID, vpnTenantCommunityRoutingInstanceModel.RoutingInstanceID);

            return View(vpnTenantCommunityRoutingInstanceModel);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vpnTenantCommunityRoutingInstance = await VpnTenantCommunityRoutingInstanceService.GetByIDAsync(id.Value);
            if (vpnTenantCommunityRoutingInstance == null)
            {
                return NotFound();
            }

            var attachmentSet = await AttachmentSetService.GetByIDAsync(vpnTenantCommunityRoutingInstance.AttachmentSetID);
            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            await PopulateRoutingInstancesDropDownList(vpnTenantCommunityRoutingInstance.AttachmentSet.AttachmentSetID);

            return View(Mapper.Map<VpnTenantCommunityRoutingInstanceViewModel>(vpnTenantCommunityRoutingInstance));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("VpnTenantCommunityRoutingInstanceID,TenantCommunityID,TenantCommunitySetID,"
            + "RoutingInstanceID,AttachmentSetID,LocalIpRoutingPreference,RowVersion")]
            VpnTenantCommunityRoutingInstanceViewModel updateModel)
        {
            if (id != updateModel.VpnTenantCommunityRoutingInstanceID)
            {
                return NotFound();
            }

            var currentVpnTenantCommunityRoutingInstance = await VpnTenantCommunityRoutingInstanceService.GetByIDAsync(updateModel.VpnTenantCommunityRoutingInstanceID);
            if (currentVpnTenantCommunityRoutingInstance == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. The item was deleted by another user.");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var vpnTenantCommunityRoutingInstanceUpdate = Mapper.Map<VpnTenantCommunityRoutingInstance>(updateModel);

                    await VpnTenantCommunityRoutingInstanceService.UpdateAsync(vpnTenantCommunityRoutingInstanceUpdate);
                    return RedirectToAction("GetAllByAttachmentSetID", new
                    {
                        id = currentVpnTenantCommunityRoutingInstance.AttachmentSetID,
                        showWarningMessage = true
                    });
                }
            }

            catch (DbUpdateConcurrencyException ex)
            {
                var exceptionEntry = ex.Entries.Single();

                if (exceptionEntry.Property("RoutingInstanceID").CurrentValue != null)
                {
                    var proposedRoutingInstanceID = (int)exceptionEntry.Property("RoutingInstanceID").CurrentValue;
                    if (currentVpnTenantCommunityRoutingInstance.RoutingInstanceID != proposedRoutingInstanceID)
                    {
                        ModelState.AddModelError("RoutingInstanceID", $"Current value: {currentVpnTenantCommunityRoutingInstance.RoutingInstance.Name}");
                    }
                }

                var proposedLocalIpRoutingPreference = (int?)exceptionEntry.Property("LocalIpRoutingPreference").CurrentValue;
                if (currentVpnTenantCommunityRoutingInstance.LocalIpRoutingPreference != proposedLocalIpRoutingPreference)
                {
                    ModelState.AddModelError("LocalRoutingPreference", $"Current value: {currentVpnTenantCommunityRoutingInstance.LocalIpRoutingPreference}");
                }

                ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                    + "was modified by another user after you got the original value. The "
                    + "edit operation was cancelled and the current values in the database "
                    + "have been displayed. If you still want to edit this record, click "
                    + "the Save button again. Otherwise click the Back to List hyperlink.");

                ModelState.Remove("RowVersion");
            }

            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");

            }

            var attachmentSet = await AttachmentSetService.GetByIDAsync(currentVpnTenantCommunityRoutingInstance.AttachmentSetID);
            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            await PopulateRoutingInstancesDropDownList(currentVpnTenantCommunityRoutingInstance.AttachmentSet.AttachmentSetID, updateModel.RoutingInstanceID);

            return View(Mapper.Map<VpnTenantCommunityRoutingInstanceViewModel>(currentVpnTenantCommunityRoutingInstance));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id, int? attachmentSetID, bool? concurrencyError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vpnTenantCommunityRoutingInstance = await VpnTenantCommunityRoutingInstanceService.GetByIDAsync(id.Value);
            if (vpnTenantCommunityRoutingInstance == null)
            {
                if (concurrencyError.GetValueOrDefault())
                {
                    return RedirectToAction("GetAllByAttachmentSetID", new { id = attachmentSetID });
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

            return View(Mapper.Map<VpnTenantCommunityRoutingInstanceViewModel>(vpnTenantCommunityRoutingInstance));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(VpnTenantCommunityRoutingInstanceViewModel vpnTenantCommunityRoutingInstanceModel)
        {
            var vpnTenantCommunityRoutingInstance = await VpnTenantCommunityRoutingInstanceService.GetByIDAsync(vpnTenantCommunityRoutingInstanceModel.VpnTenantCommunityRoutingInstanceID);
            if (vpnTenantCommunityRoutingInstance == null)
            {
                return RedirectToAction("GetAllByAttachmentSetID", new
                {
                    id = vpnTenantCommunityRoutingInstanceModel.AttachmentSetID
                });
            }

            try
            {
                await VpnTenantCommunityRoutingInstanceService.DeleteAsync(Mapper.Map<VpnTenantCommunityRoutingInstance>(vpnTenantCommunityRoutingInstance));

                return RedirectToAction("GetAllByAttachmentSetID", new
                {
                    id = vpnTenantCommunityRoutingInstance.AttachmentSetID,
                    showWarningMessage = true
                });
            }

            catch (DbUpdateConcurrencyException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("Delete", new
                {
                    concurrencyError = true,
                    id = vpnTenantCommunityRoutingInstanceModel.VpnTenantCommunityRoutingInstanceID,
                    attachmentSetID = vpnTenantCommunityRoutingInstance.AttachmentSetID
                });
            }
        }

        private async Task PopulateTenantsDropDownList(object selectedTenant = null)
        {
            var tenants = await TenantService.GetAllAsync();
            ViewBag.TenantID = new SelectList(Mapper.Map<List<TenantViewModel>>(tenants),
                "TenantID", "Name", selectedTenant);
        }

        private async Task PopulateTenantCommunitiesDropDownList(int tenantID, object selectedTenantCommunity = null)
        {
            var tenantCommunities = await TenantCommunityService.GetAllByTenantIDAsync(tenantID);
            ViewBag.TenantCommunityID = new SelectList(Mapper.Map<List<TenantCommunityViewModel>>(tenantCommunities), 
                "TenantCommunityID", "Name", selectedTenantCommunity);
        }

        private async Task PopulateTenantCommunitySetsDropDownList(int tenantID, object selectedTenantCommunitySet = null)
        {
            var tenantCommunitySets = await TenantCommunitySetService.GetAllByTenantIDAsync(tenantID);
            ViewBag.TenantCommunitySetID = new SelectList(Mapper.Map<List<TenantCommunitySetViewModel>>(tenantCommunitySets),
                "TenantCommunitySetID", "Name", selectedTenantCommunitySet);
        }

        private async Task PopulateRoutingInstancesDropDownList(int attachmentSetID, object selectedRoutingInstance = null)
        {
            var vrfs = await RoutingInstanceService.GetAllByAttachmentSetIDAsync(attachmentSetID);
            ViewBag.RoutingInstanceID = new SelectList(Mapper.Map<List<RoutingInstanceViewModel>>(vrfs),
                "RoutingInstanceID", "Name", selectedRoutingInstance);
        }
    }
}
