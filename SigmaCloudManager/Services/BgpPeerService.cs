﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;
using Mind.Builders;
using Mind.Models.RequestModels;

namespace SCM.Services
{
    public class BgpPeerService : BaseService, IBgpPeerService
    {
        public BgpPeerService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<BgpPeer>> GetAllByRoutingInstanceIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return await this.UnitOfWork.BgpPeerRepository.GetAsync(
                q => 
                q.RoutingInstanceID == id,
                query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q,
                AsTrackable: asTrackable);

        }

        public async Task<BgpPeer> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return (from result in await this.UnitOfWork.BgpPeerRepository.GetAsync(
                q => 
                q.BgpPeerID == id,
                query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q,
                AsTrackable: asTrackable)
                    select result)
                    .SingleOrDefault();
        }

        /// <summary>
        /// TO-BE-REMOVED
        /// </summary>
        /// <param name="bgpPeer"></param>
        /// <returns></returns>
        public async Task<BgpPeer> AddAsync(BgpPeer bgpPeer)
        {
            this.UnitOfWork.BgpPeerRepository.Insert(bgpPeer);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(bgpPeer.BgpPeerID, deep: true, asTrackable: false);
        }

        /// <summary>
        /// TO-BE-REMOVED
        /// </summary>
        /// <param name="bgpPeer"></param>
        /// <returns></returns>
        public async Task<BgpPeer> UpdateAsync(BgpPeer bgpPeer)
        {
            this.UnitOfWork.BgpPeerRepository.Update(bgpPeer);
            await this.UnitOfWork.SaveAsync();
            return await GetByIDAsync(bgpPeer.BgpPeerID, deep: true, asTrackable: false);
        }

        public async Task DeleteAsync(int bgpPeerId)
        {
            var bgpPeer = (from result in await UnitOfWork.BgpPeerRepository.GetAsync(
                        q => q.BgpPeerID == bgpPeerId,
                           query: q => q.IncludeDeleteValidationProperties(),
                           AsTrackable: true)
                           select result)
                           .Single();

            bgpPeer.ValidateDelete();

            await this.UnitOfWork.BgpPeerRepository.DeleteAsync(bgpPeerId);
            await this.UnitOfWork.SaveAsync();
        }
    }
}
