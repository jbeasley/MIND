using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    /// <summary>
    /// Service-Logic class for management of Attachment Bandwidth data
    /// </summary>
    public class AttachmentBandwidthService : BaseService, IAttachmentBandwidthService
    {
        public AttachmentBandwidthService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<AttachmentBandwidth>> GetAllAsync()
        {
            return await this.UnitOfWork.AttachmentBandwidthRepository.GetAsync();
        }
   
        public async Task<AttachmentBandwidth> GetAsync(int bandwidth)
        {
            var dbResult = await this.UnitOfWork.AttachmentBandwidthRepository.GetAsync(q => q.BandwidthGbps == bandwidth);
            return dbResult.SingleOrDefault();
        }

        public async Task<AttachmentBandwidth> GetByIDAsync(int id)
        {
            var dbResult = await this.UnitOfWork.AttachmentBandwidthRepository.GetAsync(q => q.AttachmentBandwidthID == id, 
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<int> AddAsync(AttachmentBandwidth attachmentBandwidth)
        {
            this.UnitOfWork.AttachmentBandwidthRepository.Insert(attachmentBandwidth);
            return await this.UnitOfWork.SaveAsync();
        }
 
        public async Task<int> UpdateAsync(AttachmentBandwidth attachmentBandwidth)
        {
            this.UnitOfWork.AttachmentBandwidthRepository.Update(attachmentBandwidth);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(AttachmentBandwidth attachmentBandwidth)
        {
            this.UnitOfWork.AttachmentBandwidthRepository.Delete(attachmentBandwidth);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}
