using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IAttachmentSetService
    {
        IUnitOfWork UnitOfWork { get; }

        Task<IEnumerable<AttachmentSet>> GetAllAsync(bool includeProperties = true);
        Task<AttachmentSet> GetByIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<AttachmentSet>> GetAllByVpnIDAsync(int id, bool includeProperties = true);
        Task<IEnumerable<AttachmentSet>> GetAllByTenantAsync(Tenant tenant, bool includeProperties = true);
        Task<int> AddAsync(AttachmentSet attachmentSet);
        Task<int> UpdateAsync(AttachmentSet attachmentSet);
        Task<int> DeleteAsync(AttachmentSet attachmentSet);
    }
}
