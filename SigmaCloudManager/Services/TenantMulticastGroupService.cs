using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public class TenantMulticastGroupService : BaseService, ITenantMulticastGroupService
    {
        public TenantMulticastGroupService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        private string Properties { get; } = "Tenant";

        public async Task<IEnumerable<TenantMulticastGroup>> GetAllAsync(string searchString = "", bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            if (string.IsNullOrEmpty(searchString))
            {
                return await this.UnitOfWork.TenantMulticastGroupRepository.GetAsync(includeProperties: p, 
                    AsTrackable: false);
            }
            else
            {
                return await this.UnitOfWork.TenantMulticastGroupRepository.GetAsync(q => q.Name.Contains(searchString), 
                    includeProperties: p, 
                    AsTrackable: false);
            }
        }

        public async Task<IEnumerable<TenantMulticastGroup>> GetAllByTenantIDAsync(int id, string searchString = "", bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            if (string.IsNullOrEmpty(searchString))
            {
                return await this.UnitOfWork.TenantMulticastGroupRepository.GetAsync(q => q.TenantID == id,
                    includeProperties: p,
                    AsTrackable: false);
            }
            else
            {
                return await this.UnitOfWork.TenantMulticastGroupRepository.GetAsync(q => q.TenantID == id && q.Name.Contains(searchString),
                    includeProperties: p,
                    AsTrackable: false);
            }
        }

        public async Task<TenantMulticastGroup> GetByIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await this.UnitOfWork.TenantMulticastGroupRepository.GetAsync(q => q.TenantMulticastGroupID == id,
                includeProperties: p,
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<TenantMulticastGroup> GetByGroupAddressAsync(string groupAddress, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await this.UnitOfWork.TenantMulticastGroupRepository.GetAsync(q => q.GroupAddress == groupAddress,
                includeProperties: p,
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<int> AddAsync(TenantMulticastGroup vpnTenantMulticastGroup)
        {
            this.UnitOfWork.TenantMulticastGroupRepository.Insert(vpnTenantMulticastGroup);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> UpdateAsync(TenantMulticastGroup vpnTenantMulticastGroup)
        {
            this.UnitOfWork.TenantMulticastGroupRepository.Update(vpnTenantMulticastGroup);

            // Update the 'RequiresSync' property of all VPNs associated with the Tenant Multicast Group

            var vpns = await UnitOfWork.VpnRepository.GetAsync(q => q.VpnAttachmentSets
                                                                    .Select(x => x.AttachmentSet)
                                                                    .SelectMany(x => x.MulticastVpnRps)
                                                                    .SelectMany(x => x.VpnTenantMulticastGroups)
                                                                    .Where(x => x.TenantMulticastGroupID == vpnTenantMulticastGroup.TenantMulticastGroupID)
                                                                    .Any());

            foreach (var vpn in vpns)
            {
                vpn.RequiresSync = true;
                vpn.ShowRequiresSyncAlert = true;
                this.UnitOfWork.VpnRepository.Update(vpn);
            }

            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(TenantMulticastGroup vpnTenantMulticastGroup)
        {
            this.UnitOfWork.TenantMulticastGroupRepository.Delete(vpnTenantMulticastGroup);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}
