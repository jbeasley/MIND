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
using Microsoft.AspNetCore.Mvc.Rendering;
using Mind.Services;

namespace SCM.Controllers
{
    public class TenantCommunitySetController : BaseViewController
    {
        public TenantCommunitySetController(ITenantCommunitySetService tenantCommunitySetService,
            IVpnService vpnService,
            ITenantService tenantService,
            IRoutingPolicyMatchOptionService routingPolicyMatchOptionService,
            IMapper mapper,
            ITenantCommunitySetValidator tenantCommunitySetValidator)
        {
            TenantCommunitySetService = tenantCommunitySetService;
            VpnService = vpnService;
            TenantService = tenantService;
            RoutingPolicyMatchOptionService = routingPolicyMatchOptionService;
            Mapper = mapper;
            TenantCommunitySetValidator = tenantCommunitySetValidator;
            this.Validator = tenantCommunitySetValidator;
        }

        private ITenantCommunitySetService TenantCommunitySetService { get; set; }
        private IVpnService VpnService { get; set; }
        private ITenantService TenantService { get; set; }
        private IRoutingPolicyMatchOptionService RoutingPolicyMatchOptionService { get; set; }
        private IMapper Mapper { get; set; }
        private ITenantCommunitySetValidator TenantCommunitySetValidator { get; set; }

