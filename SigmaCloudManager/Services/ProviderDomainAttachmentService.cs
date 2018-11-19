using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using AutoMapper;
using SCM.Data;
using SCM.Models;
using SCM.Models.RequestModels;
using SCM.Services;
using Mind.Builders;

namespace Mind.Services
{
    /// <summary>
    /// Service logic for provider domain attachments
    /// </summary>
    public class ProviderDomainAttachmentService : BaseAttachmentService, IProviderDomainAttachmentService
    {
        private readonly Func<ProviderDomainAttachmentRequest, AttachmentRole, IProviderDomainAttachmentDirector> _directorFactory;
        private readonly Func<Attachment, IProviderDomainAttachmentUpdateDirector> _updateDirectorFactory;

        public ProviderDomainAttachmentService(IUnitOfWork unitOfWork,
            IMapper mapper,
            Func<ProviderDomainAttachmentRequest, AttachmentRole, IProviderDomainAttachmentDirector> directorFactory,
            Func<Attachment, IProviderDomainAttachmentUpdateDirector> updateDirectorFactory) : base(unitOfWork, mapper)
        {
            _directorFactory = directorFactory;
            _updateDirectorFactory = updateDirectorFactory;
        }

        /// <summary>
        /// Get a provider domain attachment by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deep"></param>
        /// <param name="asTrackable"></param>
        /// <returns></returns>
        public async Task<Attachment> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return await base.GetByIDAsync(id, SCM.Models.PortRoleTypeEnum.TenantFacing, deep, asTrackable);
        }

