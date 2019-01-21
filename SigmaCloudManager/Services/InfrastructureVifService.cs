﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Mind.Builders;
using Mind.Models.RequestModels;
using SCM.Data;
using SCM.Models;
using Mind.Directors;

namespace Mind.Services
{
    public class InfrastructureVifService : BaseVifService, IInfrastructureVifService
    {
        private readonly IInfrastructureVifDirector _director;
        private readonly IDestroyable<Vif> _destroyableVifDirector;

        public InfrastructureVifService(IUnitOfWork unitOfWork, IMapper mapper,
            IInfrastructureVifDirector director,
            IDestroyable<Vif> destroyableVifDirector) : base (unitOfWork, mapper)
        {
            _director = director;
            _destroyableVifDirector = destroyableVifDirector;
        }

        public async Task<Vif> AddAsync(int attachmentId, InfrastructureVifRequest request)
        {
            var vif = await _director.BuildAsync(attachmentId, request);
            UnitOfWork.VifRepository.Insert(vif);
            await UnitOfWork.SaveAsync();

            return await base.GetByIDAsync(vif.VifID, deep: true, asTrackable: false);
        }

        public async Task<Vif> UpdateAsync(int vifId, InfrastructureVifUpdate update)
        {
            var vif = (from result in await UnitOfWork.VifRepository.GetAsync(
                    q =>
                       q.VifID == vifId,
                       query: q => q.Include(x => x.RoutingInstance.Attachments)
                                    .Include(x => x.RoutingInstance.Vifs)
                                    .Include(x => x.RoutingInstance.RoutingInstanceType)
                                    .Include(x => x.ContractBandwidthPool.Attachments)
                                    .Include(x => x.ContractBandwidthPool.Vifs),
                       AsTrackable: false)
                       select result)
                       .Single();

            var updatedVif = await _director.UpdateAsync(vifId, update);

            // Cleanup old contract bandwidth pool is there are no attachments or vifs (other than the current vif) which are using it
            if (vif.ContractBandwidthPool != null && 
                vif.ContractBandwidthPool.ContractBandwidthPoolID != updatedVif.ContractBandwidthPool.ContractBandwidthPoolID)
            {
                if (!vif.ContractBandwidthPool.Attachments.Any() &&
                    !vif.ContractBandwidthPool.Vifs.Any(x => x.VifID != vifId))
                {
                    await UnitOfWork.ContractBandwidthPoolRepository.DeleteAsync(vif.ContractBandwidthPool.ContractBandwidthPoolID);
                }
            }

            // Cleanup old routing instance if there are no attachment or vifs (other than the current vif) which are using it.
            if (vif.RoutingInstance != null && 
                vif.RoutingInstance.RoutingInstanceID != updatedVif.RoutingInstance.RoutingInstanceID && 
                vif.RoutingInstance.RoutingInstanceType.IsInfrastructureVrf)
            {
                if (!vif.RoutingInstance.Attachments.Any() &&
                    !vif.RoutingInstance.Vifs.Any(x => x.VifID != vifId))
                {
                    await UnitOfWork.RoutingInstanceRepository.DeleteAsync(vif.RoutingInstance.RoutingInstanceID);
                }
            }

            await UnitOfWork.SaveAsync();
            return await GetByIDAsync(vifId, deep: true, asTrackable: false);
        }

        public async Task DeleteAsync(int vifId)
        {
            var vif = (from result in await UnitOfWork.VifRepository.GetAsync(
                    q => 
                       q.VifID == vifId,
                       query: q => q.IncludeDeleteValidationProperties(),          
                       AsTrackable: true)
                       select result)
                       .Single();

            await _destroyableVifDirector.DestroyAsync(vif);
            await UnitOfWork.SaveAsync();
        }
    }
}
