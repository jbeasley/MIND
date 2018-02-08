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
    public class VpnAttachmentSetController : BaseViewController
    {
        public VpnAttachmentSetController(IVpnAttachmentSetService vpnAttachmentSetService,
            IVpnService vpnService,
            ITenantService tenantService,
            IAttachmentSetService attachmentSetService,
            IAttachmentSetRoutingInstanceService attachmentSetRoutingInstanceService,
            IVpnAttachmentSetValidator vpnAttachmentSetValidator,
            IMapper mapper)
        {
            TenantService = tenantService;
            VpnAttachmentSetService = vpnAttachmentSetService;
            VpnService = vpnService;
            AttachmentSetService = attachmentSetService;
            AttachmentSetRoutingInstanceService = attachmentSetRoutingInstanceService;
            VpnAttachmentSetValidator = vpnAttachmentSetValidator;
            this.Validator = vpnAttachmentSetValidator;
            Mapper = mapper;
        }
        
        private ITenantService TenantService { get; }
        private IVpnAttachmentSetService VpnAttachmentSetService { get;  }
        private IVpnService VpnService { get; }
        private IAttachmentSetService AttachmentSetService { get; }
        private IAttachmentSetRoutingInstanceService AttachmentSetRoutingInstanceService { get; }
        private IVpnAttachmentSetValidator VpnAttachmentSetValidator { get; }
        private IMapper Mapper { get; }

        [HttpGet]
        public async Task<IActionResult> GetAllByVpnID(int? id, bool showWarning = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (showWarning)
            {
                ViewData["NetworkWarningMessage"] = "The VPN requires synchronisation with the network as a result of this update. "
                  + "Follow this <a href = '/Vpn/GetAll'>link</a> to go to the VPNs page.";
            }

            var vpnAttachmentSets = await VpnAttachmentSetService.GetAllByVpnIDAsync(id.Value);
            var vpn = await VpnService.GetByIDAsync(id.Value);
            ViewBag.Vpn = Mapper.Map<VpnViewModel>(vpn);

            return View(Mapper.Map<List<VpnAttachmentSetViewModel>>(vpnAttachmentSets));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vpnAttachmentSet = await VpnAttachmentSetService.GetByIDAsync(id.Value);
            if (vpnAttachmentSet == null)
            {
                return NotFound();
            }

            var vpn = await VpnService.GetByIDAsync(vpnAttachmentSet.VpnID);
            ViewBag.Vpn = Mapper.Map<VpnViewModel>(vpn);
            return View(Mapper.Map<VpnAttachmentSetViewModel>(vpnAttachmentSet));
        }

        [HttpGet]
        public async Task<IActionResult> CreateStep1(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await PopulateTenantsDropDownList(id.Value);
            var vpn = await VpnService.GetByIDAsync(id.Value);
            ViewBag.Vpn = Mapper.Map<VpnViewModel>(vpn);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStep2([Bind("TenantID,VpnID")] VpnAttachmentSetRequestViewModel vpnAttachmentSetRequest)
        {
            await PopulateAttachmentSetsDropDownList(vpnAttachmentSetRequest);
            var vpn = await VpnService.GetByIDAsync(vpnAttachmentSetRequest.VpnID);
            ViewBag.Vpn = Mapper.Map<VpnViewModel>(vpn);
            ViewBag.VpnAttachmentSetSelection = vpnAttachmentSetRequest;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AttachmentSetID,VpnID,IsHub," +
            "IsMulticastDirectlyIntegrated")] VpnAttachmentSetViewModel vpnAttachmentSetModel,
            [Bind("VpnID,TenantID")] VpnAttachmentSetRequestViewModel vpnAttachmentSetRequestModel)
        {
            try
            {
                var vpnAttachmentSet = Mapper.Map<VpnAttachmentSet>(vpnAttachmentSetModel);
                await VpnAttachmentSetValidator.ValidateNewAsync(vpnAttachmentSet);

                if (VpnAttachmentSetValidator.ValidationDictionary.IsValid)
                {
                    await VpnAttachmentSetService.AddAsync(vpnAttachmentSet);
                    return RedirectToAction("GetAllByVpnID", new
                    {
                        id = vpnAttachmentSet.VpnID,
                        showWarning = true
                    });
                }
            }

            catch (DbUpdateException /**  ex **/  )
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }

            var vpn = await VpnService.GetByIDAsync(vpnAttachmentSetModel.VpnID);
            ViewBag.Vpn = Mapper.Map<VpnViewModel>(vpn);
            ViewBag.VpnAttachmentSetSelection = vpnAttachmentSetRequestModel;
            await PopulateAttachmentSetsDropDownList(vpnAttachmentSetRequestModel);

            return View("CreateStep2", vpnAttachmentSetModel);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vpnAttachmentSet = await VpnAttachmentSetService.GetByIDAsync(id.Value);
            if (vpnAttachmentSet == null)
            {
                return NotFound();
            }

            return View(Mapper.Map<VpnAttachmentSetViewModel>(vpnAttachmentSet));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("VpnAttachmentSetID,AttachmentSetID,VpnID,IsHub," +
            "IsMulticastDirectlyIntegrated,RowVersion")] VpnAttachmentSetViewModel vpnAttachmentSetModel)
        {
            if (id != vpnAttachmentSetModel.VpnAttachmentSetID)
            {
                return NotFound();
            }

            var currentVpnAttachmentSet = await VpnAttachmentSetService.GetByIDAsync(id);
            if (currentVpnAttachmentSet == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. The item was deleted by another user.");
            }

            var vpnAttachmentSet = Mapper.Map<VpnAttachmentSet>(vpnAttachmentSetModel);

            try
            {
                await VpnAttachmentSetValidator.ValidateChangesAsync(vpnAttachmentSet);
                if (VpnAttachmentSetValidator.ValidationDictionary.IsValid)
                {
                    await VpnAttachmentSetService.UpdateAsync(vpnAttachmentSet);
                    return RedirectToAction("GetAllByVpnID", new
                    {
                        id = currentVpnAttachmentSet.VpnID,
                        showWarning = true
                    });
                }
            }

            catch (DbUpdateConcurrencyException ex)
            {
                var exceptionEntry = ex.Entries.Single();

                if (exceptionEntry.Property("IsHub").CurrentValue != null)
                {
                    var proposedIsHub = (bool)exceptionEntry.Property("IsHub").CurrentValue;
                    if (currentVpnAttachmentSet.IsHub != proposedIsHub)
                    {
                        ModelState.AddModelError("IsHub", $"Current value: {currentVpnAttachmentSet.IsHub}");
                    }
                }

                if (exceptionEntry.Property("IsMulticastDirectlyIntegrated").CurrentValue != null)
                {
                    var proposedIsMulticastDirectlyIntegrated = (bool)exceptionEntry.Property("IsMulticastDirectlyIntegrated").CurrentValue;
                    if (currentVpnAttachmentSet.IsMulticastDirectlyIntegrated != proposedIsMulticastDirectlyIntegrated)
                    {
                        ModelState.AddModelError("IsMulticastDirectlyIntegrated", $"Current value: {currentVpnAttachmentSet.IsMulticastDirectlyIntegrated}");
                    }
                }

                ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                    + "was modified by another user after you got the original value. The "
                    + "edit operation was cancelled and the current values in the database "
                    + "have been displayed. If you still want to edit this record, click "
                    + "the Save button again. Otherwise click the Back to List hyperlink.");

                ModelState.Remove("RowVersion");
            }

            return View(Mapper.Map<VpnAttachmentSetViewModel>(currentVpnAttachmentSet));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id, int? vpnAttachmentSetID, bool? concurrencyError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vpnAttachmentSet = await VpnAttachmentSetService.GetByIDAsync(id.Value);
            if (vpnAttachmentSet == null)
            {
                if (concurrencyError.GetValueOrDefault())
                {
                    return RedirectToAction("GetAllByVpnID", new
                    {
                        id = vpnAttachmentSetID,
                        showWarning = true
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

            var vpn = await VpnService.GetByIDAsync(vpnAttachmentSet.VpnID);
            ViewBag.Vpn = Mapper.Map<VpnViewModel>(vpn);

            return View(Mapper.Map<VpnAttachmentSetViewModel>(vpnAttachmentSet));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(VpnAttachmentSetViewModel vpnAttachmentSetModel)
        {  
            try
            {
                var vpnAttachmentSet = await VpnAttachmentSetService.GetByIDAsync(vpnAttachmentSetModel.VpnAttachmentSetID);
                if (vpnAttachmentSet == null)
                {
                    return RedirectToAction("GetAllByVpnID", new
                    {
                        id = vpnAttachmentSetModel.VpnID
                    });
                }

                await VpnAttachmentSetService.DeleteAsync(Mapper.Map<VpnAttachmentSet>(vpnAttachmentSet));
                
                return RedirectToAction("GetAllByVpnID", new
                {
                    id = vpnAttachmentSet.VpnID,
                    showWarning = true
                });
            }

            catch (DbUpdateConcurrencyException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("Delete", new
                {
                    concurrencyError = true,
                    id = vpnAttachmentSetModel.VpnAttachmentSetID,
                    vpnAttachmentSetID = vpnAttachmentSetModel.VpnAttachmentSetID
                });
            }
        }

        private async Task PopulateAttachmentSetsDropDownList(VpnAttachmentSetRequestViewModel vpnAttachmentSetRequest, 
            object selectedAttachmentSet = null)
        {
            var vpn = await VpnService.GetByIDAsync(vpnAttachmentSetRequest.VpnID);
            var layer3 = vpn.VpnTopologyType.VpnProtocolType.ProtocolType == Models.ProtocolType.IP ? true : false;
            var tenant = await TenantService.GetByIDAsync(vpnAttachmentSetRequest.TenantID);
            var attachmentSets = await AttachmentSetService.GetAllByTenantAsync(tenant);

            attachmentSets = attachmentSets.Where(x => x.IsLayer3 = layer3);

            if (vpn.RegionID != null)
            {
                attachmentSets = attachmentSets.Where(q => q.RegionID == vpn.RegionID).ToList();
            }

            ViewBag.AttachmentSetID = new SelectList(Mapper.Map<List<AttachmentSetViewModel>>(attachmentSets), 
                "AttachmentSetID", "Name", selectedAttachmentSet);
        }

        private async Task PopulateTenantsDropDownList(int vpnID, object selectedTenant = null)
        {
            var vpn = await VpnService.GetByIDAsync(vpnID);
            IEnumerable<Tenant> tenants;

            // Get all Tenants if the VPN is multi-tenant, other get only the tenant owner

            if (vpn.VpnTenancyType.TenancyType == Models.TenancyType.Multi)
            {
                tenants = await TenantService.GetAllAsync();
            }
            else
            {
                tenants = new List<Tenant>
                {
                    await TenantService.GetByIDAsync(vpn.TenantID)
                };
            }
           
            ViewBag.TenantID = new SelectList(Mapper.Map<List<TenantViewModel>>(tenants), "TenantID", "Name", selectedTenant);
        }
    }
}