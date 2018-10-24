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
using Mind.WebUI.ViewComponents;
using Mind.Models.RequestModels;

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
        public IActionResult GetIpAddressingComponent(string portPoolName, string attachmentRoleName, 
            int? attachmentBandwidthGbps, bool? isMultiport)
        {
            return ViewComponent("VifIpAddressing", new
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
            return ViewComponent("VifContractBandwidthPool", new
            {
                portPoolName,
                attachmentRoleName,
                attachmentBandwidthGbps
            });
        }

        [HttpGet]
        [ValidateProviderDomainVifExists]
        public async Task<IActionResult> Details(int? vifId)
        {
            var item = await _vifService.GetByIDAsync(vifId.Value, deep: true, asTrackable: false);
            return View(_mapper.Map<ProviderDomainVifViewModel>(item));
        }

        [HttpGet]
        [ValidateProviderDomainAttachmentExists]
        public async Task<IActionResult> GetAllByAttachmentID(int? attachmentId)
        {
            var attachments = await _unitOfWork.VifRepository.GetAsync(
                    q =>
                    q.AttachmentID == attachmentId.Value,
                    query: q => q.IncludeValidationProperties(),
                    AsTrackable: false);

            ViewData["SuccessMessage"] = FormatAsHtmlList(attachments
                .Where(x => x.Created && x.ShowCreatedAlert)
                .Select(x => $"{x.Name} has been created.").ToList());

            var attachment = await _unitOfWork.AttachmentRepository.GetByIDAsync(attachmentId);
            ViewBag.Attachment = _mapper.Map<ProviderDomainAttachmentViewModel>(attachment);

            return View(_mapper.Map<List<ProviderDomainVifViewModel>>(attachments));
        }

        [HttpGet]
        [ValidateTenantExists]
        public async Task<IActionResult> Create(int? attachmentId)
        {
            var attachment = await _unitOfWork.AttachmentRepository.GetByIDAsync(attachmentId);
            ViewBag.Attachment = _mapper.Map<ProviderDomainAttachmentViewModel>(attachment);
            await PopulateVifRolesDropDownList(attachment.AttachmentID);
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
            }

            var attachment = await _unitOfWork.AttachmentRepository.GetByIDAsync(attachmentId);
            ViewBag.Attachment = _mapper.Map<ProviderDomainAttachmentViewModel>(attachment);

            await PopulateVifRolesDropDownList(attachmentId.Value, requestModel.VifRoleName);

            return View(requestModel);
        }

        [HttpGet]
        [ValidateProviderDomainVifExists]
        public async Task<ActionResult> Edit(int? vifId)
        {
            var vif = await _vifService.GetByIDAsync(vifId.Value, deep: true, asTrackable: false);
            var attachment = await _unitOfWork.AttachmentRepository.GetByIDAsync(vif.AttachmentID);
            ViewBag.Attachment = _mapper.Map<AttachmentViewModel>(attachment);
            if (vif.RoutingInstance != null)
            {
                await PopulateRoutingInstancesDropDownList(attachment.AttachmentID, vif.RoutingInstance.Name);
            }

            return View(_mapper.Map<ProviderDomainVifUpdateViewModel>(attachment));
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
                }
            }

            if (vif.RoutingInstance != null)
            {
                await PopulateRoutingInstancesDropDownList(vif.AttachmentID, vif.RoutingInstance.Name);
            }

            var attachment = await _unitOfWork.AttachmentRepository.GetByIDAsync(vif.AttachmentID);
            ViewBag.Attachment = _mapper.Map<ProviderDomainAttachmentViewModel>(attachment);

            return View(_mapper.Map<ProviderDomainVifUpdateViewModel>(attachment));
        }

        [HttpGet]
        [ValidateProviderDomainVifExists]
        public async Task<IActionResult> Delete(int? attachmentId, bool? concurrencyError = false)
        {
            var item = await _vifService.GetByIDAsync(attachmentId.Value, deep: true, asTrackable: false);
            if (concurrencyError.GetValueOrDefault()) ViewData.AddDeletePreconditionFailedMessage();

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

        private async Task PopulateRoutingInstancesDropDownList(int attachmentId, object selectedRoutingInstance = null)
        {
            var routingInstances = await _unitOfWork.RoutingInstanceRepository.GetAsync(
                            q =>
                                q.Attachments
                                .Where(
                                    x => 
                                    x.AttachmentID == attachmentId)
                                .Any() &&
                                q.RoutingInstanceType.IsTenantFacingVrf);

            ViewBag.RoutingInstance = new SelectList(_mapper.Map<List<RoutingInstanceViewModel>>(routingInstances),
                "Name", "Name", selectedRoutingInstance);
        }
    }
}