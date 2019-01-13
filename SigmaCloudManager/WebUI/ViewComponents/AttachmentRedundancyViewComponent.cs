using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mind.WebUI.Models;
using SCM.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.WebUI.ViewComponents
{
    public class AttachmentRedundancyViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AttachmentRedundancyViewComponent(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(AttachmentRedundancyComponentViewModel model)
        {
            var attachmentRedundancyOptions = await _unitOfWork.AttachmentRedundancyRepository.GetAsync();
            ViewBag.AttachmentRedundancy = new SelectList(_mapper.Map<List<AttachmentRedundancyViewModel>>(attachmentRedundancyOptions),
                "Name", "Name", model.AttachmentRedundancy);

            return View(model);
        }
    }
}
