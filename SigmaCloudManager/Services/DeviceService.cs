using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Net;
using AutoMapper;
using SCM.Models;
using SCM.Data;
using SCM.Factories;

namespace SCM.Services
{
    public class DeviceService : BaseService, IDeviceService
    {
        public DeviceService(IUnitOfWork unitOfWork, 
            IDeviceFactory deviceFactory,
            IMapper mapper) : base(unitOfWork, mapper)
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
            string searchString = "", bool deep = false, bool asTrackable = false)
        {
            var p = deep ? Properties : "DeviceRole";

            var query = from devices in await this.UnitOfWork.DeviceRepository.GetAsync(includeProperties: p,
                                   AsTrackable: asTrackable)
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
      
        public async Task<IEnumerable<Device>> GetAllByLocationIDAsync(int locationID, int? planeID = null, bool deep = false, bool asTrackable = false)
        {
            var p = deep ? Properties : string.Empty;
            var query = from devices in await this.UnitOfWork.DeviceRepository.GetAsync(q => q.LocationID == locationID, includeProperties: p,
                               AsTrackable: asTrackable)
                        select devices;

            if (planeID != null) {

                query = query.Where(x => x.PlaneID == planeID);
            }

            return query.ToList();
        }

        public async Task<IEnumerable<Device>> GetAllByTenantIDAsync(int tenantID, string searchString = "", bool deep = false, bool asTrackable = false)
        {
            var p = deep ? Properties : string.Empty;
            var query = from devices in await this.UnitOfWork.DeviceRepository.GetAsync(q => q.TenantID == tenantID, includeProperties: p,
                              AsTrackable: asTrackable)
                              select devices;

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(x => x.Name.Contains(searchString));
            }

            return query.ToList();
        }

        public async Task<Device> GetByIDAsync(int id, bool deep = false, bool asTrackable = false)
        {
            var p = deep ? Properties : string.Empty;
            var result = await this.UnitOfWork.DeviceRepository.GetAsync(d => d.DeviceID == id, 
                includeProperties: p,
                AsTrackable: asTrackable);

            return result.SingleOrDefault();
        }

        public async Task<Device> GetByNameAsync(string name, bool deep = false, bool asTrackable = false)
        {
            var p = deep ? Properties : string.Empty;
            var result = await this.UnitOfWork.DeviceRepository.GetAsync(d => d.Name == name, 
                includeProperties: p,
                AsTrackable: asTrackable);

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
    }
}
