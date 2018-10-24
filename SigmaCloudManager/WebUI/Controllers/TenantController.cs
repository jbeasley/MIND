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
using SCM.Controllers;
using Mind.Services;
using SCM.Data;
using Mind.WebUI.Models;
using Mind.WebUI.Attributes;
using Mind.Builders;
using Mind.Models;

namespace Mind.WebUI.Controllers
{
    public class TenantController : BaseViewController
    {
        private readonly ITenantService _tenantService;

        public TenantController(ITenantService tenantService, IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _tenantService = tenantService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tenants = await _tenantService.GetAllAsync();
            return View(_mapper.Map<List<TenantViewModel>>(tenants));
        }

        [HttpGet]
        [ValidateTenantExists]
        public async Task<IActionResult> Details(int? tenantId)
        {
            var item = await this._tenantService.GetByIDAsync(tenantId.Value);
            return View(_mapper.Map<TenantViewModel>(item));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TenantRequestViewModel tenant)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _tenantService.AddAsync(_mapper.Map<Tenant>(tenant));
                    return RedirectToAction(nameof(GetAll));
                }

                catch (BuilderBadArgumentsException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

                catch (BuilderUnableToCompleteException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

                catch (IllegalStateException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

                catch (DbUpdateException)
                {
                    ModelState.AddDatabaseUpdateExceptionMessage();
                }
            }

            return View(tenant);
        }

        [HttpGet]
        [ValidateTenantExists]
        public async Task<ActionResult> Edit(int? tenantId)
        {
            var tenant = await _tenantService.GetByIDAsync(tenantId.Value);
            return View(_mapper.Map<TenantUpdateViewModel>(tenant));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateTenantExists]
        public async Task<ActionResult> Edit(int tenantId, TenantUpdateViewModel update)
        {
            var tenant = await _tenantService.GetByIDAsync(tenantId);

            if (ModelState.IsValid)
            {
                if (tenant.HasPreconditionFailed(Request, update.GetConcurrencyToken()))
                {
                    ModelState.AddUpdatePreconditionFailedMessage();
                    ModelState.RemoveConcurrencyTokenItem();
                    update.UpdateConcurrencyToken(tenant.GetConcurrencyToken());
                }
                else
                {
                    try
                    {

                        var updateTenant = _mapper.Map<Tenant>(update);
                        await _tenantService.UpdateAsync(updateTenant);

                        return RedirectToAction(nameof(GetAll));
                    }

                    catch (BuilderBadArgumentsException ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }

                    catch (BuilderUnableToCompleteException ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }

                    catch (IllegalStateException ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }

                    catch (DbUpdateException)
                    {
                        ModelState.AddDatabaseUpdateExceptionMessage();
                    }
                }
            }

            return View(_mapper.Map<TenantUpdateViewModel>(tenant));
        }

        [HttpGet]
        [ValidateTenantExists]
        public async Task<IActionResult> Delete(int? tenantId, bool? concurrencyError = false)
        {
            var tenant = await _tenantService.GetByIDAsync(tenantId.Value);
            if (concurrencyError.GetValueOrDefault()) ViewData.AddDeletePreconditionFailedMessage();
            return View(_mapper.Map<TenantViewModel>(tenant));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateTenantExists]
        public async Task<IActionResult> Delete(int? tenantId, TenantViewModel model)
        {
            var tenant = await _tenantService.GetByIDAsync(tenantId.Value);
            if (tenant == null) return RedirectToAction(nameof(GetAll));

            if (tenant.HasPreconditionFailed(Request, model.GetConcurrencyToken()))
            {
                return RedirectToAction(nameof(Delete), new
                {
                    tenantId = tenant.TenantID,
                    concurrencyError = true
                });
            }

            try
            {
                await _tenantService.DeleteAsync(tenant.TenantID);
                return RedirectToAction(nameof(GetAll));
            }

            catch (IllegalDeleteAttemptException ex)
            {
                ViewData.AddDeleteValidationFailedMessage(ex.Message);
            }

            catch (DbUpdateException)
            {
                ViewData.AddDatabaseUpdateExceptionMessage();
            }

            return View(_mapper.Map<TenantViewModel>(tenant));

        }
    }
}
