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

namespace SCM.Controllers
{
    public class TenantController : BaseViewController
    {
        public TenantController(ITenantService tenantService,
            ITenantValidator tenantValidator,
            IMapper mapper)
        {
            TenantService = tenantService;
            TenantValidator = tenantValidator;
            this.Validator = tenantValidator;
            Mapper = mapper;
        }

        private ITenantService TenantService { get; set; }
        private ITenantValidator TenantValidator { get; set; }
        private IMapper Mapper { get; set; }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tenants = await TenantService.GetAllAsync();
            return View(Mapper.Map<List<TenantViewModel>>(tenants));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await TenantService.GetByIDAsync(id.Value);
            if (item == null)
            {
                return NotFound();
            }

            return View(Mapper.Map<TenantViewModel>(item));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] TenantViewModel tenant)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await TenantService.AddAsync(Mapper.Map<Tenant>(tenant));
                    return RedirectToAction("GetAll");
                }

                catch (DbUpdateException /** ex **/ )
                {
                    //Log the error (uncomment ex variable name and write a log.
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists " +
                        "see your system administrator.");
                }
            }
       
            return View(Mapper.Map<TenantViewModel>(tenant));
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
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

            return View(Mapper.Map<TenantViewModel>(tenant));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("TenantID,Name,RowVersion")] TenantViewModel tenantModel)
        {
            if (id != tenantModel.TenantID)
            {
                return NotFound();
            }

            var tenant = await TenantService.GetByIDAsync(tenantModel.TenantID);
            if (tenant == null)
            {
                ModelState.AddModelError(string.Empty, "Unable to save changes. The Tenant was deleted by another user.");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var updateTenant = Mapper.Map<Tenant>(tenantModel);
                    await TenantService.UpdateAsync(updateTenant);

                    return RedirectToAction("GetAll");
                }
            }

            catch (DbUpdateConcurrencyException ex)
            {
                var exceptionEntry = ex.Entries.Single();

                var proposedName = (string)exceptionEntry.Property("Name").CurrentValue;
                if (tenant.Name != proposedName)
                {
                    ModelState.AddModelError("Name", $"Current value: {tenant.Name}");
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

            return View(Mapper.Map<TenantViewModel>(tenant));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id, bool? concurrencyError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenant = await TenantService.GetByIDAsync(id.Value);
            if (tenant == null)
            {
                if (concurrencyError.GetValueOrDefault())
                {
                    return RedirectToAction("GetAll");
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

            return View(Mapper.Map<TenantViewModel>(tenant));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(TenantViewModel tenantModel)
        {
            try
            {
                var tenant = await TenantService.GetByIDAsync(tenantModel.TenantID);
                if (tenant == null)
                {
                    return RedirectToAction("GetAll");
                }

                this.Validator.ValidationDictionary.Clear();
                await TenantValidator.ValidateDeleteAsync(tenant);
                if (TenantValidator.ValidationDictionary.IsValid)
                {
                    await TenantService.DeleteAsync(Mapper.Map<Tenant>(tenantModel));
                    return RedirectToAction("GetAll");
                }
                else
                {
                    return View(Mapper.Map<TenantViewModel>(tenant));
                }
            }

            catch (DbUpdateConcurrencyException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction("Delete", new { concurrencyError = true, id = tenantModel.TenantID });
            }
        }
    }
}
