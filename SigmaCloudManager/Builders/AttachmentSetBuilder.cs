using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly IAttachmentSetRoutingInstanceBuilder _attachmentSetRoutingInstanceBuilder;

        public AttachmentSetBuilder(IUnitOfWork unitOfWork, 
            IAttachmentSetRoutingInstanceBuilder attachmentSetRoutingInstanceBuilder) : base(unitOfWork)
        {
            _attachmentSetRoutingInstanceBuilder = attachmentSetRoutingInstanceBuilder;
            _attachmentSet = new AttachmentSet
            {
                Name = Guid.NewGuid().ToString("N")
            };
        }

        public virtual IAttachmentSetBuilder ForTenant(int tenantId)
        {
            _args.Add(nameof(ForTenant),tenantId);
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
            if (routingInstanceRequests.Any()) _args.Add(nameof(WithRoutingInstances), routingInstanceRequests);
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

        public virtual async Task<AttachmentSet> BuildAsync()
        {
            if (_args.ContainsKey(nameof(ForTenant))) await SetTenantAsync();
            if (_args.ContainsKey(nameof(WithLayer3))) _attachmentSet.IsLayer3 = (bool)_args[nameof(WithLayer3)];
            if (_args.ContainsKey(nameof(WithAttachmentRedundancy))) await SetAttachmentRedundancyAsync();
            if (_args.ContainsKey(nameof(WithRegion))) await SetRegionAsync();
            if (_args.ContainsKey(nameof(WithSubRegion))) await SetSubRegionAsync();
            if (_args.ContainsKey(nameof(WithRoutingInstances))) await SetRoutingInstances();
            if (_args.ContainsKey(nameof(WithMulticastVpnDomainType))) await SetMulticastVpnDomainTypeAsync();

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
            var multicastVpnDomainTypeName = _args[nameof(WithMulticastVpnDomainType)].ToString();
            var multicastVpnDomainType = (from multicastVpnDomainTypes in await _unitOfWork.MulticastVpnDomainTypeRepository.GetAsync(
                        q => 
                            q.Name == multicastVpnDomainTypeName,
                            AsTrackable: true)
                            select multicastVpnDomainTypes)
                            .SingleOrDefault();

            _attachmentSet.MulticastVpnDomainType = multicastVpnDomainType ?? 
                throw new BuilderBadArgumentsException($"The multicast vpn domain type argument {multicastVpnDomainType} is not valid.");
        }

        protected virtual internal async Task SetRegionAsync()
        {
            var regionName = _args[nameof(WithRegion)].ToString();
            var region = (from regions in await _unitOfWork.RegionRepository.GetAsync(q => q.Name == regionName)
                         select regions)
                         .SingleOrDefault();

            _attachmentSet.Region = region ?? throw new BuilderBadArgumentsException($"The region argument {regionName} is not a valid region.");
        }

        protected virtual internal async Task SetSubRegionAsync()
        {
            var subregionName = _args[nameof(WithSubRegion)].ToString();
            var subregion = (from subRegions in await _unitOfWork.SubRegionRepository.GetAsync(
                          q => 
                             q.Name == subregionName && q.RegionID == _attachmentSet.Region.RegionID,
                             AsTrackable: true)
                             select subRegions)
                            .SingleOrDefault();

            _attachmentSet.SubRegion = subregion ?? throw new BuilderBadArgumentsException($"The subregion argument {subregionName} is not a valid subregion.");
        }

        protected virtual internal async Task SetRoutingInstances()
        {
            var requests = (List<RoutingInstanceForAttachmentSetRequest>)_args[nameof(WithRoutingInstances)];
            var attachmentSetRoutingInstances = await Task.WhenAll(requests.Select(x => _attachmentSetRoutingInstanceBuilder
                                                                                        .ForAttachmentSet(_attachmentSet)
                                                                                        .WithRoutingInstance(x.RoutingInstanceName)
                                                                                        .WithAdvertisedIpRoutingPreference(x.AdvertisedIpRoutingPreference)
                                                                                        .WithLocalIpRoutingPreference(x.LocalIpRoutingPreference)
                                                                                        .WithMulticastDesignatedRouterPreference(x.MulticastDesignatedRouterPreference)
                                                                                        .BuildAsync()));

            _attachmentSet.AttachmentSetRoutingInstances = attachmentSetRoutingInstances;
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

        /// <summary>
        /// Validate the state of the attachment set
        /// </summary>
        protected virtual internal void Validate()
        {
            if (_attachmentSet.AttachmentRedundancy == null)  throw new BuilderBadArgumentsException($"The attachment redundancy of the attachment set " +
                $"is not valid.");

            if (_attachmentSet.AttachmentSetRoutingInstances.Any())
            {
                if (_attachmentSet.AttachmentRedundancy.AttachmentRedundancyType == AttachmentRedundancyTypeEnum.Bronze)
                {
                    if (_attachmentSet.AttachmentSetRoutingInstances.Count != 1)
                        throw new BuilderIllegalStateException($"Attachment set '{_attachmentSet.Name}' requires 1 routing instance association.");
                }
                else if (_attachmentSet.AttachmentRedundancy.AttachmentRedundancyType == AttachmentRedundancyTypeEnum.Silver)
                {
                    if (_attachmentSet.AttachmentSetRoutingInstances.Count != 2)
                        throw new BuilderIllegalStateException($"Attachment set '{_attachmentSet.Name}' requires 2 routing instance associations.");
                }
                else if (_attachmentSet.AttachmentRedundancy.AttachmentRedundancyType == AttachmentRedundancyTypeEnum.Gold)
                {
                    if (_attachmentSet.AttachmentSetRoutingInstances.Count != 2)
                        throw new BuilderIllegalStateException($"Attachment set '{_attachmentSet.Name}' requires 2 routing instance associations.");
                }
            }
        }
    }
}
