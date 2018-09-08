using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;

namespace SCM.Services
{
    public class VlanService : BaseService, IVlanService
    {

        private string Properties { get; } = "Vif.Attachment.Interfaces.Ports," +
                                             "Vif.Attachment.Device," +
                                             "Interface.Ports";

        public VlanService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<Vlan> GetByIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await this.UnitOfWork.VlanRepository.GetAsync(q => q.VlanID == id, AsTrackable: false, includeProperties: p);
            return dbResult.SingleOrDefault();
        }

        public async Task<IEnumerable<Vlan>> GetAllAsync(bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.VlanRepository.GetAsync(AsTrackable: false, includeProperties: p);
        }

        public async Task<IEnumerable<Vlan>> GetAllByVifIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.VlanRepository.GetAsync(q => q.VifID == id,
                includeProperties: p, AsTrackable: false);
        }

        public async Task<int> AddAsync(Vlan vlan)
        {
            this.UnitOfWork.VlanRepository.Insert(vlan);
            return await this.UnitOfWork.SaveAsync();
        }
 
        public async Task<int> UpdateAsync(Vlan vlan)
        {
            this.UnitOfWork.VlanRepository.Update(vlan);

            var dbResult = await this.UnitOfWork.VifRepository.GetAsync(q => q.VifID == vlan.VifID, 
                includeProperties: "VifRole,Attachment.AttachmentRole,Attachment.Device.DeviceRole");
            var vif = dbResult.Single();

            this.UnitOfWork.VifRepository.Update(vif);

            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(Vlan vlan)
        {
            this.UnitOfWork.VlanRepository.Delete(vlan);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}
