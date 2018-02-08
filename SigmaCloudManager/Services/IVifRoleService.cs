using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IVifRoleService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<VifRole>> GetAllAsync(bool includeProperties = true);
        Task<IEnumerable<VifRole>> GetAllByAttachmentRoleIDAsync(int attachmentRoleID, bool includeProperties = true);
        Task<VifRole> GetByIDAsync(int id, bool includeProperties = true);
        Task<VifRole> GetByNameAsync(string name, bool includeProperties = true);
        Task<int> AddAsync(VifRole attachmentRole);
        Task<int> UpdateAsync(VifRole attachmentRole);
        Task<int> DeleteAsync(VifRole attachmentRole);
    }
}
