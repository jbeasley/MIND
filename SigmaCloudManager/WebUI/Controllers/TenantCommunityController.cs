using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using SCM.Models;
using SCM.Models.ViewModels;
using SCM.Services;
using SCM.Validators;
using Mind.Services;

namespace SCM.Controllers
{
    public class TenantCommunityController : BaseViewController
    {
        public TenantCommunityController(ITenantCommunityService tenantCommunityService,
            IVpnService vpnService,
            ITenantService tenantService,
            IMapper mapper,
            ITenantCommunityValidator tenantCommunityValidator)
        {
            TenantCommunityService = tenantCommunityService;
            VpnService = vpnService;
            TenantService = tenantService;
            Mapper = mapper;
            TenantCommunityValidator = tenantCommunityValidator;
            this.Validator = tenantCommunityValidator;
        }

        private ITenantCommunityService TenantCommunityService { get; set; }
        private IVpnService VpnService { get; set; }
        private ITenantService TenantService { get; set; }
        private IMapper Mapper { get; set; }
        private ITenantCommunityValidator TenantCommunityValidator { get; set; }

        [HttpGet]
        public async Task<IActionResult> GetAllByTenantID(int? id, string searchString = "", bool showWarningMessage = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenantCommunities = await TenantCommunityService.GetAllByTenantIDAsync(id.Value, searchString);
            if (showWarningMessage)
            {
                ViewData["NetworkWarningMessage"] = $"VPNs require synchronisation with the network as a result of this update. "
                            + "Follow this <a href = '/Vpn/GetAll'>link</a> to the VPNs page.";
            }

            ViewBag.Tenant = await TenantService.GetByIDAsync(id.Value);
            return View(Mapper.Map<List<TenantCommunityViewModel>>(tenantCommunities));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await TenantCommunityService.GetByIDAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            ViewBag.Tenant = await TenantService.GetByIDAsync(item.TenantID);
            return View(Mapper.Map<TenantCommunityViewModel>(item));
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.Tenant = await TenantService.GetByIDAsync(id.Value);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenantID,AutonomousSystemNumber,Number,AllowExtranet")] TenantCommunityViewModel tenantCommunityModel)
        {
            if (ModelState.IsValid)
            {
                var tenantCommunity = Mapper.Map<TenantCommunity>(tenantCommunityModel);
                await TenantCommunityValidator.ValidateNewAsync(tenantCommunity);

                if (TenantCommunityValidator.ValidationDictionary.IsValid)
                {
                    try
                    {
                        await TenantCommunityService.AddAsync(Mapper.Map<TenantCommunity>(tenantCommunityModel));
                        return RedirectToAction("GetAllByTenantID", new
                        {
                            id = tenantCommunityModel.TenantID
                        });
                    }

                    catch (DbUpdateException /* ex */)
                    {
                        //Log the error (uncomment ex variable name and write a log.
                        ModelState.AddModelError("", "Unable to save changes. " +
                            "Try again, and if the problem persists " +
                            "see your system administrator.");
                    }
                }
            }
        
            ViewBag.Tenant = await TenantService.GetByIDAsync(tenantCommunityModel.TenantID);
            return View(Mapper.Map<TenantCommunityViewModel>(tenantCommunityModel));
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenantCommunity = await TenantCommunityService.GetByIDAsync(id.Value);
            if (tenantCommunity == null)
            {
                return NotFound();
            }

            ViewBag.Tenant = await TenantService.GetByIDAsync(id.Value);
            return View(Mapper.Map<TenantCommunityViewModel>(tenantCommunity));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("TenantCommunityID,TenantID,AutonomousSystemNumber," +
            "Number,AllowExtranet,RowVersion")] TenantCommunityViewModel tenantCommunityModel)
        {
            if (id != tenantCommunityModel.TenantCommunityID)
            {
                return NotFound();
            }

            var tenantCommunity = await TenantCommunityService.GetByIDAsync(id);
            if (tenantCommunity == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. The Tenant Community was deleted by another user.");
            }

            try
            {
                var item = Mapper.Map<TenantCommunity>(tenantCommunityModel);
                await TenantCommunityValidator.ValidateChangesAsync(item);

                if (TenantCommunityValidator.ValidationDictionary.IsValid)
                {
                    await TenantCommunityService.UpdateAsync(item);
    
                    return RedirectToAction("GetAllByTenantID", new
                    {
                        id = tenantCommunity.TenantID
                    });
                }
            }

            catch (DbUpdateConcurrencyException ex)
            {
                var exceptionEntry = ex.Entries.Single();

                var proposedAutonomousSystemNumber = (int)exceptionEntry.Property("AutonomousSystemNumber").CurrentValue;
                if (tenantCommunity.AutonomousSystemNumber != proposedAutonomousSystemNumber)
                {
                    ModelState.AddModelError("AutonomousSystemNumber", $"Current value: {tenantCommunity.AutonomousSystemNumber}");
                }

                var proposedNumber = (int)exceptionEntry.Property("Number").CurrentValue;
                if (tenantCommunity.Number != proposedNumber)
                {
                    ModelState.AddModelError("Number", $"Current value: {tenantCommunity.Number}");
                }

                var proposedAllowExtranet = (bool)exceptionEntry.Property("AllowExtranet").CurrentValue;
                if (tenantCommunity.AllowExtranet != proposedAllowExtranet)
                {
                    ModelState.AddModelError("AllowExtranet", $"Current value: {tenantCommunity.AllowExtranet}");
                }

                ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                    + "was modified by another user after you got the original value. The "
                    + "edit operation was cancelled and the current values in the database "
                    + "have been displayed. If you still want to edit this record, click "
                    + "the Save button again. Otherwise click the Back to List hyperlink.");

                ModelState.Remove("RowVersion");
            }

