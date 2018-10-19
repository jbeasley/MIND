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
    public class VpnTenantCommunityOutController : BaseViewController
    {
        public VpnTenantCommunityOutController(ITenantService tenantService,
            IVpnTenantCommunityOutService vpnTenantCommunityOutService,
            IAttachmentSetService attachmentSetService,
            ITenantCommunityService tenantCommunityService,
            IVpnService vpnService,
            IAttachmentSetRoutingInstanceService attachmentSetRoutingInstanceService,
            IRoutingInstanceService vrfService,
            IBgpPeerService bgpPeerService,
            IMapper mapper)
        {
            TenantService = tenantService;
            VpnTenantCommunityOutService = vpnTenantCommunityOutService;
            AttachmentSetService = attachmentSetService;
            TenantCommunityService = tenantCommunityService;
            VpnService = vpnService;
            AttachmentSetRoutingInstanceService = attachmentSetRoutingInstanceService;
            RoutingInstanceService = vrfService;
            BgpPeerService = bgpPeerService;
            Mapper = mapper;
        }

        private ITenantService TenantService { get; }
        private IVpnTenantCommunityOutService VpnTenantCommunityOutService { get; }
        private IAttachmentSetService AttachmentSetService { get; }
        private ITenantCommunityService TenantCommunityService { get; }
        private IAttachmentSetRoutingInstanceService AttachmentSetRoutingInstanceService { get; }
        private IRoutingInstanceService RoutingInstanceService { get; }
        private IVpnService VpnService { get; }
        private IBgpPeerService BgpPeerService { get; }
        private IMapper Mapper { get; }

        [HttpGet]
        public async Task<PartialViewResult> TenantCommunities(int tenantID)
        {
            var tenantCommunities = await TenantCommunityService.GetAllByTenantIDAsync(tenantID);
            return PartialView(Mapper.Map<List<TenantCommunityViewModel>>(tenantCommunities));
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

            var vpnTenantCommunities = await VpnTenantCommunityOutService.GetAllByAttachmentSetIDAsync(id.Value);
            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);

            return View(Mapper.Map<List<VpnTenantCommunityOutViewModel>>(vpnTenantCommunities));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await VpnTenantCommunityOutService.GetByIDAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            return View(Mapper.Map<VpnTenantCommunityOutViewModel>(item));
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

            return View(new VpnTenantCommunityOutViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenantID,TenantCommunityID,AttachmentSetID,RoutingInstanceID,BgpPeerID," +
            "AdvertisedIpRoutingPreference")]
            VpnTenantCommunityOutViewModel vpnTenantCommunityOutModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var vpnTenantCommunityOut = Mapper.Map<VpnTenantCommunityOut>(vpnTenantCommunityOutModel);
                    await VpnTenantCommunityOutService.AddAsync(vpnTenantCommunityOut);

                    return RedirectToAction("GetAllByAttachmentSetID", new
                    {
                        id = vpnTenantCommunityOut.AttachmentSetID,
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

            var attachmentSet = await AttachmentSetService.GetByIDAsync(vpnTenantCommunityOutModel.AttachmentSetID);
            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            await PopulateTenantsDropDownList(vpnTenantCommunityOutModel.TenantID);
            await PopulateTenantCommunitiesDropDownList(attachmentSet.TenantID);
            await PopulateRoutingInstancesDropDownList(attachmentSet.AttachmentSetID, vpnTenantCommunityOutModel.RoutingInstanceID);
            var vrf = await AttachmentSetRoutingInstanceService.GetByIDAsync(vpnTenantCommunityOutModel.RoutingInstanceID);
            if (vrf != null)
            {
                await PopulateBgpPeersDropDownList(vrf.RoutingInstanceID, vpnTenantCommunityOutModel.BgpPeerID);
            }

            return View(vpnTenantCommunityOutModel);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vpnTenantCommunityOut = await VpnTenantCommunityOutService.GetByIDAsync(id.Value);
            if (vpnTenantCommunityOut == null)
            {
                return NotFound();
            }

            var attachmentSet = await AttachmentSetService.GetByIDAsync(vpnTenantCommunityOut.AttachmentSetID.Value);
            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            var bgpPeer = await BgpPeerService.GetByIDAsync(vpnTenantCommunityOut.BgpPeerID); 
            if (bgpPeer != null)
            {
                await PopulateRoutingInstancesDropDownList(vpnTenantCommunityOut.AttachmentSet.AttachmentSetID, bgpPeer.RoutingInstanceID);
                await PopulateBgpPeersDropDownList(bgpPeer.RoutingInstanceID, bgpPeer.BgpPeerID);
            }

            return View(Mapper.Map<VpnTenantCommunityOutViewModel>(vpnTenantCommunityOut));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("VpnTenantCommunityOutID,TenantCommunityID,AttachmentSetID,"
            + "RoutingInstanceID,BgpPeerID,AdvertisedIpRoutingPreference,RowVersion")]
            VpnTenantCommunityOutViewModel updateModel)
        {
            if (id != updateModel.VpnTenantCommunityOutID)
            {
                return NotFound();
            }

            var currentVpnTenantCommunityOut = await VpnTenantCommunityOutService.GetByIDAsync(updateModel.VpnTenantCommunityOutID);
            if (currentVpnTenantCommunityOut == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. The item was deleted by another user.");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var vpnTenantCommunityOutUpdate = Mapper.Map<VpnTenantCommunityOut>(updateModel);

                    await VpnTenantCommunityOutService.UpdateAsync(vpnTenantCommunityOutUpdate);
                    return RedirectToAction("GetAllByAttachmentSetID", new
                    {
                        id = currentVpnTenantCommunityOut.AttachmentSetID,
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
                    if (currentVpnTenantCommunityOut.BgpPeerID != proposedBgpPeerID)
                    {
                        ModelState.AddModelError("BgpPeerID", $"Current value: {currentVpnTenantCommunityOut.BgpPeer.Name}");
                    }
                }

                var proposedAdvertisedIpRoutingPreference = (int?)exceptionEntry.Property("AdvertisedIpRoutingPreference").CurrentValue;
                if (currentVpnTenantCommunityOut.AdvertisedIpRoutingPreference != proposedAdvertisedIpRoutingPreference)
                {
                    ModelState.AddModelError("AdvertisedRoutingPreference", $"Current value: {currentVpnTenantCommunityOut.AdvertisedIpRoutingPreference}");
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

            var attachmentSet = await AttachmentSetService.GetByIDAsync(currentVpnTenantCommunityOut.AttachmentSetID.Value);
            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            await PopulateRoutingInstancesDropDownList(currentVpnTenantCommunityOut.AttachmentSet.AttachmentSetID, updateModel.RoutingInstanceID);
            var bgpPeer = await BgpPeerService.GetByIDAsync(updateModel.BgpPeerID);
            if (bgpPeer != null)
            {
                await PopulateBgpPeersDropDownList(bgpPeer.RoutingInstanceID, bgpPeer.BgpPeerID);
            }

            return View(Mapper.Map<VpnTenantCommunityOutViewModel>(currentVpnTenantCommunityOut));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id, int? attachmentSetID, bool? concurrencyError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vpnTenantCommunityOut = await VpnTenantCommunityOutService.GetByIDAsync(id.Value);
            if (vpnTenantCommunityOut == null)
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

            return View(Mapper.Map<VpnTenantCommunityOutViewModel>(vpnTenantCommunityOut));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(VpnTenantCommunityOutViewModel vpnTenantCommunityOutModel)
        {
            var vpnTenantCommunityOut = await VpnTenantCommunityOutService.GetByIDAsync(vpnTenantCommunityOutModel.VpnTenantCommunityOutID);
            if (vpnTenantCommunityOut == null)
            {
                return RedirectToAction("GetAllByAttachmentSetID", new
                {
                    id = vpnTenantCommunityOutModel.AttachmentSetID
                });
            }

            try
            {
                await VpnTenantCommunityOutService.DeleteAsync(Mapper.Map<VpnTenantCommunityOut>(vpnTenantCommunityOut));

                return RedirectToAction("GetAllByAttachmentSetID", new
                {
                    id = vpnTenantCommunityOut.AttachmentSetID,
                    showWarningMessage = true
                });
            }

            catch (DbUpdateConcurrencyException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("Delete", new
                {
                    concurrencyError = true,
                    id = vpnTenantCommunityOutModel.VpnTenantCommunityOutID,
                    attachmentSetID = vpnTenantCommunityOut.AttachmentSetID
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
