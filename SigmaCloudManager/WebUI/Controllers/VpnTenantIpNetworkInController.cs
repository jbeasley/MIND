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
    public class VpnTenantIpNetworkInController : BaseViewController
    {
        private IVpnTenantIpNetworkInService _vpnTenantIpNetworkInService { get; }
        private IAttachmentSetService _attachmentSetService { get; }
        private ITenantIpNetworkService _tenantIpNetworkService { get; }
        private IRoutingInstanceService _routingInstanceService { get; }
        private IVpnService _vpnService { get; }
        private IBgpPeerService _bgpPeerService { get; }

        public VpnTenantIpNetworkInController(IVpnTenantIpNetworkInService vpnTenantIpNetworkInService,
            IVpnService vpnService, 
            IAttachmentSetService attachmentSetService,
            ITenantIpNetworkService tenantIpNetworkService,
            IRoutingInstanceService vrfService,
            IBgpPeerService bgpPeerService,
            IMapper mapper) : base(vpnTenantIpNetworkInService, mapper)
        {
            _vpnTenantIpNetworkInService = vpnTenantIpNetworkInService;
            _attachmentSetService = attachmentSetService;
            _tenantIpNetworkService = tenantIpNetworkService;
            _vpnService = vpnService;
            _routingInstanceService = vrfService;
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

            if (showWarningMessage.GetValueOrDefault())
            {
                ViewData["NetworkWarningMessage"] = "The VPN requires synchronisation with the network as a result of this update. "
                        + "Follow this <a href = '/Vpn/GetAll'>link</a> to go to the VPNs page.";
            }

            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            var vpnTenantNetworkIns = await _vpnTenantIpNetworkInService.GetAllByAttachmentSetIDAsync(id.Value, deep: true);

            return View(Mapper.Map<List<VpnTenantIpNetworkInViewModel>>(vpnTenantNetworkIns));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _vpnTenantIpNetworkInService.GetByIDAsync(id.Value, deep: true);
            if (item == null)
            {
                return NotFound();
            }

            return View(Mapper.Map<VpnTenantIpNetworkInViewModel>(item));
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
            await PopulateTenantIpNetworksDropDownList(attachmentSet.TenantID);
            await PopulateRoutingInstancesDropDownList(attachmentSet.AttachmentSetID);

            return View(new VpnTenantIpNetworkInViewModel());
        }

        [HttpGet]
        public async Task<PartialViewResult> BgpPeers(int? vrfID)
        {
            var bgpPeers = new List<BgpPeer>();
            if (vrfID != null)
            {
                var result = await _bgpPeerService.GetAllByRoutingInstanceIDAsync(vrfID.Value);
                bgpPeers = result.ToList();
            }

            return PartialView(Mapper.Map<List<BgpPeerViewModel>>(bgpPeers));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenantIpNetworkID,AttachmentSetID," +
            "RoutingInstanceID,AddToAllBgpPeersInAttachmentSet,BgpPeerID,LocalIpRoutingPreference")]
            VpnTenantIpNetworkInViewModel vpnTenantNetworkInModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var vpnTenantIpNetworkIn = Mapper.Map<VpnTenantIpNetworkIn>(vpnTenantNetworkInModel);
                    await _vpnTenantIpNetworkInService.AddAsync(vpnTenantIpNetworkIn);

                    return RedirectToAction("GetAllByAttachmentSetID", new
                    {
                        id = vpnTenantIpNetworkIn.AttachmentSetID,
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

            var attachmentSet = await _attachmentSetService.GetByIDAsync(vpnTenantNetworkInModel.AttachmentSetID, deep: true);
            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            await PopulateTenantIpNetworksDropDownList(attachmentSet.TenantID);
            await PopulateRoutingInstancesDropDownList(attachmentSet.AttachmentSetID, vpnTenantNetworkInModel.RoutingInstanceID);

            if (vpnTenantNetworkInModel.RoutingInstanceID != null)
            {
                var vrf = await _routingInstanceService.GetByIDAsync(vpnTenantNetworkInModel.RoutingInstanceID.Value);
                if (vrf != null)
                {
                    await PopulateBgpPeersDropDownList(vrf.RoutingInstanceID, vpnTenantNetworkInModel.BgpPeerID);
                }
            }

            return View(vpnTenantNetworkInModel);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vpnTenantNetworkIn = await _vpnTenantIpNetworkInService.GetByIDAsync(id.Value, deep: true);
            if (vpnTenantNetworkIn == null)
            {
                return NotFound();
            }

            var attachmentSet = await _attachmentSetService.GetByIDAsync(vpnTenantNetworkIn.AttachmentSetID.Value, deep:true);
            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            await PopulateRoutingInstancesDropDownList(vpnTenantNetworkIn.AttachmentSet.AttachmentSetID);
            if (vpnTenantNetworkIn.BgpPeerID != null)
            {
                var bgpPeer = await _bgpPeerService.GetByIDAsync(vpnTenantNetworkIn.BgpPeerID.Value);
                await PopulateBgpPeersDropDownList(bgpPeer.RoutingInstanceID, bgpPeer.BgpPeerID);
            }

            return View(Mapper.Map<VpnTenantIpNetworkInViewModel>(vpnTenantNetworkIn));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("VpnTenantIpNetworkInID,TenantIpNetworkID,RoutingInstanceID,AttachmentSetID,"
            + "AddToAllBgpPeersInAttachmentSet,BgpPeerID,LocalIpRoutingPreference,RowVersion")]
            VpnTenantIpNetworkInViewModel updateModel)
        {
            if (id != updateModel.VpnTenantIpNetworkInID)
            {
                return NotFound();
            }

            var currentVpnTenantNetworkIn = await _vpnTenantIpNetworkInService.GetByIDAsync(updateModel.VpnTenantIpNetworkInID);
            if (currentVpnTenantNetworkIn == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. The item was deleted by another user.");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var vpnTenantNetworkInUpdate = Mapper.Map<VpnTenantIpNetworkIn>(updateModel);

                    await _vpnTenantIpNetworkInService.UpdateAsync(vpnTenantNetworkInUpdate);
                    return RedirectToAction("GetAllByAttachmentSetID", new
                    {
                        id = currentVpnTenantNetworkIn.AttachmentSetID,
                        showWarningMessage = true
                    });
                }
            }

            catch (DbUpdateConcurrencyException ex)
            {
                var exceptionEntry = ex.Entries.Single();

                if (exceptionEntry.Property("AddToAllBgpPeersInAttachmentSet").CurrentValue != null)
                {
                    var proposedAddToAllBgpPeersInAttachmentSet = (bool)exceptionEntry.Property("AddToAllBgpPeersInAttachmentSet").CurrentValue;
                    if (currentVpnTenantNetworkIn.AddToAllBgpPeersInAttachmentSet != proposedAddToAllBgpPeersInAttachmentSet)
                    {
                        ModelState.AddModelError("AddToAllBgpPeersInAttachmentSet", $"Current value: {currentVpnTenantNetworkIn.AddToAllBgpPeersInAttachmentSet}");
                    }
                }

                if (exceptionEntry.Property("BgpPeerID").CurrentValue != null)
                {
                    var proposedBgpPeerID = (int)exceptionEntry.Property("BgpPeerID").CurrentValue;
                    if (currentVpnTenantNetworkIn.BgpPeerID != proposedBgpPeerID)
                    {
                        ModelState.AddModelError("BgpPeerID", $"Current value: {currentVpnTenantNetworkIn.BgpPeer.Name}");
                    }
                }

                var proposedLocalIpRoutingPreference = (int?)exceptionEntry.Property("LocalIpRoutingPreference").CurrentValue;
                if (currentVpnTenantNetworkIn.LocalIpRoutingPreference != proposedLocalIpRoutingPreference)
                {
                    ModelState.AddModelError("LocalRoutingPreference", $"Current value: {currentVpnTenantNetworkIn.LocalIpRoutingPreference}");
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

            var attachmentSet = await _attachmentSetService.GetByIDAsync(currentVpnTenantNetworkIn.AttachmentSetID.Value);
            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            await PopulateRoutingInstancesDropDownList(currentVpnTenantNetworkIn.AttachmentSet.AttachmentSetID, updateModel.RoutingInstanceID);
            if (updateModel.RoutingInstanceID != null)
            {
                var vrf = await _routingInstanceService.GetByIDAsync(updateModel.RoutingInstanceID.Value);
                if (vrf != null)
                {
                    await PopulateBgpPeersDropDownList(vrf.RoutingInstanceID, updateModel.BgpPeerID);
                }
            }

            return View(Mapper.Map<VpnTenantIpNetworkInViewModel>(currentVpnTenantNetworkIn));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id, int? attachmentSetID, bool? concurrencyError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vpnTenantNetworkIn = await _vpnTenantIpNetworkInService.GetByIDAsync(id.Value);
            if (vpnTenantNetworkIn == null)
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

            return View(Mapper.Map<VpnTenantIpNetworkInViewModel>(vpnTenantNetworkIn));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(VpnTenantIpNetworkInViewModel vpnTenantNetworkInModel)
        {

            var vpnTenantNetworkIn = await _vpnTenantIpNetworkInService.GetByIDAsync(vpnTenantNetworkInModel.VpnTenantIpNetworkInID);
            if (vpnTenantNetworkIn == null)
            {
                return RedirectToAction("GetAllByAttachmentSetID", new
                {
                    id = vpnTenantNetworkInModel.AttachmentSetID,
                });
            }

            try
            {
                await _vpnTenantIpNetworkInService.DeleteAsync(vpnTenantNetworkInModel.VpnTenantIpNetworkInID);
           
                return RedirectToAction("GetAllByAttachmentSetID", new
                {
                    id = vpnTenantNetworkIn.AttachmentSetID,
                    showWarningMessage = true
                });
            }

            catch (DbUpdateConcurrencyException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("Delete", new
                {
                    concurrencyError = true,
                    id = vpnTenantNetworkInModel.VpnTenantIpNetworkInID,
                    attachmentSetID = vpnTenantNetworkIn.AttachmentSetID
                });
            }
        }

        private async Task PopulateTenantIpNetworksDropDownList(int tenantID, object selectedTenantIpNetwork = null)
        {
            var tenantIpNetworks = await _tenantIpNetworkService.GetAllByTenantIDAsync(tenantID);
            ViewBag.TenantIpNetworkID = new SelectList(Mapper.Map<List<TenantIpNetworkViewModel>>(tenantIpNetworks), 
                "TenantIpNetworkID", "CidrNameIncludingLessThanOrEqualToLength", selectedTenantIpNetwork);
        }

        private async Task PopulateRoutingInstancesDropDownList(int attachmentSetID, object selectedRoutingInstance = null)
        {
            var vrfs = await _routingInstanceService.GetAllByAttachmentSetIDAsync(attachmentSetID);
            ViewBag.RoutingInstanceID = new SelectList(Mapper.Map<List<RoutingInstanceViewModel>>(vrfs),
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