        /// <summary>
        /// Get all provider domain attachments for a given tenant
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deep"></param>
        /// <param name="asTrackable"></param>
        /// <returns></returns>
        public async Task<List<Attachment>> GetAllByTenantIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return await base.GetAllByTenantIDAsync(id, SCM.Models.PortRoleTypeEnum.TenantFacing, deep, asTrackable);
        }

        /// <summary>
        /// Create a new provider domain attachment
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Attachment> AddAsync(int tenantId, ProviderDomainAttachmentRequest request)
        {
            var attachmentRole = (from result in await UnitOfWork.AttachmentRoleRepository.GetAsync(
                                x =>
                                  x.Name == request.AttachmentRoleName)
                                  select result)
                                  .SingleOrDefault();

            if (attachmentRole == null) throw new ServiceBadArgumentsException($"Could not find attachment role with name '{request.AttachmentRoleName}'.");

            var director = _directorFactory(request, attachmentRole);
            var attachment = await director.BuildAsync(tenantId, request);
            UnitOfWork.AttachmentRepository.Insert(attachment);
            await UnitOfWork.SaveAsync();

            return await base.GetByIDAsync(attachment.AttachmentID, PortRoleTypeEnum.TenantFacing, deep: true, asTrackable: false);
        }

        /// <summary>
        /// Update an existing provider domain attachment
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public async Task<Attachment> UpdateAsync(int attachmentId, ProviderDomainAttachmentUpdate update)
        {
            // Get the current attachment as a non-tracked entity. This is necessary because we check the 
            // routing instance ID and contract bandwidth pool ID of the updated attachment later and these may have changed during the update
            var attachment = (from result in await UnitOfWork.AttachmentRepository.GetAsync(
                            q =>
                              q.AttachmentID == attachmentId,
                              query: q => q.Include(x => x.RoutingInstance.Attachments)
                                           .Include(x => x.RoutingInstance.Vifs)
                                           .Include(x => x.ContractBandwidthPool.Attachments)
                                           .Include(x => x.ContractBandwidthPool.Vifs)
                                           .Include(x => x.AttachmentRole),
                                            AsTrackable: false)
                                            select result)
                                            .Single();

            var director = _updateDirectorFactory(attachment);
            var updatedAttachment = await director.UpdateAsync(attachment, update);

            // Cleanup routing instance if there are no attachment or vifs which are using it.
            if (attachment.RoutingInstanceID != null && attachment.RoutingInstanceID != updatedAttachment.RoutingInstanceID)
            {
                if (!attachment.RoutingInstance.Attachments.Any(x => x.AttachmentID != attachmentId) && 
                    !attachment.RoutingInstance.Vifs.Any())
                {
                    await UnitOfWork.RoutingInstanceRepository.DeleteAsync(attachment.RoutingInstanceID);
                }
            }

            // Cleanup contract bandwidth pool if the attachment is no longer using it.
            if (attachment.ContractBandwidthPoolID != null && attachment.ContractBandwidthPoolID != updatedAttachment.ContractBandwidthPoolID)
            {
                if (!attachment.ContractBandwidthPool.Attachments.Any(x => x.AttachmentID != attachmentId))
                    await UnitOfWork.ContractBandwidthPoolRepository.DeleteAsync(attachment.ContractBandwidthPoolID);
            }

            await UnitOfWork.SaveAsync();
            return await base.GetByIDAsync(attachment.AttachmentID, PortRoleTypeEnum.TenantFacing, deep: true, asTrackable: false);
        }

        /// <summary>
        /// Delete a provider domain attachment
        /// </summary>
        /// <param name="attachmentId"></param>
        public async Task DeleteAsync(int attachmentId)
        {
            var attachment = (from attachments in await UnitOfWork.AttachmentRepository.GetAsync(
                            q => 
                             q.AttachmentID == attachmentId,
                             query: q => q.IncludeDeleteValidationProperties(),
                              AsTrackable: true)
                              select attachments)
                              .Single();

            // Validate the attachment can be deleted
            attachment.ValidateDelete();

            var ports = attachment.Interfaces.SelectMany(
                                                q => 
                                                q.Ports)
                                                .ToList();

            var portStatusFreeId = (from portStatuses in await UnitOfWork.PortStatusRepository.GetAsync(
                                q => 
                                    q.PortStatusType == PortStatusTypeEnum.Free, 
                                    AsTrackable: true)
                                    select portStatuses)
                                    .Single().PortStatusID;

            // Update ports to release back to inventory
            foreach (var port in ports)
            {
                port.TenantID = null;
                port.PortStatusID = portStatusFreeId;
                port.InterfaceID = null;
            }

            if (attachment.RoutingInstance != null)
            {
                if (attachment.RoutingInstance.RoutingInstanceType.Type == RoutingInstanceTypeEnum.TenantFacingVrf)
                {
                    // Check if the current attachment is the only attachment using the routing instance and no 
                    // vifs are using the routing instance. If so delete the routing instance.
                    if (!attachment.RoutingInstance.Attachments.Any(x => x.AttachmentID != attachmentId) && 
                        !attachment.RoutingInstance.Vifs.Any())
                    {
                        UnitOfWork.RoutingInstanceRepository.Delete(attachment.RoutingInstance);
                    }
                }
            }

            foreach (var routingInstance in attachment.Vifs.Select(
                                                                x => 
                                                                x.RoutingInstance)
                                                                .Where(
                                                                    x => 
                                                                    x != null && x.RoutingInstanceType.Type == RoutingInstanceTypeEnum.TenantFacingVrf))
            {
                // For each vif configured under the attachment being deleted, check if the associated routing instance can be deleted. 
                // If there are no attachments which share the routing instance, and the
                // only vifs which share the routing instance are those which belong to the attachment being deleted then the routing instance can be 
                // deleted.
                if (!routingInstance.Attachments.Any() && 
                    routingInstance.Vifs
                                   .Intersect(
                                        attachment.Vifs, new VifComparer())
                                        .Count() == routingInstance.Vifs.Count())
                {
                    UnitOfWork.RoutingInstanceRepository.Delete(routingInstance);
                }
            }

            // Delete each contract bandwidth pool associated with a vif configured under the attachment being deleted.
            // These can be deleted without any further validation - contract bandwidth pools cannot be shared between vifs configured 
            // under different attachments.
            foreach (var contractBandwidthPool in attachment.Vifs.Select(
                                                                    x => 
                                                                    x.ContractBandwidthPool))
            {
                if (contractBandwidthPool != null) UnitOfWork.ContractBandwidthPoolRepository.Delete(contractBandwidthPool);
            }

            if (attachment.ContractBandwidthPool != null) UnitOfWork.ContractBandwidthPoolRepository.Delete(attachment.ContractBandwidthPool);

            UnitOfWork.AttachmentRepository.Delete(attachment);
            await UnitOfWork.SaveAsync();
        }

        private class VifComparer : IEqualityComparer<Vif>
        {
            public bool Equals(Vif x, Vif y)
            {
                return
                 (
                     x.VifID == y.VifID ||
                     x.VifID.Equals(y.VifID)
                 );
            }

            public int GetHashCode(Vif obj)
            {
                var hashCode = 41;

                // Just had on the vifID - two instance are considered 'same' if the IDs are the same
                hashCode = hashCode * 59 + obj.VifID.GetHashCode();
                return hashCode;
            }
        }
    }
}