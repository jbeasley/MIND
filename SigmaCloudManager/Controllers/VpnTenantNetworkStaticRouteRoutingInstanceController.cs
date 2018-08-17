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
    public class VpnTenantNetworkStaticRouteRoutingInstanceController : BaseViewController
    {
        public VpnTenantNetworkStaticRouteRoutingInstanceController(IVpnTenantNetworkStaticRouteRoutingInstanceService vpnTenantNetworkStaticRouteRoutingInstanceService,
            IVpnService vpnService, 
            IAttachmentSetService attachmentSetService,
            ITenantNetworkService tenantNetworkService,
            IAttachmentSetRoutingInstanceService attachmentSetRoutingInstanceService,
            IRoutingInstanceService vrfService,
            IBgpPeerService bgpPeerService,
            IVpnTenantNetworkStaticRouteRoutingInstanceValidator vpnTenantNetworkStaticRouteRoutingInstanceValidator,
            IMapper mapper)
        {
            VpnTenantNetworkStaticRouteRoutingInstanceService = vpnTenantNetworkStaticRouteRoutingInstanceService;
            AttachmentSetService = attachmentSetService;
            TenantNetworkService = tenantNetworkService;
            VpnService = vpnService;
            AttachmentSetRoutingInstanceService = attachmentSetRoutingInstanceService;
            RoutingInstanceService = vrfService;
            BgpPeerService = bgpPeerService;
            Mapper = mapper;

            VpnTenantNetworkStaticRouteRoutingInstanceValidator = vpnTenantNetworkStaticRouteRoutingInstanceValidator;
            this.Validator = vpnTenantNetworkStaticRouteRoutingInstanceValidator;
        }
        private IVpnTenantNetworkStaticRouteRoutingInstanceService VpnTenantNetworkStaticRouteRoutingInstanceService { get; }
        private IAttachmentSetService AttachmentSetService { get; }
        private ITenantNetworkService TenantNetworkService { get; }
        private IAttachmentSetRoutingInstanceService AttachmentSetRoutingInstanceService { get; }
        private IRoutingInstanceService RoutingInstanceService { get; }
        private IVpnService VpnService { get; }
        private IBgpPeerService BgpPeerService { get; }
        private IVpnTenantNetworkStaticRouteRoutingInstanceValidator VpnTenantNetworkStaticRouteRoutingInstanceValidator { get; }
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
            var vpnTenantNetworkStaticRouteRoutingInstances = await VpnTenantNetworkStaticRouteRoutingInstanceService.GetAllByAttachmentSetIDAsync(id.Value);

            return View(Mapper.Map<List<VpnTenantNetworkStaticRouteRoutingInstanceViewModel>>(vpnTenantNetworkStaticRouteRoutingInstances));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await VpnTenantNetworkStaticRouteRoutingInstanceService.GetByIDAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            return View(Mapper.Map<VpnTenantNetworkStaticRouteRoutingInstanceViewModel>(item));
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

            return View(new VpnTenantNetworkStaticRouteRoutingInstanceViewModel());
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
        public async Task<IActionResult> Create([Bind("TenantNetworkID,AttachmentSetID,NextHopAddress," +
            "RoutingInstanceID,AddToAllRoutingInstancesInAttachmentSet,IsBfdEnabled")]
            VpnTenantNetworkStaticRouteRoutingInstanceViewModel vpnTenantNetworkStaticRouteRoutingInstanceModel)
        {
            try
            {
                var vpnTenantNetworkStaticRouteRoutingInstance = Mapper.Map<VpnTenantNetworkStaticRouteRoutingInstance>(vpnTenantNetworkStaticRouteRoutingInstanceModel);
                await VpnTenantNetworkStaticRouteRoutingInstanceValidator.ValidateNewAsync(vpnTenantNetworkStaticRouteRoutingInstance);

                if (VpnTenantNetworkStaticRouteRoutingInstanceValidator.ValidationDictionary.IsValid)
                {
                    await VpnTenantNetworkStaticRouteRoutingInstanceService.AddAsync(vpnTenantNetworkStaticRouteRoutingInstance);

                    return RedirectToAction("GetAllByAttachmentSetID", new
                    {
                        id = vpnTenantNetworkStaticRouteRoutingInstance.AttachmentSetID,
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

            var attachmentSet = await AttachmentSetService.GetByIDAsync(vpnTenantNetworkStaticRouteRoutingInstanceModel.AttachmentSetID);
            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            await PopulateTenantNetworksDropDownList(attachmentSet.TenantID);
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

            var vpnTenantNetworkStaticRouteRoutingInstance = await VpnTenantNetworkStaticRouteRoutingInstanceService.GetByIDAsync(id.Value);
            if (vpnTenantNetworkStaticRouteRoutingInstance == null)
            {
                return NotFound();
            }

            var attachmentSet = await AttachmentSetService.GetByIDAsync(vpnTenantNetworkStaticRouteRoutingInstance.AttachmentSetID);
            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            await PopulateRoutingInstancesDropDownList(vpnTenantNetworkStaticRouteRoutingInstance.AttachmentSet.AttachmentSetID);

            return View(Mapper.Map<VpnTenantNetworkStaticRouteRoutingInstanceViewModel>(vpnTenantNetworkStaticRouteRoutingInstance));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("VpnTenantNetworkStaticRouteRoutingInstanceID,TenantNetworkID,RoutingInstanceID,AttachmentSetID,"
            + "AddToAllRoutingInstancesInAttachmentSet,NextHopAddress,IsBfdEnabled,RowVersion")]
            VpnTenantNetworkStaticRouteRoutingInstanceViewModel updateModel)
        {
            if (id != updateModel.VpnTenantNetworkStaticRouteRoutingInstanceID)
            {
                return NotFound();
            }

            var currentVpnTenantNetworkStaticRouteRoutingInstance = await VpnTenantNetworkStaticRouteRoutingInstanceService.GetByIDAsync(updateModel.VpnTenantNetworkStaticRouteRoutingInstanceID);
            if (currentVpnTenantNetworkStaticRouteRoutingInstance == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. The item was deleted by another user.");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var vpnTenantNetworkStaticRouteRoutingInstanceUpdate = Mapper.Map<VpnTenantNetworkStaticRouteRoutingInstance>(updateModel);

                    await VpnTenantNetworkStaticRouteRoutingInstanceService.UpdateAsync(vpnTenantNetworkStaticRouteRoutingInstanceUpdate);
                    return RedirectToAction("GetAllByAttachmentSetID", new
                    {
                        id = currentVpnTenantNetworkStaticRouteRoutingInstance.AttachmentSetID,
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
                    if (currentVpnTenantNetworkStaticRouteRoutingInstance.RoutingInstanceID != proposedRoutingInstanceID)
                    {
                        ModelState.AddModelError("RoutingInstanceID", $"Current value: {currentVpnTenantNetworkStaticRouteRoutingInstance.RoutingInstance.Name}");
                    }
                }

                if (exceptionEntry.Property("IsBfdEnabled").CurrentValue != null)
                {
                    var proposedIsBfdEnabled = (bool)exceptionEntry.Property("IsBfdEnabled").CurrentValue;
                    if (currentVpnTenantNetworkStaticRouteRoutingInstance.IsBfdEnabled != proposedIsBfdEnabled)
                    {
                        ModelState.AddModelError("IsBfdEnabled", $"Current value: {currentVpnTenantNetworkStaticRouteRoutingInstance.IsBfdEnabled}");
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

            var attachmentSet = await AttachmentSetService.GetByIDAsync(currentVpnTenantNetworkStaticRouteRoutingInstance.AttachmentSetID);
            ViewBag.AttachmentSet = Mapper.Map<AttachmentSetViewModel>(attachmentSet);
            await PopulateRoutingInstancesDropDownList(currentVpnTenantNetworkStaticRouteRoutingInstance.AttachmentSet.AttachmentSetID, updateModel.RoutingInstanceID);

            return View(Mapper.Map<VpnTenantNetworkStaticRouteRoutingInstanceViewModel>(currentVpnTenantNetworkStaticRouteRoutingInstance));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id, int? attachmentSetID, bool? concurrencyError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vpnTenantNetworkStaticRouteRoutingInstance = await VpnTenantNetworkStaticRouteRoutingInstanceService.GetByIDAsync(id.Value);
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

            return View(Mapper.Map<VpnTenantNetworkStaticRouteRoutingInstanceViewModel>(vpnTenantNetworkStaticRouteRoutingInstance));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(VpnTenantNetworkStaticRouteRoutingInstanceViewModel vpnTenantNetworkStaticRouteRoutingInstanceModel)
        {

            var vpnTenantNetworkStaticRouteRoutingInstance = await VpnTenantNetworkStaticRouteRoutingInstanceService.GetByIDAsync(vpnTenantNetworkStaticRouteRoutingInstanceModel.VpnTenantNetworkStaticRouteRoutingInstanceID);
            if (vpnTenantNetworkStaticRouteRoutingInstance == null)
            {
                return RedirectToAction("GetAllByAttachmentSetID", new
                {
                    id = vpnTenantNetworkStaticRouteRoutingInstanceModel.AttachmentSetID,
                });
            }

            try
            {
                await VpnTenantNetworkStaticRouteRoutingInstanceService.DeleteAsync(Mapper.Map<VpnTenantNetworkStaticRouteRoutingInstance>(vpnTenantNetworkStaticRouteRoutingInstanceModel));
           
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
                    id = vpnTenantNetworkStaticRouteRoutingInstanceModel.VpnTenantNetworkStaticRouteRoutingInstanceID,
                    attachmentSetID = vpnTenantNetworkStaticRouteRoutingInstance.AttachmentSetID
                });
            }
        }

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
