using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
using IO.NovaAttSwagger.Client;

namespace Mind.WebUI.Controllers
{
    public class InfrastructureVifController : BaseViewController
    {
        private readonly IInfrastructureVifService _vifService;
        public InfrastructureVifController(IInfrastructureVifService vifService, IUnitOfWork unitOfWork, IMapper mapper) :
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
        [ValidateInfrastructureVifExists]
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

            ViewBag.Attachment = _mapper.Map<InfrastructureAttachmentViewModel>(attachment);
            return View(_mapper.Map<InfrastructureVifViewModel>(item));
        }

        [HttpGet]
        [ValidateInfrastructureAttachmentExists]
        [SetInfrastructureAttachmentCookieState]
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

            ViewBag.Attachment = _mapper.Map<InfrastructureAttachmentViewModel>(attachment);

            return View(_mapper.Map<List<InfrastructureVifViewModel>>(vifs));
        }

        [HttpGet]
        [ValidateInfrastructureAttachmentExists]
        public async Task<IActionResult> Create(int? attachmentId)
        {
            var attachment = (from result in await _unitOfWork.AttachmentRepository.GetAsync(
                        q =>
                             q.AttachmentID == attachmentId,
                             query: q => q.IncludeDeepProperties(),
                             AsTrackable: false)
                              select result)
                             .Single();

            ViewBag.Attachment = _mapper.Map<InfrastructureAttachmentViewModel>(attachment);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInfrastructureAttachmentExists]
        public async Task<IActionResult> Create(int? attachmentId, InfrastructureVifRequestViewModel requestModel, bool? syncToNetwork)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var request = _mapper.Map<InfrastructureVifRequest>(requestModel);
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
            var attachment = (from result in await _unitOfWork.AttachmentRepository.GetAsync(
                        q =>
                             q.AttachmentID == attachmentId,
                             query: q => q.IncludeDeepProperties(),
                             AsTrackable: false)
                              select result)
                             .Single();

            ViewBag.Attachment = _mapper.Map<InfrastructureAttachmentViewModel>(attachment);
            return View(requestModel);
        }

        [HttpGet]
        [ValidateInfrastructureVifExists]
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

            ViewBag.Attachment = _mapper.Map<InfrastructureAttachmentViewModel>(attachment);

            return View(_mapper.Map<InfrastructureVifUpdateViewModel>(vif));
        }

        [HttpPost]
        [ValidateInfrastructureVifExists]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? vifId, InfrastructureVifUpdateViewModel update, bool? syncToNetwork)
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
                    var vifUpdate = _mapper.Map<InfrastructureVifUpdate>(update);

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

                    catch (ServiceBadArgumentsException ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }

                    catch (ApiException)
                    {
                        ModelState.AddNovaClientApiExceptionMessage();
                    }
                }
            }

            var attachment = (from result in await _unitOfWork.AttachmentRepository.GetAsync(
                        q =>
                             q.AttachmentID == vif.AttachmentID,
                             query: q => q.IncludeDeepProperties(),
                             AsTrackable: false)
                              select result)
                             .Single();

            ViewBag.Attachment = _mapper.Map<InfrastructureAttachmentViewModel>(attachment);

            return View(update);
        }

        [HttpGet]
        [ValidateInfrastructureVifExists]
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

            ViewBag.Attachment = _mapper.Map<InfrastructureAttachmentViewModel>(attachment);

            return View(_mapper.Map<InfrastructureVifDeleteViewModel>(item));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(InfrastructureVifDeleteViewModel model)
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

            ViewBag.Attachment = _mapper.Map<InfrastructureAttachmentViewModel>(attachment);

            return View(_mapper.Map<InfrastructureVifDeleteViewModel>(vif));
        }
    }
}