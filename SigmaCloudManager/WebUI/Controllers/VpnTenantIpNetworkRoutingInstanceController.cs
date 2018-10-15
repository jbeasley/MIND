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

namespace SCM.Controllers
{
    public class VpnTenantNetworkRoutingInstanceController : BaseViewController
    {
        private readonly ITenantService _tenantService;
        private readonly IVpnTenantIpNetworkRoutingInstanceService _vpnTenantNetworkRoutingInstanceService;
        private readonly IAttachmentSetService _attachmentSetService;
        private readonly ITenantIpNetworkService _tenantIpNetworkService;
        private readonly IAttachmentSetRoutingInstanceService _attachmentSetRoutingInstanceService;
        private readonly IRoutingInstanceService _routingInstanceService;
        private readonly IVpnService _vpnService;

        public VpnTenantNetworkRoutingInstanceController(ITenantService tenantService,
            IVpnTenantIpNetworkRoutingInstanceService vpnTenantIpNetworkRoutingInstanceService,
            IVpnService vpnService, 
            IAttachmentSetService attachmentSetService,
            ITenantIpNetworkService tenantIpNetworkService,
            IAttachmentSetRoutingInstanceService attachmentSetRoutingInstanceService,
            IRoutingInstanceService vrfService,
            IMapper mapper) : base(vpnTenantIpNetworkRoutingInstanceService, mapper)
        {
            _tenantService = tenantService;
            _vpnTenantNetworkRoutingInstanceService = vpnTenantIpNetworkRoutingInstanceService;
            _attachmentSetService = attachmentSetService;
            _tenantIpNetworkService = tenantIpNetworkService;
            _vpnService = vpnService;
            _attachmentSetRoutingInstanceService = attachmentSetRoutingInstanceService;
            _routingInstanceService = vrfService;
        }

