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
using Mind.Validators;
using Mind.Builders;

namespace Mind.Services
{
    /// <summary>
    /// Service logic for provider domain attachments
    /// </summary>
    public class ProviderDomainAttachmentService : BaseAttachmentService, IProviderDomainAttachmentService
    {
        private readonly Func<ProviderDomainAttachmentRequest, IProviderDomainAttachmentDirector> _directorFactory;
        private readonly Func<Attachment, IProviderDomainAttachmentUpdateDirector> _updateDirectorFactory;
        private readonly IProviderDomainAttachmentValidator _validator;

        public ProviderDomainAttachmentService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IProviderDomainAttachmentValidator validator,
            Func<ProviderDomainAttachmentRequest, IProviderDomainAttachmentDirector> directorFactory,
            Func<Attachment, IProviderDomainAttachmentUpdateDirector> updateDirectorFactory) : base(unitOfWork, mapper, validator)
        {
            _directorFactory = directorFactory;
            _updateDirectorFactory = updateDirectorFactory;
            _validator = validator;
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
            var director = _directorFactory(request);
            var attachment = await director.BuildAsync(tenantId, request);
            UnitOfWork.AttachmentRepository.Insert(attachment);
            await UnitOfWork.SaveAsync();

            return await base.GetByIDAsync(attachment.AttachmentID, PortRoleTypeEnum.TenantFacing, deep: true, asTrackable: false);
        }

