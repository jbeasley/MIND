using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using AutoMapper;
using SCM.Data;
using SCM.Models;
using SCM.Models.RequestModels;
using SCM.Factories;
using SCM.Validators;
using Mind.Builders;

namespace SCM.Services
{
    /// <summary>
    /// Base service logic for attachments
    /// </summary>
    public abstract class BaseAttachmentService : BaseService
    {
        public BaseAttachmentService(IUnitOfWork unitOfWork, IMapper mapper, IValidator validator) : base(unitOfWork, mapper, validator)
        {
        }

        public BaseAttachmentService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        /// <summary>
        /// Find an attachment by ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deep"></param>
        /// <param name="portRoleType"></param>
        /// <param name="asTrackable"></param>
        /// <returns></returns>
        protected internal async virtual Task<Attachment> GetByIDAsync(int id, SCM.Models.PortRoleTypeEnum portRoleType, 
            bool? deep = false, bool asTrackable = false)
        {
            return (from attachments in await UnitOfWork.AttachmentRepository.GetAsync(
                q => 
                    q.AttachmentID == id 
                    && q.AttachmentRole.PortPool.PortRole.PortRoleType == portRoleType,
                    query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q,
                    AsTrackable: asTrackable)
                    select attachments)
                    .SingleOrDefault();

        }

        /// <summary>
        /// Find all attachments for a given tenant
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deep"></param>
        /// <param name="asTrackable"></param>
        /// <param name="portRoleType"></param>
        /// <returns></returns>
        protected internal async virtual Task<List<Attachment>> GetAllByTenantIDAsync(int id, SCM.Models.PortRoleTypeEnum portRoleType,
            bool? deep = false, bool asTrackable = false)
        {
            return (from attachments in await UnitOfWork.AttachmentRepository.GetAsync(
                q => 
                    q.TenantID == id
                    && q.AttachmentRole.PortPool.PortRole.PortRoleType == portRoleType,
                    query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties() : q,
                    AsTrackable: asTrackable)
                    select attachments)
                    .ToList();
        }
    }
}