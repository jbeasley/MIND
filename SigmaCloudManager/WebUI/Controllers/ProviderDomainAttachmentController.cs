using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper;
using SCM.Models.RequestModels;
using SCM.Models;
using SCM.Controllers;
using Mind.Services;
using SCM.Data;
using Mind.Models;
using Mind.Builders;
using Mind.WebUI.Attributes;
using Mind.WebUI.Models;
using IO.NovaAttSwagger.Client;

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
        public IActionResult GetLocationSelectorComponent(LocationSelectorViewModel locationSelectorModel)
        {
            return ViewComponent("LocationSelector", locationSelectorModel);
        }

        [HttpGet]
        public IActionResult GetAttachmentBundleAndMultiportOptionsComponent(AttachmentBundleAndMultiportOptionsComponentViewModel model)
        {
            return ViewComponent("AttachmentBundleAndMultiportOptions", new { model });
        }

        [HttpGet]
        public IActionResult GetAttachmentBandwidthComponent(AttachmentBandwidthComponentViewModel model)
        {
            return ViewComponent("AttachmentBandwidth", model);
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
        public IActionResult GetContractBandwidthPoolComponent(ContractBandwidthComponentViewModel model)
        {
            return ViewComponent("ContractBandwidthPool", new { model });
        }

        [HttpGet]
        public IActionResult GetBgpPeersComponent(BgpPeersComponentViewModel model)
        {
            return ViewComponent("RoutingInstanceBgpPeers", new { model });
        }

        [HttpPost]
        public IActionResult GetBgpPeerGridData([FromBody]List<BgpPeerRequestViewModel> bgpPeerRequests)
        {
            return ViewComponent("RoutingInstanceBgpPeersGridData", new { bgpPeerRequests });
        }

        [HttpGet]
        public IActionResult GetAttachmentPortPoolAndRoleComponent(AttachmentPortPoolAndRoleComponentViewModel model)
        {
            return ViewComponent("AttachmentPortPoolAndRole", new { model });
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

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateTenantExists]
        public async Task<IActionResult> Create(int? tenantId, ProviderDomainAttachmentRequestViewModel requestModel, bool? syncToNetwork)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var request = _mapper.Map<ProviderDomainAttachmentRequest>(requestModel);
                    var attachment = await _attachmentService.AddAsync(tenantId.Value, request, syncToNetwork.GetValueOrDefault());
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

                catch (ServiceBadArgumentsException ex)
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
        public async Task<ActionResult> Edit(int? attachmentId, ProviderDomainAttachmentUpdateViewModel update, bool? syncToNetwork)
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
                        await _attachmentService.UpdateAsync(attachmentId.Value, attachmentUpdate, syncToNetwork.GetValueOrDefault());
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

                    catch (ServiceBadArgumentsException ex)
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