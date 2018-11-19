using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Mind.Builders;
using Mind.Models.RequestModels;
using SCM.Data;
using SCM.Models;

namespace Mind.Services
{
    public class ProviderDomainInterfaceService : BaseInterfaceService, IProviderDomainInterfaceService
    {
        public ProviderDomainInterfaceService(IUnitOfWork unitOfWork, IMapper mapper) : base (unitOfWork, mapper)
        {
        }

        public Task<IEnumerable<Interface>> GetAllByAttachmentIDAsync(int attachmentId, bool? deep = false, bool asTrackable = false)
        {
            return base.GetAllByAttachmentIDAsync(attachmentId, isTenantFacing: true, deep: deep, asTrackable: asTrackable);
        }

        public Task<Interface> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false)
        {
            return base.GetByIDAsync(id, isTenantFacing: true, deep: deep, asTrackable: asTrackable);
        }
    }
}