        [HttpGet]
        public async Task<IActionResult> GetAllByTenantID(int? id, bool showWarningMessage = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenantCommunities = await TenantCommunitySetService.GetAllByTenantIDAsync(id.Value);
            if (showWarningMessage)
            {
                ViewData["NetworkWarningMessage"] = $"VPNs require synchronisation with the network as a result of this update. "
                            + "Follow this <a href = '/Vpn/GetAll'>link</a> to the VPNs page.";
            }

            ViewBag.Tenant = await TenantService.GetByIDAsync(id.Value);
            return View(Mapper.Map<List<TenantCommunitySetViewModel>>(tenantCommunities));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await TenantCommunitySetService.GetByIDAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            ViewBag.Tenant = await TenantService.GetByIDAsync(item.TenantID);
            return View(Mapper.Map<TenantCommunitySetViewModel>(item));
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.Tenant = await TenantService.GetByIDAsync(id.Value);
            await PopulateRoutingPolicyMatchOptionsDropDownListAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenantID,Name,RoutingPolicyMatchOptionID")] TenantCommunitySetViewModel tenantCommunitySetModel)
        {
            if (ModelState.IsValid)
            {
                var tenantCommunitySet = Mapper.Map<TenantCommunitySet>(tenantCommunitySetModel);
                await TenantCommunitySetValidator.ValidateNewAsync(tenantCommunitySet);

                if (TenantCommunitySetValidator.ValidationDictionary.IsValid)
                {
                    try
                    {
                        await TenantCommunitySetService.AddAsync(Mapper.Map<TenantCommunitySet>(tenantCommunitySetModel));
                        return RedirectToAction("GetAllByTenantID", new
                        {
                            id = tenantCommunitySetModel.TenantID
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
        
            ViewBag.Tenant = await TenantService.GetByIDAsync(tenantCommunitySetModel.TenantID);
            await PopulateRoutingPolicyMatchOptionsDropDownListAsync(tenantCommunitySetModel.RoutingPolicyMatchOptionID);

            return View(Mapper.Map<TenantCommunitySetViewModel>(tenantCommunitySetModel));
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenantCommunitySet = await TenantCommunitySetService.GetByIDAsync(id.Value);
            if (tenantCommunitySet == null)
            {
                return NotFound();
            }

            ViewBag.Tenant = await TenantService.GetByIDAsync(id.Value);
            await PopulateRoutingPolicyMatchOptionsDropDownListAsync(tenantCommunitySet.RoutingPolicyMatchOptionID);

            return View(Mapper.Map<TenantCommunitySetViewModel>(tenantCommunitySet));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("TenantCommunitySetID,TenantID,Name," +
            "RoutingPolicyMatchOptionID,RowVersion")] TenantCommunitySetViewModel tenantCommunitySetModel)
        {
            if (id != tenantCommunitySetModel.TenantCommunitySetID)
            {
                return NotFound();
            }

            var tenantCommunitySet = await TenantCommunitySetService.GetByIDAsync(id);
            if (tenantCommunitySet == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. The item was deleted by another user.");
            }

            try
            {
                var item = Mapper.Map<TenantCommunitySet>(tenantCommunitySetModel);
                await TenantCommunitySetValidator.ValidateChangesAsync(item);

                if (TenantCommunitySetValidator.ValidationDictionary.IsValid)
                {
                    await TenantCommunitySetService.UpdateAsync(item);

                    // Check if VPNs need re-sync to network as a result of the change to the Tenant Community Set
                    // and generate a message for the view if so

                    var vpns = await VpnService.GetAllByTenantCommunitySetIDAsync(tenantCommunitySet.TenantCommunitySetID);
    
                    return RedirectToAction("GetAllByTenantID", new
                    {
                        id = tenantCommunitySet.TenantID,
                        showWarningMessage = vpns.Any()
                    });
                }
            }

            catch (DbUpdateConcurrencyException ex)
            {
                var exceptionEntry = ex.Entries.Single();

                var proposedName = (string)exceptionEntry.Property("Name").CurrentValue;
                if (tenantCommunitySet.Name != proposedName)
                {
                    ModelState.AddModelError("Name", $"Current value: {tenantCommunitySet.Name}");
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

            ViewBag.Tenant = await TenantService.GetByIDAsync(tenantCommunitySet.TenantID);
            await PopulateRoutingPolicyMatchOptionsDropDownListAsync(tenantCommunitySet.RoutingPolicyMatchOptionID);

            return View(Mapper.Map<TenantCommunitySetViewModel>(tenantCommunitySet));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id, int? tenantID, bool? concurrencyError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenantCommunitySet = await TenantCommunitySetService.GetByIDAsync(id.Value);
            if (tenantCommunitySet == null)
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

            var tenant = await TenantService.GetByIDAsync(tenantCommunitySet.TenantID);
            ViewBag.Tenant = Mapper.Map<TenantViewModel>(tenant);

            return View(Mapper.Map<TenantCommunitySetViewModel>(tenantCommunitySet));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(TenantCommunitySetViewModel tenantCommunitySetModel)
        {
            var tenantCommunitySet = await TenantCommunitySetService.GetByIDAsync(tenantCommunitySetModel.TenantCommunitySetID);
            if (tenantCommunitySet == null)
            {
                return RedirectToAction("GetAllByTenantID", new
                {
                    id = tenantCommunitySetModel.TenantID
                });
            }

            try
            {
                this.Validator.ValidationDictionary.Clear();
                await TenantCommunitySetValidator.ValidateDeleteAsync(tenantCommunitySet);

                if (TenantCommunitySetValidator.ValidationDictionary.IsValid)
                {
                    await TenantCommunitySetService.DeleteAsync(Mapper.Map<TenantCommunitySet>(tenantCommunitySetModel));
                    return RedirectToAction("GetAllByTenantID", new
                    {
                        id = tenantCommunitySetModel.TenantID
                    });
                }
            }

            catch (DbUpdateConcurrencyException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("Delete", new
                {
                    concurrencyError = true,
                    id = tenantCommunitySetModel.TenantCommunitySetID,
                    tenantID = tenantCommunitySetModel.TenantID
                });
            }

            var tenant = await TenantService.GetByIDAsync(tenantCommunitySet.TenantID);
            ViewBag.Tenant = Mapper.Map<TenantViewModel>(tenant);

            return View(Mapper.Map<TenantCommunitySetViewModel>(tenantCommunitySet));
        }

        /// <summary>
        /// Helper to populate a drop-down list of Routing Policy Match Options
        /// </summary>
        /// <returns></returns>
        private async Task PopulateRoutingPolicyMatchOptionsDropDownListAsync(object selectedRoutingPolicyMatchOption = null)
        {
            var routingPolicyMatchOptions = await RoutingPolicyMatchOptionService.GetAllAsync();
            ViewBag.RoutingPolicyMatchOptionID = new SelectList(routingPolicyMatchOptions, 
                "RoutingPolicyMatchOptionID", "Name", selectedRoutingPolicyMatchOption);
        }
    }
}