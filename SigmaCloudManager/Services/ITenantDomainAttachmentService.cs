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
    public interface ITenantDomainAttachmentService: IBaseService
    {
        Task<Attachment> GetByIDAsync(int attachmentId, bool? deep = false, bool asTrackable = false);
        Task<List<Attachment>> GetAllByDeviceIDAsync(int deviceId, bool? deep = false, bool asTrackable = false);
        Task<Attachment> AddAsync(int deviceId, TenantDomainAttachmentRequest request);
        Task<Attachment> UpdateAsync(int attachmentId, TenantDomainAttachmentUpdate update);
        Task DeleteAsync(int attachmentId);
    }
}
