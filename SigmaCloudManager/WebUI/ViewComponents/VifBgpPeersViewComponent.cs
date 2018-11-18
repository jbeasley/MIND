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
    public class VifBgpPeersViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VifBgpPeersViewComponent(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? attachmentId, int? vifId, string vifRoleName, IEnumerable<BgpPeerRequestViewModel> bgpPeerRequests)
        {
            if (bgpPeerRequests != null) return View(bgpPeerRequests);

            if (vifId.HasValue)
            {
                var vif = (from result in await _unitOfWork.VifRepository.GetAsync(
                                 q =>
                                 q.VifID == vifId,
                                 query: q =>
                                 q.Include(x => x.VifRole),
                                 AsTrackable: false)
                                  select result)
                                .SingleOrDefault();

                // MUST pass null as the model to the view - see https://github.com/aspnet/Announcements/issues/221
                if (vif == null) return View(model: null as List<BgpPeerRequestViewModel>);
                if (!vif.VifRole.IsLayer3Role) return View(model: null as List<BgpPeerRequestViewModel>);
            }

            if (attachmentId.HasValue && !string.IsNullOrEmpty(vifRoleName))
            {
                var attachment = (from result in await _unitOfWork.AttachmentRepository.GetAsync(
                                q =>
                                  q.AttachmentID == attachmentId,
                                  query: q =>
                                  q.Include(x => x.AttachmentRole))
                                  select result)
                                 .SingleOrDefault();
                                   
                if (attachment == null) return View(model: null as List<BgpPeerRequestViewModel>);

                var vifRole = (from result in await _unitOfWork.VifRoleRepository.GetAsync(
                                      q =>
                                      q.Name == vifRoleName &&
                                      q.AttachmentRole.Name == attachment.AttachmentRole.Name)
                                      select result)
                                      .SingleOrDefault();

                // Must pass an empty list of the required model type here - https://github.com/aspnet/Mvc/issues/5597
                if (vifRole == null) return View(model: null as List<BgpPeerRequestViewModel>);
                if (vifRole.IsLayer3Role) return View(model: new List<BgpPeerRequestViewModel>());
            }

            return View(model: null as List<BgpPeerRequestViewModel>);
        }
    }
}

