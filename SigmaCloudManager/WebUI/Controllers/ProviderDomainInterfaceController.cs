using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mind.Builders;
using Mind.Models;
using Mind.Services;
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
        {
        }

        [HttpGet]
        [ValidateProviderDomainInterfaceExists]
        public async Task<IActionResult> Details(int? interfaceId)
        {
            var iface = await _interfaceService.GetByIDAsync(interfaceId.Value, deep: true, asTrackable: false);
            var attachment = await _unitOfWork.AttachmentRepository.GetByIDAsync(iface.AttachmentID);
            ViewBag.Attachment = Mapper.Map<ProviderDomainAttachmentViewModel>(attachment);

            return View(Mapper.Map<InterfaceViewModel>(iface));
        }

        [HttpGet]
        [ValidateProviderDomainAttachmentExists]
        public async Task<IActionResult> GetAllByAttachmentID(int? attachmentId)
        {
            var attachment = await _unitOfWork.AttachmentRepository.GetByIDAsync(attachmentId.Value);
            ViewBag.Attachment = Mapper.Map<ProviderDomainAttachmentViewModel>(attachment);

            var ifaces = await _unitOfWork.InterfaceRepository.GetAsync(
                q =>
                q.AttachmentID == attachmentId,
                query: q => q.IncludeDeepProperties(),
                AsTrackable: false);

            return View(Mapper.Map<List<InterfaceViewModel>>(ifaces));
        }
    }
}
