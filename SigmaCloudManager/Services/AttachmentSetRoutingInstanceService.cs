using System;
using System.Collections.Generic;
using System.Linq;
using SCM.Data;
using SCM.Models;
using SCM.Models.RequestModels;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mind.Services;
using SCM.Validators;
using SCM.Services;
using Mind.Models.RequestModels;
using Mind.Builders;

namespace Mind.Services
{
    public class AttachmentSetRoutingInstanceService : BaseService, IAttachmentSetRoutingInstanceService
    {
        private readonly IAttachmentSetRoutingInstanceValidator _validator;
        private readonly IAttachmentSetRoutingInstanceDirector _director;

        public AttachmentSetRoutingInstanceService(IUnitOfWork unitOfWork, IAttachmentSetRoutingInstanceValidator validator, 
            IAttachmentSetRoutingInstanceDirector director) : base(unitOfWork,validator)
        {
            _validator = validator;
            _director = director;
        }

        private readonly string _properties = "AttachmentSet.Tenant,"
               + "RoutingInstance.Device.Location.SubRegion.Region,"
               + "RoutingInstance.Device.Plane,"
               + "RoutingInstance.Tenant,"
               + "RoutingInstance.Attachments.ContractBandwidthPool.Tenant,"
               + "RoutingInstance.Attachments.Interfaces.Ports,"
               + "RoutingInstance.Vifs.ContractBandwidthPool.Tenant,"
               + "RoutingInstance.Vifs.Attachment.Interfaces.Ports";

        public async Task<IEnumerable<AttachmentSetRoutingInstance>> GetAllByAttachmentSetIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return await UnitOfWork.AttachmentSetRoutingInstanceRepository.GetAsync(q => q.AttachmentSetID == id,
               includeProperties: deep.HasValue && deep.Value ? _properties : string.Empty,
               AsTrackable: asTrackable);
        }

        public async Task<AttachmentSetRoutingInstance> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return (from result in await UnitOfWork.AttachmentSetRoutingInstanceRepository.GetAsync(q => q.AttachmentSetRoutingInstanceID == id,
                    includeProperties: deep.HasValue && deep.Value ? _properties : string.Empty,
                    AsTrackable: asTrackable)
                    select result)
                   .SingleOrDefault();
        }

        public async Task<AttachmentSetRoutingInstance> GetByAttachmentSetIDAndRoutingInstanceIDAsync(int attachmentSetId, int routingInstanceId,
            bool? deep = false, bool asTrackable = false)
        {
            return (from result in await UnitOfWork.AttachmentSetRoutingInstanceRepository.GetAsync(q => 
                    q.AttachmentSetID == attachmentSetId
                    && q.RoutingInstanceID == routingInstanceId,
                    includeProperties: deep.HasValue && deep.Value ? _properties : string.Empty,
                    AsTrackable: asTrackable)
                    select result)
                   .SingleOrDefault();
        }

        /// <summary>
        /// DEFUNCT - REMOVE
        /// </summary>
        /// <param name="attachmentSetRoutingInstance"></param>
        /// <returns></returns>
        public async Task<AttachmentSetRoutingInstance> AddAsync(AttachmentSetRoutingInstance attachmentSetRoutingInstance)
        {
            this.UnitOfWork.AttachmentSetRoutingInstanceRepository.Insert(attachmentSetRoutingInstance);
            await this.UnitOfWork.SaveAsync();

            return await GetByIDAsync(attachmentSetRoutingInstance.AttachmentSetRoutingInstanceID, deep: true, asTrackable: false);
        }

        public async Task<AttachmentSetRoutingInstance> AddAsync(int attachmentSetId, RoutingInstanceForAttachmentSetRequest request)
        {
            var attachmentSetRoutingInstance = await _director.BuildAsync(attachmentSetId, request);
            UnitOfWork.AttachmentSetRoutingInstanceRepository.Insert(attachmentSetRoutingInstance);
            await UnitOfWork.SaveAsync();

            return await GetByIDAsync(attachmentSetRoutingInstance.AttachmentSetRoutingInstanceID, deep: true, asTrackable: false);
        }

        public async Task<AttachmentSetRoutingInstance> UpdateAsync(AttachmentSetRoutingInstance attachmentSetRoutingInstance)
        {
            this.UnitOfWork.AttachmentSetRoutingInstanceRepository.Update(attachmentSetRoutingInstance);
            await this.UnitOfWork.SaveAsync();

            return await GetByIDAsync(attachmentSetRoutingInstance.AttachmentSetRoutingInstanceID, deep: true, asTrackable: false);
        }

        public async Task DeleteAsync(int attachmentSetId, int routingInstanceId)
        {
            var attachmentSetRoutingInstance = (from result in await UnitOfWork.AttachmentSetRoutingInstanceRepository.GetAsync(q =>
                                                q.AttachmentSetID == attachmentSetId && q.RoutingInstanceID == routingInstanceId)
                                                select result)
                                                .Single();

            await _validator.ValidateDeleteAsync(attachmentSetRoutingInstance.AttachmentSetRoutingInstanceID);
            if (!_validator.IsValid) throw new ServiceValidationException("Validation failed");

            this.UnitOfWork.AttachmentSetRoutingInstanceRepository.Delete(attachmentSetRoutingInstance);
            await this.UnitOfWork.SaveAsync();
        }

        /// <summary>
        /// Get all routing instances which are candidates for satisfying an attachment set routing instance request.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<IEnumerable<RoutingInstance>> GetCandidateRoutingInstances(AttachmentSetRoutingInstanceRequest request)
        {
            var query = (from result in await UnitOfWork.RoutingInstanceRepository.GetAsync(q => 
                        q.Device.LocationID == request.LocationID &&
                        q.TenantID == request.TenantID,
                        includeProperties: "Device,Tenant",
                        AsTrackable: false)
                        select result);

            if (request.PlaneID != null)
            {
                query = query.Where(q => q.Device.PlaneID == request.PlaneID);
            }

            return query.ToList();
        }
    }
}