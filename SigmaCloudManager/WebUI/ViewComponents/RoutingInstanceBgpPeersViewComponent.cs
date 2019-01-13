using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mind.WebUI.Models;
using SCM.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.WebUI.ViewComponents
{
    public class RoutingInstanceBgpPeersViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoutingInstanceBgpPeersViewComponent(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(BgpPeersComponentViewModel model)
        {
            // If a BGP peers collection is supplied then return it for rendering the view
            if (model.BgpPeers != null) return View(model.BgpPeers);

            // If a routing instance ID has been supplied then get all BGP peers for the routing instance
            // and return to render the view
            if (model.RoutingInstanceId.HasValue)
            {
                return await GetViewByRoutingInstanceIdAsync(model.RoutingInstanceId.Value);
            }

            if (model.AttachmentId.HasValue)
            {
                return await GetViewByAttachmentIdAsync(model.AttachmentId.Value);
            }

            if (model.VifId.HasValue)
            {
                return await GetViewByVifIdAsync(model.VifId.Value);
            }

            if (!string.IsNullOrEmpty(model.PortPoolName) && !string.IsNullOrEmpty(model.AttachmentRoleName))
            {
                return await GetViewByAttachmentRoleAsync(model.PortPoolName, model.AttachmentRoleName);
            }

            if (!string.IsNullOrEmpty(model.AttachmentRoleName) && !string.IsNullOrEmpty(model.VifRoleName))
            {
                return await GetViewByVifRoleAsync(model.AttachmentRoleName, model.VifRoleName);
            }

            // Default - return null to indicate no BGP peers are required
            return View(model: null as List<BgpPeerRequestViewModel>);
        }

        private async Task<IViewComponentResult> GetViewByRoutingInstanceIdAsync(int routingInstanceId)
        {
            var bgpPeers = await _unitOfWork.BgpPeerRepository.GetAsync(
                             q =>
                             q.RoutingInstanceID == routingInstanceId,
                             AsTrackable: false);

            return View(_mapper.Map<List<BgpPeerRequestViewModel>>(bgpPeers));
        
        }

        /// <summary>
        /// If an attachment ID has been supplied then check the attachment role is a layer 3 role, 
        /// then get all BGP peers for the routing instance associated with the attachment
        /// and return to render the view
        /// </summary>
        /// <returns>The view by attachment identifier</returns>
        /// <param name="attachmentId">Attachment identifier.</param>
        private async Task<IViewComponentResult> GetViewByAttachmentIdAsync(int attachmentId)
        {
            var attachment = (from result in await _unitOfWork.AttachmentRepository.GetAsync(
                q => q.AttachmentID == attachmentId,
                  query: q => q.Include(x => x.AttachmentRole)
                  .Include(x => x.RoutingInstance.BgpPeers),
                  AsTrackable: false)
                              select result)
                  .SingleOrDefault();

            if (attachment == null || !attachment.AttachmentRole.IsLayer3Role)
            {
                return View(model: null as List<BgpPeerRequestViewModel>);
            }

            return View(_mapper.Map<List<BgpPeerRequestViewModel>>(attachment.RoutingInstance.BgpPeers));
        }

        /// <summary>
        /// If a vif ID has been supplied then check the vif role is a layer 3 role, 
        /// then get all BGP peers for the routing instance associated with the vif
        /// and return to render the view
        /// </summary>
        /// <returns>The view by vif identifier async.</returns>
        /// <param name="vifId">Vif identifier.</param>
        private async Task<IViewComponentResult> GetViewByVifIdAsync(int vifId)
        {
            var vif = (from result in await _unitOfWork.VifRepository.GetAsync(
                q => q.VifID == vifId,
                  query: q => q.Include(x => x.VifRole)
                  .Include(x => x.RoutingInstance.BgpPeers),
                  AsTrackable: false)
                       select result)
                  .SingleOrDefault();

            if (vif == null || !vif.VifRole.IsLayer3Role)
            {
                return View(model: null as List<BgpPeerRequestViewModel>);
            }

            return View(_mapper.Map<List<BgpPeerRequestViewModel>>(vif.RoutingInstance.BgpPeers));
        }

        /// <summary>
        /// If port pool and attachment role arguments are supplied then check if the attachment role
        /// corresponds with a layer 3 role. If it does pass an empty collection to the view for rendering,
        /// otherwise pass null to indicate no BGP peers should be requested
        /// </summary>
        /// <returns>The view by attachment role async.</returns>
        /// <param name="portPoolName">Port pool name.</param>
        /// <param name="attachmentRoleName">Attachment role name.</param>
        private async Task<IViewComponentResult> GetViewByAttachmentRoleAsync(string portPoolName, string attachmentRoleName)
        {
            var attachmentRole = (from result in await _unitOfWork.AttachmentRoleRepository.GetAsync(
                                q =>
                                 q.Name == attachmentRoleName &&
                                 q.PortPool.Name == portPoolName)
                                 select result)
                                 .SingleOrDefault();

            if (attachmentRole == null) return View(model: null as List<BgpPeerRequestViewModel>);
            if (attachmentRole.IsLayer3Role) return View(model: new List<BgpPeerRequestViewModel>());

            return View(model: null as List<BgpPeerRequestViewModel>);
        }

        /// <summary>
        /// If attachment role and vif role arguments are supplied then check if the vif role
        /// corresponds with a layer 3 role. If it does pass an empty collection to the view for rendering,
        /// otherwise pass null to indicate no BGP peers should be requested
        /// </summary>
        /// <returns>The view by vif role async.</returns>
        /// <param name="attachmentRoleName">Attachment role name.</param>
        /// <param name="vifRoleName">Vif role name.</param>
        private async Task<IViewComponentResult> GetViewByVifRoleAsync(string attachmentRoleName, string vifRoleName)
        {
            var vifRole = (from result in await _unitOfWork.VifRoleRepository.GetAsync(
                         q =>
                           q.Name == vifRoleName &&
                           q.AttachmentRole.Name == attachmentRoleName)
                           select result)
                           .SingleOrDefault();

            if (vifRole == null) return View(model: null as List<BgpPeerRequestViewModel>);
            if (vifRole.IsLayer3Role) return View(model: new List<BgpPeerRequestViewModel>());

            return View(model: null as List<BgpPeerRequestViewModel>);
        }
    }
}

