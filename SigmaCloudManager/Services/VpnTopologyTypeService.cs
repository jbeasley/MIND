using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public class VpnTopologyTypeService : BaseService, IVpnTopologyTypeService
    {
        public VpnTopologyTypeService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        private string Properties { get; } = "VpnProtocolType";

        public async Task<IEnumerable<VpnTopologyType>> GetAllAsync(bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.VpnTopologyTypeRepository.GetAsync(includeProperties: p, AsTrackable: false);
        }

        public async Task<IEnumerable<VpnTopologyType>> GetByVpnProtocolTypeIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.VpnTopologyTypeRepository.GetAsync(q => q.VpnProtocolTypeID == id,
                includeProperties: p, AsTrackable: false);
        }

        public async Task<VpnTopologyType> GetByIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await this.UnitOfWork.VpnTopologyTypeRepository.GetAsync(q => q.VpnTopologyTypeID == id,
                includeProperties: p, AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<VpnTopologyType> GetByNameAsync(string name, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await this.UnitOfWork.VpnTopologyTypeRepository.GetAsync(q => q.Name == name,
                includeProperties: p, AsTrackable: false);
            return dbResult.SingleOrDefault();
        }

        public async Task<int> AddAsync(VpnTopologyType vpnTopologyType)
        {
            this.UnitOfWork.VpnTopologyTypeRepository.Insert(vpnTopologyType);
            return await this.UnitOfWork.SaveAsync();
        }
 
        public async Task<int> UpdateAsync(VpnTopologyType vpnTopologyType)
        {
            this.UnitOfWork.VpnTopologyTypeRepository.Update(vpnTopologyType);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(VpnTopologyType vpnTopologyType)
        {
            this.UnitOfWork.VpnTopologyTypeRepository.Delete(vpnTopologyType);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}
