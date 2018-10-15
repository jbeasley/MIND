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

namespace Mind.WebUI.Controllers
{
    public class ProviderDomainAttachmentController : BaseViewController
    {
        private readonly IProviderDomainAttachmentService _attachmentService;
        public ProviderDomainAttachmentController(IProviderDomainAttachmentService attachmentService, IUnitOfWork unitOfWork, IMapper mapper) :
            base(unitOfWork, mapper)
        {
            _attachmentService = attachmentService;
        }

        [HttpGet]
        public async Task<PartialViewResult> SubRegions(int? id)
        {
            var subRegions = await _unitOfWork.SubRegionRepository.GetAsync(
                q =>
                q.RegionID == id);

            return PartialView(Mapper.Map<List<SubRegionViewModel>>(subRegions));
        }

        [HttpGet]
        public async Task<PartialViewResult> Locations(int? id)
        {
            var locations = await _unitOfWork.LocationRepository.GetAsync(
                q =>
                q.SubRegionID == id,
                AsTrackable: false);

            return PartialView(Mapper.Map<List<LocationViewModel>>(locations));
        }

        [HttpGet]
        public async Task<PartialViewResult> AttachmentRoles(int? portPoolId)
        {
            var attachmentRoles = await _unitOfWork.AttachmentRoleRepository.GetAsync(
                q =>
                q.PortPoolID == portPoolId,
                AsTrackable: false);

            return PartialView(Mapper.Map<List<AttachmentRoleViewModel>>(attachmentRoles));
        }

        [HttpGet]
        [ValidateProviderDomainAttachmentExists]
        public async Task<IActionResult> Details(int? attachmentId)
        {
            var item = await _attachmentService.GetByIDAsync(attachmentId.Value, deep: true, asTrackable: false);
            return View(Mapper.Map<ProviderDomainAttachmentViewModel>(item));
        }

