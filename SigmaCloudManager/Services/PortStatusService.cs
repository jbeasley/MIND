using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public class PortStatusService : BaseService, IPortStatusService
    {
        public PortStatusService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<PortStatus>> GetAllAsync()
        {
            return await this.UnitOfWork.PortStatusRepository.GetAsync(AsTrackable: false);
        }

        public async Task<PortStatus> GetByIDAsync(int id)
        {
            var dbResult = await this.UnitOfWork.PortStatusRepository.GetAsync(q => q.PortStatusID == id, 
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<int> AddAsync(PortStatus portStatus)
        {
            this.UnitOfWork.PortStatusRepository.Insert(portStatus);
            return await this.UnitOfWork.SaveAsync();
        }
 
        public async Task<int> UpdateAsync(PortStatus portStatus)
        {
            this.UnitOfWork.PortStatusRepository.Update(portStatus);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(PortStatus portStatus)
        {
            this.UnitOfWork.PortStatusRepository.Delete(portStatus);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}