        [HttpGet]
        public async Task<PartialViewResult> TenantIpNetworks(int tenantID)
        {
            var tenantIpNetworks = await _tenantIpNetworkService.GetAllByTenantIDAsync(tenantID);
            return PartialView(Mapper.Map<List<TenantIpNetworkViewModel>>(tenantIpNetworks));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByAttachmentSetID(int? id, bool? showWarningMessage = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attachmentSet = await _attachmentSetService.GetByIDAsync(id.Value);
            if (attachmentSet == null)
            {
                return NotFound();
            }

            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            var vpnTenantNetworkRoutingInstances = await _vpnTenantNetworkRoutingInstanceService.GetAllByAttachmentSetIDAsync(id.Value);

            return View(Mapper.Map<List<VpnTenantIpNetworkRoutingInstanceViewModel>>(vpnTenantNetworkRoutingInstances));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _vpnTenantNetworkRoutingInstanceService.GetByIDAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            return View(Mapper.Map<VpnTenantIpNetworkRoutingInstanceViewModel>(item));
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attachmentSet = await _attachmentSetService.GetByIDAsync(id.Value);
            if (attachmentSet == null)
            {
                return NotFound();
            }

            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            await PopulateTenantsDropDownList();
            await PopulateRoutingInstancesDropDownList(attachmentSet.AttachmentSetID);

            return View(new VpnTenantIpNetworkRoutingInstanceViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenantID,TenantIpNetworkID,AttachmentSetID," +
            "RoutingInstanceID,LocalIpRoutingPreference")]
            VpnTenantIpNetworkRoutingInstanceViewModel vpnTenantNetworkRoutingInstanceModel)
        {
            try
            {
                var vpnTenantNetworkRoutingInstance = Mapper.Map<VpnTenantIpNetworkRoutingInstance>(vpnTenantNetworkRoutingInstanceModel);
                await _vpnTenantNetworkRoutingInstanceService.AddAsync(vpnTenantNetworkRoutingInstance);

                return RedirectToAction("GetAllByAttachmentSetID", new
                {
                    id = vpnTenantNetworkRoutingInstance.AttachmentSetID,
                    showWarningMessage = true
                });
            }

            catch (DbUpdateException /** ex **/ )
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            var attachmentSet = await _attachmentSetService.GetByIDAsync(vpnTenantNetworkRoutingInstanceModel.AttachmentSetID);
            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            await PopulateTenantsDropDownList();
            await PopulateTenantIpNetworksDropDownList(vpnTenantNetworkRoutingInstanceModel.TenantID);
            await PopulateRoutingInstancesDropDownList(attachmentSet.AttachmentSetID, vpnTenantNetworkRoutingInstanceModel.RoutingInstanceID);

            return View(vpnTenantNetworkRoutingInstanceModel);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vpnTenantNetworkRoutingInstance = await _vpnTenantNetworkRoutingInstanceService.GetByIDAsync(id.Value);
            if (vpnTenantNetworkRoutingInstance == null)
            {
                return NotFound();
            }

            var attachmentSet = await _attachmentSetService.GetByIDAsync(vpnTenantNetworkRoutingInstance.AttachmentSetID);
            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            await PopulateRoutingInstancesDropDownList(vpnTenantNetworkRoutingInstance.AttachmentSetID);

            return View(Mapper.Map<VpnTenantIpNetworkRoutingInstanceViewModel>(vpnTenantNetworkRoutingInstance));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("VpnTenantIpNetworkRoutingInstanceID,TenantIpNetworkID,RoutingInstanceID,AttachmentSetID,"
            + "LocalIpRoutingPreference,RowVersion")]
            VpnTenantIpNetworkRoutingInstanceViewModel updateModel)
        {
            if (id != updateModel.VpnTenantIpNetworkRoutingInstanceID)
            {
                return NotFound();
            }

            var currentVpnTenantNetworkRoutingInstance = await _vpnTenantNetworkRoutingInstanceService.GetByIDAsync(updateModel.VpnTenantIpNetworkRoutingInstanceID);
            if (currentVpnTenantNetworkRoutingInstance == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. The item was deleted by another user.");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var vpnTenantNetworkRoutingInstanceUpdate = Mapper.Map<VpnTenantIpNetworkRoutingInstance>(updateModel);

                    await _vpnTenantNetworkRoutingInstanceService.UpdateAsync(vpnTenantNetworkRoutingInstanceUpdate);
                    return RedirectToAction("GetAllByAttachmentSetID", new
                    {
                        id = currentVpnTenantNetworkRoutingInstance.AttachmentSetID,
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
                    if (currentVpnTenantNetworkRoutingInstance.RoutingInstanceID != proposedRoutingInstanceID)
                    {
                        ModelState.AddModelError("RoutingInstanceID", $"Current value: {currentVpnTenantNetworkRoutingInstance.RoutingInstance.Name}");
                    }
                }

                var proposedLocalIpRoutingPreference = (int?)exceptionEntry.Property("LocalIpRoutingPreference").CurrentValue;
                if (currentVpnTenantNetworkRoutingInstance.LocalIpRoutingPreference != proposedLocalIpRoutingPreference)
                {
                    ModelState.AddModelError("LocalRoutingPreference", $"Current value: {currentVpnTenantNetworkRoutingInstance.LocalIpRoutingPreference}");
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

            var attachmentSet = await _attachmentSetService.GetByIDAsync(currentVpnTenantNetworkRoutingInstance.AttachmentSetID);
            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            await PopulateRoutingInstancesDropDownList(currentVpnTenantNetworkRoutingInstance.AttachmentSetID, updateModel.RoutingInstanceID);

            return View(Mapper.Map<VpnTenantIpNetworkRoutingInstanceViewModel>(currentVpnTenantNetworkRoutingInstance));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id, int? attachmentSetID, bool? concurrencyError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vpnTenantNetworkRoutingInstance = await _vpnTenantNetworkRoutingInstanceService.GetByIDAsync(id.Value);
            if (vpnTenantNetworkRoutingInstance == null)
            {
                if (concurrencyError.GetValueOrDefault())
                {
                    return RedirectToAction("GetAllByAttachmentSetID", new
                    {
                        id = attachmentSetID
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

            return View(Mapper.Map<VpnTenantIpNetworkRoutingInstanceViewModel>(vpnTenantNetworkRoutingInstance));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(VpnTenantIpNetworkRoutingInstanceViewModel vpnTenantNetworkRoutingInstanceModel)
        {

            var vpnTenantNetworkRoutingInstance = await _vpnTenantNetworkRoutingInstanceService.GetByIDAsync(vpnTenantNetworkRoutingInstanceModel.VpnTenantIpNetworkRoutingInstanceID);
            if (vpnTenantNetworkRoutingInstance == null)
            {
                return RedirectToAction("GetAllByAttachmentSetID", new
                {
                    id = vpnTenantNetworkRoutingInstanceModel.AttachmentSetID,
                });
            }

            try
            {
                await _vpnTenantNetworkRoutingInstanceService.DeleteAsync(vpnTenantNetworkRoutingInstance.VpnTenantIpNetworkRoutingInstanceID);
           
                return RedirectToAction("GetAllByAttachmentSetID", new
                {
                    id = vpnTenantNetworkRoutingInstance.AttachmentSetID,
                    showWarningMessage = true
                });
            }

            catch (DbUpdateConcurrencyException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("Delete", new
                {
                    concurrencyError = true,
                    id = vpnTenantNetworkRoutingInstanceModel.VpnTenantIpNetworkRoutingInstanceID,
                    attachmentSetID = vpnTenantNetworkRoutingInstance.AttachmentSetID
                });
            }
        }

        private async Task PopulateTenantsDropDownList(object selectedTenant = null)
        {
            var tenants = await _tenantService.GetAllAsync();
            ViewBag.TenantID = new SelectList(Mapper.Map<List<TenantViewModel>>(tenants),
                "TenantID", "Name", selectedTenant);
        }

        /// <summary>
        /// Helper to populate the Tenant IP Networks drop-down list with all Tenant IP Networks for a given Tenant.
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="selectedTenantIpNetwork"></param>
        /// <returns></returns>
        private async Task PopulateTenantIpNetworksDropDownList(int tenantID, object selectedTenantIpNetwork = null)
        {
            var tenantNetworks = await _tenantIpNetworkService.GetAllByTenantIDAsync(tenantID);
            ViewBag.TenantNetworkID = new SelectList(Mapper.Map<List<TenantIpNetworkViewModel>>(tenantNetworks),
                "TenantIpNetworkID", "CidrName", selectedTenantIpNetwork);
        }

        private async Task PopulateRoutingInstancesDropDownList(int attachmentSetID, object selectedRoutingInstance = null)
        {
            var routingInstances = await _routingInstanceService.GetAllByAttachmentSetIDAsync(attachmentSetID);
            ViewBag.RoutingInstanceID = new SelectList(Mapper.Map<List<RoutingInstanceViewModel>>(routingInstances),
                "RoutingInstanceID", "Name", selectedRoutingInstance);
        }
    }
}
