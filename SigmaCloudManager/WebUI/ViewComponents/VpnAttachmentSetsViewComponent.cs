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

        public async Task<IViewComponentResult> InvokeAsync(int? vpnId, IEnumerable<VpnAttachmentSetRequestViewModel> vpnAttachmentSets)
        {
            if (vpnId.HasValue)
            {
                if (vpnAttachmentSets != null) return View(vpnAttachmentSets);

                // Existing VPN - get the attachment sets which are bound to the vpn
                var items = await _unitOfWork.VpnAttachmentSetRepository.GetAsync(
                                    q =>
                                    q.VpnID == vpnId,
                                    query: q => q.IncludeDeepProperties(),
                                    AsTrackable: false);

                return View(_mapper.Map<List<VpnAttachmentSetRequestViewModel>>(items));
            }

            return View(vpnAttachmentSets);
        }
    }
}
