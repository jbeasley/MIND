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
    public class BgpPeersViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BgpPeersViewComponent(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(BgpPeersComponentViewModel model)
        {
            if (model.BgpPeers != null) return View(model.BgpPeers);

            if (model.RoutingInstanceId.HasValue)
            {
                var routingInstance = (from result in await _unitOfWork.RoutingInstanceRepository.GetAsync(
                                 q =>
                                 q.RoutingInstanceID == model.RoutingInstanceId,
                                 query: q =>
                                 q.Include(x => x.BgpPeers),
                                 AsTrackable: false)
                                  select result)
                                .SingleOrDefault();

                // MUST pass null as the model to the view - see https://github.com/aspnet/Announcements/issues/221
                if (routingInstance == null) return View(model: null as List<BgpPeerRequestViewModel>);

                return View(_mapper.Map<List<BgpPeerRequestViewModel>>(routingInstance.BgpPeers));
            }

            return View(model: null as List<BgpPeerRequestViewModel>);
        }
    }
}

