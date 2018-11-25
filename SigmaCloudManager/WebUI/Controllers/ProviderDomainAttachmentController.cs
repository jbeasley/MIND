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
using IO.Swagger.Client;

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
        public async Task<PartialViewResult> SubRegions(int? regionId)
        {
            var subRegions = await _unitOfWork.SubRegionRepository.GetAsync(
                q =>
                q.RegionID == regionId);

            return PartialView(_mapper.Map<List<SubRegionViewModel>>(subRegions));
        }

        [HttpGet]
        public async Task<PartialViewResult> Locations(int? subRegionId)
        {
            var locations = await _unitOfWork.LocationRepository.GetAsync(
                q =>
                q.SubRegionID == subRegionId,
                AsTrackable: false);

            return PartialView(_mapper.Map<List<LocationViewModel>>(locations));
        }

        [HttpGet]
        public async Task<PartialViewResult> AttachmentBandwidths(bool? bundleRequired, bool? multiportRequired)
        {
            var query = (from result in await _unitOfWork.AttachmentBandwidthRepository.GetAsync()
                         select result);

            if (bundleRequired.GetValueOrDefault())
            {
                query = query.Where(x => x.SupportedByBundle);
            }
            else if (multiportRequired.GetValueOrDefault())
            {
                query = query.Where(x => x.SupportedByMultiPort);
            }
            else
            {
                query = query.Where(x => !x.MustBeBundleOrMultiPort);
            }

            return PartialView(_mapper.Map<List<AttachmentBandwidthViewModel>>(query.ToList().OrderBy(b => b.BandwidthGbps)));
        }

        [HttpGet]
        public IActionResult GetIpAddressingComponent(string portPoolName, string attachmentRoleName, 
            int? attachmentBandwidthGbps, bool? isMultiport)
        {
            return ViewComponent("AttachmentIpAddressing", new
            {
                portPoolName,
                attachmentRoleName,
                attachmentBandwidthGbps,
                isMultiport
            });
        }

        [HttpGet]
        public IActionResult GetContractBandwidthPoolComponent(string portPoolName, string attachmentRoleName,
            int? attachmentBandwidthGbps)
        {
            return ViewComponent("AttachmentContractBandwidthPool", new
            {
                portPoolName,
                attachmentRoleName,
                attachmentBandwidthGbps
            });
        }

        [HttpGet]
        public IActionResult GetBgpPeersComponent(string portPoolName, string attachmentRoleName)
        {
            return ViewComponent("AttachmentBgpPeers", new
            {
                portPoolName,
                attachmentRoleName
            });
        }

        [HttpGet]
        public async Task<PartialViewResult> AttachmentRoles(string portPoolName)
        {
            var attachmentRoles = await _unitOfWork.AttachmentRoleRepository.GetAsync(
                q =>
                q.PortPool.Name == portPoolName,
                AsTrackable: false);

            return PartialView(_mapper.Map<List<AttachmentRoleViewModel>>(attachmentRoles));
        }

        [HttpPost]
        public IActionResult GetBgpPeerGridData([FromBody]List<BgpPeerRequestViewModel> bgpPeerRequests)
        {
            return ViewComponent("BgpPeersGridData", new { bgpPeerRequests });
        }

        [HttpGet]
        [ValidateProviderDomainAttachmentExists]
        public async Task<IActionResult> Details(int? attachmentId)
        {
            var item = await _attachmentService.GetByIDAsync(attachmentId.Value, deep: true, asTrackable: false);
            return View(_mapper.Map<ProviderDomainAttachmentViewModel>(item));
        }

        [HttpGet]
        [ValidateTenantExists]
        [SetTenantCookieState]
        public async Task<IActionResult> GetAllByTenantID(int? tenantId)
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

            var tenant = await _unitOfWork.TenantRepository.GetByIDAsync(tenantId);               
            ViewBag.Tenant = _mapper.Map<TenantViewModel>(tenant);

            return View(_mapper.Map<List<ProviderDomainAttachmentViewModel>>(attachments));
        }

        [HttpGet]
        [ValidateTenantExists]
        public async Task<IActionResult> Create(int? tenantId)
        {
            var tenant = await _unitOfWork.TenantRepository.GetByIDAsync(tenantId);
            ViewBag.Tenant = _mapper.Map<TenantViewModel>(tenant);
            await PopulateRegionsDropDownList();
            await PopulatePlanesDropDownList();
            await PopulatePortPoolsDropDownList();
            await PopulateBandwidthsDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateTenantExists]
        public async Task<IActionResult> Create(int? tenantId, ProviderDomainAttachmentRequestViewModel requestModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var request = _mapper.Map<ProviderDomainAttachmentRequest>(requestModel);
                    var attachment = await _attachmentService.AddAsync(tenantId.Value, request);
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

                catch (ApiException)
                {
                    ModelState.AddNovaClientApiExceptionMessage();
                }
            }

            var tenant = await _unitOfWork.TenantRepository.GetByIDAsync(tenantId);
            ViewBag.Tenant = _mapper.Map<TenantViewModel>(tenant);

            await PopulateRegionsDropDownList(requestModel.RegionId);
            await PopulateSubRegionsDropDownList(requestModel.RegionId.GetValueOrDefault(), requestModel.SubRegionId);
            await PopulateLocationsDropDownList(requestModel.SubRegionId.GetValueOrDefault(), requestModel.LocationName);
            await PopulatePlanesDropDownList(requestModel.PlaneName?.ToString());
            await PopulatePortPoolsDropDownList(requestModel.PortPoolName);
            await PopulateAttachmentRolesDropDownList(requestModel.PortPoolName, selectedAttachmentRole: requestModel.AttachmentRoleName);
            await PopulateBandwidthsDropDownList(requestModel.BundleRequired, requestModel.MultiportRequired, requestModel.AttachmentBandwidthGbps);

            return View(requestModel);
        }

        [HttpGet]
        [ValidateProviderDomainAttachmentExists]
        public async Task<ActionResult> Edit(int? attachmentId)
        {
            var attachment = await _attachmentService.GetByIDAsync(attachmentId.Value, deep: true, asTrackable: false);
            var tenant = await _unitOfWork.TenantRepository.GetByIDAsync(attachment.TenantID);
            ViewBag.Tenant = _mapper.Map<TenantViewModel>(tenant);
            if (attachment.RoutingInstance != null)
            {
                await PopulateRoutingInstancesDropDownList(attachment.TenantID.Value, attachment.DeviceID, attachment.RoutingInstance.Name);
            }

            return View(_mapper.Map<ProviderDomainAttachmentUpdateViewModel>(attachment));
        }

        [HttpPost]
        [ValidateProviderDomainAttachmentExists]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? attachmentId, ProviderDomainAttachmentUpdateViewModel update)
        {
            var attachment = await _attachmentService.GetByIDAsync(attachmentId.Value, deep: true, asTrackable: false);

            if (ModelState.IsValid)
            {
                if (attachment.HasPreconditionFailed(Request, update.GetConcurrencyToken()))
                {
                    ModelState.AddUpdatePreconditionFailedMessage();
                    ModelState.RemoveConcurrencyTokenItem();
                    update.UpdateConcurrencyToken(attachment.GetConcurrencyToken());
                }
                else
                {
                    var attachmentUpdate = _mapper.Map<ProviderDomainAttachmentUpdate>(update);

                    try
                    {
                        await _attachmentService.UpdateAsync(attachmentId.Value, attachmentUpdate);
                        return RedirectToAction(nameof(GetAllByTenantID), new { tenantId = attachment.TenantID });
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

            if (attachment.RoutingInstance != null)
            {
                await PopulateRoutingInstancesDropDownList(attachment.TenantID.Value, attachment.DeviceID, attachment.RoutingInstance.Name);
            }

            var tenant = await _unitOfWork.TenantRepository.GetByIDAsync(attachment.TenantID);
            ViewBag.Tenant = _mapper.Map<TenantViewModel>(tenant);

            return View(update);
        }

        [HttpGet]
        [ValidateProviderDomainAttachmentExists]
        public async Task<IActionResult> Delete(int? attachmentId, bool? concurrencyError = false)
        {
            var item = await _attachmentService.GetByIDAsync(attachmentId.Value, deep: true, asTrackable: false);
            if (concurrencyError.GetValueOrDefault()) ViewData.AddDeletePreconditionFailedMessage();

            return View(_mapper.Map<ProviderDomainAttachmentDeleteViewModel>(item));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ProviderDomainAttachmentDeleteViewModel model)
        {
            var attachment = await _attachmentService.GetByIDAsync(model.AttachmentId.Value, deep: true, asTrackable: false);
            if (attachment == null) return RedirectToAction(nameof(GetAllByTenantID), new { tenantId = model.TenantId });

            if (attachment.HasPreconditionFailed(Request, model.GetConcurrencyToken()))
            {
                return RedirectToAction(nameof(Delete), new
                {
                    attachmentId = attachment.AttachmentID,
                    concurrencyError = true
                });
            }

            try
            {
                await _attachmentService.DeleteAsync(attachment.AttachmentID);
                return RedirectToAction(nameof(GetAllByTenantID), new { tenantId = attachment.TenantID });
            }

            catch (IllegalDeleteAttemptException ex)
            {
                ViewData.AddDeleteValidationFailedMessage(ex.Message);
            }

            catch (DbUpdateException)
            {
                ViewData.AddDatabaseUpdateExceptionMessage();
            }

            catch (ApiException)
            {
                ModelState.AddNovaClientApiExceptionMessage();
            }

            return View(_mapper.Map<ProviderDomainAttachmentDeleteViewModel>(attachment));
        }

        /// <summary>
        /// Sync an attachment to the network.
        /// </summary>
        /// <returns>An awaitable task</returns>
        /// <param name="attachmentId">The ID of the attachment</param>
        [HttpPost]
        [ValidateProviderDomainAttachmentExists]
        public async Task<IActionResult> SyncToNetwork(int? attachmentId)
        {
            var attachment = await _attachmentService.GetByIDAsync(attachmentId.Value);
            try
            {
                await _attachmentService.SyncToNetworkAsync(attachmentId.Value);
            }

            catch (BuilderBadArgumentsException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            catch (ApiException)
            {
                ModelState.AddNovaClientApiExceptionMessage();
            }

            ViewData.AddNetworkSyncSuccessMessage();
            return Ok();
        }


        private async Task PopulatePortPoolsDropDownList(object selectedPortPool = null)
        {
            var portPools = await _unitOfWork.PortPoolRepository.GetAsync(
                    q =>
                        q.PortRole.PortRoleType == PortRoleTypeEnum.TenantFacing,
                        AsTrackable: false);

            ViewBag.PortPool = new SelectList(_mapper.Map<List<PortPoolViewModel>>(portPools), "Name", "Name", selectedPortPool);
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
                                                    .Any(q => 
                                                         q.DeviceRoleID == deviceRoleId));

            var attachmentRoles = query.ToList();
            ViewBag.AttachmentRole = new SelectList(_mapper.Map<List<AttachmentRoleViewModel>>(attachmentRoles),
                "Name", "Name", selectedAttachmentRole);
        }

        private async Task PopulateBandwidthsDropDownList(bool? bundleRequired = null, bool? multiportRequired = null, object selectedBandwidth = null)
        {
            var query = (from result in await _unitOfWork.AttachmentBandwidthRepository.GetAsync()
                         select result);

            if (bundleRequired.GetValueOrDefault())
            {
                query = query.Where(x => x.SupportedByBundle);
            }
            else if (multiportRequired.GetValueOrDefault())
            {
                query = query.Where(x => x.SupportedByMultiPort);
            }
            else
            {
                query = query.Where(x => !x.MustBeBundleOrMultiPort);
            }
        
            ViewBag.AttachmentBandwidth = new SelectList(_mapper.Map<List<AttachmentBandwidthViewModel>>(query.ToList().OrderBy(b => b.BandwidthGbps)), 
                "BandwidthGbps", "BandwidthGbps", selectedBandwidth);
        }

        private async Task PopulateRegionsDropDownList(object selectedRegion = null)
        {
            var regions = await _unitOfWork.RegionRepository.GetAsync();
            ViewBag.Region = new SelectList(_mapper.Map<List<RegionViewModel>>(regions), "RegionId", "Name", selectedRegion);
        }

        private async Task PopulateSubRegionsDropDownList(int regionId, object selectedSubRegion = null)
        {
            var subRegions = await _unitOfWork.SubRegionRepository.GetAsync(
                             q => 
                                q.RegionID == regionId);
            ViewBag.SubRegion = new SelectList(_mapper.Map<List<SubRegionViewModel>>(subRegions), "SubRegionId", "Name", selectedSubRegion);
        }

        private async Task PopulateLocationsDropDownList(int subRegionId, object selectedLocation = null)
        {
            var locations = await _unitOfWork.LocationRepository.GetAsync(
                            q => 
                                q.SubRegionID == subRegionId);
            ViewBag.Location = new SelectList(_mapper.Map<List<LocationViewModel>>(locations), "SiteName", "SiteName", selectedLocation);
        }

        private async Task PopulatePlanesDropDownList(object selectedPlane = null)
        {
            var planes = await _unitOfWork.PlaneRepository.GetAsync();
            ViewBag.Plane = new SelectList(_mapper.Map<List<PlaneViewModel>>(planes), "Name", "Name", selectedPlane);
        }

        private async Task PopulateRoutingInstancesDropDownList(int tenantId, int deviceId, object selectedRoutingInstance = null)
        {
            var routingInstances = await _unitOfWork.RoutingInstanceRepository.GetAsync(
                            q =>
                                q.TenantID == tenantId &&
                                q.DeviceID == deviceId &&
                                q.RoutingInstanceType.IsTenantFacingVrf);

            ViewBag.RoutingInstance = new SelectList(_mapper.Map<List<ProviderDomainRoutingInstanceViewModel>>(routingInstances),
                "Name", "Name", selectedRoutingInstance);
        }
    }
}