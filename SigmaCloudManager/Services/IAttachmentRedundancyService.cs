using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IAttachmentRedundancyService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<AttachmentRedundancy>> GetAllAsync();
        Task<AttachmentRedundancy> GetByIDAsync(int id);
        //Task<AttachmentRedundancy> GetByNameAsync(string name);
        Task<int> AddAsync(AttachmentRedundancy attachmentRedundancy);
        Task<int> UpdateAsync(AttachmentRedundancy attachmentRedundancy);
        Task<int> DeleteAsync(AttachmentRedundancy attachmentRedundancy);
    }
}
