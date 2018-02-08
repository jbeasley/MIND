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
    public class VpnTenantNetworkOutController : BaseViewController
    {
        public VpnTenantNetworkOutController(ITenantService tenantService,
            IVpnTenantNetworkOutService vpnTenantNetworkOutService,
            IVpnService vpnService,
            IAttachmentSetService attachmentSetService,
            ITenantNetworkService tenantNetworkService,
            IAttachmentSetRoutingInstanceService attachmentSetRoutingInstanceService,
            IRoutingInstanceService vrfService,
            IBgpPeerService bgpPeerService,
            IMapper mapper)
        {
            TenantService = tenantService;
            VpnTenantNetworkOutService = vpnTenantNetworkOutService;
            AttachmentSetService = attachmentSetService;
            TenantNetworkService = tenantNetworkService;
            VpnService = vpnService;
            AttachmentSetRoutingInstanceService = attachmentSetRoutingInstanceService;
            RoutingInstanceService = vrfService;
            BgpPeerService = bgpPeerService;
            Mapper = mapper;
        }

        private ITenantService TenantService { get; }
        private IVpnTenantNetworkOutService VpnTenantNetworkOutService { get; }
        private IAttachmentSetService AttachmentSetService { get; }
        private ITenantNetworkService TenantNetworkService { get; }
        private IAttachmentSetRoutingInstanceService AttachmentSetRoutingInstanceService { get; }
        private IRoutingInstanceService RoutingInstanceService { get; }
        private IVpnService VpnService { get; }
        private IBgpPeerService BgpPeerService { get; }
        private IMapper Mapper { get; }

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
            var vpnTenantNetworkOuts = await VpnTenantNetworkOutService.GetAllByAttachmentSetIDAsync(id.Value);

            return View(Mapper.Map<List<VpnTenantNetworkOutViewModel>>(vpnTenantNetworkOuts));
        }


        [HttpGet]
        public async Task<PartialViewResult> TenantNetworks(int tenantID)
        {
            var tenantCommunities = await TenantNetworkService.GetAllByTenantIDAsync(tenantID);
            return PartialView(Mapper.Map<List<TenantNetworkViewModel>>(tenantCommunities));
        }

        [HttpGet]
        public async Task<PartialViewResult> BgpPeers(int? vrfID)
        {
            var bgpPeers = new List<BgpPeer>();

            if (vrfID != null)
            {
                var result = await BgpPeerService.GetAllByRoutingInstanceIDAsync(vrfID.Value);
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

            var item = await VpnTenantNetworkOutService.GetByIDAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            return View(Mapper.Map<VpnTenantNetworkOutViewModel>(item));
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

            return View(new VpnTenantNetworkOutViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenantID,TenantNetworkID,AttachmentSetID," +
            "AttachmentSetRoutingInstanceID,BgpPeerID,AdvertisedIpRoutingPreference")]
            VpnTenantNetworkOutViewModel vpnTenantNetworkOutModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var vpnTenantNetworkOut = Mapper.Map<VpnTenantNetworkOut>(vpnTenantNetworkOutModel);
                    await VpnTenantNetworkOutService.AddAsync(vpnTenantNetworkOut);

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

            var attachmentSet = await AttachmentSetService.GetByIDAsync(vpnTenantNetworkOutModel.AttachmentSetID);
            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            await PopulateTenantsDropDownList();
            await PopulateTenantNetworksDropDownList(vpnTenantNetworkOutModel.TenantID);
            await PopulateRoutingInstancesDropDownList(attachmentSet.AttachmentSetID, vpnTenantNetworkOutModel.RoutingInstanceID);

            var vrf = await AttachmentSetRoutingInstanceService.GetByIDAsync(vpnTenantNetworkOutModel.RoutingInstanceID);
            if (vrf != null)
            {
                await PopulateBgpPeersDropDownList(vrf.RoutingInstanceID, vpnTenantNetworkOutModel.BgpPeerID);
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

            var vpnTenantNetworkOut = await VpnTenantNetworkOutService.GetByIDAsync(id.Value);
            if (vpnTenantNetworkOut == null)
            {
                return NotFound();
            }

            var attachmentSet = await AttachmentSetService.GetByIDAsync(vpnTenantNetworkOut.AttachmentSetID);
            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            var bgpPeer = await BgpPeerService.GetByIDAsync(vpnTenantNetworkOut.BgpPeerID);
            if (bgpPeer != null)
            {
                await PopulateRoutingInstancesDropDownList(vpnTenantNetworkOut.AttachmentSetID, bgpPeer.RoutingInstanceID);
                await PopulateBgpPeersDropDownList(bgpPeer.RoutingInstanceID, bgpPeer.BgpPeerID);
            }

            return View(Mapper.Map<VpnTenantNetworkOutViewModel>(vpnTenantNetworkOut));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("VpnTenantNetworkOutID,TenantNetworkID,AttachmentSetID,"
            + "BgpPeerID,AdvertisedIpRoutingPreference,RowVersion")]
            VpnTenantNetworkOutViewModel updateModel)
        {
            if (id != updateModel.VpnTenantNetworkOutID)
            {
                return NotFound();
            }

            var currentVpnTenantNetworkOut = await VpnTenantNetworkOutService.GetByIDAsync(updateModel.VpnTenantNetworkOutID);
            if (currentVpnTenantNetworkOut == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. The item was deleted by another user.");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var vpnTenantNetworkOutUpdate = Mapper.Map<VpnTenantNetworkOut>(updateModel);

                    await VpnTenantNetworkOutService.UpdateAsync(vpnTenantNetworkOutUpdate);
                    return RedirectToAction("GetAllByAttachmentSetID", new
                    {
                        id = currentVpnTenantNetworkOut.AttachmentSetID,
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
                    if (currentVpnTenantNetworkOut.BgpPeerID != proposedBgpPeerID)
                    {
                        ModelState.AddModelError("BgpPeerID", $"Current value: {currentVpnTenantNetworkOut.BgpPeer.Name}");
                    }
                }

                var proposedAdvertisedIpRoutingPreference = (int?)exceptionEntry.Property("AdvertisedIpRoutingPreference").CurrentValue;
                if (currentVpnTenantNetworkOut.AdvertisedIpRoutingPreference != proposedAdvertisedIpRoutingPreference)
                {
                    ModelState.AddModelError("AdvertisedRoutingPreference", $"Current value: {currentVpnTenantNetworkOut.AdvertisedIpRoutingPreference}");
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

            var attachmentSet = await AttachmentSetService.GetByIDAsync(currentVpnTenantNetworkOut.AttachmentSetID);
            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            await PopulateRoutingInstancesDropDownList(currentVpnTenantNetworkOut.AttachmentSetID, updateModel.RoutingInstanceID);
            var bgpPeer = await BgpPeerService.GetByIDAsync(updateModel.BgpPeerID);
            if (bgpPeer != null)
            {
                await PopulateBgpPeersDropDownList(bgpPeer.RoutingInstanceID, updateModel.BgpPeerID);
            }

            return View(Mapper.Map<VpnTenantNetworkOutViewModel>(currentVpnTenantNetworkOut));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id, int? attachmentSetID, bool? concurrencyError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vpnTenantNetworkOut = await VpnTenantNetworkOutService.GetByIDAsync(id.Value);
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

            return View(Mapper.Map<VpnTenantNetworkOutViewModel>(vpnTenantNetworkOut));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(VpnTenantNetworkOutViewModel vpnTenantNetworkOutModel)
        {

            var vpnTenantNetworkOut = await VpnTenantNetworkOutService.GetByIDAsync(vpnTenantNetworkOutModel.VpnTenantNetworkOutID);
            if (vpnTenantNetworkOut == null)
            {
                return RedirectToAction("GetAllByAttachmentSetID", new
                {
                    id = vpnTenantNetworkOutModel.AttachmentSetID,
                });
            }

            try
            {
                await VpnTenantNetworkOutService.DeleteAsync(Mapper.Map<VpnTenantNetworkOut>(vpnTenantNetworkOutModel));

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
                    id = vpnTenantNetworkOutModel.VpnTenantNetworkOutID,
                    attachmentSetID = vpnTenantNetworkOut.AttachmentSetID
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
        /// Helper to populate the Tenant Networks drop-down list with all Tenant Networks which belong to
        /// a given Tenant
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

        private async Task PopulateBgpPeersDropDownList(int vrfID, object selectedBgpPeer = null)
        {
            var bgpPeers = await BgpPeerService.GetAllByRoutingInstanceIDAsync(vrfID);
            ViewBag.BgpPeerID = new SelectList(Mapper.Map<List<BgpPeerViewModel>>(bgpPeers),
                "BgpPeerID", "Name", selectedBgpPeer);
        }
    }
}
