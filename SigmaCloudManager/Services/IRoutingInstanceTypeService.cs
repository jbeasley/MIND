using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Models;
using SCM.Data;
using SCM.Models.RequestModels;

namespace SCM.Services
{
    public interface IRoutingInstanceTypeService
    {
        IUnitOfWork UnitOfWork { get; }
        Task<RoutingInstanceType> GetByIDAsync(int id, bool includeProperties = true);
        Task<RoutingInstanceType> GetByTypeAsync(RoutingInstanceTypeEnum routingInstanceTypeEnum, bool includeProperties = true);
    }
}
