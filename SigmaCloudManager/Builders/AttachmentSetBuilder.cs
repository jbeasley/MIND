using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mind.Models.RequestModels;
using SCM.Data;
using SCM.Models;

namespace Mind.Builders
{
    /// <summary>
    /// Builder for attachment sets
    /// </summary>
    public class AttachmentSetBuilder : BaseBuilder, IAttachmentSetBuilder
    {
        protected internal AttachmentSet _attachmentSet;
        private readonly IAttachmentSetRoutingInstanceDirector _attachmentSetRoutingInstanceDirector;
        private readonly IProviderDomainIpNetworkInboundPolicyDirector _providerDomainIpNetworkInboundPolicyDirector;
        private readonly IProviderDomainIpNetworkOutboundPolicyDirector _providerDomainIpNetworkOutboundPolicyDirector;

        public AttachmentSetBuilder(IUnitOfWork unitOfWork,
            IAttachmentSetRoutingInstanceDirector attachmentSetRoutingInstanceDirector, 
            IProviderDomainIpNetworkInboundPolicyDirector providerDomainIpNetworkInboundPolicyDirector, 
            IProviderDomainIpNetworkOutboundPolicyDirector providerDomainIpNetworkOutboundPolicyDirector) : base(unitOfWork)
        {
            _attachmentSetRoutingInstanceDirector = attachmentSetRoutingInstanceDirector;
            _providerDomainIpNetworkInboundPolicyDirector = providerDomainIpNetworkInboundPolicyDirector;
            _providerDomainIpNetworkOutboundPolicyDirector = providerDomainIpNetworkOutboundPolicyDirector;

            _attachmentSet = new AttachmentSet
            {
                Name = Guid.NewGuid().ToString("N"),
                VpnAttachmentSets = new List<VpnAttachmentSet>(),
                VpnTenantCommunitiesIn = new List<VpnTenantCommunityIn>(),
                VpnTenantCommunitiesOut = new List<VpnTenantCommunityOut>(),
                VpnTenantIpNetworksIn = new List<VpnTenantIpNetworkIn>(),
                VpnTenantIpNetworksOut = new List<VpnTenantIpNetworkOut>(),
                VpnTenantCommunitiesRoutingInstance = new List<VpnTenantCommunityRoutingInstance>(),
                VpnTenantIpNetworkRoutingInstanceStaticRoutes = new List<VpnTenantIpNetworkRoutingInstanceStaticRoute>(),
                VpnTenantMulticastGroups = new List<VpnTenantMulticastGroup>()
            };
        }

        public virtual IAttachmentSetBuilder ForTenant(int? tenantId)
        {
            if (tenantId.HasValue) _args.Add(nameof(ForTenant), tenantId);
            return this;
        }

        public IAttachmentSetBuilder ForAttachmentSet(int? attachmentSetId)
        {
            if (attachmentSetId.HasValue) _args.Add(nameof(ForAttachmentSet), attachmentSetId);
            return this;
        }

        public virtual IAttachmentSetBuilder WithAttachmentRedundancy(string attachmentRedundancy)
        {
            if (!string.IsNullOrEmpty(attachmentRedundancy)) _args.Add(nameof(WithAttachmentRedundancy), attachmentRedundancy);
            return this;
        }

        public virtual IAttachmentSetBuilder WithLayer3(bool? isLayer3)
        {
            if (isLayer3.HasValue) _args.Add(nameof(WithLayer3), isLayer3.Value);
            return this;
        }

        public virtual IAttachmentSetBuilder WithMulticastVpnDomainType(string multicastVpnDomainType)
        {
            if (!string.IsNullOrEmpty(multicastVpnDomainType)) _args.Add(nameof(WithMulticastVpnDomainType), multicastVpnDomainType);
            return this;
        }

        public virtual IAttachmentSetBuilder WithRoutingInstances(List<RoutingInstanceForAttachmentSetRequest> routingInstanceRequests)
        {
            if (routingInstanceRequests != null) _args.Add(nameof(WithRoutingInstances), routingInstanceRequests);
            return this;
        }

