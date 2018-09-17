using System;
using System.Collections.Generic;
using System.Linq;
using SCM.Data;
using SCM.Models;
using System.Threading.Tasks;
using Mind.Builders;
using Mind.Models.RequestModels;
using SCM.Services;

namespace Mind.Services
{
    public class VpnAttachmentSetService : BaseService, IVpnAttachmentSetService
    {
        private readonly IVpnAttachmentSetDirector _director;
        private readonly IVpnAttachmentSetUpdateDirector _updateDirector;

        public VpnAttachmentSetService(IUnitOfWork unitOfWork, IVpnAttachmentSetDirector director, 
            IVpnAttachmentSetUpdateDirector updateDirector) : base(unitOfWork)
        {
            _director = director;
            _updateDirector = updateDirector;
        }

        /// <summary>
        /// Return an attachment set association with a vpn.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deep"></param>
        /// <param name="asTrackable"></param>
        /// <returns></returns>
        public async Task<VpnAttachmentSet> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return (from result in await UnitOfWork.VpnAttachmentSetRepository.GetAsync(
                q => 
                    q.VpnAttachmentSetID == id,
                    query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q,
                    AsTrackable: asTrackable)
                    select result)
                    .SingleOrDefault();
        }

        /// <summary>
        /// Return an attachment set association with a vpn from the attachment set ID and vpn ID
        /// </summary>
        /// <param name="vpnId"></param>
        /// <param name="attachmentSetId"></param>
        /// <param name="deep"></param>
        /// <param name="asTrackable"></param>
        /// <returns></returns>
        public async Task<VpnAttachmentSet> GetByVpnIDAndAttachmentSetIDAsync(int vpnId, int attachmentSetId, bool? deep = false, bool asTrackable = false)
        {
            return (from result in await UnitOfWork.VpnAttachmentSetRepository.GetAsync(
                q =>
                    q.VpnID == vpnId && q.AttachmentSetID == attachmentSetId,
                    query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q,
                    AsTrackable: asTrackable)
                    select result)
                    .SingleOrDefault();
        }

        /// <summary>
        /// Return all vpn attachment set associations for a given attachment set.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deep"></param>
        /// <param name="asTrackable"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnAttachmentSet>> GetAllByAttachmentSetIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return (from result in await UnitOfWork.VpnAttachmentSetRepository.GetAsync(
                q =>
                    q.AttachmentSetID == id,
                    query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q,
                    AsTrackable: asTrackable)
                    select result)
                    .ToList();
        }

        /// <summary>
        /// Return all vpn attachment set associations for a given vpn.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deep"></param>
        /// <param name="asTrackable"></param>
        /// <returns></returns>
        public async Task<IEnumerable<VpnAttachmentSet>> GetAllByVpnIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return (from result in await UnitOfWork.VpnAttachmentSetRepository.GetAsync(
                q =>
                    q.VpnID == id,
                    query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q,
                    AsTrackable: asTrackable)
                    select result)
                    .ToList();
        }

        /// <summary>
        /// TO-BE-REMOVED
        /// </summary>
        /// <param name="vpnAttachmentSet"></param>
        /// <returns></returns>
        public async Task<VpnAttachmentSet> AddAsync(VpnAttachmentSet vpnAttachmentSet)
        {
            this.UnitOfWork.VpnAttachmentSetRepository.Insert(vpnAttachmentSet);
            await this.UnitOfWork.SaveAsync();

            return await GetByIDAsync(vpnAttachmentSet.VpnAttachmentSetID, deep: true, asTrackable: false);
        }

        /// <summary>
        /// Create a new association between an attachment set and a vpn
        /// </summary>
        /// <param name="vpnId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<VpnAttachmentSet> AddAsync(int vpnId, VpnAttachmentSetRequest request)
        {
            var vpnAttachmentSet = await _director.BuildAsync(vpnId, request);
            UnitOfWork.VpnAttachmentSetRepository.Insert(vpnAttachmentSet);
            await UnitOfWork.SaveAsync();

            return await GetByIDAsync(vpnAttachmentSet.VpnAttachmentSetID, deep: true, asTrackable: false);
        }

        /// <summary>
        /// TO-BE-REMOVED
        /// </summary>
        /// <param name="vpnAttachmentSet"></param>
        /// <returns></returns>
        public async Task<VpnAttachmentSet> UpdateAsync(VpnAttachmentSet vpnAttachmentSet)
        {
            this.UnitOfWork.VpnAttachmentSetRepository.Update(vpnAttachmentSet);
            await this.UnitOfWork.SaveAsync();

            return await GetByIDAsync(vpnAttachmentSet.VpnAttachmentSetID, deep: true, asTrackable: false);
        }

        /// <summary>
        /// Update an attachment set association with a vpn
        /// </summary>
        /// <param name="vpnId"></param>
        /// <param name="attachmentSetId"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public async Task<VpnAttachmentSet> UpdateAsync(int vpnId, int attachmentSetId, VpnAttachmentSetUpdate update)
        {
            var vpnAttachmentSet = await _updateDirector.UpdateAsync(vpnId, attachmentSetId, update);
            await this.UnitOfWork.SaveAsync();

            return await GetByIDAsync(vpnAttachmentSet.VpnAttachmentSetID, deep: true, asTrackable: false);
        }

        /// <summary>
        /// Remove an attachment set from a vpn
        /// </summary>
        /// <param name="vpnAttachmentSetId"></param>
        /// <returns></returns>
        public async Task DeleteAsync(int vpnAttachmentSetId)
        {
            await this.UnitOfWork.VpnAttachmentSetRepository.DeleteAsync(vpnAttachmentSetId);
            await this.UnitOfWork.SaveAsync();
        }

        /// <summary>
        /// Remove an attachment set from a vpn
        /// </summary>
        /// <param name="vpnId"></param>
        /// <param name="attachmentSetId"></param>
        /// <returns></returns>
        public async Task DeleteAsync(int vpnId, int attachmentSetId)
        {
            var vpnAttachmentSet = (from result in await UnitOfWork.VpnAttachmentSetRepository.GetAsync(
                                q =>
                                    q.VpnID == vpnId && q.AttachmentSetID == attachmentSetId,
                                    AsTrackable: true)
                                    select result)
                                    .Single();

            this.UnitOfWork.VpnAttachmentSetRepository.Delete(vpnAttachmentSet);
            await this.UnitOfWork.SaveAsync();
        }
    }
}