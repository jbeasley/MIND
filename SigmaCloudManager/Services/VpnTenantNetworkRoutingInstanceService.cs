using System;
using System.Collections.Generic;
using System.Linq;
using SCM.Data;
using SCM.Models;
using System.Threading.Tasks;

namespace SCM.Services
{
    public class VpnTenantIpNetworkRoutingInstanceService : BaseService, IVpnTenantIpNetworkRoutingInstanceService
    {
        private readonly string _properties = "AttachmentSet,"
        + "TenantIpNetwork,"
        + "RoutingInstance";

        public VpnTenantIpNetworkRoutingInstanceService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// Get all tenant IP networks for a given attachment set.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantIpNetworkRoutingInstance>> GetAllByAttachmentSetIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return await this.UnitOfWork.VpnTenantIpNetworkRoutingInstanceRepository.GetAsync(q => q.AttachmentSetID == id, 
                includeProperties: deep.HasValue && deep.Value ? _properties : "TenantIpNetwork",
                AsTrackable: asTrackable);
        }

        /// <summary>
        /// Get all VPN Tenant Networks for a given VPN.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantIpNetworkRoutingInstance>> GetAllByVpnIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return await UnitOfWork.VpnTenantIpNetworkRoutingInstanceRepository.GetAsync(q => q.AttachmentSet.VpnAttachmentSets
                        .Where(x => x.VpnID == id).Any(),
                        includeProperties: deep.HasValue && deep.Value ? _properties : "TenantIpNetwork",
                        AsTrackable: asTrackable);
        }

        /// <summary>
        /// Get a single VPN Tenant Network.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<VpnTenantIpNetworkRoutingInstance> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return (from result in await UnitOfWork.VpnTenantIpNetworkRoutingInstanceRepository.GetAsync(q =>
                    q.VpnTenantIpNetworkRoutingInstanceID == id,
                    includeProperties: deep.HasValue && deep.Value ? _properties : "TenantIpNetwork",
                    AsTrackable: asTrackable)
                    select result)
                    .SingleOrDefault();
        }

        public async Task<VpnTenantIpNetworkRoutingInstance> AddAsync(VpnTenantIpNetworkRoutingInstance vpnTenantIpNetworkRoutingInstance)
        {
            this.UnitOfWork.VpnTenantIpNetworkRoutingInstanceRepository.Insert(vpnTenantIpNetworkRoutingInstance);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(vpnTenantIpNetworkRoutingInstance.VpnTenantIpNetworkRoutingInstanceID, deep: true, asTrackable: false);
        }

        public async Task<VpnTenantIpNetworkRoutingInstance> UpdateAsync(VpnTenantIpNetworkRoutingInstance vpnTenantIpNetworkRoutingInstance)
        {
            this.UnitOfWork.VpnTenantIpNetworkRoutingInstanceRepository.Update(vpnTenantIpNetworkRoutingInstance);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(vpnTenantIpNetworkRoutingInstance.VpnTenantIpNetworkRoutingInstanceID, deep: true, asTrackable: false);
        }

        public async Task DeleteAsync(int vpnTenantIpNetworkRoutingInstanceId)
        {
            await this.UnitOfWork.VpnTenantIpNetworkRoutingInstanceRepository.DeleteAsync(vpnTenantIpNetworkRoutingInstanceId);
            await this.UnitOfWork.SaveAsync();
        }
    }
}