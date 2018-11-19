using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper;
using SCM.Models.RequestModels;
using SCM.Models.ViewModels;
using SCM.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using SCM.Controllers;
using Mind.Services;
using SCM.Data;
using Mind.Models;
using Mind.Builders;
using Mind.WebUI.Attributes;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Mind.WebUI.Models;
using Mind.Models.RequestModels;
using Mind.WebUI.ViewComponents;

namespace Mind.WebUI.Controllers
{
    public class TenantIpNetworkController : BaseViewController
    {
        private readonly ITenantIpNetworkService _tenantIpNetworkService;
        public TenantIpNetworkController(ITenantIpNetworkService tenantIpNetworkService, IUnitOfWork unitOfWork, IMapper mapper) :
            base(unitOfWork, mapper)
        {
            _tenantIpNetworkService = tenantIpNetworkService;
        }

        [HttpGet]
        [ValidateTenantIpNetworkExists]
        public async Task<IActionResult> Details(int? tenantIpNetworkId)
        {
            var item = await _tenantIpNetworkService.GetByIDAsync(tenantIpNetworkId.Value, deep: true, asTrackable: false);
            return View(_mapper.Map<TenantIpNetworkViewModel>(item));
        }

        [HttpGet]
        [ValidateTenantExists]
        public async Task<IActionResult> GetAllByTenantID(int? tenantId)
        {
            var tenantIpNetworks = await _unitOfWork.TenantIpNetworkRepository.GetAsync(
                    q =>
                    q.TenantID == tenantId.Value,
                    query: q => q.IncludeDeepProperties(),
                    AsTrackable: false);

            var tenant = await _unitOfWork.TenantRepository.GetByIDAsync(tenantId);
            ViewBag.Tenant = _mapper.Map<TenantViewModel>(tenant);

            return View(_mapper.Map<List<TenantIpNetworkViewModel>>(tenantIpNetworks));
        }

        [HttpGet]
        [ValidateTenantExists]
        public async Task<IActionResult> Create(int? tenantId)
        {
            var tenant = await _unitOfWork.TenantRepository.GetByIDAsync(tenantId);
            ViewBag.Tenant = _mapper.Map<TenantViewModel>(tenant);
            PopulateIpRoutingBehavioursDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateTenantExists]
        public async Task<IActionResult> Create(int? tenantId, TenantIpNetworkRequestViewModel requestModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var request = _mapper.Map<TenantIpNetworkRequest>(requestModel);

                    var attachment = await _tenantIpNetworkService.AddAsync(tenantId.Value, request);
                    return RedirectToAction(nameof(GetAllByTenantID), new { tenantId });
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

            var tenant = await _unitOfWork.TenantRepository.GetByIDAsync(tenantId);
            ViewBag.Tenant = _mapper.Map<TenantViewModel>(tenant);
            PopulateIpRoutingBehavioursDropDownList();

            return View(requestModel);
        }

        [HttpGet]
        [ValidateTenantIpNetworkExists]
        public async Task<ActionResult> Edit(int? tenantIpNetworkId)
        {
            var tenantIpNetwork = await _tenantIpNetworkService.GetByIDAsync(tenantIpNetworkId.Value, deep: true, asTrackable: false);
            var tenant = await _unitOfWork.TenantRepository.GetByIDAsync(tenantIpNetwork.TenantID);
            ViewBag.Tenant = _mapper.Map<TenantViewModel>(tenant);
            PopulateIpRoutingBehavioursDropDownList(tenantIpNetwork.IpRoutingBehaviour);

            return View(_mapper.Map<TenantIpNetworkUpdateViewModel>(tenantIpNetwork));
        }

