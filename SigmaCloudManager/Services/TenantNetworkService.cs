using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public class TenantNetworkService : BaseService, ITenantNetworkService
    {
        public TenantNetworkService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        private string Properties { get; } = "Tenant,"
            + "VpnTenantNetworksIn.AttachmentSet,"
            + "VpnTenantNetworksOut.AttachmentSet";

        public async Task<IEnumerable<TenantNetwork>> GetAllAsync(string searchString = "", bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            if (string.IsNullOrEmpty(searchString))
            {
                return await this.UnitOfWork.TenantNetworkRepository.GetAsync(includeProperties: p, 
                    AsTrackable: false);
            }
            else
            {
                return await this.UnitOfWork.TenantNetworkRepository.GetAsync(q => q.CidrName.Contains(searchString), 
                    includeProperties: p, 
                    AsTrackable: false);
            }
        }

        public async Task<IEnumerable<TenantNetwork>> GetAllByTenantIDAsync(int id, string searchString = "", bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            if (string.IsNullOrEmpty(searchString))
            {
                return await this.UnitOfWork.TenantNetworkRepository.GetAsync(q => q.TenantID == id,
                     includeProperties: p,
                     AsTrackable: false);
            }
            else
            {
                return await this.UnitOfWork.TenantNetworkRepository.GetAsync(q => q.TenantID == id && q.CidrName.Contains(searchString),
                    includeProperties: p,
                    AsTrackable: false);
            }
        }

        public async Task<TenantNetwork> GetByIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await this.UnitOfWork.TenantNetworkRepository.GetAsync(q => q.TenantNetworkID == id,
                includeProperties: p,
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        /// <summary>
        /// Get a Tenant Network from its CIDR name.
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public async Task<TenantNetwork> GetByCidrNameAsync(string cidrName, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await this.UnitOfWork.TenantNetworkRepository.GetAsync(q => q.CidrName == cidrName,
                 includeProperties: p,
                 AsTrackable: false);

            return dbResult.SingleOrDefault();
        }

        public async Task<int> AddAsync(TenantNetwork tenantNetwork)
        {
            this.UnitOfWork.TenantNetworkRepository.Insert(tenantNetwork);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> UpdateAsync(TenantNetwork tenantNetwork)
        {
            this.UnitOfWork.TenantNetworkRepository.Update(tenantNetwork);

            // Update the 'RequiresSync' property of all VPNs associated with the Tenant Network

            var vpns = await UnitOfWork.VpnRepository.GetAsync(q => q.VpnAttachmentSets
                                                        .Select(x => x.AttachmentSet)
                                                        .SelectMany(x => x.VpnTenantNetworksIn)
                                                        .Select(y => y.TenantNetwork)
                                                        .Concat(q.VpnAttachmentSets
                                                        .Select(x => x.AttachmentSet)
                                                        .SelectMany(x => x.VpnTenantNetworksOut)
                                                        .Select(y => y.TenantNetwork)
                                                        .Concat(q.VpnAttachmentSets
                                                        .Select(x => x.AttachmentSet)
                                                        .SelectMany(x => x.VpnTenantNetworksRoutingInstance)
                                                        .Select(y => y.TenantNetwork)
                                                        .Concat(q.VpnAttachmentSets
                                                        .Select(x => x.AttachmentSet)
                                                        .SelectMany(x => x.VpnTenantNetworkStaticRoutesRoutingInstance)
                                                        .Select(y => y.TenantNetwork))))
                                                        .Where(x => x.TenantNetworkID == tenantNetwork.TenantNetworkID)
                                                        .Any());

            foreach (var vpn in vpns.GroupBy(x => x.VpnID).Select(group => group.First()))
            {
                vpn.RequiresSync = true;
                vpn.ShowRequiresSyncAlert = true;
                this.UnitOfWork.VpnRepository.Update(vpn);
            }

            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(TenantNetwork tenantNetwork)
        {
            this.UnitOfWork.TenantNetworkRepository.Delete(tenantNetwork);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}
