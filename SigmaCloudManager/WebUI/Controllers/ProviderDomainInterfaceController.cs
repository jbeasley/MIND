using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mind.Builders;
using Mind.Models;
using Mind.WebUI.Attributes;
using Mind.WebUI.Models;
using SCM.Controllers;
using SCM.Data;
using SCM.Models;
using SCM.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.WebUI.Controllers
{
    public class ProviderDomainInterfaceController : BaseViewController
    {
        private readonly IProviderDomainInterfaceService _interfaceService;

        public ProviderDomainInterfaceController(IProviderDomainInterfaceService interfaceService, IUnitOfWork unitOfWork, IMapper mapper) : 
            base(unitOfWork, mapper)

        [HttpGet]
        [ValidateProviderDomainAttachmentExists]
        public async Task<IActionResult> GetAllByAttachmentId(int? attachmentId)
        {
            var attachment = await _unitOfWork.AttachmentRepository.GetByIDAsync(attachmentId.Value);
            ViewBag.Attachment = Mapper.Map<ProviderDomainAttachmentViewModel>(attachment);

            var ifaces = await _unitOfWork.InterfaceRepository.GetAsync(
                q =>
                q.AttachmentID == attachmentId,
                query: q => q.IncludeDeepProperties(),
                AsTrackable: false);

            return View(Mapper.Map<List<ProviderDomainInterfaceViewModel>>(ifaces));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateModelState]
        [ValidateProviderDomainInterfaceExists]
        public async Task<ActionResult> Edit(int? interfaceId, ProviderDomainInterfaceUpdateViewModel updateModel)
        {
            var iface = await _interfaceService.GetByIDAsync(interfaceId.Value);
            if (iface.HasPreconditionFailed(Request, updateModel.RowVersion.ToString()))
            {
                ModelState.PopulateModelState(iface);
                return View(Mapper.Map<ProviderDomainUpdateViewModel>(iface));
            }

            var update = Mapper.Map<ProviderDomainInterfaceUpdateViewModel>(updateModel);

            try
            {
                await _interfaceService.UpdateAsync(interfaceId.Value, update);
                return RedirectToAction(nameof(GetAllByAttachmentId), new { attachmentId = iface.AttachmentID });
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

            var attachment = await _unitOfWork.AttachmentRepository.GetByIDAsync(iface.AttachmentID);
            ViewBag.Attachment = Mapper.Map<ProviderDomainAttachmentViewModel>(attachment);

            return View(Mapper.Map<ProviderDomainInterfaceUpdateViewModel>(iface));
        }
    }
}
