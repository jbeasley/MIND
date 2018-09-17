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
    public class VpnTenantIpNetworkRoutingInstanceStaticRouteController : BaseViewController
    {
        private readonly IVpnTenantIpNetworkRoutingInstanceStaticRouteService _vpnTenantIpNetworkRoutingInstanceStaticRouteService;
        private readonly IAttachmentSetService _attachmentSetService;
        private readonly ITenantIpNetworkService _tenantIpNetworkService;
        private readonly IAttachmentSetRoutingInstanceService _attachmentSetRoutingInstanceService;
        private readonly IRoutingInstanceService _routingInstanceService;
        private readonly IVpnService _vpnService;
        private readonly IBgpPeerService _bgpPeerService;

        public VpnTenantIpNetworkRoutingInstanceStaticRouteController(IVpnTenantIpNetworkRoutingInstanceStaticRouteService vpnTenantIpNetworkStaticRouteRoutingInstanceService,
            IVpnService vpnService, 
            IAttachmentSetService attachmentSetService,
            ITenantIpNetworkService tenantNetworkService,
            IAttachmentSetRoutingInstanceService attachmentSetRoutingInstanceService,
            IRoutingInstanceService vrfService,
            IBgpPeerService bgpPeerService,
            IMapper mapper) : base(vpnTenantIpNetworkStaticRouteRoutingInstanceService, mapper)
        {
            _vpnTenantIpNetworkRoutingInstanceStaticRouteService = vpnTenantIpNetworkStaticRouteRoutingInstanceService;
            _attachmentSetService = attachmentSetService;
            _tenantIpNetworkService = tenantNetworkService;
            _vpnService = vpnService;
            _attachmentSetRoutingInstanceService = attachmentSetRoutingInstanceService;
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

            var attachmentSet = await _attachmentSetService.GetByIDAsync(id.Value);
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
            var vpnTenantNetworkStaticRouteRoutingInstances = await _vpnTenantIpNetworkRoutingInstanceStaticRouteService.GetAllByAttachmentSetIDAsync(id.Value);

            return View(Mapper.Map<List<VpnTenantIpNetworkRoutingInstanceStaticRouteViewModel>>(vpnTenantNetworkStaticRouteRoutingInstances));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _vpnTenantIpNetworkRoutingInstanceStaticRouteService.GetByIDAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            return View(Mapper.Map<VpnTenantIpNetworkRoutingInstanceStaticRouteViewModel>(item));
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
            await PopulateTenantIpNetworksDropDownList(attachmentSet.TenantID);
            await PopulateRoutingInstancesDropDownList(attachmentSet.AttachmentSetID);

            return View(new VpnTenantIpNetworkRoutingInstanceStaticRouteViewModel());
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
        public async Task<IActionResult> Create([Bind("TenantIpNetworkID,AttachmentSetID,NextHopAddress," +
            "RoutingInstanceID,AddToAllRoutingInstancesInAttachmentSet,IsBfdEnabled")]
            VpnTenantIpNetworkRoutingInstanceStaticRouteViewModel vpnTenantNetworkStaticRouteRoutingInstanceModel)
        {
            try
            {
                var vpnTenantNetworkStaticRouteRoutingInstance = Mapper.Map<VpnTenantIpNetworkRoutingInstanceStaticRoute>(vpnTenantNetworkStaticRouteRoutingInstanceModel);
                await _vpnTenantIpNetworkRoutingInstanceStaticRouteService.AddAsync(vpnTenantNetworkStaticRouteRoutingInstance);

                return RedirectToAction("GetAllByAttachmentSetID", new
                {
                    id = vpnTenantNetworkStaticRouteRoutingInstance.AttachmentSetID,
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

            var attachmentSet = await _attachmentSetService.GetByIDAsync(vpnTenantNetworkStaticRouteRoutingInstanceModel.AttachmentSetID);
            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            await PopulateTenantIpNetworksDropDownList(attachmentSet.TenantID);
            await PopulateRoutingInstancesDropDownList(attachmentSet.AttachmentSetID, vpnTenantNetworkStaticRouteRoutingInstanceModel.RoutingInstanceID);

            return View(vpnTenantNetworkStaticRouteRoutingInstanceModel);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vpnTenantNetworkStaticRouteRoutingInstance = await _vpnTenantIpNetworkRoutingInstanceStaticRouteService.GetByIDAsync(id.Value);
            if (vpnTenantNetworkStaticRouteRoutingInstance == null)
            {
                return NotFound();
            }

            var attachmentSet = await _attachmentSetService.GetByIDAsync(vpnTenantNetworkStaticRouteRoutingInstance.AttachmentSetID);
            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            await PopulateRoutingInstancesDropDownList(vpnTenantNetworkStaticRouteRoutingInstance.AttachmentSet.AttachmentSetID);

            return View(Mapper.Map<VpnTenantIpNetworkRoutingInstanceStaticRouteViewModel>(vpnTenantNetworkStaticRouteRoutingInstance));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("VpnTenantIpNetworkRoutingInstanceStaticRouteID,TenantIpNetworkID,RoutingInstanceID,AttachmentSetID,"
            + "AddToAllRoutingInstancesInAttachmentSet,NextHopAddress,IsBfdEnabled,RowVersion")]
            VpnTenantIpNetworkRoutingInstanceStaticRouteViewModel updateModel)
        {
            if (id != updateModel.VpnTenantIpNetworkRoutingInstanceStaticRouteID)
            {
                return NotFound();
            }

            var currentVpnTenantIpNetworkRoutingInstanceStaticRoute = await _vpnTenantIpNetworkRoutingInstanceStaticRouteService.GetByIDAsync(updateModel.VpnTenantIpNetworkRoutingInstanceStaticRouteID);
            if (currentVpnTenantIpNetworkRoutingInstanceStaticRoute == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. The item was deleted by another user.");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var vpnTenantNetworkStaticRouteRoutingInstanceUpdate = Mapper.Map<VpnTenantIpNetworkRoutingInstanceStaticRoute>(updateModel);

                    await _vpnTenantIpNetworkRoutingInstanceStaticRouteService.UpdateAsync(vpnTenantNetworkStaticRouteRoutingInstanceUpdate);
                    return RedirectToAction("GetAllByAttachmentSetID", new
                    {
                        id = currentVpnTenantIpNetworkRoutingInstanceStaticRoute.AttachmentSetID,
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
                    if (currentVpnTenantIpNetworkRoutingInstanceStaticRoute.RoutingInstanceID != proposedRoutingInstanceID)
                    {
                        ModelState.AddModelError("RoutingInstanceID", $"Current value: {currentVpnTenantIpNetworkRoutingInstanceStaticRoute.RoutingInstance.Name}");
                    }
                }

                if (exceptionEntry.Property("IsBfdEnabled").CurrentValue != null)
                {
                    var proposedIsBfdEnabled = (bool)exceptionEntry.Property("IsBfdEnabled").CurrentValue;
                    if (currentVpnTenantIpNetworkRoutingInstanceStaticRoute.IsBfdEnabled != proposedIsBfdEnabled)
                    {
                        ModelState.AddModelError("IsBfdEnabled", $"Current value: {currentVpnTenantIpNetworkRoutingInstanceStaticRoute.IsBfdEnabled}");
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

            var attachmentSet = await _attachmentSetService.GetByIDAsync(currentVpnTenantIpNetworkRoutingInstanceStaticRoute.AttachmentSetID);
            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            await PopulateRoutingInstancesDropDownList(currentVpnTenantIpNetworkRoutingInstanceStaticRoute.AttachmentSet.AttachmentSetID, updateModel.RoutingInstanceID);

            return View(Mapper.Map<VpnTenantIpNetworkRoutingInstanceStaticRouteViewModel>(currentVpnTenantIpNetworkRoutingInstanceStaticRoute));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id, int? attachmentSetID, bool? concurrencyError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vpnTenantNetworkStaticRouteRoutingInstance = await _vpnTenantIpNetworkRoutingInstanceStaticRouteService.GetByIDAsync(id.Value);
            if (vpnTenantNetworkStaticRouteRoutingInstance == null)
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

            return View(Mapper.Map<VpnTenantIpNetworkRoutingInstanceStaticRouteViewModel>(vpnTenantNetworkStaticRouteRoutingInstance));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(VpnTenantIpNetworkRoutingInstanceStaticRouteViewModel vpnTenantNetworkStaticRouteRoutingInstanceModel)
        {

            var vpnTenantNetworkStaticRouteRoutingInstance = await _vpnTenantIpNetworkRoutingInstanceStaticRouteService.GetByIDAsync(vpnTenantNetworkStaticRouteRoutingInstanceModel.VpnTenantIpNetworkRoutingInstanceStaticRouteID);
            if (vpnTenantNetworkStaticRouteRoutingInstance == null)
            {
                return RedirectToAction("GetAllByAttachmentSetID", new
                {
                    id = vpnTenantNetworkStaticRouteRoutingInstanceModel.AttachmentSetID,
                });
            }

            try
            {
                await _vpnTenantIpNetworkRoutingInstanceStaticRouteService.DeleteAsync(vpnTenantNetworkStaticRouteRoutingInstanceModel.VpnTenantIpNetworkRoutingInstanceStaticRouteID);
           
                return RedirectToAction("GetAllByAttachmentSetID", new
                {
                    id = vpnTenantNetworkStaticRouteRoutingInstance.AttachmentSetID,
                    showWarningMessage = true
                });
            }

            catch (DbUpdateConcurrencyException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("Delete", new
                {
                    concurrencyError = true,
                    id = vpnTenantNetworkStaticRouteRoutingInstanceModel.VpnTenantIpNetworkRoutingInstanceStaticRouteID,
                    attachmentSetID = vpnTenantNetworkStaticRouteRoutingInstance.AttachmentSetID
                });
            }
        }

        private async Task PopulateTenantIpNetworksDropDownList(int tenantID, object selectedTenantNetwork = null)
        {
            var tenantNetworks = await _tenantIpNetworkService.GetAllByTenantIDAsync(tenantID);
            ViewBag.TenantNetworkID = new SelectList(Mapper.Map<List<TenantIpNetworkViewModel>>(tenantNetworks), 
                "TenantIpNetworkID", "CidrName", selectedTenantNetwork);
        }

        private async Task PopulateRoutingInstancesDropDownList(int attachmentSetID, object selectedRoutingInstance = null)
        {
            var routingInstances = await _routingInstanceService.GetAllByAttachmentSetIDAsync(attachmentSetID);
            ViewBag.RoutingInstanceID = new SelectList(Mapper.Map<List<RoutingInstanceViewModel>>(routingInstances),
                "RoutingInstanceID", "Name", selectedRoutingInstance);
        }
    }
}
