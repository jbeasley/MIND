using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    /// <summary>
    /// Service-Logic class for management of Attachment Redundancy data
    /// </summary>
    public class AttachmentRedundancyService : BaseService, IAttachmentRedundancyService
    {
        public AttachmentRedundancyService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<AttachmentRedundancy>> GetAllAsync()
        {
            return await this.UnitOfWork.AttachmentRedundancyRepository.GetAsync();
        }

        public async Task<AttachmentRedundancy> GetByIDAsync(int id)
        {
            var dbResult = await this.UnitOfWork.AttachmentRedundancyRepository.GetAsync(q => q.AttachmentRedundancyID == id,
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<int> AddAsync(AttachmentRedundancy attachmentRedundancy)
        {
            this.UnitOfWork.AttachmentRedundancyRepository.Insert(attachmentRedundancy);
            return await this.UnitOfWork.SaveAsync();
        }
 
        public async Task<int> UpdateAsync(AttachmentRedundancy attachmentRedundancy)
        {
            this.UnitOfWork.AttachmentRedundancyRepository.Update(attachmentRedundancy);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(AttachmentRedundancy attachmentRedundancy)
        {
            this.UnitOfWork.AttachmentRedundancyRepository.Delete(attachmentRedundancy);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}
