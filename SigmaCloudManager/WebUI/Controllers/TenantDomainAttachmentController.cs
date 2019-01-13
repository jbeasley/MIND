using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class TenantDomainAttachmentController : BaseViewController
    {
        private readonly ITenantDomainAttachmentService _attachmentService;
        public TenantDomainAttachmentController(ITenantDomainAttachmentService attachmentService, IUnitOfWork unitOfWork, IMapper mapper) :
            base(unitOfWork, mapper)
        {
            _attachmentService = attachmentService;
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
        public IActionResult GetContractBandwidthPoolComponent(ContractBandwidthComponentViewModel model)
        {
            return ViewComponent("ContractBandwidthPool", new { model });
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
        [ValidateTenantDomainAttachmentExists]
        public async Task<IActionResult> Details(int? attachmentId)
        {
            var item = await _attachmentService.GetByIDAsync(attachmentId.Value, deep: true, asTrackable: false);
            var device = await _unitOfWork.DeviceRepository.GetByIDAsync(item.DeviceID);
            ViewBag.Device = _mapper.Map<TenantDomainDeviceViewModel>(device);

            return View(_mapper.Map<TenantDomainAttachmentViewModel>(item));
        }

        [HttpGet]
        [ValidateTenantDomainDeviceExists]
        public async Task<IActionResult> GetAllByDeviceID(int? deviceId)
        {
            var attachments = await _unitOfWork.AttachmentRepository.GetAsync(
                    q =>
                    q.DeviceID == deviceId.Value && 
                    q.AttachmentRole.PortPool.PortRole.PortRoleType == Mind.Models.PortRoleTypeEnum.ProviderFacing,
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

            var device = (from result in await _unitOfWork.DeviceRepository.GetAsync(
                    q =>
                        q.DeviceID == deviceId,
                        query: q =>
                        q.IncludeDeepProperties())
                        select result)
                        .Single();

            ViewBag.Device = _mapper.Map<TenantDomainDeviceViewModel>(device);

            return View(_mapper.Map<List<TenantDomainAttachmentViewModel>>(attachments));
        }

        [HttpGet]
        [ValidateTenantDomainDeviceExists]
        public async Task<IActionResult> Create(int? deviceId)
        {
            var device = (from result in await _unitOfWork.DeviceRepository.GetAsync(
                        q =>
                          q.DeviceID == deviceId,
                          query: q =>
                          q.IncludeDeepProperties())
                          select result)
                          .Single();

            ViewBag.Device = _mapper.Map<TenantDomainDeviceViewModel>(device);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateTenantDomainDeviceExists]
        public async Task<IActionResult> Create(int? deviceId, TenantDomainAttachmentRequestViewModel requestModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var request = _mapper.Map<TenantDomainAttachmentRequest>(requestModel);
                    var attachment = await _attachmentService.AddAsync(deviceId.Value, request);
                    return RedirectToAction(nameof(GetAllByDeviceID), new { deviceId });
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

            var device = await _unitOfWork.DeviceRepository.GetByIDAsync(deviceId);
            ViewBag.Device = _mapper.Map<TenantDomainDeviceViewModel>(device);

            return View(requestModel);
        }

        [HttpGet]
        [ValidateTenantDomainAttachmentExists]
        public async Task<ActionResult> Edit(int? attachmentId)
        {
            var attachment = await _attachmentService.GetByIDAsync(attachmentId.Value, deep: true, asTrackable: false);
            var device = (from result in await _unitOfWork.DeviceRepository.GetAsync(
                        q =>
                          q.DeviceID == attachment.DeviceID,
                          query: q =>
                          q.IncludeDeepProperties())
                          select result)
                          .Single();

            ViewBag.Device = _mapper.Map<TenantDomainDeviceViewModel>(device);

            return View(_mapper.Map<TenantDomainAttachmentUpdateViewModel>(attachment));
        }

        [HttpPost]
        [ValidateTenantDomainAttachmentExists]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? attachmentId, TenantDomainAttachmentUpdateViewModel update)
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
                    var attachmentUpdate = _mapper.Map<TenantDomainAttachmentUpdate>(update);

                    try
                    {
                        await _attachmentService.UpdateAsync(attachmentId.Value, attachmentUpdate);
                        return RedirectToAction(nameof(GetAllByDeviceID), new { deviceId = attachment.DeviceID });
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

            var device = (from result in await _unitOfWork.DeviceRepository.GetAsync(
                        q =>
                          q.DeviceID == attachment.DeviceID,
                          query: q =>
                          q.IncludeDeepProperties())
                          select result)
                          .Single();

            ViewBag.Device = _mapper.Map<TenantDomainDeviceViewModel>(device);

            return View(update);
        }

        [HttpGet]
        [ValidateTenantDomainAttachmentExists]
        public async Task<IActionResult> Delete(int? attachmentId, bool? concurrencyError = false)
        {
            var item = await _attachmentService.GetByIDAsync(attachmentId.Value, deep: true, asTrackable: false);
            if (concurrencyError.GetValueOrDefault()) ViewData.AddDeletePreconditionFailedMessage();

            var device = (from result in await _unitOfWork.DeviceRepository.GetAsync(
                        q =>
                          q.DeviceID == item.DeviceID,
                          query: q =>
                          q.IncludeDeepProperties())
                          select result)
                          .Single();

            ViewBag.Device = _mapper.Map<TenantDomainDeviceViewModel>(device);

            return View(_mapper.Map<TenantDomainAttachmentDeleteViewModel>(item));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(TenantDomainAttachmentDeleteViewModel model)
        {
            var attachment = await _attachmentService.GetByIDAsync(model.AttachmentId.Value, deep: true, asTrackable: false);
            if (attachment == null) return RedirectToAction(nameof(GetAllByDeviceID), new { deviceId = model.DeviceId });

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
                return RedirectToAction(nameof(GetAllByDeviceID), new { deviceId = attachment.DeviceID });
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

            var device = (from result in await _unitOfWork.DeviceRepository.GetAsync(
                        q =>
                          q.DeviceID == attachment.DeviceID,
                          query: q =>
                          q.IncludeDeepProperties())
                          select result)
                          .Single();

            ViewBag.Device = _mapper.Map<TenantDomainDeviceViewModel>(device);

            return View(_mapper.Map<TenantDomainAttachmentDeleteViewModel>(attachment));
        }
    }
}