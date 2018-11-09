using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mind.WebUI.Models;
using SCM.Data;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.WebUI.ViewComponents
{ 
    public class AttachmentSetRoutingInstancesViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AttachmentSetRoutingInstancesViewComponent(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? attachmentSetId, IEnumerable<AttachmentSetRoutingInstanceRequestViewModel> attachmentSetRoutingInstanceRequests)
        {
            if (attachmentSetId.HasValue)
            {
                if (attachmentSetRoutingInstanceRequests != null) return View(attachmentSetRoutingInstanceRequests);

                // Existing attachment set - get the routing instances which are bound to the attachment set
                var items = await _unitOfWork.AttachmentSetRoutingInstanceRepository.GetAsync(
                                    q =>
                                    q.AttachmentSetID == attachmentSetId,
                                    query: q => q.IncludeDeepProperties(),
                                    AsTrackable: false);

                return View(_mapper.Map<List<AttachmentSetRoutingInstanceRequestViewModel>>(items));
            }

            return View(attachmentSetRoutingInstanceRequests);
        }
    }
}
