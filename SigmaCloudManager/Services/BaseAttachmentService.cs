using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using AutoMapper;
using SCM.Data;
using SCM.Models;
using SCM.Models.RequestModels;
using SCM.Factories;
using SCM.Validators;
using Mind.Builders;

namespace SCM.Services
{
    /// <summary>
    /// Base Service logic for Attachments
    /// </summary>
    public abstract class BaseAttachmentService : BaseService, IBaseAttachmentService
    {
        public BaseAttachmentService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IValidator validator) : base(unitOfWork, mapper, validator)
        {
        }

        protected string Properties { get; } = "Tenant,"
                + "AttachmentRole.PortPool.PortRole,"
                + "Mtu,"
                + "Device.Location.SubRegion.Region,"
                + "Device.Plane,"
                + "Device.DeviceRole,"
                + "Device.DeviceStatus,"
                + "Device.DeviceRole,"
                + "Device.DeviceModel,"
                + "RoutingInstance.RoutingInstanceType,"
                + "RoutingInstance.BgpPeers,"
                + "RoutingInstance.Tenant,"
                + "RoutingInstance.AttachmentSetRoutingInstances.AttachmentSet.Tenant,"
                + "AttachmentBandwidth,"
                + "ContractBandwidthPool.ContractBandwidth,"
                + "ContractBandwidthPool.Tenant,"
                + "Interfaces.Device,"
                + "Interfaces.Ports.Device,"
                + "Interfaces.Ports.PortSfp,"
                + "Interfaces.Ports.PortStatus,"
                + "Interfaces.Ports.PortPool.PortRole,"
                + "Interfaces.Ports.PortConnector,"
                + "Interfaces.Ports.PortBandwidth,"
                + "Interfaces.Ports.Interface.Vlans.Vif,"
                + "Vifs.RoutingInstance.BgpPeers,"
                + "Vifs.RoutingInstance.Tenant,"
                + "Vifs.RoutingInstance.AttachmentSetRoutingInstances.AttachmentSet.Tenant,"
                + "Vifs.Vlans.Vif.ContractBandwidthPool,"
                + "Vifs.ContractBandwidthPool.ContractBandwidth,"
                + "Vifs.ContractBandwidthPool.Tenant,"
                + "Vifs.VifRole";

        /// <summary>
        /// Find an Attachment by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Attachment> GetByIDAsync(int id, bool deep = false, bool asTrackable = false)
        {
            var p = deep ? Properties : string.Empty;
            var dbResult = await UnitOfWork.AttachmentRepository.GetAsync(q => q.AttachmentID == id,
                includeProperties: p,
                AsTrackable: asTrackable);

            return dbResult.SingleOrDefault();
        }

        /// <summary>
        /// Find an Attachment from the name of the device and the attachment name
        /// </summary>
        /// <param name="deviceName"></param>
        /// <param name="attachmentName"></param>
        /// <returns></returns>
        public async Task<Attachment> GetByNameAsync(string deviceName, string attachmentName, bool deep = false, bool asTrackable = false)
        {
            var p = deep ? Properties : string.Empty;
            var dbResult = await UnitOfWork.AttachmentRepository.GetAsync(q => q.Device.Name == deviceName && q.Name == attachmentName,
                includeProperties: p,
                AsTrackable: asTrackable);

            return dbResult.SingleOrDefault();
        }

        /// <summary>
        /// Get all Attachments for a given routing instance.
        /// </summary>
        /// <param name="routingInstanceID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Attachment>> GetAllByRoutingInstanceIDAsync(int routingInstanceID, bool deep = true, bool asTrackable = false)
        {
            var p = deep ? Properties : string.Empty;

            return await UnitOfWork.AttachmentRepository.GetAsync(q => q.RoutingInstanceID == routingInstanceID,
                includeProperties: p,
                AsTrackable: asTrackable);
        }

        /// <summary>
        /// Get an Attachment for a given Interface.
        /// </summary>
        /// <param name= interfaceID"></param>
        /// <returns></returns>
        public async Task<Attachment> GetByInterfaceIDAsync(int interfaceID, bool deep = true, bool asTrackable = false)
        {
            var p = deep ? Properties : string.Empty;

            var dbResult = await UnitOfWork.AttachmentRepository.GetAsync(q => q.Interfaces.Where(x => x.InterfaceID == interfaceID).Any(),
                includeProperties: p,
                AsTrackable: asTrackable);

            return dbResult.SingleOrDefault();
        }
    }
}