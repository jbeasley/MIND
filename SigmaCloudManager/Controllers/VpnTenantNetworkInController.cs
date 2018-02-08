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
    public class VpnTenantNetworkInController : BaseViewController
    {
        public VpnTenantNetworkInController(IVpnTenantNetworkInService vpnTenantNetworkInService,
            IVpnService vpnService, 
            IAttachmentSetService attachmentSetService,
            ITenantNetworkService tenantNetworkService,
            IRoutingInstanceService vrfService,
            IBgpPeerService bgpPeerService,
            IMapper mapper)
        {
            VpnTenantNetworkInService = vpnTenantNetworkInService;
            AttachmentSetService = attachmentSetService;
            TenantNetworkService = tenantNetworkService;
            VpnService = vpnService;
            RoutingInstanceService = vrfService;
            BgpPeerService = bgpPeerService;
            Mapper = mapper;
        }

        private IVpnTenantNetworkInService VpnTenantNetworkInService { get; }
        private IAttachmentSetService AttachmentSetService { get; }
        private ITenantNetworkService TenantNetworkService { get; }
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
            var vpnTenantNetworkIns = await VpnTenantNetworkInService.GetAllByAttachmentSetIDAsync(id.Value);

            return View(Mapper.Map<List<VpnTenantNetworkInViewModel>>(vpnTenantNetworkIns));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await VpnTenantNetworkInService.GetByIDAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            return View(Mapper.Map<VpnTenantNetworkInViewModel>(item));
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
            await PopulateTenantNetworksDropDownList(attachmentSet.TenantID);
            await PopulateRoutingInstancesDropDownList(attachmentSet.AttachmentSetID);

            return View(new VpnTenantNetworkInViewModel());
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenantNetworkID,AttachmentSetID," +
            "RoutingInstanceID,AddToAllBgpPeersInAttachmentSet,BgpPeerID,LocalIpRoutingPreference")]
            VpnTenantNetworkInViewModel vpnTenantNetworkInModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var vpnTenantNetworkIn = Mapper.Map<VpnTenantNetworkIn>(vpnTenantNetworkInModel);
                    await VpnTenantNetworkInService.AddAsync(vpnTenantNetworkIn);

                    return RedirectToAction("GetAllByAttachmentSetID", new
                    {
                        id = vpnTenantNetworkIn.AttachmentSetID,
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

            var attachmentSet = await AttachmentSetService.GetByIDAsync(vpnTenantNetworkInModel.AttachmentSetID);
            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            await PopulateTenantNetworksDropDownList(attachmentSet.TenantID);
            await PopulateRoutingInstancesDropDownList(attachmentSet.AttachmentSetID, vpnTenantNetworkInModel.RoutingInstanceID);

            if (vpnTenantNetworkInModel.RoutingInstanceID != null)
            {
                var vrf = await RoutingInstanceService.GetByIDAsync(vpnTenantNetworkInModel.RoutingInstanceID.Value);
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

            var vpnTenantNetworkIn = await VpnTenantNetworkInService.GetByIDAsync(id.Value);
            if (vpnTenantNetworkIn == null)
            {
                return NotFound();
            }

            var attachmentSet = await AttachmentSetService.GetByIDAsync(vpnTenantNetworkIn.AttachmentSetID);
            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            await PopulateRoutingInstancesDropDownList(vpnTenantNetworkIn.AttachmentSet.AttachmentSetID);
            if (vpnTenantNetworkIn.BgpPeerID != null)
            {
                var bgpPeer = await BgpPeerService.GetByIDAsync(vpnTenantNetworkIn.BgpPeerID.Value);
                await PopulateBgpPeersDropDownList(bgpPeer.RoutingInstanceID, bgpPeer.BgpPeerID);
            }

            return View(Mapper.Map<VpnTenantNetworkInViewModel>(vpnTenantNetworkIn));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("VpnTenantNetworkInID,TenantNetworkID,RoutingInstanceID,AttachmentSetID,"
            + "AddToAllBgpPeersInAttachmentSet,BgpPeerID,LocalIpRoutingPreference,RowVersion")]
            VpnTenantNetworkInViewModel updateModel)
        {
            if (id != updateModel.VpnTenantNetworkInID)
            {
                return NotFound();
            }

            var currentVpnTenantNetworkIn = await VpnTenantNetworkInService.GetByIDAsync(updateModel.VpnTenantNetworkInID);
            if (currentVpnTenantNetworkIn == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. The item was deleted by another user.");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var vpnTenantNetworkInUpdate = Mapper.Map<VpnTenantNetworkIn>(updateModel);

                    await VpnTenantNetworkInService.UpdateAsync(vpnTenantNetworkInUpdate);
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

            var attachmentSet = await AttachmentSetService.GetByIDAsync(currentVpnTenantNetworkIn.AttachmentSetID);
            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            await PopulateRoutingInstancesDropDownList(currentVpnTenantNetworkIn.AttachmentSet.AttachmentSetID, updateModel.RoutingInstanceID);
            if (updateModel.RoutingInstanceID != null)
            {
                var vrf = await RoutingInstanceService.GetByIDAsync(updateModel.RoutingInstanceID.Value);
                if (vrf != null)
                {
                    await PopulateBgpPeersDropDownList(vrf.RoutingInstanceID, updateModel.BgpPeerID);
                }
            }

            return View(Mapper.Map<VpnTenantNetworkInViewModel>(currentVpnTenantNetworkIn));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id, int? attachmentSetID, bool? concurrencyError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vpnTenantNetworkIn = await VpnTenantNetworkInService.GetByIDAsync(id.Value);
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

            return View(Mapper.Map<VpnTenantNetworkInViewModel>(vpnTenantNetworkIn));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(VpnTenantNetworkInViewModel vpnTenantNetworkInModel)
        {

            var vpnTenantNetworkIn = await VpnTenantNetworkInService.GetByIDAsync(vpnTenantNetworkInModel.VpnTenantNetworkInID);
            if (vpnTenantNetworkIn == null)
            {
                return RedirectToAction("GetAllByAttachmentSetID", new
                {
                    id = vpnTenantNetworkInModel.AttachmentSetID,
                });
            }

            try
            {
                await VpnTenantNetworkInService.DeleteAsync(Mapper.Map<VpnTenantNetworkIn>(vpnTenantNetworkInModel));
           
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
                    id = vpnTenantNetworkInModel.VpnTenantNetworkInID,
                    attachmentSetID = vpnTenantNetworkIn.AttachmentSetID
                });
            }
        }

        private async Task PopulateTenantNetworksDropDownList(int tenantID, object selectedTenantNetwork = null)
        {
            var tenantNetworks = await TenantNetworkService.GetAllByTenantIDAsync(tenantID);
            ViewBag.TenantNetworkID = new SelectList(Mapper.Map<List<TenantNetworkViewModel>>(tenantNetworks), 
                "TenantNetworkID", "CidrNameIncludingLessThanOrEqualToLength", selectedTenantNetwork);
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
