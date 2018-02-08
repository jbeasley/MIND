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
    public class VpnTenantNetworkRoutingInstanceController : BaseViewController
    {
        public VpnTenantNetworkRoutingInstanceController(ITenantService tenantService,
            IVpnTenantNetworkRoutingInstanceService vpnTenantNetworkRoutingInstanceService,
            IVpnService vpnService, 
            IAttachmentSetService attachmentSetService,
            ITenantNetworkService tenantNetworkService,
            IAttachmentSetRoutingInstanceService attachmentSetRoutingInstanceService,
            IRoutingInstanceService vrfService,
            IMapper mapper)
        {
            TenantService = tenantService;
            VpnTenantNetworkRoutingInstanceService = vpnTenantNetworkRoutingInstanceService;
            AttachmentSetService = attachmentSetService;
            TenantNetworkService = tenantNetworkService;
            VpnService = vpnService;
            AttachmentSetRoutingInstanceService = attachmentSetRoutingInstanceService;
            RoutingInstanceService = vrfService;
            Mapper = mapper;
        }

        private ITenantService TenantService { get; }
        private IVpnTenantNetworkRoutingInstanceService VpnTenantNetworkRoutingInstanceService { get; }
        private IAttachmentSetService AttachmentSetService { get; }
        private ITenantNetworkService TenantNetworkService { get; }
        private IAttachmentSetRoutingInstanceService AttachmentSetRoutingInstanceService { get; }
        private IRoutingInstanceService RoutingInstanceService { get; }
        private IVpnService VpnService { get; }
        private IMapper Mapper { get; }

        [HttpGet]
        public async Task<PartialViewResult> TenantNetworks(int tenantID)
        {
            var tenantNetworks = await TenantNetworkService.GetAllByTenantIDAsync(tenantID);
            return PartialView(Mapper.Map<List<TenantNetworkViewModel>>(tenantNetworks));
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

            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            var vpnTenantNetworkRoutingInstances = await VpnTenantNetworkRoutingInstanceService.GetAllByAttachmentSetIDAsync(id.Value);

            return View(Mapper.Map<List<VpnTenantNetworkRoutingInstanceViewModel>>(vpnTenantNetworkRoutingInstances));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await VpnTenantNetworkRoutingInstanceService.GetByIDAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            return View(Mapper.Map<VpnTenantNetworkRoutingInstanceViewModel>(item));
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

            return View(new VpnTenantNetworkRoutingInstanceViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenantID,TenantNetworkID,AttachmentSetID," +
            "RoutingInstanceID,LocalIpRoutingPreference")]
            VpnTenantNetworkRoutingInstanceViewModel vpnTenantNetworkRoutingInstanceModel)
        {
            try
            {
                var vpnTenantNetworkRoutingInstance = Mapper.Map<VpnTenantNetworkRoutingInstance>(vpnTenantNetworkRoutingInstanceModel);
                await VpnTenantNetworkRoutingInstanceService.AddAsync(vpnTenantNetworkRoutingInstance);

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

            var attachmentSet = await AttachmentSetService.GetByIDAsync(vpnTenantNetworkRoutingInstanceModel.AttachmentSetID);
            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            await PopulateTenantsDropDownList();
            await PopulateTenantNetworksDropDownList(vpnTenantNetworkRoutingInstanceModel.TenantID);
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

            var vpnTenantNetworkRoutingInstance = await VpnTenantNetworkRoutingInstanceService.GetByIDAsync(id.Value);
            if (vpnTenantNetworkRoutingInstance == null)
            {
                return NotFound();
            }

            var attachmentSet = await AttachmentSetService.GetByIDAsync(vpnTenantNetworkRoutingInstance.AttachmentSetID);
            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            await PopulateRoutingInstancesDropDownList(vpnTenantNetworkRoutingInstance.AttachmentSetID);

            return View(Mapper.Map<VpnTenantNetworkRoutingInstanceViewModel>(vpnTenantNetworkRoutingInstance));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("VpnTenantNetworkRoutingInstanceID,TenantNetworkID,RoutingInstanceID,AttachmentSetID,"
            + "LocalIpRoutingPreference,RowVersion")]
            VpnTenantNetworkRoutingInstanceViewModel updateModel)
        {
            if (id != updateModel.VpnTenantNetworkRoutingInstanceID)
            {
                return NotFound();
            }

            var currentVpnTenantNetworkRoutingInstance = await VpnTenantNetworkRoutingInstanceService.GetByIDAsync(updateModel.VpnTenantNetworkRoutingInstanceID);
            if (currentVpnTenantNetworkRoutingInstance == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. The item was deleted by another user.");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var vpnTenantNetworkRoutingInstanceUpdate = Mapper.Map<VpnTenantNetworkRoutingInstance>(updateModel);

                    await VpnTenantNetworkRoutingInstanceService.UpdateAsync(vpnTenantNetworkRoutingInstanceUpdate);
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

            var attachmentSet = await AttachmentSetService.GetByIDAsync(currentVpnTenantNetworkRoutingInstance.AttachmentSetID);
            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            await PopulateRoutingInstancesDropDownList(currentVpnTenantNetworkRoutingInstance.AttachmentSetID, updateModel.RoutingInstanceID);

            return View(Mapper.Map<VpnTenantNetworkRoutingInstanceViewModel>(currentVpnTenantNetworkRoutingInstance));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id, int? attachmentSetID, bool? concurrencyError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vpnTenantNetworkRoutingInstance = await VpnTenantNetworkRoutingInstanceService.GetByIDAsync(id.Value);
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

            return View(Mapper.Map<VpnTenantNetworkRoutingInstanceViewModel>(vpnTenantNetworkRoutingInstance));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(VpnTenantNetworkRoutingInstanceViewModel vpnTenantNetworkRoutingInstanceModel)
        {

            var vpnTenantNetworkRoutingInstance = await VpnTenantNetworkRoutingInstanceService.GetByIDAsync(vpnTenantNetworkRoutingInstanceModel.VpnTenantNetworkRoutingInstanceID);
            if (vpnTenantNetworkRoutingInstance == null)
            {
                return RedirectToAction("GetAllByAttachmentSetID", new
                {
                    id = vpnTenantNetworkRoutingInstanceModel.AttachmentSetID,
                });
            }

            try
            {
                await VpnTenantNetworkRoutingInstanceService.DeleteAsync(Mapper.Map<VpnTenantNetworkRoutingInstance>(vpnTenantNetworkRoutingInstanceModel));
           
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
                    id = vpnTenantNetworkRoutingInstanceModel.VpnTenantNetworkRoutingInstanceID,
                    attachmentSetID = vpnTenantNetworkRoutingInstance.AttachmentSetID
                });
            }
        }

        private async Task PopulateTenantsDropDownList(object selectedTenant = null)
        {
            var tenants = await TenantService.GetAllAsync();
            ViewBag.TenantID = new SelectList(Mapper.Map<List<TenantViewModel>>(tenants),
                "TenantID", "Name", selectedTenant);
        }

        /// <summary>
        /// Helper to populate the Tenant Networks drop-down list with all Tenant Networks for a given Tenant.
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="selectedTenantNetwork"></param>
        /// <returns></returns>
        private async Task PopulateTenantNetworksDropDownList(int tenantID, object selectedTenantNetwork = null)
        {
            var tenantNetworks = await TenantNetworkService.GetAllByTenantIDAsync(tenantID);
            ViewBag.TenantNetworkID = new SelectList(Mapper.Map<List<TenantNetworkViewModel>>(tenantNetworks),
                "TenantNetworkID", "CidrName", selectedTenantNetwork);
        }

        private async Task PopulateRoutingInstancesDropDownList(int attachmentSetID, object selectedRoutingInstance = null)
        {
            var vrfs = await RoutingInstanceService.GetAllByAttachmentSetIDAsync(attachmentSetID);
            ViewBag.RoutingInstanceID = new SelectList(Mapper.Map<List<RoutingInstanceViewModel>>(vrfs),
                "RoutingInstanceID", "Name", selectedRoutingInstance);
        }
    }
}
