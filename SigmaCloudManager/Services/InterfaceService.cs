using System;
using System.Collections.Generic;
using System.Linq;
using SCM.Data;
using SCM.Models;
using System.Threading.Tasks;
using AutoMapper;

namespace SCM.Services
{
    public class InterfaceService : BaseService, IInterfaceService
    {
        public InterfaceService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        private string Properties { get; } = "Device,"
            + "Attachment.Tenant,"
            + "Ports.PortBandwidth";

        public async Task<Interface> GetByIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await UnitOfWork.InterfaceRepository.GetAsync(q => q.InterfaceID == id,
                includeProperties: p,
                AsTrackable: false);

            return dbResult.SingleOrDefault();
        }

        public async Task<IEnumerable<Interface>> GetAllByDeviceIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await UnitOfWork.InterfaceRepository.GetAsync(q => q.DeviceID == id, 
                includeProperties: p,
                AsTrackable: false);
        }

        public async Task<IEnumerable<Interface>> GetAllByAttachmentIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await UnitOfWork.InterfaceRepository.GetAsync(q => q.AttachmentID == id,
                includeProperties: p,
                AsTrackable: false);
        }

        public async Task<int> AddAsync(Interface iface)
        {
            this.UnitOfWork.InterfaceRepository.Insert(iface);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> UpdateAsync(Interface iface)
        {
            this.UnitOfWork.InterfaceRepository.Update(iface);

            // Find the Attachment where the Interface is used and flag
            // as requiring sync with the network
            
            var dbResult = await UnitOfWork.AttachmentRepository.GetAsync(x => x.Interfaces.Where(y => y.InterfaceID == iface.InterfaceID).Any());
            var attachment = dbResult.SingleOrDefault();

            if (attachment != null) { 
                attachment.RequiresSync = true;
                UnitOfWork.AttachmentRepository.Update(attachment);
            }

            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(Interface iface)
        {
            this.UnitOfWork.InterfaceRepository.Delete(iface);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}