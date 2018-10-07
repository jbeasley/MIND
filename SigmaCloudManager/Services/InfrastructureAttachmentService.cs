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
    /// Service logic for infrastructure attachments
    /// </summary>
    public class InfrastructureAttachmentService : BaseAttachmentService, IInfrastructureAttachmentService
    {
        private readonly Func<InfrastructureAttachmentRequest, AttachmentRole, IInfrastructureAttachmentDirector> _directorFactory;
        private readonly Func<Attachment, IInfrastructureAttachmentUpdateDirector> _updateDirectorFactory;

        public InfrastructureAttachmentService(IUnitOfWork unitOfWork,
            IMapper mapper,
            Func<InfrastructureAttachmentRequest, AttachmentRole, IInfrastructureAttachmentDirector> directorFactory,
            Func<Attachment, IInfrastructureAttachmentUpdateDirector> updateDirectorFactory) : base(unitOfWork, mapper)
        {
            _directorFactory = directorFactory;
            _updateDirectorFactory = updateDirectorFactory;
        }

        /// <summary>
        /// Get a infrastructure attachment by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deep"></param>
        /// <param name="asTrackable"></param>
        /// <returns></returns>
        public async Task<Attachment> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return await base.GetByIDAsync(id, SCM.Models.PortRoleTypeEnum.ProviderInfrastructure, deep, asTrackable);
        }

        /// <summary>
        /// Get all infrastructure attachments for a given device
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
        /// Create a new infrastructure attachment
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Attachment> AddAsync(int deviceId, InfrastructureAttachmentRequest request)
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

            return await base.GetByIDAsync(attachment.AttachmentID, PortRoleTypeEnum.ProviderInfrastructure, deep: true, asTrackable: false);
        }

        /// <summary>
        /// Update an existing infrastructure attachment
        /// </summary>
        /// <param name="attachmentId"></param>
        /// <param name="update"></param>
        /// <returns></returns>
        public async Task<Attachment> UpdateAsync(int attachmentId, InfrastructureAttachmentUpdate update)
        {
            var attachment = (from result in await UnitOfWork.AttachmentRepository.GetAsync(
                            q =>
                              q.AttachmentID == attachmentId,
                              AsTrackable: true)
                              select result)
                              .Single();

            var director = _updateDirectorFactory(attachment);
            var updatedAttachment = await director.UpdateAsync(attachment, update);

            await UnitOfWork.SaveAsync();
            return await base.GetByIDAsync(attachment.AttachmentID, PortRoleTypeEnum.ProviderInfrastructure, deep: true, asTrackable: false);
        }

        /// <summary>
        /// Delete an infrastructure attachment
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

            UnitOfWork.AttachmentRepository.Delete(attachment);
            await UnitOfWork.SaveAsync();
        }
    }
}