        public virtual IAttachmentSetBuilder WithRegion(string region)
        {
            if (!string.IsNullOrEmpty(region)) _args.Add(nameof(WithRegion), region);
            return this;
        }

        public virtual IAttachmentSetBuilder WithSubRegion(string subregion)
        {
            if (!string.IsNullOrEmpty(subregion)) _args.Add(nameof(WithSubRegion), subregion);
            return this;
        }

        public virtual IAttachmentSetBuilder WithBgpIpNetworkInboundPolicy(List<VpnTenantIpNetworkInRequest> bgpIpNetworkInboundPolicy)
        {
            if (bgpIpNetworkInboundPolicy != null) _args.Add(nameof(WithBgpIpNetworkInboundPolicy), bgpIpNetworkInboundPolicy);
            return this;
        }

        public virtual IAttachmentSetBuilder WithBgpIpNetworkOutboundPolicy(List<VpnTenantIpNetworkOutRequest> bgpIpNetworkOutboundPolicy)
        {
            if (bgpIpNetworkOutboundPolicy != null) _args.Add(nameof(WithBgpIpNetworkOutboundPolicy), bgpIpNetworkOutboundPolicy);
            return this;
        }

        public virtual async Task<AttachmentSet> BuildAsync()
        {
            if (_args.ContainsKey(nameof(ForAttachmentSet)))
            {
                // Existing attachment set to update
                await SetAttachmentSetAsync();
            }
            else
            {
                // Create a new attachment set
                if (_args.ContainsKey(nameof(ForTenant))) await SetTenantAsync();
                if (_args.ContainsKey(nameof(WithLayer3))) _attachmentSet.IsLayer3 = (bool)_args[nameof(WithLayer3)];
                if (_args.ContainsKey(nameof(WithRegion))) await SetRegionAsync();
            }

            if (_args.ContainsKey(nameof(WithAttachmentRedundancy))) await SetAttachmentRedundancyAsync();
            if (_args.ContainsKey(nameof(WithSubRegion))) await SetSubRegionAsync();
            if (_args.ContainsKey(nameof(WithRoutingInstances))) await SetRoutingInstances();
            if (_args.ContainsKey(nameof(WithMulticastVpnDomainType))) await SetMulticastVpnDomainTypeAsync();
            if (_args.ContainsKey(nameof(WithBgpIpNetworkInboundPolicy))) await SetBgpIpNetworkInboundPolicy();
            if (_args.ContainsKey(nameof(WithBgpIpNetworkOutboundPolicy))) await SetBgpIpNetworkOutboundPolicy();

            _attachmentSet.Validate();
            return _attachmentSet;
        }

        protected internal virtual async Task SetTenantAsync()
        {
            var tenantId = (int)_args[nameof(ForTenant)];
            var tenant = (from result in await _unitOfWork.TenantRepository.GetAsync(
                     q =>
                          q.TenantID == tenantId,
                          AsTrackable: true)
                          select result)
                          .SingleOrDefault();

            _attachmentSet.Tenant = tenant ?? throw new BuilderBadArgumentsException($"The tenant with ID '{tenantId}' was not found.");
        }

        protected virtual internal async Task SetMulticastVpnDomainTypeAsync()
        {
            var multicastVpnDomainType = _args[nameof(WithMulticastVpnDomainType)].ToString();
            var mVpnDomainType = (from multicastVpnDomainTypes in await _unitOfWork.MulticastVpnDomainTypeRepository.GetAsync(
                                    q =>
                                         q.MvpnDomainType.ToString() == multicastVpnDomainType,
                                         AsTrackable: true)
                                  select multicastVpnDomainTypes)
                                         .SingleOrDefault();

            _attachmentSet.MulticastVpnDomainType = mVpnDomainType ??
                throw new BuilderBadArgumentsException($"The multicast vpn domain type argument {mVpnDomainType} is not valid.");
        }

