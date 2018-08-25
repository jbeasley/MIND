using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public class VpnTenantMulticastGroupService : BaseService, IVpnTenantMulticastGroupService
    {
        private readonly string _properties = "TenantMulticastGroup,"
                                    + "AttachmentSet,"
                                    + "AttachmentSet.VpnAttachmentSets.Vpn.MulticastVpnServiceType,"
                                    + "MulticastVpnRp,"
                                    + "MulticastGeographicalScope";

        public VpnTenantMulticastGroupService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<VpnTenantMulticastGroup>> GetAllByAttachmentSetIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? _properties : string.Empty;
            return await this.UnitOfWork.VpnTenantMulticastGroupRepository.GetAsync(q => q.AttachmentSetID == id,
                includeProperties: p,
                AsTrackable: false);
        }

        public async Task<IEnumerable<VpnTenantMulticastGroup>> GetAllByVpnIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? _properties : string.Empty;
            return await this.UnitOfWork.VpnTenantMulticastGroupRepository.GetAsync(q => q.AttachmentSet.VpnAttachmentSets
            .Where(x => x.VpnID == id).Any(), 
                includeProperties: p, 
                AsTrackable: false);
        }

        /// <summary>
        /// Get all VPN Tenant Multicast Groups for a given Multicast VPN Rendezvous-Point
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantMulticastGroup>> GetAllByMulticastVpnRpIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? _properties : string.Empty;
            return await this.UnitOfWork.VpnTenantMulticastGroupRepository.GetAsync(q => q.MulticastVpnRpID == id,
                includeProperties: p,
                AsTrackable: false);
        }

        /// <summary>
        /// Get all VPN Tenant Multicast Groups for a given Tenant Multicast Group.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantMulticastGroup>> GetAllByTenantMulticastGroupIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? _properties : string.Empty;
            return await UnitOfWork.VpnTenantMulticastGroupRepository.GetAsync(q => q.TenantMulticastGroupID == id,
                includeProperties: p,
                AsTrackable: false);
        }

        public async Task<VpnTenantMulticastGroup> GetByIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? _properties : string.Empty;
            var dbResult = await this.UnitOfWork.VpnTenantMulticastGroupRepository.GetAsync(q => q.VpnTenantMulticastGroupID == id,
                includeProperties: p,
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<VpnTenantMulticastGroup> GetByGroupAddressAsync(string groupAddress, bool includeProperties = true)
        {
            var p = includeProperties ? _properties : string.Empty;
            var dbResult = await this.UnitOfWork.VpnTenantMulticastGroupRepository.GetAsync(q => q.TenantMulticastGroup.GroupAddress == groupAddress,
                includeProperties: p,
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<int> AddAsync(VpnTenantMulticastGroup vpnTenantMulticastGroup)
        {
            this.UnitOfWork.VpnTenantMulticastGroupRepository.Insert(vpnTenantMulticastGroup);
            return await this.UnitOfWork.SaveAsync();
        }
 
        public async Task<int> UpdateAsync(VpnTenantMulticastGroup vpnTenantMulticastGroup)
        {
            this.UnitOfWork.VpnTenantMulticastGroupRepository.Update(vpnTenantMulticastGroup);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(VpnTenantMulticastGroup vpnTenantMulticastGroup)
        {
            this.UnitOfWork.VpnTenantMulticastGroupRepository.Delete(vpnTenantMulticastGroup);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}
