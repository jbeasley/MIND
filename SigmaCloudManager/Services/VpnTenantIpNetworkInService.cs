using System;
using System.Collections.Generic;
using System.Linq;
using SCM.Data;
using SCM.Models;
using System.Threading.Tasks;
using Mind.Models.RequestModels;
using Mind.Builders;
using Microsoft.EntityFrameworkCore;

namespace SCM.Services
{
    public class VpnTenantIpNetworkInService : BaseService, IVpnTenantIpNetworkInService
    {
        private readonly IVpnTenantIpNetworkInDirector _director;
        private readonly IVpnTenantIpNetworkInUpdateDirector _updateDirector;

        public VpnTenantIpNetworkInService(IUnitOfWork unitOfWork, IVpnTenantIpNetworkInDirector director, 
            IVpnTenantIpNetworkInUpdateDirector updateDirector) : base(unitOfWork)
        {
            _director = director;
            _updateDirector = updateDirector;
        }

        /// <summary>
        /// Get all VPN Tenant IP Networks for a given Attachment Set.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="asTrackable"></param>
        /// <param name="deep"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantIpNetworkIn>> GetAllByAttachmentSetIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return await this.UnitOfWork.VpnTenantIpNetworkInRepository.GetAsync(
                    q => 
                        q.AttachmentSetID == id,
                        query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q.Include(x => x.TenantIpNetwork),
                        AsTrackable: asTrackable);
        }

        /// <summary>
        /// Get all VPN tenant IP networks for a given tenant which are associated with a given VPN 
        /// </summary>
        /// <param name="vpnId"></param>
        /// <param name="asTrackable"></param>
        /// <param name="deep"></param>
        /// <param name="extranet"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantIpNetworkIn>> GetAllByVpnIDAsync(int vpnId, int? tenantId = null, bool extranet = false, bool? deep = false, bool asTrackable = false)
        {
            var query = (from result in await UnitOfWork.VpnTenantIpNetworkInRepository.GetAsync(
                  q =>
                        q.AttachmentSet.VpnAttachmentSets.Select(x =>
                        x.VpnID == vpnId).Any(),
                        query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q.Include(x => x.TenantIpNetwork),
                        AsTrackable: asTrackable)
                         select result);

            if (tenantId != null) query = query.Where(q => q.TenantIpNetwork.TenantID == tenantId);
            return query.ToList();
        }

        /// <summary>
        /// Get a single VPN Tenant IP Network.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deep"></param>
        /// <param name="asTrackable"></param>
        /// <returns></returns>
        public async Task<VpnTenantIpNetworkIn> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return (from result in await UnitOfWork.VpnTenantIpNetworkInRepository.GetAsync(
                q => 
                    q.VpnTenantIpNetworkInID == id,
                    query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q.Include(x => x.TenantIpNetwork),
                    AsTrackable: asTrackable)
                    select result)
                    .SingleOrDefault();
        }

        /// <summary>
        /// TO-BE-REMOVED
        /// </summary>
        /// <param name="vpnTenantNetworkIn"></param>
        /// <returns></returns>
        public async Task<VpnTenantIpNetworkIn> AddAsync(VpnTenantIpNetworkIn vpnTenantNetworkIn)
        {
            this.UnitOfWork.VpnTenantIpNetworkInRepository.Insert(vpnTenantNetworkIn);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(vpnTenantNetworkIn.VpnTenantIpNetworkInID, deep: true, asTrackable: false);
        }

        public async Task<VpnTenantIpNetworkIn> AddAsync(int attachmentSetId, VpnTenantIpNetworkInRequest request)
        {
            var vpnTenantIpNetworkIn = await _director.BuildAsync(attachmentSetId, request);  
            this.UnitOfWork.VpnTenantIpNetworkInRepository.Insert(vpnTenantIpNetworkIn);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(vpnTenantIpNetworkIn.VpnTenantIpNetworkInID, deep: true, asTrackable: false);
        }

        /// <summary>
        /// TO-BE-REMOVED
        /// </summary>
        /// <param name="vpnTenantIpNetworkIn"></param>
        /// <returns></returns>
        public async Task<VpnTenantIpNetworkIn> UpdateAsync(VpnTenantIpNetworkIn vpnTenantIpNetworkIn)
        {
            this.UnitOfWork.VpnTenantIpNetworkInRepository.Update(vpnTenantIpNetworkIn);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(vpnTenantIpNetworkIn.VpnTenantIpNetworkInID, deep: true, asTrackable: false);
        }

        public async Task<VpnTenantIpNetworkIn> UpdateAsync(int vpnTenantIpNetworkInId, VpnTenantIpNetworkInUpdate update)
        {
            await _updateDirector.UpdateAsync(vpnTenantIpNetworkInId, update);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(vpnTenantIpNetworkInId, deep: true, asTrackable: false);
        }

        public async Task DeleteAsync(int vpnTenantNetworkInId)
        {
            await this.UnitOfWork.VpnTenantIpNetworkInRepository.DeleteAsync(vpnTenantNetworkInId);
            await this.UnitOfWork.SaveAsync();
        }
    }
}