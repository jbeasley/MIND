using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public class PortPoolService : BaseService, IPortPoolService
    {
        public PortPoolService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        private string Properties { get; } = "PortRole";

        public async Task<IEnumerable<PortPool>> GetAllAsync(bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.PortPoolRepository.GetAsync(includeProperties: p, AsTrackable: false);
        }

        public async Task<IEnumerable<PortPool>> GetAllByPortRoleIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.PortPoolRepository.GetAsync(q => q.PortRoleID == id, includeProperties:p, AsTrackable: false);
        }

        public async Task<PortPool> GetByIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await this.UnitOfWork.PortPoolRepository.GetAsync(q => q.PortPoolID == id, includeProperties: p,
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<int> AddAsync(PortPool portPool)
        {
            this.UnitOfWork.PortPoolRepository.Insert(portPool);
            return await this.UnitOfWork.SaveAsync();
        }
 
        public async Task<int> UpdateAsync(PortPool portPool)
        {
            this.UnitOfWork.PortPoolRepository.Update(portPool);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(PortPool portPool)
        {
            this.UnitOfWork.PortPoolRepository.Delete(portPool);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}
