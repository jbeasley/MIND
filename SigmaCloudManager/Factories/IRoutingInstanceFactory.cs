using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Models.RequestModels;

namespace SCM.Factories
{
    public interface IRoutingInstanceFactory
    {
        Task<FactoryResult> NewAsync(RoutingInstance routingInstance);
        Task<FactoryResult> NewAsync(AttachmentRequest request);
        Task<FactoryResult> NewAsync(VifRequest request);
        Task<FactoryResult> NewAsync(AttachmentUpdate update);
        Task<FactoryResult> NewAsync(VifUpdate update);
    }
}
