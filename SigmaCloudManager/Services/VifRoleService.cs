using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public class VifRoleService : BaseService, IVifRoleService
    {
        public VifRoleService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        private string Properties { get; } = "AttachmentRole,RoutingInstanceType";

        public async Task<IEnumerable<VifRole>> GetAllAsync(bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.VifRoleRepository.GetAsync(AsTrackable: false);
        }

        public async Task<IEnumerable<VifRole>> GetAllByAttachmentRoleIDAsync(int attachmentRoleID, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.VifRoleRepository.GetAsync(q => q.AttachmentRoleID == attachmentRoleID, 
                includeProperties: p, 
                AsTrackable: false);
        }

        public async Task<VifRole> GetByIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await this.UnitOfWork.VifRoleRepository.GetAsync(q => q.VifRoleID == id,
                includeProperties: p,
                AsTrackable: false);

            return dbResult.SingleOrDefault();
        }

        public async Task<VifRole> GetByNameAsync(string name, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await this.UnitOfWork.VifRoleRepository.GetAsync(q => q.Name == name,
                includeProperties: p,
                AsTrackable: false);

            return dbResult.SingleOrDefault();
        }

        public async Task<int> AddAsync(VifRole attachmentRole)
        {
            this.UnitOfWork.VifRoleRepository.Insert(attachmentRole);
            return await this.UnitOfWork.SaveAsync();
        }
 
        public async Task<int> UpdateAsync(VifRole attachmentRole)
        {
            this.UnitOfWork.VifRoleRepository.Update(attachmentRole);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(VifRole attachmentRole)
        {
            this.UnitOfWork.VifRoleRepository.Delete(attachmentRole);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}
