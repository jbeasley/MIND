using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SCM.Data;
using SCM.Models;
using SCM.Models.RequestModels;
using Mind.Builders;
using SCM.Services;
using Mind.Directors;
using IO.NovaVpnSwagger.Client;

namespace Mind.Services
{
    public class VpnService : BaseService, IVpnService
    {
        private readonly Func<Mind.Models.RequestModels.VpnRequest, IVpnDirector> _directorFactory;
        private readonly Func<Vpn, IVpnDirector> _updateDirectorFactory;
        private readonly Func<Vpn, IDestroyable<Vpn>> _destroyableVpnDirectorFactory;
        private readonly Func<Vpn, INetworkSynchronizable<Vpn>> _networkSyncVpnDirectorFactory;

        public VpnService(IUnitOfWork unitOfWork, IMapper mapper, Func<Mind.Models.RequestModels.VpnRequest, IVpnDirector> directorFactory,
                          Func<Vpn, IVpnDirector> updateDirectorFactory, 
                          Func<Vpn, IDestroyable<Vpn>> destroyableVpnDirectorFactory,
                          Func<Vpn, INetworkSynchronizable<Vpn>> networkSyncVpnDirectorFactory) : base(unitOfWork, mapper)
        {
            _directorFactory = directorFactory;
            _updateDirectorFactory = updateDirectorFactory;
            _destroyableVpnDirectorFactory = destroyableVpnDirectorFactory;
            _networkSyncVpnDirectorFactory = networkSyncVpnDirectorFactory;
        }

        /// <summary>
        /// Return all vpns
        /// </summary>
        /// <param name="asTrackable"></param>
        /// <param name="created"></param>
        /// <param name="deep"></param>
        /// <param name="isExtranet"></param>
        /// <param name="searchString"></param>
        /// <param name="showCreatedAlert"></param>
        /// <param name="sortKey"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Vpn>> GetAllAsync(bool? isExtranet = null, bool? created = null, bool? showCreatedAlert = null,
           bool? deep = false, bool asTrackable = false, string sortKey = "", string searchString = "")
        {
            var query = from vpns in await this.UnitOfWork.VpnRepository.GetAsync(
                        query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q.IncludeShallowProperties(),
                        AsTrackable: asTrackable)
                        select vpns;

            if (!string.IsNullOrEmpty(searchString)) query = query.Where(x => x.Name.Contains(searchString));
            if (isExtranet.HasValue) query = query.Where(x => x.IsExtranet = isExtranet.Value);
            if (created.HasValue) query = query.Where(x => x.Created = created.Value);
            if (showCreatedAlert.HasValue) query = query.Where(x => x.ShowCreatedAlert);

            return query.ToList();
        }

