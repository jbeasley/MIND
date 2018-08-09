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
    public interface IBaseAttachmentService : IBaseService
    {
        Task<Attachment> GetByNameAsync(string deviceName, string attachmentName, bool deep = false, bool asTrackable = false);
        Task<Attachment> GetByIDAsync(int id, bool deep = false, bool asTrackable = false);
        Task<Attachment> GetByInterfaceIDAsync(int interfaceID, bool deep = false, bool asTrackable = false);
        Task<IEnumerable<Attachment>> GetAllByRoutingInstanceIDAsync(int routingInstanceID, bool deep = false, bool asTrackable = false);
    }
}
