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
using Mind.Validators;
using Mind.Builders;

namespace Mind.Services
{
    /// <summary>
    /// Service logic for tenant domain attachments
    /// </summary>
    public class TenantDomainAttachmentService : BaseAttachmentService, ITenantDomainAttachmentService
    {
        private readonly Func<TenantDomainAttachmentRequest, AttachmentRole, ITenantDomainAttachmentDirector> _directorFactory;
        private readonly Func<Attachment, ITenantDomainAttachmentUpdateDirector> _updateDirectorFactory;

        public TenantDomainAttachmentService(IUnitOfWork unitOfWork,
            IMapper mapper,
            Func<TenantDomainAttachmentRequest, AttachmentRole, ITenantDomainAttachmentDirector> directorFactory,
            Func<Attachment, ITenantDomainAttachmentUpdateDirector> updateDirectorFactory) : base(unitOfWork, mapper)
        {
            _directorFactory = directorFactory;
            _updateDirectorFactory = updateDirectorFactory;
        }

        /// <summary>
        /// Get a tenant domain attachment by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deep"></param>
        /// <param name="asTrackable"></param>
        /// <returns></returns>
        public async Task<Attachment> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return await base.GetByIDAsync(id, SCM.Models.PortRoleTypeEnum.TenantInfrastructure, deep, asTrackable);
        }

        /// <summary>
        /// Get all tenant domain attachments for a given device
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deep"></param>
        /// <param name="asTrackable"></param>
        /// <returns></returns>
        public async Task<List<Attachment>> GetAllByDeviceIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return await base.GetAllByDeviceIDAsync(id, SCM.Models.PortRoleTypeEnum.TenantInfrastructure, deep, asTrackable);
        }

        /// <summary>
        /// Create a new tenant domain attachment
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Attachment> AddAsync(int deviceId, TenantDomainAttachmentRequest request)
        {
            var attachmentRole = (from result in await UnitOfWork.AttachmentRoleRepository.GetAsync(
                                x =>
                                  x.Name == request.AttachmentRoleName)
                                  select result)
                                  .SingleOrDefault();

            if (attachmentRole == null) throw new ServiceBadArgumentsException($"Could not find attachment role with name '{request.AttachmentRoleName}'."); 
            var director = _directorFactory(request, attachmentRole);
            var attachment = await director.BuildAsync(deviceId, request);
            UnitOfWork.AttachmentRepository.Insert(attachment);
            await UnitOfWork.SaveAsync();

            return await base.GetByIDAsync(attachment.AttachmentID, PortRoleTypeEnum.TenantInfrastructure, deep: true, asTrackable: false);
        }

        /// <summary>
        /// Update an existing tenant domain attachment
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public async Task<Attachment> UpdateAsync(int attachmentId, TenantDomainAttachmentUpdate update)
        {
            var attachment = (from result in await UnitOfWork.AttachmentRepository.GetAsync(
                            q =>
                              q.AttachmentID == attachmentId,
                             query: q => q.Include(x => x.ContractBandwidthPool.Attachments)
                                          .Include(x => x.ContractBandwidthPool.Vifs),
                              AsTrackable: true)
                              select result)
                              .Single();

            var director = _updateDirectorFactory(attachment);
            var updatedAttachment = await director.UpdateAsync(attachment, update);

            // Cleanup contract bandwidth pool if the attachment is no longer using it.
            if (attachment.ContractBandwidthPoolID != null && attachment.ContractBandwidthPoolID != updatedAttachment.ContractBandwidthPoolID)
            {
                if (!attachment.ContractBandwidthPool.Attachments.Any(x => x.AttachmentID != attachmentId))
                    UnitOfWork.ContractBandwidthPoolRepository.Delete(attachment.ContractBandwidthPool);
            }

            await UnitOfWork.SaveAsync();
            return await base.GetByIDAsync(attachment.AttachmentID, PortRoleTypeEnum.TenantInfrastructure, deep: true, asTrackable: false);
        }

        /// <summary>
        /// Delete a tenant domain attachment
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
    }
}