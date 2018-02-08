using System;
using System.Collections.Generic;
using System.Linq;
using SCM.Data;
using SCM.Models;
using System.Threading.Tasks;

namespace SCM.Services
{
    public class VpnAttachmentSetService : BaseService, IVpnAttachmentSetService
    {
        public VpnAttachmentSetService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        private string Properties { get; } = "AttachmentSet.Tenant,"
               + "Vpn.VpnTopologyType,"
               + "Vpn.VpnTenancyType,"
               + "Vpn.MulticastVpnServiceType,"
               + "Vpn.MulticastVpnDirectionType,"
               + "AttachmentSet.MulticastVpnDomainType,"
               + "AttachmentSet.AttachmentRedundancy,"
               + "AttachmentSet.AttachmentSetRoutingInstances.RoutingInstance.Tenant,"
               + "AttachmentSet.AttachmentSetRoutingInstances.RoutingInstance.Vifs.Attachment.Interfaces.Ports,"
               + "AttachmentSet.AttachmentSetRoutingInstances.RoutingInstance.Attachments.Interfaces.Ports";

        /// <summary>
        /// Return all VPN Attachment Sets
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<VpnAttachmentSet>> GetAllAsync(bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.VpnAttachmentSetRepository.GetAsync(AsTrackable: false,
                includeProperties: p);
        }

        /// <summary>
        /// Return a VPN Attachment Set
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<VpnAttachmentSet> GetByIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await UnitOfWork.VpnAttachmentSetRepository.GetAsync(q => q.VpnAttachmentSetID == id, 
                includeProperties: p,
                AsTrackable: false);

            return dbResult.SingleOrDefault();
        }

        /// <summary>
        /// Return a VPN Attachment Set from a given VPN and Attachment Set.
        /// </summary>
        /// <param name="vpnID"></param>
        /// <param name="attachmentSetID"></param>
        /// <returns></returns>
        public async Task<VpnAttachmentSet> GetByVpnAndAttachmentSetAsync(int vpnID, int attachmentSetID, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await UnitOfWork.VpnAttachmentSetRepository.GetAsync(q => q.VpnID == vpnID && q.AttachmentSetID == attachmentSetID,
                includeProperties: p,
                AsTrackable: false);

            return dbResult.SingleOrDefault();
        }

        /// <summary>
        /// Return all VPN Attachment Sets for a given Attachment Set.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnAttachmentSet>> GetAllByAttachmentSetIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await UnitOfWork.VpnAttachmentSetRepository.GetAsync(q => q.AttachmentSetID == id,
                includeProperties: p,
                AsTrackable: false);
        }

        /// <summary>
        /// Return all VPN Attachment Sets for a given VPN.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnAttachmentSet>> GetAllByVpnIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await UnitOfWork.VpnAttachmentSetRepository.GetAsync(q => q.VpnID == id,
                includeProperties: p,
                AsTrackable: false);
        }

        /// <summary>
        /// Add a VPN Attachment Set.
        /// </summary>
        /// <param name="vpnAttachmentSet"></param>
        /// <returns></returns>
        public async Task<int> AddAsync(VpnAttachmentSet vpnAttachmentSet)
        {
            this.UnitOfWork.VpnAttachmentSetRepository.Insert(vpnAttachmentSet);
            await UpdateVpnSyncStateAsync(vpnAttachmentSet.VpnID);
            return await this.UnitOfWork.SaveAsync();
        }

        /// <summary>
        /// Update a VPN Attachment Set.
        /// </summary>
        /// <param name="vpnAttachmentSet"></param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(VpnAttachmentSet vpnAttachmentSet)
        {
            this.UnitOfWork.VpnAttachmentSetRepository.Update(vpnAttachmentSet);
            await UpdateVpnSyncStateAsync(vpnAttachmentSet.VpnID);
            return await this.UnitOfWork.SaveAsync();
        }

        /// <summary>
        /// Delete a VPN Attachment Set.
        /// </summary>
        /// <param name="vpnAttachmentSet"></param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(VpnAttachmentSet vpnAttachmentSet)
        {
            this.UnitOfWork.VpnAttachmentSetRepository.Delete(vpnAttachmentSet);
            await UpdateVpnSyncStateAsync(vpnAttachmentSet.VpnID);
            return await this.UnitOfWork.SaveAsync();
        }

        /// <summary>
        /// Update the RequiresSync state of a Vpn.
        /// </summary>
        /// <param name="vpn"></param>
        /// <returns></returns>
        private async Task UpdateVpnSyncStateAsync(int vpnID)
        {
            var vpn = await UnitOfWork.VpnRepository.GetByIDAsync(vpnID);
            vpn.RequiresSync = true;
            UnitOfWork.VpnRepository.Update(vpn);
        }
    }
}