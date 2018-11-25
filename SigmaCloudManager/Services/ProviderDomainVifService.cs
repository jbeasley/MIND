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
using IO.Swagger.Api;

namespace Mind.Services
{
    public class ProviderDomainVifService : BaseVifService, IProviderDomainVifService
    {
        private readonly IProviderDomainVifDirector _director;
        private readonly IDestroyable<Vif> _destroyableVifDirector;
        private readonly IDataApi _novaApiClient;
      

        public ProviderDomainVifService(IUnitOfWork unitOfWork, IMapper mapper,
            IProviderDomainVifDirector director,
            IDestroyable<Vif> destroyableVifDirector,
            IDataApi novaApiClient) : base (unitOfWork, mapper)
        {
            _director = director;
            _destroyableVifDirector = destroyableVifDirector;
            _novaApiClient = novaApiClient;
        }

        public async Task<Vif> AddAsync(int attachmentId, ProviderDomainVifRequest request)
        {
            var attachment = await UnitOfWork.AttachmentRepository.GetByIDAsync(attachmentId);

            // Create the vif, and add to the network if the parent attachment is non-bundle, non-multiport.
            var vif = await _director.BuildAsync(attachmentId, request, !attachment.IsBundle && !attachment.IsMultiPort);
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
        public async Task<Vif> UpdateAsync(int vifId, ProviderDomainVifUpdate update)
        {
            var vif = (from result in await UnitOfWork.VifRepository.GetAsync(
                    q =>
                       q.VifID == vifId,
                       query: q => q.Include(x => x.Attachment),
                       AsTrackable: false)
                       select result)
                       .Single();

            // Update the vif and add to the network if the parent attachment is non-bundle, non-multiport
            var updatedVif = await _director.UpdateAsync(vifId, update, !vif.Attachment.IsBundle && ! vif.Attachment.IsMultiPort);

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
            await _destroyableVifDirector.DestroyAsync(vif, !vif.Attachment.IsBundle && !vif.Attachment.IsMultiPort);

            await UnitOfWork.SaveAsync();
        }
    }
}
