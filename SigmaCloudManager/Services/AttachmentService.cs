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
    /// Service logic for Attachments
    /// </summary>
    public class AttachmentService : BaseService, IAttachmentService
    {
        protected readonly IAttachmentFactory AttachmentFactory;
        protected readonly IRoutingInstanceFactory RoutingInstanceFactory;


        public AttachmentService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IAttachmentFactory attachmentFactory,
            IRoutingInstanceFactory vrfFactory) : base(unitOfWork, mapper)
        {
            AttachmentFactory = attachmentFactory;
            RoutingInstanceFactory = vrfFactory;
        }

        protected string Properties { get; } = "Tenant,"
                + "AttachmentRole.PortPool.PortRole,"
                + "Mtu,"
                + "Device.Location.SubRegion.Region,"
                + "Device.Plane,"
                + "Device.DeviceRole,"
                + "RoutingInstance.RoutingInstanceType,"
                + "RoutingInstance.BgpPeers,"
                + "RoutingInstance.Tenant,"
                + "RoutingInstance.AttachmentSetRoutingInstances.AttachmentSet.Tenant,"
                + "AttachmentBandwidth,"
                + "ContractBandwidthPool.ContractBandwidth,"
                + "ContractBandwidthPool.Tenant,"
                + "Interfaces.Device,"
                + "Interfaces.Ports.Device,"
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
        public async Task<Attachment> GetByIDAsync(int id, bool deep = false)
        {
            var p = deep ? Properties : string.Empty;
            var dbResult = await UnitOfWork.AttachmentRepository.GetAsync(q => q.AttachmentID == id,
                includeProperties: p,
                AsTrackable: false);

            return dbResult.SingleOrDefault();
        }

        /// <summary>
        /// Find an Attachment from the name of the device and the attachment name
        /// </summary>
        /// <param name="deviceName"></param>
        /// <param name="attachmentName"></param>
        /// <returns></returns>
        public async Task<Attachment> GetByNameAsync(string deviceName, string attachmentName, bool deep = false)
        {
            var p = deep ? Properties : string.Empty;
            var dbResult = await UnitOfWork.AttachmentRepository.GetAsync(q => q.Device.Name == deviceName && q.Name == attachmentName,
                includeProperties: p,
                AsTrackable: false);

            return dbResult.SingleOrDefault();
        }

        /// <summary>
        /// Get all Attachments for a given routing instance.
        /// </summary>
        /// <param name="routingInstanceID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Attachment>> GetAllByRoutingInstanceIDAsync(int routingInstanceID, bool deep = true)
        {
            var p = deep ? Properties : string.Empty;

            return await UnitOfWork.AttachmentRepository.GetAsync(q => q.RoutingInstanceID == routingInstanceID,
                includeProperties: p,
                AsTrackable: false);
        }

        /// <summary>
        /// Get an Attachment for a given Interface.
        /// </summary>
        /// <param name="interfaceID"></param>
        /// <returns></returns>
        public async Task<Attachment> GetByInterfaceIDAsync(int interfaceID, bool deep = true)
        {
            var p = deep ? Properties : string.Empty;

            var dbResult = await UnitOfWork.AttachmentRepository.GetAsync(q => q.Interfaces.Where(x => x.InterfaceID == interfaceID).Any(),
                includeProperties: p,
                AsTrackable: false);

            return dbResult.SingleOrDefault();
        }

        /// <summary>
        /// Create a new Attachment
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ServiceResult> AddAsync(AttachmentRequest request)
        {
            // Hand off to method to generate the Attachment

            if (request.BundleRequired)
            {
                // Create bundle 
                return await AddBundleAttachmentAsync(request);
            }
            else if (request.MultiPortRequired)
            {
                // Create multi-port
                return await AddMultiPortAsync(request);
            }
            else
            {
                // Create default Attachment type
                return await AddAttachmentAsync(request);
            }
        }

        /// <summary>
        /// Delete an attachment from the network and from the inventory
        /// </summary>
        /// <param name="attachment"></param>
        /// <returns></returns>
        public async Task<ServiceResult> DeleteAsync(Attachment attachment)
        {
            var result = new ServiceResult
            {
                IsSuccess = true
            };

            using (var transaction = UnitOfWork.Database.BeginTransaction())
            {
                var vifs = attachment.Vifs.ToList();
                var ifaces = attachment.Interfaces;
                var ports = ifaces.SelectMany(x => x.Ports).ToList();
                var vlans = vifs.SelectMany(x => x.Vlans);
                var routingInstances = vifs.Select(x => x.RoutingInstance).Where(x => x != null).ToList();
                if (attachment.RoutingInstance != null)
                {
                    routingInstances.Add(attachment.RoutingInstance);
                }
                var contractBandwidthPools = vifs.Select(x => x.ContractBandwidthPool).Where(x => x != null).ToList();
                if (attachment.ContractBandwidthPool != null)
                {
                    contractBandwidthPools.Add(attachment.ContractBandwidthPool);
                }

                foreach (var vlan in vlans)
                {
                    UnitOfWork.VlanRepository.Delete(vlan);
                }

                foreach (var contractBandwidthPool in contractBandwidthPools)
                {
                    UnitOfWork.ContractBandwidthPoolRepository.Delete(contractBandwidthPool);
                }

                foreach (var iface in ifaces)
                {
                    UnitOfWork.InterfaceRepository.Delete(iface);
                }

                var portStatusDbResult = await UnitOfWork.PortStatusRepository.GetAsync(q => q.PortStatusType == PortStatusTypeEnum.Free);
                var portStatus = portStatusDbResult.SingleOrDefault();

                foreach (var port in ports)
                {
                    port.TenantID = null;
                    port.InterfaceID = null;
                    port.PortStatusID = portStatus.PortStatusID;
                    UnitOfWork.PortRepository.Update(port);
                }

                foreach (var vif in vifs)
                {
                    UnitOfWork.VifRepository.Delete(vif);
                }
                UnitOfWork.AttachmentRepository.Delete(attachment);
                await UnitOfWork.SaveAsync();

                foreach (var routingInstance in routingInstances)
                {
                    var currentRoutingInstanceResult = await UnitOfWork.RoutingInstanceRepository.GetAsync(x =>
                        x.RoutingInstanceID == routingInstance.RoutingInstanceID,
                        includeProperties: "RoutingInstanceType,Attachments,Vifs,BgpPeers,LogicalInterfaces");

                    // Routing Instance can be deleted if no more VIFs or Attachments reference it and 
                    // it is not the default routing instance for the device.

                    var currentRoutingInstance = currentRoutingInstanceResult.Single();
                    if (!currentRoutingInstance.Vifs.Any() && !currentRoutingInstance.Attachments.Any())
                    {
                        if (routingInstance.RoutingInstanceType.IsDefault)
                        {
                            // There are no more logical attachments whcih belong to the default routing instance
                            // Clean up BGP peers and Logical Interfaces which belong to the default routing instance

                            foreach (var bgpPeer in routingInstance.BgpPeers)
                            {
                                UnitOfWork.BgpPeerRepository.Delete(bgpPeer);
                            }
                            foreach (var logicalInterface in routingInstance.LogicalInterfaces)
                            {
                                UnitOfWork.LogicalInterfaceRepository.Delete(logicalInterface);
                            }
                        }
                        else
                        {
                            // Not the default routing instance, so delete the routing instance

                            UnitOfWork.RoutingInstanceRepository.Delete(currentRoutingInstance);
                        }
                    }
                }

                await UnitOfWork.SaveAsync();
                transaction.Commit();
            }

            return result;
        }

        /// <summary>
        /// Update am Attachment
        /// </summary>
        /// <param name="attachmentUpdate"></param>
        /// <returns></returns>
        public async Task<ServiceResult> UpdateAttachmentAsync(AttachmentUpdate update)
        {
            var result = new ServiceResult
            {
                IsSuccess = true
            };

            using (var transaction = UnitOfWork.Database.BeginTransaction())
            {
                // Get the existing Attachment for update

                var dbResult = await this.UnitOfWork.AttachmentRepository.GetAsync(q => q.AttachmentID == update.AttachmentID,
                includeProperties: "Tenant,Device.DeviceRole,ContractBandwidthPool,Interfaces.Ports,"
                + "RoutingInstance.RoutingInstanceType,AttachmentRole",
                AsTrackable: false);

                var attachment = dbResult.Single();

                if (update.CreateNewRoutingInstance)
                {
                    // Request to create a new routing instance - get the existing routing instance 

                    var routingInstanceResult = await UnitOfWork.RoutingInstanceRepository.GetAsync(x => 
                        x.RoutingInstanceID == attachment.RoutingInstanceID,
                        includeProperties: "Attachments,Vifs", AsTrackable: false);
                    var routingInstance = routingInstanceResult.Single();

                    // Create a new routing instance

                    update.DeviceID = routingInstance.DeviceID;
                    update.TenantID = routingInstance.TenantID;
                    update.RoutingInstanceTypeID = routingInstance.RoutingInstanceTypeID;
                    var vrfFactoryResult = await RoutingInstanceFactory.NewAsync(update);
                    var newRoutingInstance = (RoutingInstance)vrfFactoryResult.Item;
                    UnitOfWork.RoutingInstanceRepository.Insert(newRoutingInstance);
                    await UnitOfWork.SaveAsync();

                    // Add new routing instance to the Attachment

                    attachment.RoutingInstanceID = newRoutingInstance.RoutingInstanceID;

                    // Check to delete the old routing instance

                    if (routingInstance.Attachments.Count == 1 && !routingInstance.Vifs.Any())
                    {
                        // The only Attachment which belongs to the routing instance is the current Attachment
                        // so routing instance can be deleted

                        UnitOfWork.RoutingInstanceRepository.Delete(routingInstance);
                    }
                }
                else if (update.RoutingInstanceID != null && update.RoutingInstanceID != attachment.RoutingInstanceID)
                {
                    // An existing routing instance has been selected - check to delete old routing instance

                    var routingInstanceResult = await UnitOfWork.RoutingInstanceRepository.GetAsync(x =>
                        x.RoutingInstanceID == attachment.RoutingInstanceID,
                        includeProperties: "Attachments,Vifs");
                    var routingInstance = routingInstanceResult.Single();

                    if (routingInstance.Attachments.Count == 1 && !routingInstance.Vifs.Any())
                    {
                        // The only Logical Attachment which belongs to the routing instance is the current Attachment
                        // so routing instance can be deleted

                        UnitOfWork.RoutingInstanceRepository.Delete(routingInstance);
                    }

                    // Add new selected routing instance to the Attachment

                    attachment.RoutingInstanceID = update.RoutingInstanceID;
                    attachment.RoutingInstance = null;
                }

                if (attachment.ContractBandwidthPoolID != null)
                {
                    if (update.ContractBandwidthID != attachment.ContractBandwidthPool.ContractBandwidthID)
                    {
                        // New contract bandwidth has been selected
                        // Create a new Contract Bandwidth Pool

                        var newContractBandwidthPool = Mapper.Map<ContractBandwidthPool>(update);
                        UnitOfWork.ContractBandwidthPoolRepository.Insert(newContractBandwidthPool);
                        await UnitOfWork.SaveAsync();

                        // Delete old Contract Bandwidth Pool and associate the new Contract Bandwidth Pool with the Attachnent

                        await this.UnitOfWork.ContractBandwidthPoolRepository.DeleteAsync(attachment.ContractBandwidthPoolID.Value);
                        attachment.ContractBandwidthPoolID = newContractBandwidthPool.ContractBandwidthPoolID;
                    }
                    else
                    {
                        // Check for updates to the existing Contract Bandwidth Pool

                        if (attachment.ContractBandwidthPool.TrustReceivedCosAndDscp != update.TrustReceivedCosDscp)
                        {
                            attachment.ContractBandwidthPool.TrustReceivedCosAndDscp = update.TrustReceivedCosDscp;
                            this.UnitOfWork.ContractBandwidthPoolRepository.Update(attachment.ContractBandwidthPool);
                        }
                    }
                }

                if (attachment.IsBundle)
                {
                    attachment.BundleMinLinks= update.BundleMinLinks;
                    attachment.BundleMaxLinks = update.BundleMaxLinks;
                }

                attachment.MtuID = update.MtuID;
                attachment.Description = update.Description;
                attachment.Notes = update.Notes;

                // Copy over RowVersion property for concurrency checks

                attachment.RowVersion = update.RowVersion;

                // Set 'RequiresSync' property of Attachment and the associated Device, 
                // to indicate these entities require sync with the network

                attachment.RequiresSync = attachment.AttachmentRole.RequireSyncToNetwork;
                attachment.ShowRequiresSyncAlert = attachment.AttachmentRole.RequireSyncToNetwork;
                attachment.Device.RequiresSync = attachment.Device.DeviceRole.RequireSyncToNetwork;
                attachment.Device.ShowRequiresSyncAlert = attachment.Device.DeviceRole.RequireSyncToNetwork;

                this.UnitOfWork.AttachmentRepository.Update(attachment);
                this.UnitOfWork.DeviceRepository.Update(attachment.Device);
                await this.UnitOfWork.SaveAsync();
                result.Item = attachment;

                transaction.Commit();
            }

            return result;
        }

        /// <summary>
        /// Make changes to a Port which is associated with an Attachment
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        public async Task<ServiceResult> UpdateAttachmentPortAsync(AttachmentPortUpdate update)
        {
            var result = new ServiceResult
            {
                IsSuccess = true
            };

            if (update.CurrentPortID == update.PortID)
            {
                // Nothing to do

                return result;
            }

            var currentPortResult = await UnitOfWork.PortRepository.GetAsync(x => x.ID == update.CurrentPortID,
                includeProperties: "Device");
            var currentPort = currentPortResult.Single();
            var currentDevice = currentPort.Device;
            var port = await UnitOfWork.PortRepository.GetByIDAsync(update.PortID);
            var iface = await UnitOfWork.InterfaceRepository.GetByIDAsync(currentPort.InterfaceID);
            var attachmentResult = await UnitOfWork.AttachmentRepository.GetAsync(x => x.AttachmentID == iface.AttachmentID,
                includeProperties: "Vifs");
            var attachment = attachmentResult.Single();

            // Update the Port

            port.InterfaceID = currentPort.InterfaceID;
            port.TenantID = currentPort.TenantID;
            port.PortStatusID = currentPort.PortStatusID;

            // Check if the Device has changed. If so then the Interface, Attachment, and VRF (if configured)
            // must also be updated with the new Device reference

            if (port.DeviceID != currentDevice.DeviceID)
            {
                iface.DeviceID = port.DeviceID;
                UnitOfWork.InterfaceRepository.Update(iface);

                attachment.DeviceID = port.DeviceID;
                UnitOfWork.AttachmentRepository.Update(attachment);

                if (attachment.RoutingInstanceID != null)
                {
                    var vrf = await UnitOfWork.RoutingInstanceRepository.GetByIDAsync(attachment.RoutingInstanceID);
                    vrf.DeviceID = port.DeviceID;
                    UnitOfWork.RoutingInstanceRepository.Update(vrf);
                }

                // Check Attachment for VIFs and update the Device referernce of any VRF which
                // is associated with a VIF

                if (attachment.Vifs.Any())
                {
                    foreach (var vif in attachment.Vifs)
                    {
                        if (vif.RoutingInstanceID != null)
                        {
                            var vrf = await UnitOfWork.RoutingInstanceRepository.GetByIDAsync(vif.RoutingInstanceID);
                            vrf.DeviceID = port.DeviceID;
                            UnitOfWork.RoutingInstanceRepository.Update(vrf);
                        }
                    }
                }

                var newDevice = await UnitOfWork.DeviceRepository.GetByIDAsync(port.DeviceID);
                newDevice.RequiresSync = true;
                UnitOfWork.DeviceRepository.Update(newDevice);
            }

            // Clear Interface and Tenant references and change Port Status to 'Free' for the current port
            // This frees the port for re-use

            currentPort.InterfaceID = null;
            currentPort.TenantID = null;
            var portStatusResult = await UnitOfWork.PortStatusRepository.GetAsync(q => q.PortStatusType == PortStatusTypeEnum.Free);
            var portStatus = portStatusResult.SingleOrDefault();
            if (portStatus == null)
            {
                throw new ServiceException("Could not find port status with name of 'Free'");
            }

            currentPort.PortStatusID = portStatus.PortStatusID;

            // Flag Devices for sync with the network. Since the port has changed the Device
            // will need re-sync in order to remove data for the old port

            currentDevice.RequiresSync = true;

            // Save changes

            UnitOfWork.PortRepository.Update(port);
            UnitOfWork.PortRepository.Update(currentPort);
            UnitOfWork.DeviceRepository.Update(currentDevice);
            await UnitOfWork.SaveAsync();

            return result;
        }

        /// <summary>
        /// Update an Attachment
        /// </summary>
        /// <param name="attachment"></param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(Attachment attachment)
        {
            this.UnitOfWork.AttachmentRepository.Update(attachment);
            return await this.UnitOfWork.SaveAsync();
        }

        /// <summary>
        /// Update Attachments
        /// </summary>
        /// <param name="attachments"></param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(IEnumerable<Attachment> attachments)
        {
            foreach (var attachment in attachments)
            {
                this.UnitOfWork.AttachmentRepository.Update(attachment);
            }

            return await this.UnitOfWork.SaveAsync();
        }

        /// <summary>
        /// Helper to add an Attachment to the inventory
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<ServiceResult> AddAttachmentAsync(AttachmentRequest request)
        {
            var result = new ServiceResult
            {
                IsSuccess = true
            };

            using (var transaction = UnitOfWork.Database.BeginTransaction())
            {
                var attachmentFactoryResult = await AttachmentFactory.NewAsync(request);
                var attachment = (Attachment)attachmentFactoryResult.Item;
                result.Item = attachment;

                UnitOfWork.AttachmentRepository.Insert(attachment);
                await UnitOfWork.SaveAsync();

                // Update the port record - it is now assigned

                var port = request.Ports.Single();
                port.TenantID = request.TenantID;
                var portStatusDbResult = await UnitOfWork.PortStatusRepository.GetAsync(q => 
                    q.PortStatusType == PortStatusTypeEnum.Assigned);
                var portStatus = portStatusDbResult.SingleOrDefault();
                port.PortStatusID = portStatus.PortStatusID;
                port.InterfaceID = attachment.Interfaces.Single().InterfaceID;
                UnitOfWork.PortRepository.Update(port);

                await UnitOfWork.SaveAsync();

                transaction.Commit();
            }

            return result;
        }

        /// <summary>
        /// Helper to add a Bundle Attachment to the inventory
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<ServiceResult> AddBundleAttachmentAsync(AttachmentRequest request)
        {
            var result = new ServiceResult
            {
                IsSuccess = true
            };

            var attachmentFactoryResult = await AttachmentFactory.NewAsync(request);
            var attachment = (Attachment)attachmentFactoryResult.Item;
            result.Item = attachment;

            using (var transaction = UnitOfWork.Database.BeginTransaction())
            {
                UnitOfWork.AttachmentRepository.Insert(attachment);
                await UnitOfWork.SaveAsync();

                var portStatusDbResult = await UnitOfWork.PortStatusRepository.GetAsync(q => 
                    q.PortStatusType == PortStatusTypeEnum.Assigned);
                var portStatus = portStatusDbResult.SingleOrDefault();

                // Update port records - each port is now assigned

                foreach (var p in request.Ports)
                {
                    p.PortStatusID = portStatus.PortStatusID;
                    p.TenantID = request.TenantID;
                    p.InterfaceID = attachment.Interfaces.Single().InterfaceID;
                    UnitOfWork.PortRepository.Update(p);
                }

                await UnitOfWork.SaveAsync();
                transaction.Commit();
            }
     
            return result;
        }

        /// <summary>
        /// Helper to add a multiport Attachment to the inventory
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<ServiceResult> AddMultiPortAsync(AttachmentRequest request)
        {
            var result = new ServiceResult
            {
                IsSuccess = true
            };

            var attachmentFactoryResult = await AttachmentFactory.NewAsync(request);
            var attachment = (Attachment)attachmentFactoryResult.Item;
            result.Item = attachment;

            using (var transaction = UnitOfWork.Database.BeginTransaction())
            {
                UnitOfWork.AttachmentRepository.Insert(attachment);
                await UnitOfWork.SaveAsync();

                var portStatusDbResult = await UnitOfWork.PortStatusRepository.GetAsync(q => 
                    q.PortStatusType == PortStatusTypeEnum.Assigned);
                var portStatus = portStatusDbResult.SingleOrDefault();
                var ports = request.Ports.ToList();
                var interfaces = attachment.Interfaces.ToList();
                var portCount = ports.Count();

                for (var i = 0; i < portCount; i++)
                {
                    // Update port records - each port is now assigned

                    var p = ports[i];
                    p.TenantID = request.TenantID;
                    p.PortStatusID = portStatus.PortStatusID;
                    p.InterfaceID = interfaces[i].InterfaceID;
                    UnitOfWork.PortRepository.Update(p);
                }

                await UnitOfWork.SaveAsync();
                transaction.Commit();
            }

            return result;
        }
    }
}