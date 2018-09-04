using AutoMapper;
using SCM.Data;
using SCM.Models;
using SCM.Services;
using SCM.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Services
{
    /// <summary>
    /// Base service logic for vifs
    /// </summary>
    public abstract class BaseVifService : BaseService
    {
        protected internal readonly string _properties = "VifRole,"
                + "Attachment.Tenant,"
                + "VifRole.RoutingInstanceType,"
                + "Attachment.AttachmentRole,"
                + "Attachment.Device.Location.SubRegion.Region,"
                + "Attachment.Device.Plane,"
                + "Attachment.Device.DeviceRole,"
                + "Attachment.RoutingInstance.BgpPeers,"
                + "Attachment.RoutingInstance.AttachmentSetRoutingInstances.AttachmentSet,"
                + "Attachment.AttachmentBandwidth,"
                + "Attachment.ContractBandwidthPool.ContractBandwidth,"
                + "Attachment.Interfaces.Device,"
                + "Attachment.Interfaces.Ports.Device,"
                + "Attachment.Interfaces.Ports.PortBandwidth,"
                + "Attachment.Interfaces.Ports.Interface.Vlans.Vif,"
                + "Attachment.Vifs.Tenant,"
                + "Attachment.Vifs.RoutingInstance.BgpPeers,"
                + "Attachment.Vifs.Vlans.Vif.ContractBandwidthPool,"
                + "Attachment.Vifs.ContractBandwidthPool.Tenant,"
                + "Attachment.Vifs.ContractBandwidthPool.ContractBandwidth,"
                + "RoutingInstance.RoutingInstanceType,"
                + "RoutingInstance.BgpPeers,"
                + "RoutingInstance.Device,"
                + "RoutingInstance.Tenant,"
                + "RoutingInstance.AttachmentSetRoutingInstances.AttachmentSet.Tenant,"
                + "Vlans,"
                + "ContractBandwidthPool.ContractBandwidth,"
                + "ContractBandwidthPool.Tenant,"
                + "Tenant,"
                + "Mtu";

        public BaseVifService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IValidator validator) : base(unitOfWork, mapper, validator)
        {
        }

        /// <summary>
        /// Get a vif by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deep"></param>
        /// <param name="asTrackable"></param>
        /// <returns></returns>
        public virtual async Task<Vif> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return (from result in await UnitOfWork.VifRepository.GetAsync(
                q => 
                    q.VifID == id,
                    includeProperties: deep.HasValue && deep.Value ? _properties : "VifRole",
                    AsTrackable: asTrackable)
                    select result)
                    .SingleOrDefault();
        }

        /// <summary>
        /// Get all vifs for a given attachment.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deep"></param>
        /// <param name="asTrackable"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<Vif>> GetAllByAttachmentIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return (from result in await UnitOfWork.VifRepository.GetAsync(
                 q =>
                    q.AttachmentID == id,
                    includeProperties: deep.HasValue && deep.Value ? _properties : "VifRole",
                    AsTrackable: asTrackable)
                    select result)
                    .ToList();
        }
    }
}
