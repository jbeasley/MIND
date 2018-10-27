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
    public class VpnTenantMulticastGroupController : BaseViewController
    {
        public VpnTenantMulticastGroupController(ITenantMulticastGroupService tenantMulticastGroupService,
            IAttachmentSetService attachmentSetService,
            IVpnTenantMulticastGroupService vpnTenantMulticastGroupService,
            IMulticastVpnRpService multicastVpnRpService,
            IMulticastGeographicalScopeService multicastGeographicalScopeService,
            IMapper mapper,
            IVpnTenantMulticastGroupValidator multicastVpnGroupValidator)
        {
            TenantMulticastGroupService = tenantMulticastGroupService;
            AttachmentSetService = attachmentSetService;
            VpnTenantMulticastGroupService = vpnTenantMulticastGroupService;
            MulticastVpnRpService = multicastVpnRpService;
            MulticastGeographicalScopeService = multicastGeographicalScopeService;
            Mapper = mapper;

            VpnTenantMulticastGroupValidator = multicastVpnGroupValidator;
            this.Validator = multicastVpnGroupValidator;
        }

        private ITenantMulticastGroupService TenantMulticastGroupService { get; }
        private IAttachmentSetService AttachmentSetService { get; set; }
        private IVpnTenantMulticastGroupService VpnTenantMulticastGroupService { get; }
        private IMulticastVpnRpService MulticastVpnRpService { get; }
        private IVpnTenantMulticastGroupValidator VpnTenantMulticastGroupValidator { get; }
        private IMulticastGeographicalScopeService MulticastGeographicalScopeService { get; }
        private IMapper Mapper { get; }

        [HttpGet]
        public async Task<IActionResult> GetAllByAttachmentSetID(int? id, bool showWarning = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (showWarning)
            {
                ViewData["NetworkWarningMessage"] = "The VPN requires synchronisation with the network as a result of this update. "
                            + "Follow this <a href = '/Vpn/GetAll'>link</a> to go to the VPNs page.";
            }

            var attachmentSet = await AttachmentSetService.GetByIDAsync(id.Value);
            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            var multicastVpnGroups = await VpnTenantMulticastGroupService.GetAllByAttachmentSetIDAsync(id.Value);

            return View(Mapper.Map<List<VpnTenantMulticastGroupViewModel>>(multicastVpnGroups));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await VpnTenantMulticastGroupService.GetByIDAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            var attachmentSet = await AttachmentSetService.GetByIDAsync(item.AttachmentSetID);
            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);

            return View(Mapper.Map<VpnTenantMulticastGroupViewModel>(item));
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
            await PopulateMulticastVpnRpsDropDownList(attachmentSet.AttachmentSetID);
            await PopulateTenantMulticastGroupRangesDropDownList(attachmentSet.TenantID);
            await PopulateMulticastGeographicalScopesDropDownList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AttachmentSetID,MulticastVpnRpID,TenantMulticastGroupID,MulticastGeographicalScopeID")]
        VpnTenantMulticastGroupViewModel vpnTenantMulticastGroupModel)
        {
            if (ModelState.IsValid)
            {
                var vpnTenantMulticastGroup = Mapper.Map<VpnTenantMulticastGroup>(vpnTenantMulticastGroupModel);
                await VpnTenantMulticastGroupValidator.ValidateNewAsync(vpnTenantMulticastGroup);
                if (VpnTenantMulticastGroupValidator.ValidationDictionary.IsValid)
                {
                    try
                    {
                        await VpnTenantMulticastGroupService.AddAsync(vpnTenantMulticastGroup);
                        return RedirectToAction("GetAllByAttachmentSetID", new
                        {
                            id = vpnTenantMulticastGroup.AttachmentSetID,
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
                }
            }

            var attachmentSet = await AttachmentSetService.GetByIDAsync(vpnTenantMulticastGroupModel.AttachmentSetID);
            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            await PopulateMulticastVpnRpsDropDownList(attachmentSet.AttachmentSetID);
            await PopulateTenantMulticastGroupRangesDropDownList(attachmentSet.TenantID);
            await PopulateMulticastGeographicalScopesDropDownList();

            return View("Create", vpnTenantMulticastGroupModel);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vpnTenantMulticastGroup = await VpnTenantMulticastGroupService.GetByIDAsync(id.Value);
            if (vpnTenantMulticastGroup == null)
            {
                return NotFound();
            }

            var attachmentSet = await AttachmentSetService.GetByIDAsync(vpnTenantMulticastGroup.AttachmentSetID);
            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            await PopulateMulticastGeographicalScopesDropDownList(vpnTenantMulticastGroup.MulticastGeographicalScopeID);
            await PopulateMulticastVpnRpsDropDownList(attachmentSet.AttachmentSetID);

            return View(Mapper.Map<VpnTenantMulticastGroupViewModel>(vpnTenantMulticastGroup));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("VpnTenantMulticastGroupID,TenantMulticastGroupID,AttachmentSetID,"
            + "MulticastVpnRpID,MulticastGeographicalScopeID,RowVersion")] VpnTenantMulticastGroupViewModel updateModel)
        {
            if (id != updateModel.VpnTenantMulticastGroupID)
            {
                return NotFound();
            }

            var currentVpnTenantMulticastGroup = await VpnTenantMulticastGroupService.GetByIDAsync(updateModel.VpnTenantMulticastGroupID);
            if (currentVpnTenantMulticastGroup == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. The item was deleted by another user.");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var vpnTenantMulticastGroupUpdate = Mapper.Map<VpnTenantMulticastGroup>(updateModel);
                    await VpnTenantMulticastGroupValidator.ValidateChangesAsync(vpnTenantMulticastGroupUpdate);
                    if (Validator.ValidationDictionary.IsValid)
                    {
                        await VpnTenantMulticastGroupService.UpdateAsync(vpnTenantMulticastGroupUpdate);
                        return RedirectToAction("GetAllByAttachmentSetID", new
                        {
                            id = currentVpnTenantMulticastGroup.AttachmentSetID
                        });
                    }
                }
            }

            catch (DbUpdateConcurrencyException ex)
            {
                var exceptionEntry = ex.Entries.Single();

                if (exceptionEntry.Property("MulticastVpnRpID").CurrentValue != null)
                {
                    var proposedMulticastVpnRpID = (int)exceptionEntry.Property("MulticastVpnRpID").CurrentValue;
                    if (currentVpnTenantMulticastGroup.MulticastVpnRpID != proposedMulticastVpnRpID)
                    {
                        ModelState.AddModelError("MulticastVpnRpID", $"Current value: {currentVpnTenantMulticastGroup.MulticastVpnRp.IpAddress}");
                    }
                }

                if (exceptionEntry.Property("MulticastGeographicalScopeID").CurrentValue != null)
                {
                    var proposedMulticastGeographicalScopeID = (int)exceptionEntry.Property("MulticastGeographicalScopeID").CurrentValue;
                    if (currentVpnTenantMulticastGroup.MulticastGeographicalScopeID != proposedMulticastGeographicalScopeID)
                    {
                        ModelState.AddModelError("MulticastGeographicalScopeID", $"Current value: {currentVpnTenantMulticastGroup.MulticastGeographicalScope.Name}");
                    }
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

            var attachmentSet = await AttachmentSetService.GetByIDAsync(currentVpnTenantMulticastGroup.AttachmentSetID);
            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            await PopulateMulticastGeographicalScopesDropDownList(currentVpnTenantMulticastGroup.MulticastGeographicalScopeID);
            await PopulateMulticastVpnRpsDropDownList(attachmentSet.AttachmentSetID);

            return View(Mapper.Map<VpnTenantMulticastGroupViewModel>(currentVpnTenantMulticastGroup));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id, int? attachmentSetID, bool? concurrencyError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vpnTenantMulticastGroup = await VpnTenantMulticastGroupService.GetByIDAsync(id.Value);
            if (vpnTenantMulticastGroup == null)
            {
                if (concurrencyError.GetValueOrDefault())
                {
                    return RedirectToAction("GetAllByAttachmentSetID", new
                    {
                        id = attachmentSetID,
                        showWarning = true
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

            var attachmentSet = await AttachmentSetService.GetByIDAsync(vpnTenantMulticastGroup.AttachmentSetID);
            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);

            return View(Mapper.Map<VpnTenantMulticastGroupViewModel>(vpnTenantMulticastGroup));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(VpnTenantMulticastGroupViewModel vpnTenantMulticastGroupModel)
        {
            var vpnTenantMulticastGroup = await VpnTenantMulticastGroupService.GetByIDAsync(vpnTenantMulticastGroupModel.VpnTenantMulticastGroupID);
            if (vpnTenantMulticastGroup == null)
            {
                return RedirectToAction("GetAllByAttachmentSetID", new
                {
                    id = vpnTenantMulticastGroupModel.AttachmentSetID
                });
            }

            try
            {
                await VpnTenantMulticastGroupService.DeleteAsync(vpnTenantMulticastGroup);
                return RedirectToAction("GetAllByAttachmentSetID", new
                {
                    id = vpnTenantMulticastGroup.AttachmentSetID,
                    showWarning = true
                });
            }

            catch (DbUpdateConcurrencyException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("Delete", new
                {
                    concurrencyError = true,
                    id = vpnTenantMulticastGroupModel.VpnTenantMulticastGroupID,
                    attachmentSetID = vpnTenantMulticastGroup.AttachmentSetID
                });
            }

            catch (Exception /** ex **/)
            {
                ViewData["ErrorMessage"] = "Failed to complete this request. "
                    + "This most likely happened because the network server is not available. "
                    + "Try again later or contact your system administrator.";

                var attachmentSet = await AttachmentSetService.GetByIDAsync(vpnTenantMulticastGroup.AttachmentSetID);
                ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);

                return View(Mapper.Map<VpnTenantMulticastGroupViewModel>(vpnTenantMulticastGroup));
            }
        }

        private async Task PopulateTenantMulticastGroupRangesDropDownList(int tenantID, object selectedTenantMulticastGroupRange = null)
        {
            var tenantMulticastGroupRanges = await TenantMulticastGroupService.GetAllByTenantIDAsync(tenantID);
            ViewBag.TenantMulticastGroupID = new SelectList(Mapper.Map<List<TenantMulticastGroupViewModel>>(tenantMulticastGroupRanges), 
                "TenantMulticastGroupID", "Name", selectedTenantMulticastGroupRange);
        }

        private async Task PopulateMulticastVpnRpsDropDownList(int attachmentSetID, object selectedMulticastVpnRp = null)
        {
            var multicastVpnRps = await MulticastVpnRpService.GetAllByAttachmentSetIDAsync(attachmentSetID);
            ViewBag.MulticastVpnRpID = new SelectList(Mapper.Map<List<MulticastVpnRpViewModel>>(multicastVpnRps), 
                "MulticastVpnRpID", "IpAddress", selectedMulticastVpnRp);
        }

        private async Task PopulateMulticastGeographicalScopesDropDownList(object selectedMulticastGeographicalScope = null)
        {
            var multicastGeographicalScopes = await MulticastGeographicalScopeService.GetAllAsync();
            ViewBag.MulticastGeographicalScopeID = new SelectList(Mapper.Map<List<MulticastGeographicalScopeViewModel>>(multicastGeographicalScopes), 
                "MulticastGeographicalScopeID", "Name", selectedMulticastGeographicalScope);
        }
    }
}