        [HttpGet]
        [ValidateTenantExists]
        public async Task<IActionResult> GetAllByTenantId(int? tenantId)
        {
            var attachments = await _unitOfWork.AttachmentRepository.GetAsync(
                    q =>
                    q.TenantID == tenantId.Value,
                    query: q => q.IncludeValidationProperties(),
                    AsTrackable: false);

            ViewData["SuccessMessage"] = FormatAsHtmlList(attachments
                .Where(x => x.Created && x.ShowCreatedAlert)
                .Select(x => $"{x.Name} has been created.").ToList());

            // Display errors if the ports of the attachment are mis-configured
            try
            {
                foreach (var attachment in attachments)
                {
                    attachment.ValidatePortsConfiguredCorrectly();
                }
            }

            catch (IllegalStateException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            var tenant = await _unitOfWork.TenantRepository.GetAsync(
                q =>
                    q.TenantID == tenantId,
                    query: q => q.IncludeDeepProperties(),
                    AsTrackable: false);

            ViewBag.Tenant = Mapper.Map<TenantViewModel>(tenant);
            return View(Mapper.Map<List<ProviderDomainAttachmentViewModel>>(attachments));
        }

        [HttpGet]
        [ValidateTenantExists]
        public async Task<IActionResult> Create(int? tenantId)
        {
            var tenant = await _unitOfWork.TenantRepository.GetByIDAsync(tenantId.Value);
            ViewBag.Tenant = Mapper.Map<TenantViewModel>(tenant);
            await PopulateRegionsDropDownList();
            await PopulatePlanesDropDownList();
            await PopulateContractBandwidthsDropDownList();
            await PopulatePortPoolsDropDownList();
            await PopulateBandwidthsDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateModelState]
        [ValidateTenantExists]
        public async Task<IActionResult> Create(int? tenantId, ProviderDomainAttachmentRequestViewModel requestModel)
        {
            try
            {
                var request = Mapper.Map<ProviderDomainAttachmentRequest>(requestModel);
                var attachment = await _attachmentService.AddAsync(tenantId.Value, request);
                return RedirectToAction(nameof(GetAllByTenantId), new { tenantId });
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

            var tenant = await _unitOfWork.TenantRepository.GetAsync(
                q =>
                    q.TenantID == tenantId,
                    query: q => q.IncludeDeepProperties(),
                    AsTrackable: false);

            ViewBag.Tenant = Mapper.Map<TenantViewModel>(tenant);

            await PopulateRegionsDropDownList(requestModel.RegionId);
            await PopulateSubRegionsDropDownList(requestModel.RegionId.Value, requestModel.SubRegionId);
            await PopulateLocationsDropDownList(requestModel.SubRegionId.Value, requestModel.LocationName);
            await PopulatePlanesDropDownList(requestModel.PlaneName.ToString());
            await PopulateContractBandwidthsDropDownList();
            await PopulatePortPoolsDropDownList(requestModel.PortPoolName);
            await PopulateAttachmentRolesDropDownList(requestModel.PortPoolName, selectedAttachmentRole: requestModel.AttachmentRoleName);
            await PopulateBandwidthsDropDownList();

            return View(requestModel);
        }

        [HttpGet]
        [ValidateProviderDomainAttachmentExists]
        public async Task<ActionResult> Edit(int? attachmentId)
        {
            var attachment = await _attachmentService.GetByIDAsync(attachmentId.Value, deep: true, asTrackable: false);
            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);
            await PopulateContractBandwidthsDropDownList(attachment.ContractBandwidthPoolID);

            return View(Mapper.Map<AttachmentUpdateViewModel>(attachment));
        }

        [HttpPost]
        [ValidateModelState]
        [ValidateProviderDomainAttachmentExists]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? attachmentId, ProviderDomainAttachmentUpdateViewModel updateModel)
        {
            var attachment = await _attachmentService.GetByIDAsync(attachmentId.Value);
            if (attachment.HasPreconditionFailed(Request))
            {
                ModelState.PopulateModelState(attachment);
                return View(Mapper.Map<ProviderDomainAttachmentUpdateViewModel>(attachment));
            }

            var attachmentUpdate = Mapper.Map<ProviderDomainAttachmentUpdate>(updateModel);

            try
            {
                await _attachmentService.UpdateAsync(attachmentId.Value, attachmentUpdate);
                return RedirectToAction(nameof(GetAllByTenantId), new { tenantId = attachment.TenantID });
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

            await PopulateAttachmentRolesDropDownList(attachment.AttachmentRole.PortPool.Name,
                attachment.Device.DeviceRoleID, attachment.AttachmentRole.Name);

            ViewBag.Attachment = Mapper.Map<AttachmentViewModel>(attachment);

            return View(Mapper.Map<ProviderDomainAttachmentUpdateViewModel>(attachment));
        }

        [HttpGet]
        [ValidateTenantExists]
        public async Task<IActionResult> Delete(int? tenantId, int? attachmentId, bool? concurrencyError = false)
        {
            var item = await _attachmentService.GetByIDAsync(attachmentId.Value);
            if (item == null)
            {
                return RedirectToAction(nameof(GetAllByTenantId), new { tenantId });
            }

            if (concurrencyError.GetValueOrDefault()) ViewData.AddDeletePreconditionFailedMessage();

            return View(Mapper.Map<ProviderDomainAttachmentViewModel>(item));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(AttachmentViewModel model)
        {
            var attachment = await _attachmentService.GetByIDAsync(model.AttachmentID);
            if (attachment == null) return RedirectToAction(nameof(GetAllByTenantId), new { tenantId = model.TenantID });

            if (attachment.HasPreconditionFailed(Request, model.RowVersion.ToString()))
            {
                return RedirectToAction(nameof(Delete), new
                {
                    tenantId = attachment.TenantID,
                    attachmentId = attachment.AttachmentID,
                    concurrencyError = true
                });
            }

            try
            {
                await _attachmentService.DeleteAsync(attachment.AttachmentID);
                return RedirectToAction(nameof(GetAllByTenantId), new { tenantId = attachment.TenantID });
            }

            catch (DbUpdateException)
            {
                ViewData.AddDatabaseUpdateExceptionMessage();
            }

            return View(Mapper.Map<ProviderDomainAttachmentViewModel>(attachment));
        }

        private async Task PopulatePortPoolsDropDownList(object selectedPortPool = null)
        {
            var portPools = await _unitOfWork.PortPoolRepository.GetAsync(
                    q =>
                        q.PortRole.PortRoleType == PortRoleTypeEnum.TenantFacing,
                        AsTrackable: false);

            ViewBag.PortPoolID = new SelectList(Mapper.Map<List<PortPoolViewModel>>(portPools), "PortPoolID", "Name", selectedPortPool);
        }

        private async Task PopulateAttachmentRolesDropDownList(string portPoolName, int? deviceRoleId = null, object selectedAttachmentRole = null)
        {
            var query = (from result in await _unitOfWork.AttachmentRoleRepository.GetAsync(
                         q =>
                            q.PortPool.Name == portPoolName)
                            select result);

            if (deviceRoleId.HasValue) query = query.Where(
                                                        x =>
                                                        x.DeviceRoleAttachmentRoles
                                                    .Where(
                                                        q =>
                                                        q.DeviceRoleID == deviceRoleId)
                                                    .Any());

            var attachmentRoles = query.ToList();
            ViewBag.AttachmentRoleID = new SelectList(Mapper.Map<List<AttachmentRoleViewModel>>(attachmentRoles),
                "Name", "Name", selectedAttachmentRole);
        }

        private async Task PopulateBandwidthsDropDownList(object selectedBandwidth = null)
        {
            var bandwidths = await _unitOfWork.AttachmentBandwidthRepository.GetAsync();
            ViewBag.BandwidthID = new SelectList(Mapper.Map<List<AttachmentBandwidthViewModel>>(bandwidths.OrderBy(b => b.BandwidthGbps)), 
                "AttachmentBandwidthID", "BandwidthGbps", selectedBandwidth);
        }

        private async Task PopulateRegionsDropDownList(object selectedRegion = null)
        {
            var regions = await _unitOfWork.RegionRepository.GetAsync();
            ViewBag.RegionID = new SelectList(Mapper.Map<List<RegionViewModel>>(regions), "RegionID", "Name", selectedRegion);
        }

        private async Task PopulateSubRegionsDropDownList(int regionId, object selectedSubRegion = null)
        {
            var subRegions = await _unitOfWork.SubRegionRepository.GetAsync(
                             q => 
                                q.RegionID == regionId);
            ViewBag.SubRegionID = new SelectList(Mapper.Map<List<SubRegionViewModel>>(subRegions), "SubRegionID", "Name", selectedSubRegion);
        }

        protected async Task PopulateLocationsDropDownList(int subRegionId, object selectedLocation = null)
        {
            var locations = await _unitOfWork.LocationRepository.GetAsync(
                            q => 
                                q.SubRegionID == subRegionId);
            ViewBag.LocationID = new SelectList(Mapper.Map<List<LocationViewModel>>(locations), "SiteName", "SiteName", selectedLocation);
        }

        private async Task PopulatePlanesDropDownList(object selectedPlane = null)
        {
            var planes = await _unitOfWork.PlaneRepository.GetAsync();
            ViewBag.PlaneID = new SelectList(Mapper.Map<List<PlaneViewModel>>(planes), "Name", "Name", selectedPlane);
        }

        private async Task PopulateContractBandwidthsDropDownList(object selectedContractBandwidth = null)
        {
            var contractBandwidths = await _unitOfWork.ContractBandwidthPoolRepository.GetAsync();
            ViewBag.ContractBandwidthID = new SelectList(Mapper.Map<List<ContractBandwidthViewModel>>(contractBandwidths)
                .OrderBy(b => b.BandwidthMbps),
                "BandwidthMbps", "BandwidthMbps", selectedContractBandwidth);
        }
    }
}