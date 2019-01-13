using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper;
using SCM.Models;
using SCM.Controllers;
using Mind.Services;
using SCM.Data;
using Mind.Models;
using Mind.Builders;
using Mind.WebUI.Attributes;
using Mind.WebUI.Models;
using Mind.Models.RequestModels;

namespace Mind.WebUI.Controllers
{
    public class TenantCommunityController : BaseViewController
    {
        private readonly ITenantCommunityService _tenantCommunityService;
        public TenantCommunityController(ITenantCommunityService tenantCommunityService, IUnitOfWork unitOfWork, IMapper mapper) :
            base(unitOfWork, mapper)
        {
            _tenantCommunityService = tenantCommunityService;
        }

        [HttpGet]
        [ValidateTenantCommunityExists]
        public async Task<IActionResult> Details(int? tenantCommunityId)
        {
            var item = await _tenantCommunityService.GetByIDAsync(tenantCommunityId.Value, deep: true, asTrackable: false);
            return View(_mapper.Map<TenantCommunityViewModel>(item));
        }

        [HttpGet]
        [ValidateTenantExists]
        public async Task<IActionResult> GetAllByTenantID(int? tenantId)
        {
            var tenantCommunitys = await _unitOfWork.TenantCommunityRepository.GetAsync(
                    q =>
                    q.TenantID == tenantId.Value,
                    query: q => q.IncludeDeepProperties(),
                    AsTrackable: false);

            var tenant = await _unitOfWork.TenantRepository.GetByIDAsync(tenantId);
            ViewBag.Tenant = _mapper.Map<TenantViewModel>(tenant);

            return View(_mapper.Map<List<TenantCommunityViewModel>>(tenantCommunitys));
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
        public async Task<IActionResult> Create(int? tenantId, TenantCommunityRequestViewModel requestModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var request = _mapper.Map<TenantCommunityRequest>(requestModel);

                    var attachment = await _tenantCommunityService.AddAsync(tenantId.Value, request);
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
        [ValidateTenantCommunityExists]
        public async Task<ActionResult> Edit(int? tenantCommunityId)
        {
            var tenantCommunity = await _tenantCommunityService.GetByIDAsync(tenantCommunityId.Value, deep: true, asTrackable: false);
            var tenant = await _unitOfWork.TenantRepository.GetByIDAsync(tenantCommunity.TenantID);
            ViewBag.Tenant = _mapper.Map<TenantViewModel>(tenant);
            PopulateIpRoutingBehavioursDropDownList(tenantCommunity.IpRoutingBehaviour);

            return View(_mapper.Map<TenantCommunityUpdateViewModel>(tenantCommunity));
        }

        [HttpPost]
        [ValidateTenantCommunityExists]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? tenantCommunityId, TenantCommunityUpdateViewModel update)
        {
            var tenantCommunity = await _tenantCommunityService.GetByIDAsync(tenantCommunityId.Value, deep: true, asTrackable: false);

            if (ModelState.IsValid)
            {
                if (tenantCommunity.HasPreconditionFailed(Request, update.GetConcurrencyToken()))
                {
                    ModelState.AddUpdatePreconditionFailedMessage();
                    ModelState.RemoveConcurrencyTokenItem();
                    update.UpdateConcurrencyToken(tenantCommunity.GetConcurrencyToken());
                }
                else
                {

                    var tenantCommunityUpdate = _mapper.Map<TenantCommunityRequest>(update);

                    try
                    {
                        await _tenantCommunityService.UpdateAsync(tenantCommunityId.Value, tenantCommunityUpdate);
                        return RedirectToAction(nameof(GetAllByTenantID), new { tenantId = tenantCommunity.TenantID });
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

            var tenant = await _unitOfWork.TenantRepository.GetByIDAsync(tenantCommunity.TenantID);
            ViewBag.Tenant = _mapper.Map<TenantViewModel>(tenant);
            PopulateIpRoutingBehavioursDropDownList(update.IpRoutingBehaviour);

            return View(_mapper.Map<TenantCommunityUpdateViewModel>(tenantCommunity));
        }

        [HttpGet]
        [ValidateTenantCommunityExists]
        public async Task<IActionResult> Delete(int? tenantCommunityId, bool? concurrencyError = false)
        {
            var item = await _tenantCommunityService.GetByIDAsync(tenantCommunityId.Value, deep: true, asTrackable: false);
            if (concurrencyError.GetValueOrDefault()) ViewData.AddDeletePreconditionFailedMessage();
            var tenant = await _unitOfWork.TenantRepository.GetByIDAsync(item.TenantID);
            ViewBag.Tenant = _mapper.Map<TenantViewModel>(tenant);

            return View(_mapper.Map<TenantCommunityDeleteViewModel>(item));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(TenantCommunityDeleteViewModel model)
        {
            var tenantCommunity = await _tenantCommunityService.GetByIDAsync(model.TenantCommunityId.Value, deep: true, asTrackable: false);
            if (tenantCommunity == null) return RedirectToAction(nameof(GetAllByTenantID), new { tenantId = model.TenantId });

            if (tenantCommunity.HasPreconditionFailed(Request, model.GetConcurrencyToken()))
            {
                return RedirectToAction(nameof(Delete), new
                {
                    tenantCommunityId = tenantCommunity.TenantCommunityID,
                    concurrencyError = true
                });
            }

            try
            {
                await _tenantCommunityService.DeleteAsync(tenantCommunity.TenantCommunityID);
                return RedirectToAction(nameof(GetAllByTenantID), new { tenantId = tenantCommunity.TenantID });
            }

            catch (IllegalDeleteAttemptException ex)
            {
                ViewData.AddDeleteValidationFailedMessage(ex.Message);
            }

            catch (DbUpdateException)
            {
                ViewData.AddDatabaseUpdateExceptionMessage();
            }

            var tenant = await _unitOfWork.TenantRepository.GetByIDAsync(tenantCommunity.TenantID);
            ViewBag.Tenant = _mapper.Map<TenantViewModel>(tenant);

            return View(_mapper.Map<TenantCommunityDeleteViewModel>(tenantCommunity));
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