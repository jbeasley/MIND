using System;
using System.Collections.Generic;
using System.Linq;
using SCM.Data;
using SCM.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SCM.Models.RequestModels;
using AutoMapper;

namespace SCM.Services
{
    public class RoutingInstanceTypeService : BaseService, IRoutingInstanceTypeService
    {
        public RoutingInstanceTypeService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<RoutingInstanceType> GetByIDAsync(int id, bool includeProperties = true)
        {
            var dbResult = await UnitOfWork.RoutingInstanceTypeRepository.GetAsync(q => q.RoutingInstanceTypeID == id,
                AsTrackable: false);

            return dbResult.SingleOrDefault();
        }

        public async Task<RoutingInstanceType> GetByTypeAsync(RoutingInstanceTypeEnum routingInstanceTypeEnum, bool includeProperties = true)
        {
            var dbResult= await UnitOfWork.RoutingInstanceTypeRepository.GetAsync(q => q.Type == routingInstanceTypeEnum,
                AsTrackable: false);

            return dbResult.SingleOrDefault();
        }
    }
}