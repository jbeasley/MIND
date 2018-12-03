using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Mind.Builders;
using Mind.Models.RequestModels;
using SCM.Data;
using SCM.Models;
using Mind.Directors;
using IO.NovaAttSwagger.Api;
using IO.NovaAttSwagger.Client;

namespace Mind.Services
{
    public class ProviderDomainVifService : BaseVifService, IProviderDomainVifService
    {
        private readonly IProviderDomainVifDirector _director;
        private readonly IDestroyable<Vif> _destroyableVifDirector;
        private readonly IDataApi _novaApiClient;
        private readonly Func<Attachment, INetworkSynchronizable<Vif>> _networkSyncVifDirectorFactory;


        public ProviderDomainVifService(IUnitOfWork unitOfWork, IMapper mapper,
            IProviderDomainVifDirector director,
            IDestroyable<Vif> destroyableVifDirector,
            Func<Attachment, INetworkSynchronizable<Vif>> networkSyncVifDirectorFactory,
            IDataApi novaApiClient) : base (unitOfWork, mapper)
        {
            _director = director;
            _destroyableVifDirector = destroyableVifDirector;
            _novaApiClient = novaApiClient;
            _networkSyncVifDirectorFactory = networkSyncVifDirectorFactory;
        }

        public async Task<Vif> AddAsync(int attachmentId, ProviderDomainVifRequest request, bool stage = true, bool syncToNetwork = false)
        {
            var attachment = await UnitOfWork.AttachmentRepository.GetByIDAsync(attachmentId);

            // Create the vif, and add to the network if the parent attachment is non-bundle, non-multiport.

            var allowStage = (attachment.NetworkStatus == Models.NetworkStatusEnum.Active || attachment.NetworkStatus == Models.NetworkStatusEnum.Staged) && 
                              !attachment.IsBundle && !attachment.IsMultiPort;

            if (stage && !allowStage)
            {
                throw new ServiceBadArgumentsException($"The vif cannot be staged. Currently only vifs which belong to tagged attachments " +
                    "which are not bundles or multiports support staging.");
            }

            var allowNetworkSync = attachment.NetworkStatus == Models.NetworkStatusEnum.Active && 
                                   !attachment.IsBundle && !attachment.IsMultiPort;

            if (syncToNetwork && !allowNetworkSync)
            {
                throw new ServiceBadArgumentsException($"The vif cannot be synchronised with the network. Currently only vifs which belong to tagged attachments " +
                    "which are not bundles or multiports support network sync.");
            }

            var vif = await _director.BuildAsync(attachmentId, request, stage, syncToNetwork);
            UnitOfWork.VifRepository.Insert(vif);

            // Save changes to the db
            await UnitOfWork.SaveAsync();

            return await base.GetByIDAsync(vif.VifID, deep: true, asTrackable: false);
        }

        /// <summary>
        /// Update a vif
        /// </summary>
        /// <returns>A task</returns>
        /// <param name="vifId">The ID of the vif to update</param>
        /// <param name="update">Update object containing the updates to apply to the vif</param>
        /// <param name="stage"></param>
        /// <param name="syncToNetwork"></param>
        public async Task<Vif> UpdateAsync(int vifId, ProviderDomainVifUpdate update, bool stage = true, bool syncToNetwork = false)
        {
            var vif = (from result in await UnitOfWork.VifRepository.GetAsync(
                    q =>
                       q.VifID == vifId,
                       query: q => q.Include(x => x.Attachment),
                       AsTrackable: false)
                       select result)
                       .Single();

            // Update the vif and add to the network if the parent attachment is non-bundle, non-multiport
            var allowStage = (vif.Attachment.NetworkStatus == Models.NetworkStatusEnum.Staged || vif.Attachment.NetworkStatus == Models.NetworkStatusEnum.Active) && 
                              !vif.Attachment.IsBundle && !vif.Attachment.IsMultiPort;


            if (stage && !allowStage)
            {
                throw new ServiceBadArgumentsException($"The vif cannot be staged. Currently only vifs which belong to tagged attachments " +
                    "which are not bundles or multiports support staging.");
            }

            var allowNetworkSync = vif.Attachment.NetworkStatus == Models.NetworkStatusEnum.Active && 
                                    !vif.Attachment.IsBundle && !vif.Attachment.IsMultiPort;

            if (syncToNetwork && !allowNetworkSync)
            {
                throw new ServiceBadArgumentsException($"The vif cannot be synchronised with the network. Currently only vifs which belong to tagged attachments " +
                    "which are not bundles or multiports support network sync.");
            }

            var updatedVif = await _director.UpdateAsync(vifId, update, stage, syncToNetwork);

            // Save changes to the db
            await UnitOfWork.SaveAsync();

            return await GetByIDAsync(vifId, deep: true, asTrackable: false);
        }

        /// <summary>
        /// Deletes a vif.
        /// </summary>
        /// <returns>An awaitable task</returns>
        /// <param name="vifId">The ID of the vif to delete</param>
        public async Task DeleteAsync(int vifId)
        {
            var vif = (from result in await UnitOfWork.VifRepository.GetAsync(
                    q =>
                       q.VifID == vifId,
                       query: q => q.Include(x => x.Attachment),
                       AsTrackable: false)
                       select result)
                       .Single();
                
            // Destroy the vif and conditionally clean up the network.
            // Only vifs configured under non-bundle, non-multiport tagged attachment are currently provisioned to the network and therefore
            // require the network to be cleaned up when destroyed.
            await _destroyableVifDirector.DestroyAsync(vif, vif.NetworkStatus == Models.NetworkStatusEnum.Active && 
                !vif.Attachment.IsBundle && 
                !vif.Attachment.IsMultiPort);

            await UnitOfWork.SaveAsync();
        }

        /// <summary>
        /// Sync a vif to the network with a put operation.
        /// </summary>
        /// <returns>An awaitable task</returns>
        /// <param name="vifId">The ID of the vif</param>
        public async Task SyncToNetworkPutAsync(int vifId)
        {
            var vif = (from result in await UnitOfWork.VifRepository.GetAsync(q =>
                       q.VifID == vifId,
                       query: q => q.Include(x => x.Attachment),
                       AsTrackable: false)
                       select result)
                       .Single();

            // Allow the vif to be synchronised with the network if it is in staged, active, or activation failure state
            if (vif.NetworkStatus == Models.NetworkStatusEnum.Staged ||
                vif.NetworkStatus == Models.NetworkStatusEnum.Active ||
                vif.NetworkStatus == Models.NetworkStatusEnum.ActivationFailure)
            {
                var director = _networkSyncVifDirectorFactory(vif.Attachment);

                try
                {
                    var updatedVif = await director.SyncToNetworkPutAsync(vif);
                }

                catch (ApiException)
                {
                    // Rethrow the exception to be caught further up the stack
                    throw;
                }

                finally
                {                    
                    // Save network status change for the vif
                    await UnitOfWork.SaveAsync();
                }
            }
            else
            {
                throw new ServiceBadArgumentsException($"The vif cannot be synchronised with the network because it is not staged. " +
                    "Edit and stage the vif first.");
            }
        }
    }
}
