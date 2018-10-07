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
    public class VpnTenantCommunityInController : BaseViewController
    {
        public VpnTenantCommunityInController(IVpnTenantCommunityInService vpnTenantCommunityInService,
            IAttachmentSetService attachmentSetService,
            ITenantCommunityService tenantCommunityService,
            IVpnService vpnService,
            IRoutingInstanceService vrfService,
            IBgpPeerService bgpPeerService,
            IMapper mapper)
        {
            VpnTenantCommunityInService = vpnTenantCommunityInService;
            AttachmentSetService = attachmentSetService;
            TenantCommunityService = tenantCommunityService;
            VpnService = vpnService;
            RoutingInstanceService = vrfService;
            BgpPeerService = bgpPeerService;
            Mapper = mapper;

        }
        private IVpnTenantCommunityInService VpnTenantCommunityInService { get; }
        private IAttachmentSetService AttachmentSetService { get; }
        private ITenantCommunityService TenantCommunityService { get; }
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

            var vpnTenantCommunities = await VpnTenantCommunityInService.GetAllByAttachmentSetIDAsync(id.Value);
            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);

            return View(Mapper.Map<List<VpnTenantCommunityInViewModel>>(vpnTenantCommunities));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await VpnTenantCommunityInService.GetByIDAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            return View(Mapper.Map<VpnTenantCommunityInViewModel>(item));
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
            await PopulateTenantCommunitiesDropDownList(attachmentSet.TenantID);
            await PopulateRoutingInstancesDropDownList(attachmentSet.AttachmentSetID);

            return View(new VpnTenantCommunityInViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenantCommunityID,AttachmentSetID,RoutingInstanceID,BgpPeerID," +
            "AddToAllBgpPeersInAttachmentSet,LocalIpRoutingPreference")]
            VpnTenantCommunityInViewModel vpnTenantCommunityInModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var vpnTenantCommunityIn = Mapper.Map<VpnTenantCommunityIn>(vpnTenantCommunityInModel);
                    await VpnTenantCommunityInService.AddAsync(vpnTenantCommunityIn);

                    return RedirectToAction("GetAllByAttachmentSetID", new
                    {
                        id = vpnTenantCommunityIn.AttachmentSetID,
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

            var attachmentSet = await AttachmentSetService.GetByIDAsync(vpnTenantCommunityInModel.AttachmentSetID);
            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            await PopulateTenantCommunitiesDropDownList(attachmentSet.TenantID);
            await PopulateRoutingInstancesDropDownList(attachmentSet.AttachmentSetID, vpnTenantCommunityInModel.RoutingInstanceID);
            if (vpnTenantCommunityInModel.RoutingInstanceID != null)
            {
                var vrf = await RoutingInstanceService.GetByIDAsync(vpnTenantCommunityInModel.RoutingInstanceID.Value);
                if (vrf != null)
                {
                    await PopulateBgpPeersDropDownList(vrf.RoutingInstanceID, vpnTenantCommunityInModel.BgpPeerID);
                }
            }

            return View(vpnTenantCommunityInModel);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vpnTenantCommunityIn = await VpnTenantCommunityInService.GetByIDAsync(id.Value);
            if (vpnTenantCommunityIn == null)
            {
                return NotFound();
            }

            var attachmentSet = await AttachmentSetService.GetByIDAsync(vpnTenantCommunityIn.AttachmentSetID.Value);
            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            await PopulateRoutingInstancesDropDownList(vpnTenantCommunityIn.AttachmentSet.AttachmentSetID);
            if (vpnTenantCommunityIn.BgpPeerID != null) { 

                await PopulateBgpPeersDropDownList(vpnTenantCommunityIn.BgpPeer.RoutingInstanceID, vpnTenantCommunityIn.BgpPeerID);
            }

            return View(Mapper.Map<VpnTenantCommunityInViewModel>(vpnTenantCommunityIn));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("VpnTenantCommunityInID,TenantCommunityID,RoutingInstanceID,AttachmentSetID,"
            + "AddToAllBgpPeersInAttachmentSet,BgpPeerID,LocalIpRoutingPreference,RowVersion")]
            VpnTenantCommunityInViewModel updateModel)
        {
            if (id != updateModel.VpnTenantCommunityInID)
            {
                return NotFound();
            }

            var currentVpnTenantCommunityIn = await VpnTenantCommunityInService.GetByIDAsync(updateModel.VpnTenantCommunityInID);
            if (currentVpnTenantCommunityIn == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. The item was deleted by another user.");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var vpnTenantCommunityInUpdate = Mapper.Map<VpnTenantCommunityIn>(updateModel);

                    await VpnTenantCommunityInService.UpdateAsync(vpnTenantCommunityInUpdate);
                    return RedirectToAction("GetAllByAttachmentSetID", new
                    {
                        id = currentVpnTenantCommunityIn.AttachmentSetID,
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
                    if (currentVpnTenantCommunityIn.AddToAllBgpPeersInAttachmentSet != proposedAddToAllBgpPeersInAttachmentSet)
                    {
                        ModelState.AddModelError("AddToAllBgpPeersInAttachmentSet", $"Current value: {currentVpnTenantCommunityIn.AddToAllBgpPeersInAttachmentSet}");
                    }
                }

                if (exceptionEntry.Property("BgpPeerID").CurrentValue != null)
                {
                    var proposedBgpPeerID = (int)exceptionEntry.Property("BgpPeerID").CurrentValue;
                    if (currentVpnTenantCommunityIn.BgpPeerID != proposedBgpPeerID)
                    {
                        ModelState.AddModelError("BgpPeerID", $"Current value: {currentVpnTenantCommunityIn.BgpPeer.Name}");
                    }
                }

                var proposedLocalIpRoutingPreference = (int?)exceptionEntry.Property("LocalIpRoutingPreference").CurrentValue;
                if (currentVpnTenantCommunityIn.LocalIpRoutingPreference != proposedLocalIpRoutingPreference)
                {
                    ModelState.AddModelError("LocalRoutingPreference", $"Current value: {currentVpnTenantCommunityIn.LocalIpRoutingPreference}");
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

            var attachmentSet = await AttachmentSetService.GetByIDAsync(currentVpnTenantCommunityIn.AttachmentSetID.Value);
            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            await PopulateRoutingInstancesDropDownList(currentVpnTenantCommunityIn.AttachmentSet.AttachmentSetID, updateModel.RoutingInstanceID);
            if (updateModel.RoutingInstanceID != null)
            {
                var vrf = await RoutingInstanceService.GetByIDAsync(updateModel.RoutingInstanceID.Value);
                if (vrf != null)
                {
                    await PopulateBgpPeersDropDownList(vrf.RoutingInstanceID, updateModel.BgpPeerID);
                }
            }

            return View(Mapper.Map<VpnTenantCommunityInViewModel>(currentVpnTenantCommunityIn));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id, int? attachmentSetID, bool? concurrencyError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vpnTenantCommunityIn = await VpnTenantCommunityInService.GetByIDAsync(id.Value);
            if (vpnTenantCommunityIn == null)
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

            return View(Mapper.Map<VpnTenantCommunityInViewModel>(vpnTenantCommunityIn));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(VpnTenantCommunityInViewModel vpnTenantCommunityInModel)
        {
            var vpnTenantCommunityIn = await VpnTenantCommunityInService.GetByIDAsync(vpnTenantCommunityInModel.VpnTenantCommunityInID);
            if (vpnTenantCommunityIn == null)
            {
                return RedirectToAction("GetAllByAttachmentSetID", new
                {
                    id = vpnTenantCommunityInModel.AttachmentSetID
                });
            }

            try
            {
                await VpnTenantCommunityInService.DeleteAsync(Mapper.Map<VpnTenantCommunityIn>(vpnTenantCommunityIn));

                return RedirectToAction("GetAllByAttachmentSetID", new
                {
                    id = vpnTenantCommunityIn.AttachmentSetID,
                    showWarningMessage = true
                });
            }

            catch (DbUpdateConcurrencyException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("Delete", new
                {
                    concurrencyError = true,
                    id = vpnTenantCommunityInModel.VpnTenantCommunityInID,
                    attachmentSetID = vpnTenantCommunityIn.AttachmentSetID
                });
            }
        }

        private async Task PopulateTenantCommunitiesDropDownList(int tenantID, object selectedTenantCommunity = null)
        {
            var tenantCommunities = await TenantCommunityService.GetAllByTenantIDAsync(tenantID);
            ViewBag.TenantCommunityID = new SelectList(Mapper.Map<List<TenantCommunityViewModel>>(tenantCommunities), 
                "TenantCommunityID", "Name", selectedTenantCommunity);
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