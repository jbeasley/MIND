﻿using SCM.Models;
using SCM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Services
{
    public interface IProviderDomainVifService : IBaseService
    {
        Task<Vif> GetByIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<IEnumerable<Vif>> GetAllByAttachmentIDAsync(int id, bool? deep = false, bool asTrackable = false);
        Task<Vif> AddAsync(int attachmentId, Mind.Models.RequestModels.ProviderDomainVifRequest request, bool stage = true, bool syncToNetwork = false);
        Task<Vif> UpdateAsync(int vifId, Mind.Models.RequestModels.ProviderDomainVifUpdate vifUpdate, bool stage = true, bool syncToNetwork = false);
        Task DeleteAsync(int vifId);
        Task SyncToNetworkPutAsync(int vifId);
    }
}
