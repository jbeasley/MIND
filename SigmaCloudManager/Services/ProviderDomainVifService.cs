﻿using System;
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
        private readonly IProviderDomainVifUpdateDirector _updateDirector;
        private readonly IProviderDomainVifValidator _validator;

        public ProviderDomainVifService(IUnitOfWork unitOfWork, IMapper mapper,
            IProviderDomainVifDirector director, 
            IProviderDomainVifUpdateDirector updateDirector, IProviderDomainVifValidator validator) : base (unitOfWork, mapper, validator)
        {
            _director = director;
            _updateDirector = updateDirector;
            _validator = validator;
        }

        public async Task<Vif> AddAsync(int attachmentId, ProviderDomainVifRequest request)
        {
            var vif = await _director.BuildAsync(attachmentId, request);
            UnitOfWork.VifRepository.Insert(vif);
            await UnitOfWork.SaveAsync();

            return await base.GetByIDAsync(vif.VifID, deep: true, asTrackable: false);
        }

        public async Task<Vif> UpdateAsync(int vifId, ProviderDomainVifUpdate update)
        {
            var vif = await GetByIDAsync(vifId, asTrackable: false);

            // Remember old routing instance ID and contract bandwidth pool ID for later removal checks
            var oldRoutingInstanceID = vif.RoutingInstanceID;
            var oldContractBandwidthPoolID = vif.ContractBandwidthPoolID;

            var updatedVif = await _updateDirector.UpdateAsync(vifId, update);

            // Cleanup old contract bandwidth pool is there are no attachments or vifs (other than the current vif) which are using it
            if (oldContractBandwidthPoolID != null && oldContractBandwidthPoolID != updatedVif.ContractBandwidthPoolID)
            {
                var oldContractBandwidthPool = (from contractBandwidthPools in await UnitOfWork.ContractBandwidthPoolRepository.GetAsync(
                        x =>
                            x.ContractBandwidthPoolID == oldContractBandwidthPoolID,
                            includeProperties: "Attachments,Vifs", AsTrackable: true)
                                                select contractBandwidthPools)
                                               .Single();

                if (!oldContractBandwidthPool.Attachments.Any() &&
                    oldContractBandwidthPool.Vifs.Where(x => x.VifID == vifId).Count() == vif.ContractBandwidthPool.Vifs.Count)
                {
                    UnitOfWork.ContractBandwidthPoolRepository.Delete(oldContractBandwidthPool);
                }
            }

            // Cleanup old routing instance if there are no attachment or vifs (other than the current vif) which are using it.
            if (oldRoutingInstanceID != null && oldRoutingInstanceID != updatedVif.RoutingInstanceID)
            {
                var oldRoutingInstance = (from routingInstances in await UnitOfWork.RoutingInstanceRepository.GetAsync(
                    x =>
                        x.RoutingInstanceID == oldRoutingInstanceID,
                        includeProperties: "Attachments,Vifs", 
                        AsTrackable: true)
                                          select routingInstances)
                                          .Single();

                if (!oldRoutingInstance.Attachments.Any() && 
                    oldRoutingInstance.Vifs.Where(x => x.VifID == vifId).Count() == oldRoutingInstance.Vifs.Count)
                {
                    UnitOfWork.RoutingInstanceRepository.Delete(oldRoutingInstance);
                }
            }

            await UnitOfWork.SaveAsync();
            return await GetByIDAsync(vifId, deep: true, asTrackable: false);
        }

        public async Task DeleteAsync(int vifId)
        {
            await _validator.ValidateDeleteAsync(vifId);
            if (!_validator.IsValid) throw new ServiceValidationException();

            var vif = (from result in await UnitOfWork.VifRepository.GetAsync(q => q.VifID == vifId,
                includeProperties: "RoutingInstance.Vifs," +
                "RoutingInstance.Attachments," +
                "ContractBandwidthPool," +
                "Vlans," +
                "RoutingInstance.RoutingInstanceType," +
                "ContractBandwidthPool.Vifs," +
                "ContractBandwidthPool.Attachments," +
                "RoutingInstance.BgpPeers", 
                AsTrackable: true)
                              select result)
                              .Single();

            if (vif.RoutingInstance != null)
            {
                if (vif.RoutingInstance.RoutingInstanceType.Type == RoutingInstanceTypeEnum.TenantFacingVrf)
                {
                    // Check if the current vif is the only vif using the routing instance and no 
                    // attachments are using the routing instance. If so delete the routing instance.
                    if (vif.RoutingInstance.Vifs.Where(x => x.VifID == vifId).Count() == vif.RoutingInstance.Vifs.Count && 
                        !vif.RoutingInstance.Attachments.Any())
                    {
                        UnitOfWork.RoutingInstanceRepository.Delete(vif.RoutingInstance);
                    }
                }
            }

            if (vif.ContractBandwidthPool != null)
            {
                // Check if the current vif is the only vif using the contract bandwidth pool and no 
                // attachments are using the contract bandwidth pool. If so delete the contract bandwidth pool.
                if (vif.ContractBandwidthPool.Vifs.Where(x => x.VifID == vifId).Count() == vif.ContractBandwidthPool.Vifs.Count && 
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
