using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using Mind.Models.RequestModels;
using SCM.Services;

namespace Mind.Services
{
    public interface IProviderDomainInterfaceService : IBaseService
    { 
        Task<Interface> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<IEnumerable<Interface>> GetAllByAttachmentIDAsync(int attachmentId, bool? deep = false, bool asTrackable = false);
    }
}
