using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;
using SCM.Models.RequestModels;

namespace SCM.Services
{
    public interface IProviderDomainRoutingInstanceService : IBaseService
    {
        Task<RoutingInstance> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<IEnumerable<RoutingInstance>> GetAllByTenantIDAsync(int tenantId, bool? deep = false, bool asTrackable = false, 
            string providerDomainLocationName = "");
        Task<IEnumerable<RoutingInstance>> GetAllByAttachmentSetIDAsync(int attachmentSetId, bool? deep = false, bool asTrackable = false);
    }
}
