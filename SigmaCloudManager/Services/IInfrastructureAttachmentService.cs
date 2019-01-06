using System.Collections.Generic;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Models.RequestModels;
using SCM.Services;

namespace Mind.Services
{
    public interface IInfrastructureAttachmentService: IBaseService
    {
        Task<Attachment> GetByIDAsync(int attachmentId, bool? deep = false, bool asTrackable = false);
        Task<List<Attachment>> GetAllByDeviceIDAsync(int deviceId, bool? deep = false, bool asTrackable = false);
        Task<Attachment> AddAsync(int deviceId, InfrastructureAttachmentRequest request);
        Task<Attachment> UpdateAsync(int attachmentId, InfrastructureAttachmentUpdate update);
        Task DeleteAsync(int attachmentId);
    }
}
