using AutoMapper;
using SCM.Data;
using SCM.Factories;
using SCM.Models;
using SCM.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SCM.Services
{
    public class InfrastructureAttachmentService : AttachmentService, IInfrastructureAttachmentService
    {
        public InfrastructureAttachmentService(IUnitOfWork unitOfWork,
            IMapper mapper,
            INetworkSyncService netSync,
            IAttachmentFactory attachmentFactory,
            IRoutingInstanceFactory vrfFactory) : base(unitOfWork, mapper, netSync, attachmentFactory, vrfFactory)
        {
        }

        private new string Properties { get; } = "AttachmentRole,"
               + "Mtu,"
               + "Device.Location.SubRegion.Region,"
               + "Device.Plane,"
               + "RoutingInstance.BgpPeers,"
               + "RoutingInstance.AttachmentSetRoutingInstances.AttachmentSet,"
               + "AttachmentBandwidth,"
               + "Interfaces.Device,"
               + "Interfaces.Ports.Device,"
               + "Interfaces.Ports.PortBandwidth,"
               + "Interfaces.Ports.Interface.Vlans.Vif,"
               + "Vifs.RoutingInstance.BgpPeers,"
               + "Vifs.Vlans";

        /// <summary>
        /// Get all Infrastructure Attachments.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Attachment>> GetAllAsync(string searchString = "", bool includeProperties = true)
        {
            var p = includeProperties ? Properties : "Device";

            var query = from attachments in await UnitOfWork.AttachmentRepository.GetAsync(q => 
            q.AttachmentRole.PortPool.PortRole.PortRoleType == PortRoleType.ProviderInfrastructure,
                includeProperties: p,
                AsTrackable: false)
                        select attachments;

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(x => x.Device.Name.Contains(searchString));

            }

            return query.ToList();
        }

        /// <summary>
        /// Create a new Attachment
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public new async Task<ServiceResult> AddAsync(AttachmentRequest request)
        {
            // Clear the 'Created' flag for any existing attachments. This will suppress messages displayed in the UI
            // so that only messages for the new attachment will be displayed.

            var lastCreatedAttachments = await UnitOfWork.AttachmentRepository.GetAsync(q => 
            q.AttachmentRole.PortPool.PortRole.PortRoleType == PortRoleType.ProviderInfrastructure && q.Created);

            foreach (var attachment in lastCreatedAttachments)
            {
                attachment.Created = false;
                UnitOfWork.AttachmentRepository.Update(attachment);
            }

            await UnitOfWork.SaveAsync();

            return await base.AddAsync(request);
            
        }
    }
}