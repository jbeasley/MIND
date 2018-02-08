using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public interface IAttachmentBandwidthService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<IEnumerable<AttachmentBandwidth>> GetAllAsync();
        Task<AttachmentBandwidth> GetByIDAsync(int id);
        Task<AttachmentBandwidth> GetAsync(int bandwidth);
        Task<int> AddAsync(AttachmentBandwidth attachmentBandwidth);
        Task<int> UpdateAsync(AttachmentBandwidth attachmentBandwidth);
        Task<int> DeleteAsync(AttachmentBandwidth attachmentBandwidth);
    }
}
