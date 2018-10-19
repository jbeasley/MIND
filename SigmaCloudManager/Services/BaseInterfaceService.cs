using System;
using System.Collections.Generic;
using System.Linq;
using SCM.Data;
using SCM.Models;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SCM.Services;

namespace Mind.Services
{
    public class BaseInterfaceService : BaseService
    {
        public BaseInterfaceService(IUnitOfWork unitOfWork, IMapper mapper) : base (unitOfWork, mapper)
        {
        }

        public async Task<Interface> GetByIDAsync(int id, bool isTenantFacing = false, bool isProviderInfrastructure = false, 
            bool? deep = false, bool asTrackable = false)
        {
            var query = (from result in await UnitOfWork.InterfaceRepository.GetAsync(
                    q =>
                         q.InterfaceID == id,
                         query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties()
                                                                    .Include(x => x.Attachment.AttachmentRole.PortPool.PortRole) : 
                                                                   q.Include(x => x.Attachment.AttachmentRole.PortPool.PortRole),
                         AsTrackable: asTrackable)
                         select result);

            if (isTenantFacing) query = query.Where(
                                              x =>
                                              x.Attachment.AttachmentRole.PortPool.PortRole.PortRoleType == PortRoleTypeEnum.TenantFacing);

            if (isProviderInfrastructure) query = query.Where(
                                                        x =>
                                                        x.Attachment.AttachmentRole.PortPool.PortRole.PortRoleType == PortRoleTypeEnum.ProviderInfrastructure);

            return query.SingleOrDefault();
        }

        public async Task<IEnumerable<Interface>> GetAllByAttachmentIDAsync(int attachmentId, bool isTenantFacing = false, 
            bool isProviderInfrastructure = false, bool? deep = false, bool asTrackable = false)
        {
            var query = (from result in await UnitOfWork.InterfaceRepository.GetAsync(
                    q =>
                        q.AttachmentID == attachmentId,
                         query: q => deep.HasValue && deep.Value ? q.IncludeDeepProperties()
                                                                    .Include(x => x.Attachment.AttachmentRole.PortPool.PortRole) :
                                                                   q.Include(x => x.Attachment.AttachmentRole.PortPool.PortRole),
                        AsTrackable: asTrackable)
                        select result);

            if (isTenantFacing) query = query.Where(
                                              x =>
                                              x.Attachment.AttachmentRole.PortPool.PortRole.PortRoleType == PortRoleTypeEnum.TenantFacing);

            if (isProviderInfrastructure) query = query.Where(
                                                        x =>
                                                        x.Attachment.AttachmentRole.PortPool.PortRole.PortRoleType == PortRoleTypeEnum.ProviderInfrastructure);

            return query.ToList();
        }
    }
}