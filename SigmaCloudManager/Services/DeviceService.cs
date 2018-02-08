using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Net;
using AutoMapper;
using SCM.Models;
using SCM.Data;
using SCM.Models.NetModels.AttachmentNetModels;
using SCM.Models.SerializableModels.SerializableAttachmentModels;
using SCM.Factories;

namespace SCM.Services
{
    public class DeviceService : BaseService, IDeviceService
    {
        public DeviceService(IUnitOfWork unitOfWork, 
            IDeviceFactory deviceFactory,
            IMapper mapper, 
            INetworkSyncService netsync) : base(unitOfWork, mapper, netsync)
        {
            DeviceFactory = deviceFactory;
        }
        
        private IDeviceFactory DeviceFactory { get; }

        private string Properties { get; } = "RoutingInstances.RoutingInstanceType,"
                               + "Ports.PortBandwidth,"
                               + "Ports.PortStatus,"
                               + "Interfaces.Ports,"
                               + "Attachments.Interfaces.Ports.Interface.Vlans,"
                               + "Attachments.AttachmentBandwidth,"
                               + "Attachments.RoutingInstance.BgpPeers,"
                               + "Attachments.RoutingInstance.Tenant,"
                               + "Attachments.ContractBandwidthPool.ContractBandwidth,"
                               + "Attachments.ContractBandwidthPool.Tenant,"
                               + "Attachments.Vifs.Vlans,"
                               + "Attachments.Vifs.RoutingInstance.BgpPeers,"
                               + "Attachments.Vifs.RoutingInstance.Tenant,"
                               + "Attachments.Vifs.ContractBandwidthPool.ContractBandwidth,"
                               + "Attachments.Vifs.ContractBandwidthPool.Tenant,"
                               + "Attachments.Vifs.VifRole,"
                               + "Attachments.Mtu,"
                               + "Attachments.AttachmentRole.PortPool.PortRole,"
                               + "Plane,"
                               + "Location.SubRegion.Region,"
                               + "DeviceRole,"
                               + "DeviceStatus,"
                               + "DeviceModel,"
                               + "Tenant";

        public async Task<IEnumerable<Device>> GetAllAsync(bool? isProviderDomainRole = null, bool? isTenantDomainRole = null,
            bool? requiresSync = null, bool? created = null, bool? showRequiresSyncAlert = null, bool? showCreatedAlert = null,
            string searchString = "", bool includeProperties = true)
        {
            var p = includeProperties ? Properties : "DeviceRole";

            var query = from devices in await this.UnitOfWork.DeviceRepository.GetAsync(includeProperties: p,
                                   AsTrackable: false)
                        select devices;

            if (isProviderDomainRole != null)
            {
                query = query.Where(x => x.DeviceRole.IsProviderDomainRole == isProviderDomainRole);
            }

            if (isTenantDomainRole != null)
            {
                query = query.Where(x => x.DeviceRole.IsTenantDomainRole == isTenantDomainRole);
            }

            if (requiresSync != null)
            {
                query = query.Where(x => x.RequiresSync);
            }

            if (created != null)
            {
                query = query.Where(x => x.Created);
            }

            if (showRequiresSyncAlert != null)
            {
                query = query.Where(x => x.ShowRequiresSyncAlert);
            }

            if (showCreatedAlert != null)
            {
                query = query.Where(x => x.ShowCreatedAlert);
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(x => x.Name.Contains(searchString));
            }

            return query.ToList();
        }
      
        public async Task<IEnumerable<Device>> GetAllByLocationIDAsync(int locationID, int? planeID = null, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var query = from devices in await this.UnitOfWork.DeviceRepository.GetAsync(q => q.LocationID == locationID, includeProperties: p,
                               AsTrackable: false)
                        select devices;

            if (planeID != null) {

                query = query.Where(x => x.PlaneID == planeID);
            }

            return query.ToList();
        }

        public async Task<IEnumerable<Device>> GetAllByTenantIDAsync(int tenantID, string searchString = "", bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var query = from devices in await this.UnitOfWork.DeviceRepository.GetAsync(q => q.TenantID == tenantID, includeProperties: p,
                              AsTrackable: false)
                              select devices;

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(x => x.Name.Contains(searchString));
            }

            return query.ToList();
        }

        public async Task<Device> GetByIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var result = await this.UnitOfWork.DeviceRepository.GetAsync(d => d.DeviceID == id, 
                includeProperties: p,
                AsTrackable: false);

