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
    public interface IInfrastructureAttachmentService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<Attachment> GetByIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<Attachment>> GetAllAsync(string searchString = "", bool includeProperties = true);
        Task<ServiceResult> AddAsync(AttachmentRequest attachmentRequest);
        Task<ServiceResult> UpdateAttachmentAsync(AttachmentUpdate update);
        Task<ServiceResult> UpdateAttachmentPortAsync(AttachmentPortUpdate update);
        Task<ServiceResult> DeleteAsync(Attachment attachment);
    }
}
