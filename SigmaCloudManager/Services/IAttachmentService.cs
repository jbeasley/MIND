using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Models.RequestModels;
using SCM.Data;
using System.Linq.Expressions;

namespace SCM.Services
{
    public interface IAttachmentService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<Attachment> GetByNameAsync(string deviceName, string attachmentName, bool includeProperties = true);
        Task<Attachment> GetByIDAsync(int id, bool includeProperties = true);
        Task<Attachment> GetByInterfaceIDAsync(int interfaceID, bool includeProperties = true);
        Task<IEnumerable<Attachment>> GetAllByRoutingInstanceIDAsync(int routingInstanceID, bool includeProperties = true);
        Task<ServiceResult> AddAsync(AttachmentRequest attachmentRequest);
        Task<ServiceResult> UpdateAttachmentAsync(AttachmentUpdate update);
        Task<ServiceResult> UpdateAttachmentPortAsync(AttachmentPortUpdate update);
        Task<ServiceResult> DeleteAsync(Attachment attachment);
    }
}