        protected virtual internal async Task SetRegionAsync()
        {
            var regionName = _args[nameof(WithRegion)].ToString();
            var region = (from regions in await _unitOfWork.RegionRepository.GetAsync(
                       q =>
                         q.Name == regionName)
                          select regions)
                         .SingleOrDefault();

            _attachmentSet.Region = region ?? throw new BuilderBadArgumentsException($"The region argument {regionName} is not a valid region.");
        }

        protected virtual internal async Task SetSubRegionAsync()
        {
            var subregionName = _args[nameof(WithSubRegion)].ToString();

            // A value 'None' mean no subregion should be defined for the attachment set
            if (subregionName == "None")
            {
                _attachmentSet.SubRegion = null;
                _attachmentSet.SubRegionID = null;
            }
            else
            {
                var subregion = (from subRegions in await _unitOfWork.SubRegionRepository.GetAsync(
                              q =>
                                 q.Name == subregionName && q.RegionID == _attachmentSet.Region.RegionID,
                                 AsTrackable: true)
                                 select subRegions)
                                .SingleOrDefault();

                _attachmentSet.SubRegion = subregion ?? throw new BuilderBadArgumentsException($"The subregion argument {subregionName} is not a " +
                    $"valid subregion.");
            }
        }

        protected virtual internal async Task SetRoutingInstances()
        {
            var requests = (List<RoutingInstanceForAttachmentSetRequest>)_args[nameof(WithRoutingInstances)];
            var attachmentSetRoutingInstances = await _attachmentSetRoutingInstanceDirector.BuildAsync(this._attachmentSet, requests);
            _attachmentSet.AttachmentSetRoutingInstances = attachmentSetRoutingInstances;
        }

        protected virtual internal async Task SetBgpIpNetworkInboundPolicy()
        {
            var requests = (List<VpnTenantIpNetworkInRequest>)_args[nameof(WithBgpIpNetworkInboundPolicy)];
            var bgpIpNetworkInboundPolicy = await _providerDomainIpNetworkInboundPolicyDirector.BuildAsync(this._attachmentSet, requests);
            _attachmentSet.VpnTenantIpNetworksIn = bgpIpNetworkInboundPolicy;
        }

        protected virtual internal async Task SetBgpIpNetworkOutboundPolicy()
        {
            var requests = (List<VpnTenantIpNetworkOutRequest>)_args[nameof(WithBgpIpNetworkOutboundPolicy)];
            var bgpIpNetworkOutboundPolicy = await _providerDomainIpNetworkOutboundPolicyDirector.BuildAsync(this._attachmentSet, requests);
            _attachmentSet.VpnTenantIpNetworksOut = bgpIpNetworkOutboundPolicy;
        }

        protected virtual internal async Task SetAttachmentRedundancyAsync()
        {
            var attachmentRedundancyName = _args[nameof(WithAttachmentRedundancy)].ToString();
            var attachmentRedundancy = (from attachmentRedundancies in await _unitOfWork.AttachmentRedundancyRepository.GetAsync(
                                     q =>
                                        q.Name == attachmentRedundancyName,
                                        AsTrackable: true)
                                        select attachmentRedundancies)
                                        .SingleOrDefault();

            _attachmentSet.AttachmentRedundancy = attachmentRedundancy;
        }

        protected virtual internal async Task SetAttachmentSetAsync()
        {
            var attachmentSetId = (int)_args[nameof(ForAttachmentSet)];
            var attachmentSet = (from result in await _unitOfWork.AttachmentSetRepository.GetAsync(
                        x =>
                            x.AttachmentSetID == attachmentSetId,
                            AsTrackable: true,
                            query: q => q.IncludeValidationProperties())
                                 select result)
                            .SingleOrDefault();

            _attachmentSet = attachmentSet ?? throw new BuilderBadArgumentsException($"The attachment set with ID '{attachmentSetId}' was not found.");

        }
    }
}