        [HttpPost]
        [ValidateTenantIpNetworkExists]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? tenantIpNetworkId, TenantIpNetworkUpdateViewModel update)
        {
            var tenantIpNetwork = await _tenantIpNetworkService.GetByIDAsync(tenantIpNetworkId.Value, deep: true, asTrackable: false);

            if (ModelState.IsValid)
            {
                if (tenantIpNetwork.HasPreconditionFailed(Request, update.GetConcurrencyToken()))
                {
                    ModelState.AddUpdatePreconditionFailedMessage();
                    ModelState.RemoveConcurrencyTokenItem();
                    update.UpdateConcurrencyToken(tenantIpNetwork.GetConcurrencyToken());
                }
                else
                {

                    var tenantIpNetworkUpdate = _mapper.Map<TenantIpNetworkRequest>(update);

                    try
                    {
                        await _tenantIpNetworkService.UpdateAsync(tenantIpNetworkId.Value, tenantIpNetworkUpdate);
                        return RedirectToAction(nameof(GetAllByTenantID), new { tenantId = tenantIpNetwork.TenantID });
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

                    catch (IllegalDeleteAttemptException ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }

                    catch (DbUpdateException)
                    {
                        ModelState.AddDatabaseUpdateExceptionMessage();
                    }
                }
            }

            var tenant = await _unitOfWork.TenantRepository.GetByIDAsync(tenantIpNetwork.TenantID);
            ViewBag.Tenant = _mapper.Map<TenantViewModel>(tenant);
            PopulateIpRoutingBehavioursDropDownList(update.IpRoutingBehaviour);

            return View(_mapper.Map<TenantIpNetworkUpdateViewModel>(tenantIpNetwork));
        }

        [HttpGet]
        [ValidateTenantIpNetworkExists]
        public async Task<IActionResult> Delete(int? tenantIpNetworkId, bool? concurrencyError = false)
        {
            var item = await _tenantIpNetworkService.GetByIDAsync(tenantIpNetworkId.Value, deep: true, asTrackable: false);
            if (concurrencyError.GetValueOrDefault()) ViewData.AddDeletePreconditionFailedMessage();
            var tenant = await _unitOfWork.TenantRepository.GetByIDAsync(item.TenantID);
            ViewBag.Tenant = _mapper.Map<TenantViewModel>(tenant);

            return View(_mapper.Map<TenantIpNetworkDeleteViewModel>(item));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(TenantIpNetworkDeleteViewModel model)
        {
            var tenantIpNetwork = await _tenantIpNetworkService.GetByIDAsync(model.TenantIpNetworkId.Value, deep: true, asTrackable: false);
            if (tenantIpNetwork == null) return RedirectToAction(nameof(GetAllByTenantID), new { tenantId = model.TenantId });

            if (tenantIpNetwork.HasPreconditionFailed(Request, model.GetConcurrencyToken()))
            {
                return RedirectToAction(nameof(Delete), new
                {
                    tenantIpNetworkId = tenantIpNetwork.TenantIpNetworkID,
                    concurrencyError = true
                });
            }

            try
            {
                await _tenantIpNetworkService.DeleteAsync(tenantIpNetwork.TenantIpNetworkID);
                return RedirectToAction(nameof(GetAllByTenantID), new { tenantId = tenantIpNetwork.TenantID });
            }

            catch (IllegalDeleteAttemptException ex)
            {
                ViewData.AddDeleteValidationFailedMessage(ex.Message);
            }

            catch (DbUpdateException)
            {
                ViewData.AddDatabaseUpdateExceptionMessage();
            }

            var tenant = await _unitOfWork.TenantRepository.GetByIDAsync(tenantIpNetwork.TenantID);
            ViewBag.Tenant = _mapper.Map<TenantViewModel>(tenant);

            return View(_mapper.Map<TenantIpNetworkDeleteViewModel>(tenantIpNetwork));
        }

        private void PopulateIpRoutingBehavioursDropDownList(object selectedIpRoutingBehavior = null)
        {
            IList<SelectListItem> list = Enum.GetValues(typeof(TenantIpRoutingBehaviourEnum))
                .Cast<TenantIpRoutingBehaviourEnum>()
                .Select(x => new SelectListItem { Text = x.ToString(), Value = ((int)x).ToString() }).ToList();

            ViewBag.IpRoutingBehaviour = new SelectList(list, "Value", "Text");
        }
    }
}