            return result.SingleOrDefault();
        }

        public async Task<Device> GetByNameAsync(string name, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var result = await this.UnitOfWork.DeviceRepository.GetAsync(d => d.Name == name, 
                includeProperties: p,
                AsTrackable: false);

            return result.SingleOrDefault();
        }

        /// <summary>
        /// Add a Device
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public async Task<ServiceResult> AddAsync(Device device)
        {
            var result = new ServiceResult
            {
                IsSuccess = true,
                Item = device
            };

            var deviceFactoryResult = await DeviceFactory.NewAsync(device);
            device = (Device)deviceFactoryResult.Item;

            this.UnitOfWork.DeviceRepository.Insert(device);
            await this.UnitOfWork.SaveAsync();
            return result;
        }

        /// <summary>
        /// Update a Device
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(Device device)
        {
            this.UnitOfWork.DeviceRepository.Update(device);
            return await this.UnitOfWork.SaveAsync();
        }

        /// <summary>
        /// Update a collection of Devices
        /// </summary>
        /// <param name="devices"></param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(IEnumerable<Device> devices)
        {
            foreach (var device in devices)
            {
                this.UnitOfWork.DeviceRepository.Update(device);
            }

            return await this.UnitOfWork.SaveAsync();
        }

        /// <summary>
        /// Delete a Device
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public async Task<ServiceResult> DeleteAsync(Device device)
        {
            var result = new ServiceResult
            {
                IsSuccess = true
            };
       
            this.UnitOfWork.DeviceRepository.Delete(device);
            await this.UnitOfWork.SaveAsync();

            return result;
        }

        /// <summary>
        /// Check a collection of Devices for sync with the network.
        /// </summary>
        /// <param name="devices"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ServiceResult>> CheckNetworkSyncAsync(IEnumerable<Device> devices,
            IProgress<ServiceResult> progress)
        {
            List<Task<ServiceResult>> tasks = (from device in devices select CheckNetworkSyncHelperAsync(device)).ToList();
            var results = new List<ServiceResult>();

            while (tasks.Count() > 0)
            {
                Task<ServiceResult> task = await Task.WhenAny(tasks);
                results.Add(task.Result);
                tasks.Remove(task);

                // Update caller with progress

                progress.Report(task.Result);
            }

            await Task.WhenAll(tasks);

            // Update each device in the DB
            foreach (var device in devices)
            {
                UnitOfWork.DeviceRepository.Update(device);
            }

            await UnitOfWork.SaveAsync();
            return results;
        }

        /// <summary>
        /// Check a Device for sync with the network.
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public async Task<ServiceResult> CheckNetworkSyncAsync(Device device)
        {
            var result = await CheckNetworkSyncHelperAsync(device);
            UnitOfWork.DeviceRepository.Update(device);
            await UnitOfWork.SaveAsync();
            return result;
        }

        /// <summary>
        /// Sync a collection of Devices with the network.
        /// </summary>
        /// <param name="devices"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ServiceResult>> SyncToNetworkAsync(IEnumerable<Device> devices,
           IProgress<ServiceResult> progress)
        {
            List<Task<ServiceResult>> tasks = (from device in devices select SyncToNetworkAsync(device)).ToList();
            var results = new List<ServiceResult>();

            while (tasks.Count() > 0)
            {
                Task<ServiceResult> task = await Task.WhenAny(tasks);
                results.Add(task.Result);
                tasks.Remove(task);

                // Update caller with progress

                progress.Report(task.Result);
            }

            await Task.WhenAll(tasks);

            return results;
        }

        /// <summary>
        /// Sync a Device to the network.
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public async Task<ServiceResult> SyncToNetworkAsync(Device device)
        {
            var result = new ServiceResult
            {
                IsSuccess = true,
                Item = device
            };

            var attachmentServiceModelData = Mapper.Map<AttachmentServiceNetModel>(device);
            var serializableAttachmentServiceModel = Mapper.Map<SerializableAttachmentService>(attachmentServiceModelData);
            await NetSync.SyncNetworkAsync(serializableAttachmentServiceModel, "/attachment/pe/" + device.Name);

            // Attachment and VIFs of the Device no longer 
            // requires sync to the network

            foreach (var attachment in device.Attachments)
            {
                attachment.RequiresSync = false;
                attachment.ShowRequiresSyncAlert = false;
                this.UnitOfWork.AttachmentRepository.Update(attachment);

                foreach (var vif in attachment.Vifs)
                {

                    vif.RequiresSync = false;
                    vif.ShowRequiresSyncAlert = false;
                    this.UnitOfWork.VifRepository.Update(vif);
                }           
            }

            // The Device no longer requires sync to the network

            device.RequiresSync = false;
            device.ShowRequiresSyncAlert = false;

            // The Device is now operational on the network

            device.Created = false;
            device.ShowCreatedAlert = false;

            UnitOfWork.DeviceRepository.Update(device);
            await UnitOfWork.SaveAsync();

            return result;
        }

        /// <summary>
        /// Delete the configuration of a Device from the network.
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public async Task<ServiceResult> DeleteFromNetworkAsync(Device device)
        {
            var result = new ServiceResult
            {
                IsSuccess = true
            };

            await NetSync.DeleteFromNetworkAsync("/attachment/pe/" + device.Name);

            // All Attachments and VIFs associated with the Device now require re-sync
            // with the network

            foreach (var attachment in device.Attachments)
            {
                attachment.RequiresSync = attachment.AttachmentRole.RequireSyncToNetwork;
                attachment.ShowRequiresSyncAlert = attachment.AttachmentRole.RequireSyncToNetwork;
                UnitOfWork.AttachmentRepository.Update(attachment);

                foreach (var vif in attachment.Vifs)
                {
                    vif.RequiresSync = vif.VifRole.RequireSyncToNetwork;
                    vif.ShowRequiresSyncAlert = vif.VifRole.RequireSyncToNetwork;
                    UnitOfWork.VifRepository.Update(vif);
                }
            }

            // Device requires re-sync with the network

            device.RequiresSync = true;
            UnitOfWork.DeviceRepository.Update(device);

            await UnitOfWork.SaveAsync();

            return result;
        }

        /// <summary>
        /// Helper to check a Device for sync with the network
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        private async Task<ServiceResult> CheckNetworkSyncHelperAsync(Device device)
        {
            var result = new ServiceResult
            {
                Item = device
            };

            var attachmentServiceModelData = Mapper.Map<AttachmentServiceNetModel>(device);
            var serializableAttachmentServiceModel = Mapper.Map<SerializableAttachmentService>(attachmentServiceModelData);
            var syncResult = await NetSync.CheckNetworkSyncAsync(serializableAttachmentServiceModel, "/attachment/pe/" + device.Name);
            result.IsSuccess = syncResult.IsSuccess;
            device.RequiresSync = !syncResult.IsSuccess;
            device.ShowRequiresSyncAlert = !syncResult.IsSuccess;
            if (!syncResult.IsSuccess)
            {
                result.Add($"'{device.Name}' is not in-sync with the network.");
            }

            return result;
        }
    }
}
