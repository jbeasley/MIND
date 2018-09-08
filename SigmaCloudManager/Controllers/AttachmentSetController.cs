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
    public class AttachmentSetController : BaseViewController
    {
        public AttachmentSetController(IAttachmentSetService attachmentSetService,
            IVpnService vpnService,
            ITenantService tenantService,
            IRegionService regionService,
            IMulticastVpnDomainTypeService multicastVpnDomainTypeService,
            IMapper mapper) : base(attachmentSetService, mapper)
        {
            AttachmentSetService = attachmentSetService;
            TenantService = tenantService;
            RegionService = regionService;
            VpnService = vpnService;
            MulticastVpnDomainTypeService = multicastVpnDomainTypeService;
        }

        private IAttachmentSetService AttachmentSetService { get; set; }
        private IVpnService VpnService { get; set; }
        private ITenantService TenantService { get; set; }
        private IRegionService RegionService { get; set; }
        private IMulticastVpnDomainTypeService MulticastVpnDomainTypeService { get; }

        [HttpGet]
        public async Task<IActionResult> GetAllByTenantID(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenant = await TenantService.GetByIDAsync(id.Value);
            if (tenant == null)
            {
                return NotFound();
            }

            ViewBag.Tenant = tenant;
            var attachmentSets = await AttachmentSetService.GetAllByTenantIDAsync(id.Value);

            return View(Mapper.Map<List<AttachmentSetViewModel>>(attachmentSets));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await AttachmentSetService.GetByIDAsync(id.Value, deep: true);
            if (item == null)
            {
                return NotFound();
            }

            return View(Mapper.Map<AttachmentSetViewModel>(item));
        }

        [HttpGet]
        public async Task<IActionResult> CreateStep1(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenant = await TenantService.GetByIDAsync(id.Value);
            if (tenant == null)
            {
                return NotFound();
            }

            ViewBag.Tenant = tenant;
            await PopulateRegionsDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStep2(int? id, [Bind("RegionID")] RegionViewModel region)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenant = await TenantService.GetByIDAsync(id.Value);
            if (tenant == null)
            {
                return NotFound();
            }

            ViewBag.Tenant = tenant;
            await PopulateSubRegionsDropDownList(region.RegionID);
            await PopulateAttachmentRedundancyDropDownList();
            await PopulateMulticastVpnDomainTypesDropDownList();
            ViewBag.Region = region;

            ModelState.Clear();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,RegionID,SubRegionID,TenantID,AttachmentRedundancyID,"
            +"MulticastVpnDomainTypeID,IsLayer3")] AttachmentSetViewModel attachmentSetModel)
        {
            if (ModelState.IsValid)
            {
                var attachmentSet = Mapper.Map<AttachmentSet>(attachmentSetModel);

                try
                {
                    await AttachmentSetService.AddAsync(Mapper.Map<AttachmentSet>(attachmentSet));
                    return RedirectToAction("GetAllByTenantID", new { id = attachmentSet.TenantID });
                }

                catch (DbUpdateException /** ex **/ )
                {
                    //Log the error (uncomment ex variable name and write a log.
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists " +
                        "see your system administrator.");
                }
            }

            ViewBag.Tenant = await TenantService.GetByIDAsync(attachmentSetModel.TenantID);
            ViewBag.Region = await RegionService.GetByIDAsync(attachmentSetModel.RegionID);
            await PopulateDropDownLists(attachmentSetModel.RegionID);

            return View("CreateStep2", attachmentSetModel);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attachmentSet = await AttachmentSetService.GetByIDAsync(id.Value, deep: true);
            if (attachmentSet == null)
            {
                return NotFound();
            }

            await PopulateMulticastVpnDomainTypesDropDownList(attachmentSet.MulticastVpnDomainTypeID);
            await PopulateDropDownLists(attachmentSet.RegionID);
            return View(Mapper.Map<AttachmentSetViewModel>(attachmentSet));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("AttachmentSetID,Name,Description,RegionID,SubRegionID,TenantID," +
            "AttachmentRedundancyID,IsLayer3,MulticastVpnDomainTypeID,RowVersion")] AttachmentSetViewModel attachmentSetModel)
        {
            if (id != attachmentSetModel.AttachmentSetID)
            {
                return NotFound();
            }

            var currentAttachmentSet = await AttachmentSetService.GetByIDAsync(id, deep: true);
            if (currentAttachmentSet == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. The Attachment Set was deleted by another user.");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    Mapper.Map(attachmentSetModel, currentAttachmentSet);

                    await AttachmentSetService.UpdateAsync(currentAttachmentSet);
                    return RedirectToAction("GetAllByTenantID", new { id = currentAttachmentSet.TenantID });
                }
            }

            catch (DbUpdateConcurrencyException ex)
            {
                var exceptionEntry = ex.Entries.Single();

                var proposedName = (string)exceptionEntry.Property("Name").CurrentValue;
                if (currentAttachmentSet.Name != proposedName)
                {
                    ModelState.AddModelError("Name", $"Current value: {currentAttachmentSet.Name}");
                }

                var proposedRegionID = (int?)exceptionEntry.Property("RegionID").CurrentValue;
                if (currentAttachmentSet.RegionID != proposedRegionID)
                {
                    ModelState.AddModelError("RegionID", $"Current value: {currentAttachmentSet.SubRegion.Name}");
                }

                var proposedSubRegionID = (int?)exceptionEntry.Property("SubRegionID").CurrentValue;
                if (currentAttachmentSet.SubRegionID != proposedSubRegionID)
                {
                    ModelState.AddModelError("SubRegionID", $"Current value: {currentAttachmentSet.SubRegion.Name}");
                }

                var proposedAttachmentRedundancyID = (int)exceptionEntry.Property("AttachmentRedundancyID").CurrentValue;
                if (currentAttachmentSet.AttachmentRedundancyID != proposedAttachmentRedundancyID)
                {
                    ModelState.AddModelError("AttachmentRedundancyID", $"Current value: {currentAttachmentSet.AttachmentRedundancy.Name}");
                }

                var proposedTenantID = (int)exceptionEntry.Property("TenantID").CurrentValue;
                if (currentAttachmentSet.TenantID != proposedTenantID)
                {
                    ModelState.AddModelError("TenantID", $"Current value: {currentAttachmentSet.Tenant.Name}");
                }

                var proposedIsLayer3 = (bool)exceptionEntry.Property("IsLayer3").CurrentValue;
                if (currentAttachmentSet.IsLayer3 != proposedIsLayer3)
                {
                    ModelState.AddModelError("IsLayer3", $"Current value: {currentAttachmentSet.IsLayer3}");
                }

                if (currentAttachmentSet.MulticastVpnDomainTypeID != null)
                {
                    var proposedMulticastVpnDomainTypeID = (int)exceptionEntry.Property("MulticastVpnDomainTypeID").CurrentValue;
                    if (currentAttachmentSet.MulticastVpnDomainTypeID != proposedMulticastVpnDomainTypeID)
                    {
                        ModelState.AddModelError("MulticastVpnDomainTypeID", $"Current value: {currentAttachmentSet.MulticastVpnDomainType.Name}");
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

            await PopulateDropDownLists(attachmentSetModel.RegionID);
            await PopulateMulticastVpnDomainTypesDropDownList(attachmentSetModel.MulticastVpnDomainTypeID);

            return View(Mapper.Map<AttachmentSetViewModel>(currentAttachmentSet));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id, int? tenantID, bool? concurrencyError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attachmentSet = await AttachmentSetService.GetByIDAsync(id.Value, deep: true);
            if (attachmentSet == null)
            {
                if (concurrencyError.GetValueOrDefault())
                {
                    return RedirectToAction("GetAllByTenantID", new { id = tenantID });
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

            return View(Mapper.Map<AttachmentSetViewModel>(attachmentSet));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(AttachmentSetViewModel attachmentSetModel)
        {
            var currentAttachmentSet = await AttachmentSetService.GetByIDAsync(attachmentSetModel.AttachmentSetID);
            if (currentAttachmentSet == null)
            {
                return RedirectToAction("GetAllByTenantID", new
                {
                    id = attachmentSetModel.TenantID
                });
            }

            try
            {
                await AttachmentSetService.DeleteAsync(attachmentSetModel.AttachmentSetID);
                return RedirectToAction("GetAllByTenantID", new { id = currentAttachmentSet.TenantID });
            }

            catch (DbUpdateConcurrencyException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("Delete", new
                {
                    concurrencyError = true,
                    id = attachmentSetModel.AttachmentSetID,
                    tenantID = attachmentSetModel.TenantID
                });
            }
        }

        /// <summary>
        /// Helper to populate the drop-down lists for the view.
        /// </summary>
        /// <param name="regionID"></param>
        /// <returns></returns>
        private async Task PopulateDropDownLists(int regionID)
        {
            await PopulateSubRegionsDropDownList(regionID);
            await PopulateAttachmentRedundancyDropDownList();
        }

        private async Task PopulateAttachmentRedundancyDropDownList(object selectedAttachmentRedundancy = null)
        {
            var attachmentRedundancies = await AttachmentSetService.UnitOfWork.AttachmentRedundancyRepository.GetAsync();
            ViewBag.AttachmentRedundancyID = new SelectList(attachmentRedundancies, "AttachmentRedundancyID", "Name", selectedAttachmentRedundancy);
        }

        private async Task PopulateRegionsDropDownList(object selectedRegion = null)
        {
            var regions = await AttachmentSetService.UnitOfWork.RegionRepository.GetAsync();
            ViewBag.RegionID = new SelectList(regions, "RegionID", "Name", selectedRegion);
        }

        private async Task PopulateSubRegionsDropDownList(int regionID, object selectedSubRegion = null)
        {
            var subRegions = await AttachmentSetService.UnitOfWork.SubRegionRepository.GetAsync(q => q.RegionID == regionID);
            ViewBag.SubRegionID = new SelectList(subRegions, "SubRegionID", "Name", selectedSubRegion);
        }

        private async Task PopulateMulticastVpnDomainTypesDropDownList(object selectedMulticastVpnDomainType = null)
        {
            var multicastVpnDomainTypes = await MulticastVpnDomainTypeService.GetAllAsync();
            ViewBag.MulticastVpnDomainTypeID = new SelectList(Mapper.Map<List<MulticastVpnDomainType>>(multicastVpnDomainTypes),
                "MulticastVpnDomainTypeID", "Name", selectedMulticastVpnDomainType);
        }
    }
}
