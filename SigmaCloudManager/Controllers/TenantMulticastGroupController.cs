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
using System.Net;

namespace SCM.Controllers
{
    public class TenantMulticastGroupController : BaseViewController
    {
        public TenantMulticastGroupController(ITenantMulticastGroupService tenantMulticastGroupService,
            IVpnService vpnService,
            ITenantService tenantService,
            IMapper mapper,
            ITenantMulticastGroupValidator tenantMulticastGroupValidator)
        {
            TenantMulticastGroupService = tenantMulticastGroupService;
            VpnService = vpnService;
            TenantService = tenantService;
            Mapper = mapper;
            TenantMulticastGroupValidator = tenantMulticastGroupValidator;
            this.Validator = tenantMulticastGroupValidator;
        }

        private ITenantMulticastGroupService TenantMulticastGroupService { get; set; }
        private IVpnService VpnService { get; set; }
        private ITenantService TenantService { get; set; }
        private IMapper Mapper { get; set; }
        private ITenantMulticastGroupValidator TenantMulticastGroupValidator { get; set; }

        [HttpGet]
        public async Task<IActionResult> GetAllByTenantID(int? id, string searchString = "", bool showWarningMessage = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenantMulticastGroups = await TenantMulticastGroupService.GetAllByTenantIDAsync(id.Value, searchString);
            if (showWarningMessage)
            {
                ViewData["NetworkWarningMessage"] = $"VPNs require synchronisation with the network as a result of this update. "
                                + "Follow this <a href = '/Vpn/GetAll'>link</a> to the VPNs page.";
            }

            var tenant = await TenantService.GetByIDAsync(id.Value);
            ViewBag.Tenant = Mapper.Map<TenantViewModel>(tenant);

            return View(Mapper.Map<List<TenantMulticastGroupViewModel>>(tenantMulticastGroups));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await TenantMulticastGroupService.GetByIDAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            var tenant = await TenantService.GetByIDAsync(item.TenantID);
            ViewBag.Tenant = Mapper.Map<TenantViewModel>(tenant);

            return View(Mapper.Map<TenantMulticastGroupViewModel>(item));
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenant = await TenantService.GetByIDAsync(id.Value);
            ViewBag.Tenant = Mapper.Map<TenantViewModel>(tenant);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenantID,SourceAddress,SourceMask,GroupAddress,"
            + "GroupMask,IsSsmGroup,AllowExtranet")] TenantMulticastGroupViewModel tenantMulticastGroupModel)
        {

            IPNetwork sourceNetwork = null;
            if (tenantMulticastGroupModel.IsSsmGroup)
            {
                // Normalise the source address according to the network mask.
                // e.g. - 10.1.1.0 255.255.0.0 becomes 10.1.0.0 255.255.0.0

                if (!IPNetwork.TryParse(tenantMulticastGroupModel.SourceAddress, tenantMulticastGroupModel.SourceMask, out sourceNetwork))
                {
                    ModelState.AddModelError(String.Empty, "Error parsing source address and mask. Correct the values and try again.");
                }
            }

            // Normalise the group address according to the group mask.
            // e.g. - 233.1.1.0 255.255.0.0 becomes 233.1.0.0 255.255.0.0

            IPNetwork groupRange;
            if (!IPNetwork.TryParse(tenantMulticastGroupModel.GroupAddress, tenantMulticastGroupModel.GroupMask, out groupRange))
            {
                ModelState.AddModelError(String.Empty, "Error parsing group address and mask. Correct the values and try again.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (sourceNetwork != null)
                    {
                        tenantMulticastGroupModel.SourceAddress = sourceNetwork.Network.ToString();
                    }

                    tenantMulticastGroupModel.GroupAddress = groupRange.Network.ToString();

                    var tenantMulticastGroup = Mapper.Map<TenantMulticastGroup>(tenantMulticastGroupModel);
                    await TenantMulticastGroupValidator.ValidateNewAsync(tenantMulticastGroup);

                    if (TenantMulticastGroupValidator.ValidationDictionary.IsValid)
                    {
                        await TenantMulticastGroupService.AddAsync(Mapper.Map<TenantMulticastGroup>(tenantMulticastGroupModel));
                        return RedirectToAction("GetAllByTenantID", new
                        {
                            id = tenantMulticastGroupModel.TenantID
                        });
                    }
                }

                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.
                    ModelState.AddModelError("", "Unable to save changes. " +
                                "Try again, and if the problem persists " +
                                "see your system administrator.");
                }

                catch (ValidationFailureException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            var tenant = await TenantService.GetByIDAsync(tenantMulticastGroupModel.TenantID);
            ViewBag.Tenant = Mapper.Map<TenantViewModel>(tenant);

            return View(Mapper.Map<TenantMulticastGroupViewModel>(tenantMulticastGroupModel));
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenantMulticastGroup = await TenantMulticastGroupService.GetByIDAsync(id.Value);
            if (tenantMulticastGroup == null)
            {
                return NotFound();
            }

            var tenant = await TenantService.GetByIDAsync(tenantMulticastGroup.TenantID);
            ViewBag.Tenant = Mapper.Map<TenantViewModel>(tenant);

            return View(Mapper.Map<TenantMulticastGroupViewModel>(tenantMulticastGroup));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("TenantMulticastGroupID,TenantID,SourceAddress,SourceMask,GroupAddress,GroupMask," +
            "AllowExtranet,IsSsmGroup,RowVersion")] TenantMulticastGroupViewModel tenantMulticastGroupModel)
        {
            if (id != tenantMulticastGroupModel.TenantMulticastGroupID)
            {
                return NotFound();
            }

            var tenantMulticastGroup = await TenantMulticastGroupService.GetByIDAsync(id);
            if (tenantMulticastGroup == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. The Tenant Multicast Group was deleted by another user.");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    if (tenantMulticastGroupModel.IsSsmGroup)
                    {
                        // Normalise the source address according to the network mask.
                        // e.g. - 10.1.1.0 255.255.0.0 becomes 10.1.0.0 255.255.0.0

                        var sourceNetwork = IPNetwork.Parse(tenantMulticastGroupModel.SourceAddress, tenantMulticastGroupModel.SourceMask);
                        tenantMulticastGroupModel.SourceAddress = sourceNetwork.Network.ToString();
                    }

                    // Normalise the group address according to the group mask.
                    // e.g. - 233.1.1.0 255.255.0.0 becomes 233.1.0.0 255.255.0.0

                    var groupRange = IPNetwork.Parse(tenantMulticastGroupModel.GroupAddress, tenantMulticastGroupModel.GroupMask);
                    tenantMulticastGroupModel.GroupAddress = groupRange.Network.ToString();

                    var item = Mapper.Map<TenantMulticastGroup>(tenantMulticastGroupModel);
                    await TenantMulticastGroupValidator.ValidateChangesAsync(item);

                    if (TenantMulticastGroupValidator.ValidationDictionary.IsValid)
                    {
                        await TenantMulticastGroupService.UpdateAsync(item);

                        // Check if VPNs need re-sync to network as a result of the change to the Tenant Multicast Group
                        // and generate a message for the view if so

                        var vpns = await VpnService.GetAllByTenantMulticastGroupIDAsync(tenantMulticastGroup.TenantMulticastGroupID);
                       
                        return RedirectToAction("GetAllByTenantID", new
                        {
                            id = tenantMulticastGroup.TenantID,
                            showWarningMessage = vpns.Any()
                        });
                    }
                }
            }

            catch (DbUpdateConcurrencyException ex)
            {
                var exceptionEntry = ex.Entries.Single();

                var proposedSourceAddress = (string)exceptionEntry.Property("SourceAddress").CurrentValue;
                if (tenantMulticastGroup.SourceAddress != proposedSourceAddress)
                {
                    ModelState.AddModelError("SourceAddress", $"Current value: {tenantMulticastGroup.SourceAddress}");
                }

                var proposedSourceMask = (string)exceptionEntry.Property("SourceMask").CurrentValue;
                if (tenantMulticastGroup.SourceMask != proposedSourceMask)
                {
                    ModelState.AddModelError("SourceMask", $"Current value: {tenantMulticastGroup.SourceMask}");
                }

                var proposedGroupAddress = (string)exceptionEntry.Property("GroupAddress").CurrentValue;
                if (tenantMulticastGroup.GroupAddress != proposedGroupAddress)
                {
                    ModelState.AddModelError("GroupAddress", $"Current value: {tenantMulticastGroup.GroupAddress}");
                }

                var proposedGroupMask = (string)exceptionEntry.Property("GroupMask").CurrentValue;
                if (tenantMulticastGroup.GroupMask != proposedGroupMask)
                {
                    ModelState.AddModelError("GroupMask", $"Current value: {tenantMulticastGroup.GroupMask}");
                }

                var proposedAllowExtranet = (bool)exceptionEntry.Property("AllowExtranet").CurrentValue;
                if (tenantMulticastGroup.AllowExtranet != proposedAllowExtranet)
                {
                    ModelState.AddModelError("AllowExtranet", $"Current value: {tenantMulticastGroup.AllowExtranet}");
                }

                ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                    + "was modified by another user after you got the original value. The "
                    + "edit operation was cancelled and the current values in the database "
                    + "have been displayed. If you still want to edit this record, click "
                    + "the Save button again. Otherwise click the Back to List hyperlink.");

                ModelState.Remove("RowVersion");
            }

            catch (DbUpdateException /* ex */ )
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");

            }

            var tenant = await TenantService.GetByIDAsync(tenantMulticastGroup.TenantID);
            ViewBag.Tenant = Mapper.Map<TenantViewModel>(tenant);

            return View(Mapper.Map<TenantMulticastGroupViewModel>(tenantMulticastGroup));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id, bool? concurrencyError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenantMulticastGroup = await TenantMulticastGroupService.GetByIDAsync(id.Value);
            if (tenantMulticastGroup == null)
            {
                if (concurrencyError.GetValueOrDefault())
                {
                    return RedirectToAction("GetAllByTenantID", new
                    {
                        id = tenantMulticastGroup.TenantID
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

            var tenant = await TenantService.GetByIDAsync(tenantMulticastGroup.TenantID);
            ViewBag.Tenant = Mapper.Map<TenantViewModel>(tenant);

            return View(Mapper.Map<TenantMulticastGroupViewModel>(tenantMulticastGroup));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(TenantMulticastGroupViewModel tenantMulticastGroupModel)
        {
            var tenantMulticastGroup = await TenantMulticastGroupService.GetByIDAsync(tenantMulticastGroupModel.TenantMulticastGroupID);
            if (tenantMulticastGroup == null)
            {
                return RedirectToAction("GetAllByTenantID", new
                {
                    id = tenantMulticastGroupModel.TenantID
                });
            }

            try
            {
                this.Validator.ValidationDictionary.Clear();
                await TenantMulticastGroupValidator.ValidateDeleteAsync(tenantMulticastGroup);
                if (TenantMulticastGroupValidator.ValidationDictionary.IsValid)
                {
                    await TenantMulticastGroupService.DeleteAsync(Mapper.Map<TenantMulticastGroup>(tenantMulticastGroupModel));
                    return RedirectToAction("GetAllByTenantID", new
                    {
                        id = tenantMulticastGroupModel.TenantID
                    });
                }
            }

            catch (DbUpdateConcurrencyException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("Delete", new
                {
                    concurrencyError = true,
                    id = tenantMulticastGroupModel.TenantMulticastGroupID,
                    tenantID = tenantMulticastGroupModel.TenantID
                });
            }

            var tenant = await TenantService.GetByIDAsync(tenantMulticastGroup.TenantID);
            ViewBag.Tenant = Mapper.Map<TenantViewModel>(tenant);

            return View(Mapper.Map<TenantMulticastGroupViewModel>(tenantMulticastGroup));
        }
    }
}
