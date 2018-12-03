using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Models.RequestModels;
using SCM.Data;
using System.Linq.Expressions;
using SCM.Services;

namespace Mind.Services
{
    public interface IProviderDomainAttachmentService: IBaseService
    {
        Task<Attachment> GetByIDAsync(int attachmentId, bool? deep = false, bool asTrackable = false);
        Task<List<Attachment>> GetAllByTenantIDAsync(int tenantId, bool? deep = false, bool asTrackable = false);
        Task<Attachment> AddAsync(int tenantId, ProviderDomainAttachmentRequest request, bool stage = true, bool syncToNetwork = false);
        Task<Attachment> UpdateAsync(int attachmentId, ProviderDomainAttachmentUpdate update, bool stage = true, bool syncToNetwork = false);
        Task DeleteAsync(int attachmentId);
        Task SyncToNetworkPutAsync(int attachmentId);
    }
}
