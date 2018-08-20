using System;
using System.Collections.Generic;
using System.Linq;
using SCM.Data;
using SCM.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mind.Services;
using Mind.Models.RequestModels;
using Mind.Builders;
using SCM.Validators;
using AutoMapper;

namespace SCM.Services
{
    public class AttachmentSetService : BaseService, IAttachmentSetService
    {
        private readonly IAttachmentSetDirector _director;
        private readonly IAttachmentSetUpdateDirector _updateDirector;
        private readonly IAttachmentSetValidator _validator;

        public AttachmentSetService(IUnitOfWork unitOfWork, 
            IMapper mapper, 
            IAttachmentSetValidator validator,
            IAttachmentSetDirector director,
            IAttachmentSetUpdateDirector updateDirector) : base(unitOfWork, mapper, validator)
        {
            _director = director;
            _updateDirector = updateDirector;
            _validator = validator;
        }

        private readonly string _properties = "Tenant,"
                + "SubRegion,"
                + "Region,"
                + "AttachmentRedundancy,"
                + "AttachmentSetRoutingInstances.RoutingInstance.Device.Plane,"
                + "AttachmentSetRoutingInstances.RoutingInstance.Tenant,"
                + "AttachmentSetRoutingInstances.RoutingInstance.Attachments.Interfaces.Ports,"
                + "AttachmentSetRoutingInstances.RoutingInstance.Vifs.Attachment.Interfaces.Ports,"
                + "MulticastVpnDomainType,"
                + "VpnTenantMulticastGroups.TenantMulticastGroup";

        public async Task<IEnumerable<AttachmentSet>> GetAllByTenantIDAsync(int tenantId, bool? deep = false, bool asTrackable = false)
        {
            return (from attachmentSets in await this.UnitOfWork.AttachmentSetRepository.GetAsync(q =>
                    q.TenantID == tenantId, includeProperties: deep.HasValue && deep.Value ? _properties : string.Empty,
                    AsTrackable: asTrackable)
                    select attachmentSets)
                    .ToList();
        }

        public async Task<AttachmentSet> GetByIDAsync(int id, bool? deep = false, bool asTrackable = true)
        {
            return (from attachmentSets in await UnitOfWork.AttachmentSetRepository.GetAsync(q => q.AttachmentSetID == id,
                    includeProperties: deep.HasValue && deep.Value ? _properties : string.Empty,
                    AsTrackable: asTrackable)
                    select attachmentSets)
                    .SingleOrDefault();
        }

        public async Task<AttachmentSet> AddAsync(int tenantId, AttachmentSetRequest request)
        {
            var attachmentSet = await _director.BuildAsync(tenantId, request);
            UnitOfWork.AttachmentSetRepository.Insert(attachmentSet);
            await UnitOfWork.SaveAsync();

            return await GetByIDAsync(attachmentSet.AttachmentSetID, deep: true, asTrackable: false);
        }

        /// <summary>
        /// Obsolete method - REMOVE
        /// </summary>
        /// <param name="attachmentSet"></param>
        /// <returns></returns>
        public async Task<AttachmentSet> AddAsync(AttachmentSet attachmentSet)
        {
            attachmentSet.Name = Guid.NewGuid().ToString("N");
            this.UnitOfWork.AttachmentSetRepository.Insert(attachmentSet);
            await this.UnitOfWork.SaveAsync();

            return attachmentSet;
        }

        /// <summary>
        /// Obsolete method - REMOVE
        /// </summary>
        /// <param name="attachmentSet"></param>
        /// <returns></returns>
        public async Task<AttachmentSet> UpdateAsync(AttachmentSet attachmentSet)
        {
            this.UnitOfWork.AttachmentSetRepository.Update(attachmentSet);
            await this.UnitOfWork.SaveAsync();

            return attachmentSet;
        }

        public async Task<AttachmentSet> UpdateAsync(int attachmentSetId, AttachmentSetUpdate update)
        {
            await _validator.ValidateChangesAsync(attachmentSetId, update);
            if (!_validator.IsValid) throw new ServiceValidationException();

            var attachmentSet = await GetByIDAsync(attachmentSetId);
            await _updateDirector.UpdateAsync(attachmentSet, update);
            this.UnitOfWork.AttachmentSetRepository.Update(attachmentSet);
            await this.UnitOfWork.SaveAsync();

            return await GetByIDAsync(attachmentSet.AttachmentSetID, deep: true, asTrackable: false);
        }

        public async Task DeleteAsync(int attachmentSetId)
        {
            await _validator.ValidateDeleteAsync(attachmentSetId);
            if (!_validator.IsValid) throw new ServiceValidationException();

            await this.UnitOfWork.AttachmentSetRepository.DeleteAsync(attachmentSetId);
            await this.UnitOfWork.SaveAsync();
        }
    }
}