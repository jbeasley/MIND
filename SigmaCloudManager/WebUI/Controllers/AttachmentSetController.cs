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
    public class AttachmentSetController : BaseViewController
    {
        private readonly IAttachmentSetService _attachmentSetService;
        public AttachmentSetController(IAttachmentSetService attachmentSetService, IUnitOfWork unitOfWork, IMapper mapper) :
            base(unitOfWork, mapper)
        {
            _attachmentSetService = attachmentSetService;
        }

        [HttpGet]
        public async Task<PartialViewResult> RoutingInstances(int? tenantId, string region, string subRegion)
        {
            var query = (from result in await _unitOfWork.RoutingInstanceRepository.GetAsync(
                    q =>
                        q.TenantID == tenantId &&
                        q.RoutingInstanceType.IsTenantFacingVrf,
                        query: q => q.Include(x => x.Device.Location.SubRegion.Region)
                                     .Include(x => x.Device.Plane))
                        select result);

            if (!string.IsNullOrEmpty(region)) query = query.Where(q => q.Device.Location.SubRegion.Region.Name == region);
            if (!string.IsNullOrEmpty(subRegion)) query = query.Where(q => q.Device.Location.SubRegion.Name == subRegion);

            var items = query.Select(
                x => 
                new ProviderDomainRoutingInstanceViewModel
                {
                    Name = x.Name,
                    DisplayName = $"{x.Name}, {x.Device.Location.SiteName}, {x.Device.Plane.Name}"
                });

            return PartialView(items);
        }

        [HttpGet]
        public async Task<PartialViewResult> SubRegions(string region)
        {
            var subRegions = await _unitOfWork.SubRegionRepository.GetAsync(
                q =>
                q.Region.Name == region);

            return PartialView(_mapper.Map<List<SubRegionViewModel>>(subRegions));
        }

        [HttpGet]
        [ValidateAttachmentSetExists]
        public async Task<IActionResult> Details(int? attachmentSetId)
        {
            var item = await _attachmentSetService.GetByIDAsync(attachmentSetId.Value, deep: true, asTrackable: false);
            return View(_mapper.Map<AttachmentSetViewModel>(item));
        }

        [HttpGet]
        [ValidateTenantExists]
        public async Task<IActionResult> GetAllByTenantID(int? tenantId)
        {
            var attachmentSets = await _unitOfWork.AttachmentSetRepository.GetAsync(
                    q =>
                    q.TenantID == tenantId.Value,
                    query: q => q.IncludeValidationProperties(),
                    AsTrackable: false);

            // Display errors if the redundancy setting of the attachment set is mis-configured
            try
            {
                foreach (var attachmentSet in attachmentSets)
                {
                    attachmentSet.ValidateAttachmentRedundancy();
                }
            }

            catch (IllegalStateException ex)
            {
                ViewData.AddWarningMessage(ex.Message);
            }

            var tenant = await _unitOfWork.TenantRepository.GetByIDAsync(tenantId);               
            ViewBag.Tenant = _mapper.Map<TenantViewModel>(tenant);

            return View(_mapper.Map<List<AttachmentSetViewModel>>(attachmentSets));
        }

        [HttpGet]
        [ValidateTenantExists]
        public async Task<IActionResult> Create(int? tenantId)
        {
            var tenant = await _unitOfWork.TenantRepository.GetByIDAsync(tenantId);
            ViewBag.Tenant = _mapper.Map<TenantViewModel>(tenant);
            await PopulateRegionsDropDownList();
            await PopulateAttachmentRedundancyOptionsDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateTenantExists]
        public async Task<IActionResult> Create(int? tenantId, AttachmentSetRequestViewModel requestModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Project the list of attachment set routing instance names into a list of AttachmentSetRoutingInstanceRequest objects
                    // to be passed to the service layer
                    requestModel.AttachmentSetRoutingInstances = requestModel.AttachmentSetRoutingInstanceNames
                        .Select(
                            routingInstanceName => 
                            new AttachmentSetRoutingInstanceRequestViewModel
                            {
                                RoutingInstanceName = routingInstanceName
                            })
                        .ToList();

                    var request = _mapper.Map<AttachmentSetRequest>(requestModel);

                    var attachment = await _attachmentSetService.AddAsync(tenantId.Value, request);
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
            await PopulateRegionsDropDownList(requestModel.Region.ToString());
            await PopulateSubRegionsDropDownList(requestModel.Region?.ToString(), requestModel.SubRegion?.ToString());
            await PopulateRoutingInstancesDropDownList(tenantId.Value, requestModel.Region?.ToString(), requestModel.SubRegion,
                requestModel.AttachmentSetRoutingInstanceNames);
            await PopulateAttachmentRedundancyOptionsDropDownList(requestModel.AttachmentRedundancy);

            return View(requestModel);
        }

        [HttpGet]
        [ValidateAttachmentSetExists]
        public async Task<ActionResult> Edit(int? attachmentSetId)
        {
            var attachmentSet = await _attachmentSetService.GetByIDAsync(attachmentSetId.Value, deep: true, asTrackable: false);
            var tenant = await _unitOfWork.TenantRepository.GetByIDAsync(attachmentSet.TenantID);
            ViewBag.Tenant = _mapper.Map<TenantViewModel>(tenant);
            await PopulateRoutingInstancesDropDownList(tenant.TenantID, attachmentSet.Region.Name, attachmentSet.SubRegion?.Name,
                attachmentSet.AttachmentSetRoutingInstances.Select(x => x.RoutingInstance.Name).ToList());
            await PopulateAttachmentRedundancyOptionsDropDownList(attachmentSet.AttachmentRedundancy.Name);
            await PopulateSubRegionsDropDownList(attachmentSet.Region.Name, attachmentSet.SubRegion.Name);

            return View(_mapper.Map<AttachmentSetUpdateViewModel>(attachmentSet));
        }

        [HttpPost]
        [ValidateAttachmentSetExists]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? attachmentSetId, AttachmentSetUpdateViewModel update)
        {
            var attachmentSet = await _attachmentSetService.GetByIDAsync(attachmentSetId.Value, deep: true, asTrackable: false);

            if (ModelState.IsValid)
            {
                if (attachmentSet.HasPreconditionFailed(Request, update.GetConcurrencyToken()))
                {
                    ModelState.AddUpdatePreconditionFailedMessage();
                    ModelState.RemoveConcurrencyTokenItem();
                    update.UpdateConcurrencyToken(attachmentSet.GetConcurrencyToken());
                }
                else
                {
                    // Project the list of attachment set routing instance names into a list of AttachmentSetRoutingInstanceRequest objects
                    // to be passed to the service layer
                    update.AttachmentSetRoutingInstances = update.AttachmentSetRoutingInstanceNames
                        .Select(
                            routingInstanceName =>
                            new AttachmentSetRoutingInstanceRequestViewModel
                            {
                                RoutingInstanceName = routingInstanceName
                            })
                        .ToList();

                    var attachmentUpdate = _mapper.Map<AttachmentSetUpdate>(update);

                    try
                    {
                        await _attachmentSetService.UpdateAsync(attachmentSetId.Value, attachmentUpdate);
                        return RedirectToAction(nameof(GetAllByTenantID), new { tenantId = attachmentSet.TenantID });
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

            var tenant = await _unitOfWork.TenantRepository.GetByIDAsync(attachmentSet.TenantID);
            ViewBag.Tenant = _mapper.Map<TenantViewModel>(tenant);
            await PopulateRoutingInstancesDropDownList(tenant.TenantID, attachmentSet.Region.Name, update.SubRegion,
                    attachmentSet.AttachmentSetRoutingInstances.Select(x => x.RoutingInstance.Name).ToList());
            await PopulateAttachmentRedundancyOptionsDropDownList(update.AttachmentRedundancy.ToString());
            await PopulateSubRegionsDropDownList(attachmentSet.Region.Name, update.SubRegion);

            return View(_mapper.Map<AttachmentSetUpdateViewModel>(attachmentSet));
        }

        [HttpGet]
        [ValidateAttachmentSetExists]
        public async Task<IActionResult> Delete(int? attachmentSetId, bool? concurrencyError = false)
        {
            var item = await _attachmentSetService.GetByIDAsync(attachmentSetId.Value, deep: true, asTrackable: false);
            if (concurrencyError.GetValueOrDefault()) ViewData.AddDeletePreconditionFailedMessage();
            var tenant = await _unitOfWork.TenantRepository.GetByIDAsync(item.TenantID);
            ViewBag.Tenant = _mapper.Map<TenantViewModel>(tenant);

            return View(_mapper.Map<AttachmentSetDeleteViewModel>(item));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(AttachmentSetDeleteViewModel model)
        {
            var attachmentSet = await _attachmentSetService.GetByIDAsync(model.AttachmentSetId.Value, deep: true, asTrackable: false);
            if (attachmentSet == null) return RedirectToAction(nameof(GetAllByTenantID), new { tenantId = model.TenantId });

            if (attachmentSet.HasPreconditionFailed(Request, model.GetConcurrencyToken()))
            {
                return RedirectToAction(nameof(Delete), new
                {
                    attachmentSetId = attachmentSet.AttachmentSetID,
                    concurrencyError = true
                });
            }

            try
            {
                await _attachmentSetService.DeleteAsync(attachmentSet.AttachmentSetID);
                return RedirectToAction(nameof(GetAllByTenantID), new { tenantId = attachmentSet.TenantID });
            }

            catch (IllegalDeleteAttemptException ex)
            {
                ViewData.AddDeleteValidationFailedMessage(ex.Message);
            }

            catch (DbUpdateException)
            {
                ViewData.AddDatabaseUpdateExceptionMessage();
            }

            var tenant = await _unitOfWork.TenantRepository.GetByIDAsync(attachmentSet.TenantID);
            ViewBag.Tenant = _mapper.Map<TenantViewModel>(tenant);

            return View(_mapper.Map<AttachmentSetDeleteViewModel>(attachmentSet));
        }

        private async Task PopulateRegionsDropDownList(object selectedRegion = null)
        {
            var regions = await _unitOfWork.RegionRepository.GetAsync();
            ViewBag.Region = new SelectList(_mapper.Map<List<RegionViewModel>>(regions), 
                "Name", "Name", selectedRegion);
        }

        private async Task PopulateSubRegionsDropDownList(string region, object selectedSubRegion = null)
        {
            var subRegions = await _unitOfWork.SubRegionRepository.GetAsync(
                             q =>
                                q.Region.Name == region);
            ViewBag.SubRegion = new SelectList(_mapper.Map<List<SubRegionViewModel>>(subRegions), 
                "Name", "Name", selectedSubRegion);
        }

        private async Task PopulateRoutingInstancesDropDownList(int tenantId, string region, string subRegion, IEnumerable<object> selectedRoutingInstances = null)
        {
            var query = (from result in await _unitOfWork.RoutingInstanceRepository.GetAsync(
                    q =>
                         q.TenantID == tenantId &&
                         q.RoutingInstanceType.IsTenantFacingVrf,
                         query: q => q.Include(x => x.Device.Location.SubRegion.Region)
                                      .Include(x => x.Device.Plane))
                         select result);

            if (!string.IsNullOrEmpty(region)) query = query.Where(q => q.Device.Location.SubRegion.Region.Name == region);
            if (!string.IsNullOrEmpty(subRegion)) query = query.Where(q => q.Device.Location.SubRegion.Name == subRegion);

            var items = query.Select(x => new { x.Name, DisplayName = $"{x.Name}, {x.Device.Location.SiteName}, {x.Device.Plane.Name}" });

            ViewBag.RoutingInstances = new MultiSelectList(items, "Name", "DisplayName", selectedRoutingInstances);
        }

        private async Task PopulateAttachmentRedundancyOptionsDropDownList(object selectedAttachmentRedundancyOption = null)
        {
            var attachmentRedundancyOptions = await _unitOfWork.AttachmentRedundancyRepository.GetAsync();
            ViewBag.AttachmentRedundancy = new SelectList(_mapper.Map<List<AttachmentRedundancyViewModel>>(attachmentRedundancyOptions),
                "Name", "Name", selectedAttachmentRedundancyOption);
        }
    }
}