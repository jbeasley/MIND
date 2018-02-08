using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public class MulticastVpnRpService : BaseService, IMulticastVpnRpService
    {
        public MulticastVpnRpService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        private string Properties { get; } = "AttachmentSet.Tenant";

        public async Task<IEnumerable<MulticastVpnRp>> GetAllByVpnIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.MulticastVpnRpRepository.GetAsync(q => q.AttachmentSet.VpnAttachmentSets.Where(x => x.VpnID == id).Any(), 
                includeProperties: p, 
                AsTrackable: false);
        }

        public async Task<IEnumerable<MulticastVpnRp>> GetAllByAttachmentSetIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.MulticastVpnRpRepository.GetAsync(q => q.AttachmentSetID == id,
                includeProperties: p,
                AsTrackable: false);
        }

        public async Task<MulticastVpnRp> GetByIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await this.UnitOfWork.MulticastVpnRpRepository.GetAsync(q => q.MulticastVpnRpID == id,
                includeProperties: p,
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<MulticastVpnRp> GetByIpAddressAsync(string ipAddress, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await this.UnitOfWork.MulticastVpnRpRepository.GetAsync(q => q.IpAddress == ipAddress,
                includeProperties: p,
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<int> AddAsync(MulticastVpnRp multicastVpnRp)
        {
            this.UnitOfWork.MulticastVpnRpRepository.Insert(multicastVpnRp);

            // RP has been added - must flag any VPNs as needing sync with network

            await UpdateVpnSyncStateAsync(multicastVpnRp.AttachmentSetID);
            return await this.UnitOfWork.SaveAsync();
        }
 
        public async Task<int> UpdateAsync(MulticastVpnRp multicastVpnRp)
        {
            this.UnitOfWork.MulticastVpnRpRepository.Update(multicastVpnRp);
            
            // RP has changed - must flag any VPNs as needing sync with network

            await UpdateVpnSyncStateAsync(multicastVpnRp.AttachmentSetID);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(MulticastVpnRp multicastVpnRp)
        {
            this.UnitOfWork.MulticastVpnRpRepository.Delete(multicastVpnRp);

            // RP has been deleted - must flag any VPNs needing sync with network

            await UpdateVpnSyncStateAsync(multicastVpnRp.AttachmentSetID);
            return await this.UnitOfWork.SaveAsync();
        }

        /// <summary>
        /// Update the RequiresSync state of VPNs.
        /// </summary>
        /// <returns></returns>
        private async Task UpdateVpnSyncStateAsync(int attachmentSetID)
        {
            var vpns = await UnitOfWork.VpnRepository.GetAsync(q => q.VpnAttachmentSets
                .Where(x => x.AttachmentSetID == attachmentSetID)
                .Any());

            foreach (var vpn in vpns)
            {
                vpn.RequiresSync = true;
                UnitOfWork.VpnRepository.Update(vpn);
            }
        }
    }
}
