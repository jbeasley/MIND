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
        [ValidateModelState]
        public async Task<IActionResult> Create(TenantRequestViewModel tenant)
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

            return View(_mapper.Map<TenantViewModel>(tenant));
        }

        [HttpGet]
        [ValidateTenantExists]
        public async Task<ActionResult> Edit(int? tenantId)
        {
            var tenant = await _tenantService.GetByIDAsync(tenantId.Value);
            return View(_mapper.Map<TenantViewModel>(tenant));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateModelState]
        [ValidateTenantExists]
        public async Task<ActionResult> Edit(int tenantId, TenantRequestViewModel tenantModel)
        {

            var tenant = await _tenantService.GetByIDAsync(tenantId);

            try
            {

                var updateTenant = _mapper.Map<Tenant>(tenantModel);
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

            return View(_mapper.Map<TenantViewModel>(tenant));
        }

        [HttpGet]
        [ValidateTenantExists]
        public async Task<IActionResult> Delete(int? tenantId, bool? concurrencyError = false)
        {
            var tenant = await _tenantService.GetByIDAsync(tenantId.Value);
            return View(_mapper.Map<TenantViewModel>(tenant));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(TenantViewModel model)
        {
            var tenant = await _tenantService.GetByIDAsync(model.TenantId.Value);
            if (tenant == null) return RedirectToAction(nameof(GetAll));

            if (tenant.HasPreconditionFailed(Request, model.RowVersion.ToString()))
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

            catch (DbUpdateException)
            {
                ViewData.AddDatabaseUpdateExceptionMessage();
            }

            return View(_mapper.Map<TenantViewModel>(tenant));

        }
    }
}
