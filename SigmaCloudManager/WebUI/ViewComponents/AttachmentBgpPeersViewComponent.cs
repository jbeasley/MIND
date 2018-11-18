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
    public class AttachmentBgpPeersViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AttachmentBgpPeersViewComponent(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? attachmentId, string portPoolName, string attachmentRoleName, IEnumerable<BgpPeerRequestViewModel> bgpPeerRequests)
        {
            if (bgpPeerRequests != null) return View(bgpPeerRequests);

            if (attachmentId.HasValue)
            {
                var attachment = (from result in await _unitOfWork.AttachmentRepository.GetAsync(
                                 q =>
                                 q.AttachmentID == attachmentId,
                                 query: q =>
                                 q.Include(x => x.AttachmentRole),
                                 AsTrackable: false)
                                  select result)
                                .SingleOrDefault();

                // MUST pass null as the model to the view - see https://github.com/aspnet/Announcements/issues/221
                if (attachment == null) return View(model: null as List<BgpPeerRequestViewModel>);
                if (!attachment.AttachmentRole.IsLayer3Role) return View(model: null as List<BgpPeerRequestViewModel>);
            }

            if (!string.IsNullOrEmpty(portPoolName) && !string.IsNullOrEmpty(attachmentRoleName))
            {
                var attachmentRole = (from result in await _unitOfWork.AttachmentRoleRepository.GetAsync(
                                      q =>
                                      q.PortPool.Name == portPoolName &&
                                      q.Name == attachmentRoleName)
                                      select result)
                                      .SingleOrDefault();

                // Must pass an empty list of the required model type here - https://github.com/aspnet/Mvc/issues/5597
                if (attachmentRole == null) return View(model: null as List<BgpPeerRequestViewModel>);
                if (attachmentRole.IsLayer3Role) return View(model: new List<BgpPeerRequestViewModel>());
            }

            return View(model: null as List<BgpPeerRequestViewModel>);
        }
    }
}

