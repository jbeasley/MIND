using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IAttachmentRoleService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<AttachmentRole>> GetAllAsync(bool includeProperties = true);
        Task<IEnumerable<AttachmentRole>> GetAllByPortPoolIDAsync(int portPoolID, int? deviceRoleID = null, bool includeProperties = true);
        Task<AttachmentRole> GetByIDAsync(int id, bool includeProperties = true);
        Task<AttachmentRole> GetByPortPoolAndRoleName(string portPoolName, string attachmentRoleName, bool includeProperties = true);
        Task<int> AddAsync(AttachmentRole attachmentRole);
        Task<int> UpdateAsync(AttachmentRole attachmentRole);
        Task<int> DeleteAsync(AttachmentRole attachmentRole);
    }
}