            catch (DbUpdateException /* ex */ )
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");

            }

            ViewBag.Tenant = await TenantService.GetByIDAsync(tenantCommunity.TenantID);
            return View(Mapper.Map<TenantCommunityViewModel>(tenantCommunity));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id, int? tenantID, bool? concurrencyError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenantCommunity = await TenantCommunityService.GetByIDAsync(id.Value);
            if (tenantCommunity == null)
            {
                if (concurrencyError.GetValueOrDefault())
                {
                    return RedirectToAction("GetAllByTenantID", new
                    {
                        id = tenantID
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

            var tenant = await TenantService.GetByIDAsync(tenantCommunity.TenantID);
            ViewBag.Tenant = Mapper.Map<TenantViewModel>(tenant);

            return View(Mapper.Map<TenantCommunityViewModel>(tenantCommunity));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(TenantCommunityViewModel tenantCommunityModel)
        {
            var tenantCommunity = await TenantCommunityService.GetByIDAsync(tenantCommunityModel.TenantCommunityID);
            if (tenantCommunity == null)
            {
                return RedirectToAction("GetAllByTenantID", new
                {
                    id = tenantCommunityModel.TenantID
                });
            }

            try
            {
                this.Validator.ValidationDictionary.Clear();
                await TenantCommunityValidator.ValidateDeleteAsync(tenantCommunity.TenantCommunityID);

                if (TenantCommunityValidator.ValidationDictionary.IsValid)
                {
                    await TenantCommunityService.DeleteAsync(tenantCommunityModel.TenantCommunityID);
                    return RedirectToAction("GetAllByTenantID", new
                    {
                        id = tenantCommunityModel.TenantID
                    });
                }
            }

            catch (DbUpdateConcurrencyException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("Delete", new
                {
                    concurrencyError = true,
                    id = tenantCommunityModel.TenantCommunityID,
                    tenantID = tenantCommunityModel.TenantID
                });
            }

            var tenant = await TenantService.GetByIDAsync(tenantCommunity.TenantID);
            ViewBag.Tenant = Mapper.Map<TenantViewModel>(tenant);

            return View(Mapper.Map<TenantCommunityViewModel>(tenantCommunity));
        }
    }
}