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
    public class VpnTenantIpNetworkOutController : BaseViewController
    {

        private readonly ITenantService _tenantService;
        private readonly IVpnTenantIpNetworkOutService _vpnTenantIpNetworkOutService;
        private readonly IAttachmentSetService _attachmentSetService;
        private readonly ITenantIpNetworkService _tenantIpNetworkService;
        private readonly IAttachmentSetRoutingInstanceService _attachmentSetRoutingInstanceService;
        private readonly IRoutingInstanceService _routingInstanceService;
        private readonly IVpnService _vpnService;
        private readonly IBgpPeerService _bgpPeerService;

        public VpnTenantIpNetworkOutController(ITenantService tenantService,
            IVpnTenantIpNetworkOutService vpnTenantIpNetworkOutService,
            IVpnService vpnService,
            IAttachmentSetService attachmentSetService,
            ITenantIpNetworkService tenantIpNetworkService,
            IAttachmentSetRoutingInstanceService attachmentSetRoutingInstanceService,
            IRoutingInstanceService routingInstanceService,
            IBgpPeerService bgpPeerService,
            IMapper mapper) : base(vpnTenantIpNetworkOutService, mapper)
        {
            _tenantService = tenantService;
            _vpnTenantIpNetworkOutService = vpnTenantIpNetworkOutService;
            _attachmentSetService = attachmentSetService;
            _tenantIpNetworkService = tenantIpNetworkService;
            _vpnService = vpnService;
            _attachmentSetRoutingInstanceService = attachmentSetRoutingInstanceService;
            _routingInstanceService = routingInstanceService;
            _bgpPeerService = bgpPeerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByAttachmentSetID(int? id, bool? showWarningMessage = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attachmentSet = await _attachmentSetService.GetByIDAsync(id.Value, deep: true);
            if (attachmentSet == null)
            {
                return NotFound();
            }

            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            var vpnTenantNetworkOuts = await _vpnTenantIpNetworkOutService.GetAllByAttachmentSetIDAsync(id.Value);

            return View(Mapper.Map<List<VpnTenantIpNetworkOutViewModel>>(vpnTenantNetworkOuts));
        }


        [HttpGet]
        public async Task<PartialViewResult> TenantNetworks(int tenantID)
        {
            var tenantNetworks = await _tenantIpNetworkService.GetAllByTenantIDAsync(tenantID);
            return PartialView(Mapper.Map<List<TenantIpNetworkViewModel>>(tenantNetworks));
        }

        [HttpGet]
        public async Task<PartialViewResult> BgpPeers(int? routingInstanceID)
        {
            var bgpPeers = new List<BgpPeer>();

            if (routingInstanceID != null)
            {
                var result = await _bgpPeerService.GetAllByRoutingInstanceIDAsync(routingInstanceID.Value);
                bgpPeers = result.ToList();
            }

            return PartialView(Mapper.Map<List<BgpPeerViewModel>>(bgpPeers));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _vpnTenantIpNetworkOutService.GetByIDAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            return View(Mapper.Map<VpnTenantIpNetworkOutViewModel>(item));
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attachmentSet = await _attachmentSetService.GetByIDAsync(id.Value, deep: true);
            if (attachmentSet == null)
            {
                return NotFound();
            }

            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            await PopulateTenantsDropDownList();
            await PopulateRoutingInstancesDropDownList(attachmentSet.AttachmentSetID);

            return View(new VpnTenantIpNetworkOutViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenantID,TenantIpNetworkID,AttachmentSetID," +
            "AttachmentSetRoutingInstanceID,BgpPeerID,AdvertisedIpRoutingPreference")]
            VpnTenantIpNetworkOutViewModel vpnTenantNetworkOutModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var vpnTenantNetworkOut = Mapper.Map<VpnTenantIpNetworkOut>(vpnTenantNetworkOutModel);
                    await _vpnTenantIpNetworkOutService.AddAsync(vpnTenantNetworkOut);

                    return RedirectToAction("GetAllByAttachmentSetID", new
                    {
                        id = vpnTenantNetworkOut.AttachmentSetID,
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

            var attachmentSet = await _attachmentSetService.GetByIDAsync(vpnTenantNetworkOutModel.AttachmentSetID, deep: true);
            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            await PopulateTenantsDropDownList();
            await PopulateTenantIpNetworksDropDownList(vpnTenantNetworkOutModel.TenantID);
            await PopulateRoutingInstancesDropDownList(attachmentSet.AttachmentSetID, vpnTenantNetworkOutModel.RoutingInstanceID);

            var routingInstance = await _attachmentSetRoutingInstanceService.GetByIDAsync(vpnTenantNetworkOutModel.RoutingInstanceID);
            if (routingInstance != null)
            {
                await PopulateBgpPeersDropDownList(routingInstance.RoutingInstanceID, vpnTenantNetworkOutModel.BgpPeerID);
            }
      
            return View(vpnTenantNetworkOutModel);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vpnTenantNetworkOut = await _vpnTenantIpNetworkOutService.GetByIDAsync(id.Value);
            if (vpnTenantNetworkOut == null)
            {
                return NotFound();
            }

            var attachmentSet = await _attachmentSetService.GetByIDAsync(vpnTenantNetworkOut.AttachmentSetID.Value, deep: true);
            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            var bgpPeer = await _bgpPeerService.GetByIDAsync(vpnTenantNetworkOut.BgpPeerID);
            if (bgpPeer != null)
            {
                await PopulateRoutingInstancesDropDownList(vpnTenantNetworkOut.AttachmentSetID.Value, bgpPeer.RoutingInstanceID);
                await PopulateBgpPeersDropDownList(bgpPeer.RoutingInstanceID, bgpPeer.BgpPeerID);
            }

            return View(Mapper.Map<VpnTenantIpNetworkOutViewModel>(vpnTenantNetworkOut));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("VpnTenantIpNetworkOutID,TenantIpNetworkID,AttachmentSetID,"
            + "BgpPeerID,AdvertisedIpRoutingPreference,RowVersion")]
            VpnTenantIpNetworkOutViewModel updateModel)
        {
            if (id != updateModel.VpnTenantIpNetworkOutID)
            {
                return NotFound();
            }

            var currentVpnTenantIpNetworkOut = await _vpnTenantIpNetworkOutService.GetByIDAsync(updateModel.VpnTenantIpNetworkOutID);
            if (currentVpnTenantIpNetworkOut == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. The item was deleted by another user.");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var vpnTenantNetworkOutUpdate = Mapper.Map<VpnTenantIpNetworkOut>(updateModel);

                    await _vpnTenantIpNetworkOutService.UpdateAsync(vpnTenantNetworkOutUpdate);
                    return RedirectToAction("GetAllByAttachmentSetID", new
                    {
                        id = currentVpnTenantIpNetworkOut.AttachmentSetID,
                        showWarningMessage = true
                    });
                }
            }

            catch (DbUpdateConcurrencyException ex)
            {
                var exceptionEntry = ex.Entries.Single();

                if (exceptionEntry.Property("BgpPeerID").CurrentValue != null)
                {
                    var proposedBgpPeerID = (int)exceptionEntry.Property("BgpPeerID").CurrentValue;
                    if (currentVpnTenantIpNetworkOut.BgpPeerID != proposedBgpPeerID)
                    {
                        ModelState.AddModelError("BgpPeerID", $"Current value: {currentVpnTenantIpNetworkOut.BgpPeer.Name}");
                    }
                }

                var proposedAdvertisedIpRoutingPreference = (int?)exceptionEntry.Property("AdvertisedIpRoutingPreference").CurrentValue;
                if (currentVpnTenantIpNetworkOut.AdvertisedIpRoutingPreference != proposedAdvertisedIpRoutingPreference)
                {
                    ModelState.AddModelError("AdvertisedRoutingPreference", $"Current value: {currentVpnTenantIpNetworkOut.AdvertisedIpRoutingPreference}");
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

            var attachmentSet = await _attachmentSetService.GetByIDAsync(currentVpnTenantIpNetworkOut.AttachmentSetID.Value,deep : true);
            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            await PopulateRoutingInstancesDropDownList(currentVpnTenantIpNetworkOut.AttachmentSetID.Value, updateModel.RoutingInstanceID);
            var bgpPeer = await _bgpPeerService.GetByIDAsync(updateModel.BgpPeerID);
            if (bgpPeer != null)
            {
                await PopulateBgpPeersDropDownList(bgpPeer.RoutingInstanceID, updateModel.BgpPeerID);
            }

            return View(Mapper.Map<VpnTenantIpNetworkOutViewModel>(currentVpnTenantIpNetworkOut));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id, int? attachmentSetID, bool? concurrencyError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vpnTenantNetworkOut = await _vpnTenantIpNetworkOutService.GetByIDAsync(id.Value);
            if (vpnTenantNetworkOut == null)
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

            return View(Mapper.Map<VpnTenantIpNetworkOutViewModel>(vpnTenantNetworkOut));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(VpnTenantIpNetworkOutViewModel vpnTenantNetworkOutModel)
        {

            var vpnTenantNetworkOut = await _vpnTenantIpNetworkOutService.GetByIDAsync(vpnTenantNetworkOutModel.VpnTenantIpNetworkOutID);
            if (vpnTenantNetworkOut == null)
            {
                return RedirectToAction("GetAllByAttachmentSetID", new
                {
                    id = vpnTenantNetworkOutModel.AttachmentSetID,
                });
            }

            try
            {
                await _vpnTenantIpNetworkOutService.DeleteAsync(vpnTenantNetworkOut.VpnTenantIpNetworkOutID);

                return RedirectToAction("GetAllByAttachmentSetID", new
                {
                    id = vpnTenantNetworkOut.AttachmentSetID,
                    showWarningMessage = true
                });
            }

            catch (DbUpdateConcurrencyException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("Delete", new
                {
                    concurrencyError = true,
                    id = vpnTenantNetworkOutModel.VpnTenantIpNetworkOutID,
                    attachmentSetID = vpnTenantNetworkOut.AttachmentSetID
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
        /// Helper to populate the Tenant IP Networks drop-down list with all Tenant IP Networks which belong to
        /// a given Tenant
        /// </summary>
        /// <param name="tenantID"></param>
        /// <param name="selectedTenantIpNetwork"></param>
        /// <returns></returns>
        private async Task PopulateTenantIpNetworksDropDownList(int tenantID, object selectedTenantIpNetwork = null)
        {
            var tenantIpNetworks = await _tenantIpNetworkService.GetAllByTenantIDAsync(tenantID);
            ViewBag.TenantIpNetworkID = new SelectList(Mapper.Map<List<TenantIpNetworkViewModel>>(tenantIpNetworks),
                "TenantIpNetworkID", "CidrName", selectedTenantIpNetwork);
        }

        private async Task PopulateRoutingInstancesDropDownList(int attachmentSetID, object selectedRoutingInstance = null)
        {
            var routingInstances = await _routingInstanceService.GetAllByAttachmentSetIDAsync(attachmentSetID);
            ViewBag.RoutingInstanceID = new SelectList(Mapper.Map<List<RoutingInstanceViewModel>>(routingInstances),
                "RoutingInstanceID", "Name", selectedRoutingInstance);
        }

        private async Task PopulateBgpPeersDropDownList(int vrfID, object selectedBgpPeer = null)
        {
            var bgpPeers = await _bgpPeerService.GetAllByRoutingInstanceIDAsync(vrfID);
            ViewBag.BgpPeerID = new SelectList(Mapper.Map<List<BgpPeerViewModel>>(bgpPeers),
                "BgpPeerID", "Name", selectedBgpPeer);
        }
    }
}