        /// <summary>
        /// Create a new provider domain attachment
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public async Task<Attachment> UpdateAsync(int attachmentId, ProviderDomainAttachmentUpdate update)
        {
            await _validator.ValidateChangesAsync(attachmentId, update);
            if (!_validator.IsValid)
            {
                throw new ServiceValidationException();
            }

            var attachment = (from attachments in await UnitOfWork.AttachmentRepository.GetAsync(q => q.AttachmentID == attachmentId,
                includeProperties: "Device,RoutingInstance,ContractBandwidthPool,AttachmentRole,AttachmentBandwidth,Interfaces.Ports", AsTrackable: true)
                              select attachments)
                              .Single();

            // Remember old routing instance ID and contract bandwidth pool ID for later removal checks
            var oldRoutingInstanceID = attachment.RoutingInstanceID;
            var oldContractBandwidthPoolID = attachment.ContractBandwidthPoolID;

            var director = _updateDirectorFactory(attachment);
            await director.UpdateAsync(attachment, update);
            UnitOfWork.AttachmentRepository.Update(attachment);

            // Cleanup routing instance if there are no attachment or vifs which are using it.
            if (oldRoutingInstanceID != null && oldRoutingInstanceID != attachment.RoutingInstanceID)
            {
                var oldRoutingInstance = (from routingInstances in await UnitOfWork.RoutingInstanceRepository.GetAsync(x =>
                    x.RoutingInstanceID == oldRoutingInstanceID,
                    includeProperties: "Attachments,Vifs", AsTrackable: true)
                                          select routingInstances)
                                          .Single();

                if (!oldRoutingInstance.Attachments.Any() && !oldRoutingInstance.Vifs.Any()) UnitOfWork.RoutingInstanceRepository.Delete(oldRoutingInstance);
            }

            // Cleanup contract bandwidth pool if the attachment is no longer using it.
            if (oldContractBandwidthPoolID != null && oldContractBandwidthPoolID != attachment.ContractBandwidthPoolID)
            {
                var oldContractBandwidthPool = (from contractBandwidthPools in await UnitOfWork.ContractBandwidthPoolRepository.GetAsync(x =>
                    x.ContractBandwidthPoolID == oldContractBandwidthPoolID,
                    includeProperties: "Attachments", AsTrackable: true)
                                                select contractBandwidthPools)
                                               .Single();

                if (!oldContractBandwidthPool.Attachments.Any()) UnitOfWork.ContractBandwidthPoolRepository.Delete(oldContractBandwidthPool);
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
            await _validator.ValidateDeleteAsync(attachmentId);
            if (!_validator.IsValid)
            {
                throw new ServiceValidationException();
            }

            var attachment = (from attachments in await UnitOfWork.AttachmentRepository.GetAsync(q => q.AttachmentID == attachmentId,
                includeProperties:
                "ContractBandwidthPool.Attachments," +
                "ContractBandwidthPool.Vifs," +
                "Interfaces.Ports.PortStatus," +
                "Vifs.Vlans," +
                "Vifs.RoutingInstance.RoutingInstanceType," +
                "Vifs.RoutingInstance.Attachments," +
                "Vifs.RoutingInstance.Vifs," +
                "Vifs.ContractBandwidthPool.Vifs," +
                "Vifs.ContractBandwidthPool.Attachments," +
                "RoutingInstance.RoutingInstanceType," +
                "RoutingInstance.Vifs," +
                "RoutingInstance.Attachments," +
                "RoutingInstance.BgpPeers",
                AsTrackable: true)
                              select attachments)
                              .Single();

            var ports = attachment.Interfaces.SelectMany(q => q.Ports).ToList();
            var portStatusFreeId = (from portStatuses in await UnitOfWork.PortStatusRepository.GetAsync(q => q.PortStatusType == PortStatusTypeEnum.Free)
                                    select portStatuses)
                                    .Single().PortStatusID;

            // Update ports to release back to inventory
            foreach (var port in ports)
            {
                port.TenantID = null;
                port.PortStatusID = portStatusFreeId;
                port.InterfaceID = null;

                UnitOfWork.PortRepository.Update(port);
            }

            if (attachment.RoutingInstance != null)
            {
                if (attachment.RoutingInstance.RoutingInstanceType.Type == RoutingInstanceTypeEnum.TenantFacingVrf)
                {
                    // Check if the current attachment is the only attachment using the routing instance and no 
                    // vifs are using the routing instance. If so delete the routing instance.
                    if (attachment.RoutingInstance.Attachments.Count == 1 && !attachment.RoutingInstance.Vifs.Any())
                    {
                        UnitOfWork.RoutingInstanceRepository.Delete(attachment.RoutingInstance);
                    }
                }
            }

            foreach (var routingInstance in attachment.Vifs.Select(x => x.RoutingInstance)
                                                           .Where(x => x != null && x.RoutingInstanceType.Type == RoutingInstanceTypeEnum.TenantFacingVrf))
            {
                // Check if the routing instance can be deleted. If there are no attachments which belong to the routing instance, and the
                // only vifs which belong to the routing instance belong to the attachment being deleted then the routing instance can be 
                // deleted.
                if (!routingInstance.Attachments.Any() && routingInstance.Vifs.Intersect(attachment.Vifs, new VifCompare()).Count() == routingInstance.Vifs.Count())
                {
                    UnitOfWork.RoutingInstanceRepository.Delete(routingInstance);
                }
            }

            foreach (var contractBandwidthPool in attachment.Vifs.Select(x => x.ContractBandwidthPool))
            {
                if (contractBandwidthPool != null) UnitOfWork.ContractBandwidthPoolRepository.Delete(contractBandwidthPool);
            }

            if (attachment.ContractBandwidthPool != null) UnitOfWork.ContractBandwidthPoolRepository.Delete(attachment.ContractBandwidthPool);

            UnitOfWork.AttachmentRepository.Delete(attachment);
            await UnitOfWork.SaveAsync();
        }

        /// <summary>
        /// Comparer for vif objects
        /// </summary>
        internal class VifCompare : IEqualityComparer<Vif>
        {

            /// <summary>
            /// Returns true if Vif instances are equal
            /// </summary>
            /// <returns>Boolean</returns>
            public bool Equals(Vif x, Vif y)
            {
                if (ReferenceEquals(null, x)) return false;
                if (ReferenceEquals(null, y)) return false;
                if (ReferenceEquals(x, y)) return true;

                return
                    (
                        x.VifID == y.VifID ||
                        x.VifID.Equals(y.VifID)
                    );

            }

            public int GetHashCode(Vif obj)
            {
                unchecked // Overflow is fine, just wrap
                {
                    var hashCode = 41;

                    // Ignore hashing the name property - we need a deeply populated vif object to calulate
                    // 'name' and we have enough properties to hash and avoid chance of collision
                    hashCode = hashCode * 59 + obj.VifID.GetHashCode();
                    hashCode = hashCode * 59 + obj.IsLayer3.GetHashCode();
                    hashCode = hashCode * 59 + obj.VlanTag.GetHashCode();
                    hashCode = hashCode * 59 + obj.AttachmentID.GetHashCode();
                    hashCode = hashCode * 59 + obj.TenantID.GetHashCode();
                    if (obj.RoutingInstance != null)
                        hashCode = hashCode * 59 + obj.RoutingInstance.GetHashCode();
                    if (obj.Vlans != null)
                        hashCode = hashCode * 59 + obj.Vlans.GetHashCode();
                    if (obj.ContractBandwidthPool != null)
                        hashCode = hashCode * 59 + obj.ContractBandwidthPool.GetHashCode();
                    return hashCode;
                }
            }
        }
    }
}