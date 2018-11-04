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
    public class VpnAttachmentSetsViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VpnAttachmentSetsViewComponent(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? vpnId, IEnumerable<VpnAttachmentSetRequestViewModel> requests)
        {
            if (vpnId.HasValue)
            {
                // Existing VPN - get the attachment sets which are bound tp the vpn
                var vpnAttachmentSets = await _unitOfWork.VpnAttachmentSetRepository.GetAsync(
                                    q =>
                                    q.VpnID == vpnId,
                                    query: q => q.IncludeDeepProperties(),
                                    AsTrackable: false);

                return View(_mapper.Map<VpnAttachmentSetRequestViewModel>(vpnAttachmentSets));
            }
            else
            {
                return View(requests);
            }
        }
    }
}
