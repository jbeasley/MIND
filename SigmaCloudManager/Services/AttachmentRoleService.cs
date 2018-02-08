using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public class AttachmentRoleService : BaseService, IAttachmentRoleService
    {
        public AttachmentRoleService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        private string Properties { get; } = "RoutingInstanceType,"
            + "PortPool.PortRole," 
            + "DeviceRoleAttachmentRoles.DeviceRole";

        public async Task<IEnumerable<AttachmentRole>> GetAllAsync(bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.AttachmentRoleRepository.GetAsync(includeProperties: p, AsTrackable: false);
        }

        public async Task<IEnumerable<AttachmentRole>> GetAllByPortPoolIDAsync(int portPoolID, int? deviceRoleID = null, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var attachmentRoles = await this.UnitOfWork.AttachmentRoleRepository.GetAsync(q => q.PortPoolID == portPoolID, 
                includeProperties: p, 
                AsTrackable: false);

            if (deviceRoleID != null)
            {
                return attachmentRoles.Where(x => x.DeviceRoleAttachmentRoles.Where(y => y.DeviceRoleID == deviceRoleID).Any());
            }

            return attachmentRoles;
        }


        public async Task<AttachmentRole> GetByIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await this.UnitOfWork.AttachmentRoleRepository.GetAsync(q => q.AttachmentRoleID == id, 
                includeProperties: p, AsTrackable: false);

            return dbResult.SingleOrDefault();
        }

        public async Task<int> AddAsync(AttachmentRole attachmentRole)
        {
            this.UnitOfWork.AttachmentRoleRepository.Insert(attachmentRole);
            return await this.UnitOfWork.SaveAsync();
        }
 
        public async Task<int> UpdateAsync(AttachmentRole attachmentRole)
        {
            this.UnitOfWork.AttachmentRoleRepository.Update(attachmentRole);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(AttachmentRole attachmentRole)
        {
            this.UnitOfWork.AttachmentRoleRepository.Delete(attachmentRole);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}
