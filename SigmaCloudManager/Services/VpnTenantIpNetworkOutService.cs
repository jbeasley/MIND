﻿using System;
using System.Collections.Generic;
using System.Linq;
using SCM.Data;
using SCM.Models;
using System.Threading.Tasks;
using Mind.Builders;
using Mind.Models.RequestModels;

namespace SCM.Services
{
    public class VpnTenantIpNetworkOutService : BaseService, IVpnTenantIpNetworkOutService
    {
        private readonly IVpnTenantIpNetworkOutDirector _director;
        private readonly IVpnTenantIpNetworkOutUpdateDirector _updateDirector;
        private readonly string _properties = "AttachmentSet,"
                + "BgpPeer.RoutingInstance,"
                + "TenantIpNetwork";

        public VpnTenantIpNetworkOutService(IUnitOfWork unitOfWork, IVpnTenantIpNetworkOutDirector director,
            IVpnTenantIpNetworkOutUpdateDirector updateDirector) : base(unitOfWork)
        {
            _director = director;
            _updateDirector = updateDirector;
        }

        /// <summary>
        /// Get all VPN tenant IP networks for a given attachment set.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantIpNetworkOut>> GetAllByAttachmentSetIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return await this.UnitOfWork.VpnTenantIpNetworkOutRepository.GetAsync(q => q.AttachmentSetID == id, 
                includeProperties: deep.HasValue && deep.Value ? _properties : "TenantIpNetwork",
                AsTrackable: asTrackable);
        }

        /// <summary>
        /// Get all VPN tenant IP networks for a given vpn.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnTenantIpNetworkOut>> GetAllByVpnIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return await UnitOfWork.VpnTenantIpNetworkOutRepository.GetAsync(q => 
                         q.AttachmentSet.VpnAttachmentSets
                         .Where(x => x.VpnID == id).Any(),
                         includeProperties: deep.HasValue && deep.Value ? _properties : "TenantIpNetwork",
                         AsTrackable: asTrackable);
        }

        /// <summary>
        /// Get a single vpn tenant IP network.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<VpnTenantIpNetworkOut> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return (from result in await UnitOfWork.VpnTenantIpNetworkOutRepository.GetAsync(q => q.VpnTenantIpNetworkOutID == id,
                   includeProperties: deep.HasValue && deep.Value ? _properties : "TenantIpNetwork",
                   AsTrackable: asTrackable)
                    select result)
                   .SingleOrDefault();
        }

        /// <summary>
        /// TO-BE-REMOVED
        /// </summary>
        /// <param name="vpnTenantIpNetworkOut"></param>
        /// <returns></returns>
        public async Task<VpnTenantIpNetworkOut> AddAsync(VpnTenantIpNetworkOut vpnTenantIpNetworkOut)
        {
            this.UnitOfWork.VpnTenantIpNetworkOutRepository.Insert(vpnTenantIpNetworkOut);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(vpnTenantIpNetworkOut.VpnTenantIpNetworkOutID, deep: true, asTrackable: false);
        }

        public async Task<VpnTenantIpNetworkOut> AddAsync(int attachmentSetId, VpnTenantIpNetworkOutRequest request)
        {
            var vpnTenantIpNetworkOut = await _director.BuildAsync(attachmentSetId, request);
            this.UnitOfWork.VpnTenantIpNetworkOutRepository.Insert(vpnTenantIpNetworkOut);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(vpnTenantIpNetworkOut.VpnTenantIpNetworkOutID, deep: true, asTrackable: false);
        }

        /// <summary>
        /// TO-BE-REMOVED
        /// </summary>
        /// <param name="vpnTenantIpNetworkOut"></param>
        /// <returns></returns>
        public async Task<VpnTenantIpNetworkOut> UpdateAsync(VpnTenantIpNetworkOut vpnTenantIpNetworkOut)
        {
            this.UnitOfWork.VpnTenantIpNetworkOutRepository.Update(vpnTenantIpNetworkOut);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(vpnTenantIpNetworkOut.VpnTenantIpNetworkOutID, deep: true, asTrackable: false);
        }

        public async Task<VpnTenantIpNetworkOut> UpdateAsync(int vpnTenantIpNetworkOutId, VpnTenantIpNetworkOutRequest request)
        {
            await _updateDirector.UpdateAsync(vpnTenantIpNetworkOutId, request);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(vpnTenantIpNetworkOutId, deep: true, asTrackable: false);
        }

        public async Task DeleteAsync(int vpnTenantIpNetworkOutId)
        {
            await this.UnitOfWork.VpnTenantIpNetworkOutRepository.DeleteAsync(vpnTenantIpNetworkOutId);
            await this.UnitOfWork.SaveAsync();
        }
    }
}