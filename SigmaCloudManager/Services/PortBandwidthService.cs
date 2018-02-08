using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public class PortBandwidthService : BaseService, IPortBandwidthService
    {
        public PortBandwidthService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<PortBandwidth>> GetAllAsync()
        {
            return await this.UnitOfWork.PortBandwidthRepository.GetAsync(AsTrackable: false);
        }
   
        public async Task<PortBandwidth> GetAsync(int bandwidth)
        {
            var dbResult = await this.UnitOfWork.PortBandwidthRepository.GetAsync(q => q.BandwidthGbps == bandwidth,
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<PortBandwidth> GetByIDAsync(int id)
        {
            var dbResult = await this.UnitOfWork.PortBandwidthRepository.GetAsync(q => q.PortBandwidthID == id, 
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<int> AddAsync(PortBandwidth portBandwidth)
        {
            this.UnitOfWork.PortBandwidthRepository.Insert(portBandwidth);
            return await this.UnitOfWork.SaveAsync();
        }
 
        public async Task<int> UpdateAsync(PortBandwidth portBandwidth)
        {
            this.UnitOfWork.PortBandwidthRepository.Update(portBandwidth);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(PortBandwidth portBandwidth)
        {
            this.UnitOfWork.PortBandwidthRepository.Delete(portBandwidth);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}
