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
        public VpnTenantMulticastGroupService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        private string Properties { get; } = "TenantMulticastGroup.Tenant,"
                                            + "AttachmentSet.Tenant,"
                                            + "AttachmentSet.VpnAttachmentSets.Vpn.MulticastVpnServiceType,"
                                            + "MulticastVpnRp,"
                                            + "MulticastGeographicalScope";

        public async Task<IEnumerable<VpnTenantMulticastGroup>> GetAllByAttachmentSetIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.VpnTenantMulticastGroupRepository.GetAsync(q => q.AttachmentSetID == id,
                includeProperties: p,
                AsTrackable: false);
        }

        public async Task<IEnumerable<VpnTenantMulticastGroup>> GetAllByVpnIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
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
            var p = includeProperties ? Properties : string.Empty;
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
            var p = includeProperties ? Properties : string.Empty;
            return await UnitOfWork.VpnTenantMulticastGroupRepository.GetAsync(q => q.TenantMulticastGroupID == id,
                includeProperties: p,
                AsTrackable: false);
        }

        public async Task<VpnTenantMulticastGroup> GetByIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await this.UnitOfWork.VpnTenantMulticastGroupRepository.GetAsync(q => q.VpnTenantMulticastGroupID == id,
                includeProperties: p,
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<VpnTenantMulticastGroup> GetByGroupAddressAsync(string groupAddress, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await this.UnitOfWork.VpnTenantMulticastGroupRepository.GetAsync(q => q.TenantMulticastGroup.GroupAddress == groupAddress,
                includeProperties: p,
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<int> AddAsync(VpnTenantMulticastGroup vpnTenantMulticastGroup)
        {
            this.UnitOfWork.VpnTenantMulticastGroupRepository.Insert(vpnTenantMulticastGroup);

            // Multicast Groups have changed - must flag VPNs as needing sync with network

            await UpdateVpnSyncStateAsync(vpnTenantMulticastGroup.AttachmentSetID);
            return await this.UnitOfWork.SaveAsync();
        }
 
        public async Task<int> UpdateAsync(VpnTenantMulticastGroup vpnTenantMulticastGroup)
        {
            this.UnitOfWork.VpnTenantMulticastGroupRepository.Update(vpnTenantMulticastGroup);

            // Multicast Groups have changed - must flag VPNs as needing sync with network

            await UpdateVpnSyncStateAsync(vpnTenantMulticastGroup.AttachmentSetID);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(VpnTenantMulticastGroup vpnTenantMulticastGroup)
        {
            this.UnitOfWork.VpnTenantMulticastGroupRepository.Delete(vpnTenantMulticastGroup);

            // Multicast Groups have changed - must flag VPNs as needing sync with network

            await UpdateVpnSyncStateAsync(vpnTenantMulticastGroup.AttachmentSetID);
            return await this.UnitOfWork.SaveAsync();
        }

        /// <summary>
        /// Update the RequiresSync state of Vpns.
        /// </summary>
        /// <returns></returns>
        private async Task UpdateVpnSyncStateAsync(int attachmentSetID)
        {
            var vpns = await UnitOfWork.VpnRepository.GetAsync(q => q.VpnAttachmentSets
            .Where(x => x.AttachmentSetID == attachmentSetID).Any());

            foreach (var vpn in vpns)
            {
                vpn.RequiresSync = true;
                UnitOfWork.VpnRepository.Update(vpn);
            }
        }
    }
}
