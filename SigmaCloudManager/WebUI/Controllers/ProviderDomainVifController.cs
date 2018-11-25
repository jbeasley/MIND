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
using IO.Swagger.Client;

namespace Mind.WebUI.Controllers
{
    public class ProviderDomainVifController : BaseViewController
    {
        private readonly IProviderDomainVifService _vifService;
        public ProviderDomainVifController(IProviderDomainVifService vifService, IUnitOfWork unitOfWork, IMapper mapper) :
            base(unitOfWork, mapper)
        {
            _vifService = vifService;
        }

        [HttpGet]
        public IActionResult GetIpAddressingComponent(int? attachmentId, string vifRoleName)
        {
            return ViewComponent("VifIpAddressing", new
            {
                attachmentId,
                vifRoleName
            });
        }

        [HttpGet]
        public IActionResult GetContractBandwidthPoolComponent(int? attachmentId, string vifRoleName)
        {
            return ViewComponent("VifContractBandwidthPool", new
            {
                attachmentId,
                vifRoleName
            });
        }

        [HttpGet]
        public IActionResult GetBgpPeersComponent(int? attachmentId, string vifRoleName)
        {
            return ViewComponent("VifBgpPeers", new
            {
                attachmentId,
                vifRoleName
            });
        }

        [HttpPost]
        public IActionResult GetBgpPeerGridData([FromBody]List<BgpPeerRequestViewModel> bgpPeerRequests)
        {
            return ViewComponent("BgpPeersGridData", new { bgpPeerRequests });
        }

        [HttpGet]
        [ValidateProviderDomainVifExists]
        public async Task<IActionResult> Details(int? vifId)
        {
            var item = await _vifService.GetByIDAsync(vifId.Value, deep: true, asTrackable: false);
            var attachment = (from result in await _unitOfWork.AttachmentRepository.GetAsync(
            q =>
                 q.AttachmentID == item.AttachmentID,
                 query: q => q.IncludeDeepProperties(),
                 AsTrackable: false)
                              select result)
                 .Single();

            ViewBag.Attachment = _mapper.Map<ProviderDomainAttachmentViewModel>(attachment);
            return View(_mapper.Map<ProviderDomainVifViewModel>(item));
        }

        [HttpGet]
        [ValidateProviderDomainAttachmentExists]
        public async Task<IActionResult> GetAllByAttachmentID(int? attachmentId)
        {
            var vifs = await _vifService.GetAllByAttachmentIDAsync(attachmentId.Value, deep: true, asTrackable: false);

            ViewData["SuccessMessage"] = FormatAsHtmlList(vifs
                .Where(x => x.Created && x.ShowCreatedAlert)
                .Select(x => $"{x.Name} has been created.").ToList());

            var attachment = (from result in await _unitOfWork.AttachmentRepository.GetAsync(
                        q =>
                             q.AttachmentID == attachmentId,
                             query: q => q.IncludeDeepProperties(),
                             AsTrackable: false)
                             select result)
                             .Single();

            ViewBag.Attachment = _mapper.Map<ProviderDomainAttachmentViewModel>(attachment);

            return View(_mapper.Map<List<ProviderDomainVifViewModel>>(vifs));
        }

        [HttpGet]
        [ValidateProviderDomainAttachmentExists]
        public async Task<IActionResult> Create(int? attachmentId)
        {
            var attachment = (from result in await _unitOfWork.AttachmentRepository.GetAsync(
                        q =>
                             q.AttachmentID == attachmentId,
                             query: q => q.IncludeDeepProperties(),
                             AsTrackable: false)
                              select result)
                             .Single();

            ViewBag.Attachment = _mapper.Map<ProviderDomainAttachmentViewModel>(attachment);
            await PopulateVifRolesDropDownList(attachment.AttachmentRoleID);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateProviderDomainAttachmentExists]
        public async Task<IActionResult> Create(int? attachmentId, ProviderDomainVifRequestViewModel requestModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var request = _mapper.Map<ProviderDomainVifRequest>(requestModel);
                    var vif = await _vifService.AddAsync(attachmentId.Value, request);
                    return RedirectToAction(nameof(GetAllByAttachmentID), new { attachmentId });
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
            var attachment = (from result in await _unitOfWork.AttachmentRepository.GetAsync(
                        q =>
                             q.AttachmentID == attachmentId,
                             query: q => q.IncludeDeepProperties(),
                             AsTrackable: false)
                              select result)
                             .Single();

            ViewBag.Attachment = _mapper.Map<ProviderDomainAttachmentViewModel>(attachment);
            await PopulateVifRolesDropDownList(attachment.AttachmentRoleID, requestModel.VifRoleName);

            return View(requestModel);
        }

