using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Mind.Builders;
using Mind.Models.RequestModels;
using Mind.Validators;
using SCM.Data;
using SCM.Models;

namespace Mind.Services
{
    public class TenantDomainVifService : BaseVifService, ITenantDomainVifService
    {
        private readonly ITenantDomainVifDirector _director;
        private readonly ITenantDomainVifUpdateDirector _updateDirector;

        public TenantDomainVifService(IUnitOfWork unitOfWork, IMapper mapper,
            ITenantDomainVifDirector director, 
            ITenantDomainVifUpdateDirector updateDirector) : base (unitOfWork, mapper)
        {
            _director = director;
            _updateDirector = updateDirector;
        }

        public async Task<Vif> AddAsync(int attachmentId, TenantDomainVifRequest request)
        {
            var vif = await _director.BuildAsync(attachmentId, request);
            UnitOfWork.VifRepository.Insert(vif);
            await UnitOfWork.SaveAsync();

            return await base.GetByIDAsync(vif.VifID, deep: true, asTrackable: false);
        }

        public async Task<Vif> UpdateAsync(int vifId, TenantDomainVifUpdate update)
        {
            var vif = (from result in await UnitOfWork.VifRepository.GetAsync(
                    q =>
                       q.VifID == vifId,
                       query: q => q.Include(x => x.ContractBandwidthPool.Attachments)
                                    .Include(x => x.ContractBandwidthPool.Vifs),
                       AsTrackable: false)
                       select result)
                       .Single();

            var updatedVif = await _updateDirector.UpdateAsync(vifId, update);

            // Cleanup old contract bandwidth pool is there are no attachments or vifs (other than the current vif) which are using it
            if (vif.ContractBandwidthPool != null && 
                vif.ContractBandwidthPool.ContractBandwidthPoolID != updatedVif.ContractBandwidthPool.ContractBandwidthPoolID)
            {
                if (!vif.ContractBandwidthPool.Attachments.Any() &&
                    !vif.ContractBandwidthPool.Vifs.Any(x => x.VifID != vifId))
                {
                    await UnitOfWork.ContractBandwidthPoolRepository.DeleteAsync(vif.ContractBandwidthPoolID);
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

            if (vif.ContractBandwidthPool != null)
            {
                // Check if the current vif is the only vif using the contract bandwidth pool and no 
                // attachments are using the contract bandwidth pool. If so delete the contract bandwidth pool.
                if (!vif.ContractBandwidthPool.Vifs.Any(x => x.VifID != vifId) && 
                    !vif.ContractBandwidthPool.Attachments.Any())
                {
                    UnitOfWork.ContractBandwidthPoolRepository.Delete(vif.ContractBandwidthPool);
                }
            }

            UnitOfWork.VifRepository.Delete(vif);
            await UnitOfWork.SaveAsync();
        }
    }
}