        /// <summary>
        /// Return a single vpn.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="asTrackable"></param>
        /// <param name="deep"></param>
        /// <returns></returns>
        public async Task<Vpn> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return (from result in await this.UnitOfWork.VpnRepository.GetAsync(
                q =>
                    q.VpnID == id,
                    query: q => deep.GetValueOrDefault() ? q.IncludeDeepProperties() : q.IncludeShallowProperties(),
                    AsTrackable: asTrackable)
                    select result)
                    .SingleOrDefault();
        }

        /// <summary>
        /// Return all vpns which are associated with a given attachment set.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="asTrackable"></param>
        /// <param name="created"></param>
        /// <param name="deep"></param>
        /// <param name="isExtranet"></param>
        /// <param name="searchString"></param>
        /// <param name="showCreatedAlert"></param>
        /// <param name="sortKey"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Vpn>> GetAllByAttachmentSetIDAsync(int id, bool? isExtranet = null, bool? created = null, bool? showCreatedAlert = null,
           bool? deep = false, bool asTrackable = false, string sortKey = "", string searchString = "")
        {
            var query = from vpns in await this.UnitOfWork.VpnRepository.GetAsync(
                    q =>
                        q.VpnAttachmentSets
                        .Any(r => r.AttachmentSetID == id),
                        query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q.IncludeShallowProperties(),
                        AsTrackable: asTrackable)
                        select vpns;

            if (!string.IsNullOrEmpty(searchString)) query = query.Where(x => x.Name.Contains(searchString));
            if (isExtranet.HasValue) query = query.Where(x => x.IsExtranet = isExtranet.Value);
            if (created.HasValue) query = query.Where(x => x.Created = created.Value);
            if (showCreatedAlert.HasValue) query = query.Where(x => x.ShowCreatedAlert);

            return query.ToList();
        }

        /// <summary>
        /// Return all vpns for a given tenant.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="asTrackable"></param>
        /// <param name="created"></param>
        /// <param name="deep"></param>
        /// <param name="isExtranet"></param>
        /// <param name="searchString"></param>
        /// <param name="showCreatedAlert"></param>
        /// <param name="sortKey"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Vpn>> GetAllByTenantIDAsync(int id, bool? isExtranet = null, bool? created = null, bool? showCreatedAlert = null,
           bool? deep = false, bool asTrackable = false, string sortKey = "", string searchString = "")
        {
            var query = from vpns in await this.UnitOfWork.VpnRepository.GetAsync(
                q => 
                    q.TenantID == id,
                    query: q => deep.GetValueOrDefault() ? q.IncludeDeepProperties() : q.IncludeShallowProperties(),
                    AsTrackable: false)
                    select vpns;

            if (!string.IsNullOrEmpty(searchString)) query = query.Where(x => x.Name.Contains(searchString));
            if (isExtranet.HasValue) query = query.Where(x => x.IsExtranet = isExtranet.Value);
            if (created.HasValue) query = query.Where(x => x.Created = created.Value);
            if (showCreatedAlert.HasValue) query = query.Where(x => x.ShowCreatedAlert);

            return query.ToList().GroupBy(q => q.VpnID).Select(r => r.First());
        }

        public async Task<Vpn> AddAsync(int tenantId, Mind.Models.RequestModels.VpnRequest request, bool stage = true, bool syncToNetwork = false)
        {
            // Build the VPN and sync to the network for unicast IP vpn with IPv4 address-family
            var allowStageAndSyncToNetwork = request.AddressFamily == Models.RequestModels.AddressFamilyEnum.IPv4 && !request.IsMulticastVpn.GetValueOrDefault();

            if (stage && !allowStageAndSyncToNetwork)
            {
                throw new ServiceBadArgumentsException($"The vpn cannot be staged. Currently only IP unicast vpn for the IPv4 address-family " +
                    "supports staging.");
            }

            if (syncToNetwork && !allowStageAndSyncToNetwork)
            {
                throw new ServiceBadArgumentsException($"The vpn cannot be synchronised to the network. Currently only IP unicast vpn for the IPv4 address-family " +
                    "supports network sync.");
            }

            var director = _directorFactory(request);
            var vpn = await director.BuildAsync(tenantId, request,
                                                stage, syncToNetwork);
            this.UnitOfWork.VpnRepository.Insert(vpn);
            await this.UnitOfWork.SaveAsync();

            return await GetByIDAsync(vpn.VpnID, deep: true);
        }

        /// <summary>
        /// Update a vpn.
        /// </summary>
        /// <param name="vpnId"></param>
        /// <param name="update"></param>
        /// <param name="stage"></param>
        /// <param name="syncToNetwork"></param>
        /// <returns></returns>
        public async Task<Vpn> UpdateAsync(int vpnId, Mind.Models.RequestModels.VpnUpdate update, bool stage = true, bool syncToNetwork = false)
        {
            var vpn = (from result in await UnitOfWork.VpnRepository.GetAsync(
                   q =>
                       q.VpnID == vpnId,
                       query: q => q.IncludeDeepProperties(),
                       AsTrackable: false)
                       select result)
                       .Single();
                      
            // Update the vpn and sync to the network for unicast IP vpn with IPv4 address-family
            var allowStageAndSyncToNetwork = vpn.AddressFamily.Name == "IPv4" && !vpn.IsMulticastVpn;

            if (stage && !allowStageAndSyncToNetwork)
            {
                throw new ServiceBadArgumentsException($"The vpn cannot be staged. Currently only IP unicast vpn for the IPv4 address-family " +
                    "supports staging.");
            }

            if (syncToNetwork && !allowStageAndSyncToNetwork)
            {
                throw new ServiceBadArgumentsException($"The vpn cannot be synchronised to the network. Currently only IP unicast vpn for the IPv4 address-family " +
                    "supports network sync.");
            }

            var updateDirector = _updateDirectorFactory(vpn);
            await updateDirector.UpdateAsync(vpnId, update, stage, syncToNetwork);
            await this.UnitOfWork.SaveAsync();

            return await GetByIDAsync(vpnId, deep: true);
        }

        /// <summary>
        /// Delete a vpn.
        /// </summary>
        /// <param name="vpnId"></param>
        /// <returns></returns>
        public async Task DeleteAsync(int vpnId)
        {
            var vpn = (from result in await UnitOfWork.VpnRepository.GetAsync(
                    q =>
                       q.VpnID == vpnId,
                       query: q => q.IncludeDeleteValidationProperties(),
                       AsTrackable: false)
                       select result)
                       .Single();

            var director = _destroyableVpnDirectorFactory(vpn);
            await director.DestroyAsync(vpn, vpn.NetworkStatus == Models.NetworkStatusEnum.Active && 
                vpn.AddressFamily.Name == "IPv4" 
                && !vpn.IsMulticastVpn);

            await this.UnitOfWork.SaveAsync();
        }

        /// <summary>
        /// Sync a vpn to the network
        /// </summary>
        /// <returns>An awaitable task</returns>
        /// <param name="vpnId">The ID of the vpn</param>
        public async Task SyncToNetworkPutAsync(int vpnId)
        {
            var vpn = (from result in await UnitOfWork.VpnRepository.GetAsync(
                    q =>
                       q.VpnID == vpnId,
                       query: q => q.IncludeBaseValidationProperties(),
                       AsTrackable: false)
                       select result)
                       .Single();

            if (vpn.NetworkStatus == Models.NetworkStatusEnum.Staged ||
                vpn.NetworkStatus == Models.NetworkStatusEnum.Active ||
                vpn.NetworkStatus == Models.NetworkStatusEnum.ActivationFailure)
            {
                var director = _networkSyncVpnDirectorFactory(vpn);

                try
                {
                    await director.SyncToNetworkPutAsync(vpn);
                }
                      
                catch (ApiException)
                {
                    // Rethrow the exception to be caught further up the stack
                    throw;
                }

                finally
                {
                    // Save network status change for the vpn
                    await UnitOfWork.SaveAsync();
                }
            }
            else
            {
                throw new ServiceBadArgumentsException($"The vpn cannot be synchronised with the network because it is not staged. " +
                    "Edit and stage the vpn first.");
            }
        }
    }
}