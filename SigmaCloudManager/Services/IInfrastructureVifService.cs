using SCM.Models;
using SCM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Services
{
    public interface IInfrastructureVifService : IBaseService
    {
        Task<Vif> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<IEnumerable<Vif>> GetAllByAttachmentIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<Vif> AddAsync(int attachmentId, Mind.Models.RequestModels.InfrastructureVifRequest request);
        Task<Vif> UpdateAsync(int vifId, Mind.Models.RequestModels.InfrastructureVifUpdate vifUpdate);
        Task DeleteAsync(int vifId);
    }
}
