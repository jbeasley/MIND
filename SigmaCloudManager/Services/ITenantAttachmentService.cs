using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Models.RequestModels;
using SCM.Data;
using System.Linq.Expressions;

namespace SCM.Services
{
    public interface ITenantAttachmentService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<Attachment> GetByNameAsync(string deviceName, string attachmentName, bool includeProperties = true);
        Task<Attachment> GetByIDAsync(int id, bool includeProperties = true);
        Task<Attachment> GetByInterfaceIDAsync(int intefaceID, bool includeProperties = true);
        Task<IEnumerable<Attachment>> GetAllByRoutingInstanceIDAsync(int vrfID, bool includeProperties = true);
        Task<IEnumerable<Attachment>> GetAllByVpnIDAsync(int vpnID, bool includeProperties = true);
        Task<IEnumerable<Attachment>> GetAllByTenantIDAsync(int tenantID, bool? roleRequireSyncToNetwork = null,
            bool? requiresSync = null, bool? created = null, bool? showRequiresSyncAlert = null, bool? showCreatedAlert = null,
            bool includeProperties = true);
        Task<ServiceResult> AddAsync(AttachmentRequest attachmentRequest);
        Task<int> UpdateAsync(IEnumerable<Attachment> attachments);
        Task<ServiceResult> UpdateAttachmentAsync(AttachmentUpdate update);
        Task<ServiceResult> UpdateAttachmentPortAsync(AttachmentPortUpdate update);
        Task<ServiceResult> DeleteAsync(Attachment attachment);
        Task<IEnumerable<ServiceResult>> CheckNetworkSyncAsync(IEnumerable<Attachment> attachments, IProgress<ServiceResult> progress);
        Task<ServiceResult> CheckNetworkSyncAsync(Attachment attachment);
        Task<IEnumerable<ServiceResult>> SyncToNetworkAsync(IEnumerable<Attachment> attachments, IProgress<ServiceResult> progress);
        Task<ServiceResult> SyncToNetworkAsync(Attachment attachment);
        Task<ServiceResult> DeleteFromNetworkAsync(Attachment attachment);
    }
}
