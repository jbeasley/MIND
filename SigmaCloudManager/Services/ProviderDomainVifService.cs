using System;
using System.Collections.Generic;
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
    public class ProviderDomainVifService : BaseVifService, IProviderDomainVifService
    {
        private readonly IProviderDomainVifDirector _director;
        private readonly IProviderDomainVifValidator _validator;

        public ProviderDomainVifService(IUnitOfWork unitOfWork, IMapper mapper,
            IProviderDomainVifDirector director, IProviderDomainVifValidator validator) : base (unitOfWork, mapper, validator)
        {
            _director = director;
            _validator = validator;
        }

        public async Task<Vif> AddAsync(int attachmentId, ProviderDomainVifRequest request)
        {
            var vif = await _director.BuildAsync(attachmentId, request);
            UnitOfWork.VifRepository.Insert(vif);
            await UnitOfWork.SaveAsync();

            return await base.GetByIDAsync(vif.VifID, deep: true, asTrackable: false);
        }

        public Task<Vif> UpdateAsync(int vifId, ProviderDomainVifUpdate vifUpdate)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int vifId)
        {
            await _validator.ValidateDeleteAsync(vifId);
            if (!_validator.IsValid) throw new ServiceValidationException();

            var vif = (from result in await UnitOfWork.VifRepository.GetAsync(q => q.VifID == vifId,
                includeProperties: "RoutingInstance.Vifs,RoutingInstance.Attachments,ContractBandwidthPool," +
                "Vlans,RoutingInstance.RoutingInstanceType,ContractBandwidthPool.Vifs,ContractBandwidthPool.Attachments," +
                "RoutingInstance.BgpPeers", AsTrackable: true)
                              select result)
                              .Single();

            if (vif.RoutingInstance != null)
            {
                if (vif.RoutingInstance.RoutingInstanceType.IsVrf)
                {
                    // Check if the current vif is the only vif using the routing instance and no 
                    // attachments are using the routing instance. If so delete the routing instance.
                    if (vif.RoutingInstance.Vifs.Count == 1 && !vif.RoutingInstance.Attachments.Any())
                    {
                        UnitOfWork.RoutingInstanceRepository.Delete(vif.RoutingInstance);
                    }
                }
            }

            if (vif.ContractBandwidthPool != null)
            {
                // Check if the current vif is the only vif using the contract bandwidth pool and no 
                // attachments are using the contract bandwidth pool. If so delete the contract bandwidth pool.
                if (vif.ContractBandwidthPool.Vifs.Count == 1 && !vif.ContractBandwidthPool.Attachments.Any())
                {
                    UnitOfWork.ContractBandwidthPoolRepository.Delete(vif.ContractBandwidthPool);
                }
            }

            UnitOfWork.VifRepository.Delete(vif);
            await UnitOfWork.SaveAsync();
        }
    }
}
