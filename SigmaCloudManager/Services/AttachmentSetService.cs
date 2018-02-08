using System;
using System.Collections.Generic;
using System.Linq;
using SCM.Data;
using SCM.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SCM.Services
{
    public class AttachmentSetService : BaseService, IAttachmentSetService
    {
        public AttachmentSetService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        private string Properties { get; } = "Tenant,"
                + "SubRegion,"
                + "Region,"
                + "AttachmentRedundancy,"
                + "AttachmentSetRoutingInstances.RoutingInstance.Device.Plane,"
                + "AttachmentSetRoutingInstances.RoutingInstance.Tenant,"
                + "AttachmentSetRoutingInstances.RoutingInstance.Attachments.Interfaces.Ports,"
                + "AttachmentSetRoutingInstances.RoutingInstance.Vifs.Attachment.Interfaces.Ports,"
                + "MulticastVpnDomainType,"
                + "VpnTenantMulticastGroups.TenantMulticastGroup";

        public async Task<IEnumerable<AttachmentSet>> GetAllAsync(bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await this.UnitOfWork.AttachmentSetRepository.GetAsync(includeProperties: p,
                AsTrackable: false);
        }

        public async Task<AttachmentSet> GetByIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            var dbResult = await UnitOfWork.AttachmentSetRepository.GetAsync(q => q.AttachmentSetID == id,
                includeProperties: p,
                AsTrackable: false);

            return dbResult.SingleOrDefault();
        }

        public async Task<IEnumerable<AttachmentSet>> GetAllByVpnIDAsync(int id, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await UnitOfWork.AttachmentSetRepository.GetAsync(q => q.VpnAttachmentSets
                .Select(r => r.VpnID == id)
                .Count() > 0, includeProperties:p,
                AsTrackable: false);
        }

        public async Task<IEnumerable<AttachmentSet>> GetAllByTenantAsync(Tenant tenant, bool includeProperties = true)
        {
            var p = includeProperties ? Properties : string.Empty;
            return await UnitOfWork.AttachmentSetRepository.GetAsync(q => q.Tenant.TenantID == tenant.TenantID,
                includeProperties: p,
                AsTrackable: false);
        }

        public async Task<int> AddAsync(AttachmentSet attachmentSet)
        {
            this.UnitOfWork.AttachmentSetRepository.Insert(attachmentSet);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> UpdateAsync(AttachmentSet attachmentSet)
        {
            this.UnitOfWork.AttachmentSetRepository.Update(attachmentSet);
            return await this.UnitOfWork.SaveAsync();
        }

        public async Task<int> DeleteAsync(AttachmentSet attachmentSet)
        {
            this.UnitOfWork.AttachmentSetRepository.Delete(attachmentSet);
            return await this.UnitOfWork.SaveAsync();
        }
    }
}