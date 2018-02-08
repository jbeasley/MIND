using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public class PortConnectorService : BaseService, IPortConnectorService
    {
        public PortConnectorService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<PortConnector>> GetAllAsync()
        {
            return await this.UnitOfWork.PortConnectorRepository.GetAsync(AsTrackable: false);
        }

        public async Task<PortConnector> GetByIDAsync(int id)
        {
            var dbResult = await this.UnitOfWork.PortConnectorRepository.GetAsync(q => q.PortConnectorID == id, 
                AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<int> AddAsync(PortConnector portConnector)
        {
            this.UnitOfWork.PortConnectorRepository.Insert(portConnector);
            return await this.UnitOfWork.SaveAsync();
        }
 
        public async Task<int> UpdateAsync(PortConnector portConnector)
        {
            this.UnitOfWork.PortConnectorRepository.Update(portConnector);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(PortConnector portConnector)
        {
            this.UnitOfWork.PortConnectorRepository.Delete(portConnector);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}