        [HttpGet]
        [ValidateProviderDomainVifExists]
        public async Task<ActionResult> Edit(int? vifId)
        {
            var vif = await _vifService.GetByIDAsync(vifId.Value, deep: true, asTrackable: false);
            var attachment = (from result in await _unitOfWork.AttachmentRepository.GetAsync(
                        q =>
                             q.AttachmentID == vif.AttachmentID,
                             query: q => q.IncludeDeepProperties(),
                             AsTrackable: false)
                              select result)
                             .Single();

            ViewBag.Attachment = _mapper.Map<ProviderDomainAttachmentViewModel>(attachment);
            if (vif.RoutingInstance != null)
            {
                await PopulateRoutingInstancesDropDownList(attachment.TenantID.Value, attachment.DeviceID, vif.RoutingInstance.Name);
            }

            return View(_mapper.Map<ProviderDomainVifUpdateViewModel>(vif));
        }

        [HttpPost]
        [ValidateProviderDomainVifExists]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? vifId, ProviderDomainVifUpdateViewModel update)
        {
            var vif = await _vifService.GetByIDAsync(vifId.Value, deep: true, asTrackable: false);

            if (ModelState.IsValid)
            {
                if (vif.HasPreconditionFailed(Request, update.GetConcurrencyToken()))
                {
                    ModelState.AddUpdatePreconditionFailedMessage();
                    ModelState.RemoveConcurrencyTokenItem();
                    update.UpdateConcurrencyToken(vif.GetConcurrencyToken());
                }
                else
                {
                    var vifUpdate = _mapper.Map<ProviderDomainVifUpdate>(update);

                    try
                    {
                        await _vifService.UpdateAsync(vifId.Value, vifUpdate);
                        return RedirectToAction(nameof(GetAllByAttachmentID), new { attachmentId = vif.AttachmentID });
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
            }

            if (vif.RoutingInstance != null)
            {
                await PopulateRoutingInstancesDropDownList(vif.Attachment.DeviceID, vif.Attachment.TenantID.Value, vif.RoutingInstance.Name);
            }

            var attachment = (from result in await _unitOfWork.AttachmentRepository.GetAsync(
                        q =>
                             q.AttachmentID == vif.AttachmentID,
                             query: q => q.IncludeDeepProperties(),
                             AsTrackable: false)
                              select result)
                             .Single();

            ViewBag.Attachment = _mapper.Map<ProviderDomainAttachmentViewModel>(attachment);

            return View(update);
        }

        [HttpGet]
        [ValidateProviderDomainVifExists]
        public async Task<IActionResult> Delete(int? vifId, bool? concurrencyError = false)
        {
            var item = await _vifService.GetByIDAsync(vifId.Value, deep: true, asTrackable: false);
            if (concurrencyError.GetValueOrDefault()) ViewData.AddDeletePreconditionFailedMessage();
            var attachment = (from result in await _unitOfWork.AttachmentRepository.GetAsync(
                        q =>
                              q.AttachmentID == item.AttachmentID,
                              query: q => q.IncludeDeepProperties(),
                              AsTrackable: false)
                              select result)
                              .Single();

            ViewBag.Attachment = _mapper.Map<ProviderDomainAttachmentViewModel>(attachment);

            return View(_mapper.Map<ProviderDomainVifDeleteViewModel>(item));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ProviderDomainVifDeleteViewModel model)
        {
            var vif = await _vifService.GetByIDAsync(model.VifId.Value, deep: true, asTrackable: false);
            if (vif == null) return RedirectToAction(nameof(GetAllByAttachmentID), new { attachmentId = model.AttachmentId });

            if (vif.HasPreconditionFailed(Request, model.GetConcurrencyToken()))
            {
                return RedirectToAction(nameof(Delete), new
                {
                    vifId = vif.VifID,
                    concurrencyError = true
                });
            }

            try
            {
                await _vifService.DeleteAsync(vif.VifID);
                return RedirectToAction(nameof(GetAllByAttachmentID), new { attachmentId = vif.AttachmentID });
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

            var attachment = (from result in await _unitOfWork.AttachmentRepository.GetAsync(
                            q =>
                              q.AttachmentID == vif.AttachmentID,
                              query: q => q.IncludeDeepProperties(),
                              AsTrackable: false)
                              select result)
                              .Single();

            ViewBag.Attachment = _mapper.Map<ProviderDomainAttachmentViewModel>(attachment);

            return View(_mapper.Map<ProviderDomainVifDeleteViewModel>(vif));
        }

        private async Task PopulateVifRolesDropDownList(int attachmentRoleId, object selectedVifRole = null)
        {
            var query = (from result in await _unitOfWork.VifRoleRepository.GetAsync(
                         q =>
                            q.AttachmentRoleID == attachmentRoleId)
                            select result);

            var attachmentRoles = query.ToList();
            ViewBag.VifRole = new SelectList(_mapper.Map<List<VifRoleViewModel>>(attachmentRoles),
                "Name", "Name", selectedVifRole);
        }

        private async Task PopulateRoutingInstancesDropDownList(int tenantId, int deviceId, object selectedRoutingInstance = null)
        {
            var routingInstances = await _unitOfWork.RoutingInstanceRepository.GetAsync(
                            q =>
                                q.TenantID == tenantId &&
                                q.DeviceID == deviceId &&
                                q.RoutingInstanceType.IsTenantFacingVrf);

            ViewBag.RoutingInstance = new SelectList(_mapper.Map<List<RoutingInstanceViewModel>>(routingInstances),
                "Name", "Name", selectedRoutingInstance);
        }
    }
}