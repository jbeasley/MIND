using System;
using System.Collections.Generic;
using System.Linq;
using SCM.Data;
using SCM.Models;
using SCM.Models.NetModels;
using System.Threading.Tasks;
using AutoMapper;

namespace SCM.Services
{
    public class PortService : BaseService, IPortService
    {
        public PortService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        private string Properties { get; } = "Device.Location,"
                + "Device.Tenant,"
                + "Tenant,"
                + "Interface.Attachment,"
                + "PortBandwidth,"
                + "PortConnector,"
                + "PortStatus,"
                + "PortSfp,"
                + "PortPool.PortRole";

        public async Task<Port> GetByIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await UnitOfWork.PortRepository.GetAsync(q => q.ID == id,
                includeProperties: p,
                AsTrackable: false);

            return dbResult.SingleOrDefault();
        }

        public async Task<IEnumerable<Port>> GetAllByDeviceIDAsync(int id, int? portPoolID = null, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var results = await UnitOfWork.PortRepository.GetAsync(q => q.DeviceID == id, 
                includeProperties: p,
                AsTrackable: false);

            if (portPoolID != null)
            {
                return results.Where(x => x.PortPoolID == portPoolID);
            }

            return results;
        }

        public async Task<IEnumerable<Port>> GetAllByInterfaceIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await UnitOfWork.PortRepository.GetAsync(q => q.InterfaceID == id,
                includeProperties: p,
                AsTrackable: false);
        }

        public async Task<IEnumerable<Port>> GetAllByAttachmentIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await UnitOfWork.PortRepository.GetAsync(q => q.Interface.AttachmentID == id,
                includeProperties: p,
                AsTrackable: false);
        }

        public async Task<int> AddAsync(Port port)
        {
            this.UnitOfWork.PortRepository.Insert(port);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> UpdateAsync(Port port)
        {
            this.UnitOfWork.PortRepository.Update(port);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(Port port)
        {
            this.UnitOfWork.PortRepository.Delete(port);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}