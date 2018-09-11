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
                Name = Guid.NewGuid().ToString("N"),
                VpnAttachmentSets = new List<VpnAttachmentSet>(),
                VpnTenantCommunitiesIn = new List<VpnTenantCommunityIn>(),
                VpnTenantCommunitiesOut = new List<VpnTenantCommunityOut>(),
                VpnTenantIpNetworksIn = new List<VpnTenantIpNetworkIn>(),
                VpnTenantIpNetworksOut = new List<VpnTenantIpNetworkOut>(),
                VpnTenantCommunitiesRoutingInstance = new List<VpnTenantCommunityRoutingInstance>(),
                VpnTenantIpNetworkStaticRoutesRoutingInstance = new List<VpnTenantIpNetworkStaticRouteRoutingInstance>(),
                VpnTenantMulticastGroups = new List<VpnTenantMulticastGroup>()
            };
        }

        public virtual IAttachmentSetBuilder ForTenant(int? tenantId)
        {
            if (tenantId.HasValue) _args.Add(nameof(ForTenant), tenantId);
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
            if (_attachmentSet.AttachmentRedundancy == null) throw new BuilderBadArgumentsException($"The attachment redundancy of the attachment set " +
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

                    var firstSilverLocation = _attachmentSet.AttachmentSetRoutingInstances.First().RoutingInstance.Device.Location;
                    var secondSilverLocation = _attachmentSet.AttachmentSetRoutingInstances.ElementAt(1).RoutingInstance.Device.Location;
                    if (firstSilverLocation.LocationID != secondSilverLocation.LocationID)
                    {
                        throw new BuilderIllegalStateException($"The location of each routing instance in attachment set '{_attachmentSet.Name}' " +
                            $"must be the same because the attachment set is configured for silver-level redundancy.");
                    }
                }
                else if (_attachmentSet.AttachmentRedundancy.AttachmentRedundancyType == AttachmentRedundancyTypeEnum.Gold)
                {
                    if (_attachmentSet.AttachmentSetRoutingInstances.Count != 2)
                        throw new BuilderIllegalStateException($"Attachment set '{_attachmentSet.Name}' requires 2 routing instance associations.");

                    if (_attachmentSet.SubRegion == null) throw new BuilderIllegalStateException("A subregion must be defined for attachment set " +
                        $"'{_attachmentSet.Name}' with gold-level redundancy");

                    var firstGoldLocation = _attachmentSet.AttachmentSetRoutingInstances.First().RoutingInstance.Device.Location;
                    var secondGoldLocation = _attachmentSet.AttachmentSetRoutingInstances.ElementAt(1).RoutingInstance.Device.Location;
                    if (firstGoldLocation.SubRegionID != secondGoldLocation.SubRegionID)
                    {
                        throw new BuilderIllegalStateException($"The subregion of each routing instance in attachment set '{_attachmentSet.Name}' " +
                            $"must be the same because the attachment set is configured for gold-level redundancy.");
                    }

                    if (firstGoldLocation.SubRegionID != _attachmentSet.SubRegion.SubRegionID)
                        throw new BuilderIllegalStateException($"Routing instance " +
                            $"'{_attachmentSet.AttachmentSetRoutingInstances.First().RoutingInstance.Name}' does not belong to the same subregion " +
                            $"as the attachment set.");

                    if (secondGoldLocation.SubRegionID != _attachmentSet.SubRegion.SubRegionID)
                        throw new BuilderIllegalStateException($"Routing instance " +
                            $"'{_attachmentSet.AttachmentSetRoutingInstances.ElementAt(1).RoutingInstance.Name}' does not belong to the same subregion " +
                            $"as the attachment set.");

                    if (firstGoldLocation.LocationID == secondGoldLocation.LocationID)
                    {
                        throw new BuilderIllegalStateException($"The location of each routing instance in attachment set '{_attachmentSet.Name}' " +
                            $"must be different because the attachment set is configured for gold-level redundancy.");
                    }
                }
            }

            (from vpnAttachmentSets in _attachmentSet.VpnAttachmentSets
             select vpnAttachmentSets)
             .ToList()
             .ForEach(
                vpnAttachmentSet =>
                {
                    var vpn = vpnAttachmentSet.Vpn;
                    if (vpn.IsMulticastVpn)
                    {
                        if (_attachmentSet.MulticastVpnDomainType == null)
                        {
                            throw new BuilderIllegalStateException("A multicast vpn domain type option for the attachment set is required because the " +
                               $"attachment Set is bound to multicast vpn '{vpn.Name}'.");
                        }
                        else
                        {
                            if (vpn.VpnTopologyType.TopologyType == SCM.Models.TopologyTypeEnum.HubandSpoke)
                            {
                                if (vpn.MulticastVpnDirectionType.MvpnDirectionType == MvpnDirectionTypeEnum.Unidirectional)
                                {
                                    // Unidirectional hub-and-spoke vpn multicast domain checks follow...

                                    if (vpnAttachmentSet.IsHub.GetValueOrDefault())
                                    {
                                        // The attachment set is a HUB for the vpn - the attachment set must be 'sender only'

                                        if (_attachmentSet.MulticastVpnDomainType.MvpnDomainType != MvpnDomainTypeEnum.SenderOnly)
                                        {
                                            throw new BuilderIllegalStateException($"The multicast vpn domain type of '{_attachmentSet.MulticastVpnDomainType.Name}' is not "
                                            + $"valid because attachment set '{_attachmentSet.Name}' is designated as a HUB for hub-and-spoke multicast vpn "
                                            + $"'{vpn.Name}'. The multicast direction type setting of the vpn is '{vpn.MulticastVpnDirectionType.Name}' and therefore "
                                            + "the multicast vpn domain type of the Attachment set must be 'Sender-Only'.");
                                        }
                                    }
                                    else
                                    {
                                        // The attachment set is a SPOKE for the vpn - the attachment set must be 'receiver only'

                                        if (_attachmentSet.MulticastVpnDomainType.MvpnDomainType != MvpnDomainTypeEnum.ReceiverOnly)
                                        {
                                            throw new BuilderIllegalStateException($"The multicast vpn domain type of '{_attachmentSet.MulticastVpnDomainType.Name}' is not "
                                            + $"valid because attachment set '{_attachmentSet.Name}' is designated as a SPOKE for hub-and-spoke multicast vpn "
                                            + $"'{vpn.Name}'. The multicast direction type setting of the vpn is '{vpn.MulticastVpnDirectionType.Name}' and therefore "
                                            + "the multicast vpn domain type of the attachment set must be 'Receiver-Only'.");
                                        }
                                    }
                                }

                                else
                                {
                                    // Bidirectioal hub-and-spoke vpn multicast domain checks follow...

                                    if (vpnAttachmentSet.IsHub.GetValueOrDefault())
                                    {
                                        // The attachment set is a HUB for the vpn - the attachment set must be 'sender only' or 'sender-and-reciever'

                                        if (_attachmentSet.MulticastVpnDomainType.MvpnDomainType != MvpnDomainTypeEnum.SenderOnly
                                            && _attachmentSet.MulticastVpnDomainType.MvpnDomainType != MvpnDomainTypeEnum.SenderAndReceiver)
                                        {
                                            throw new BuilderIllegalStateException($"The multicast vpn domain type of '{_attachmentSet.MulticastVpnDomainType.Name}' is not "
                                            + $"valid because attachment set '{_attachmentSet.Name}' is designated as a HUB for hub-and-spoke multicast vpn "
                                            + $"'{vpn.Name}'. The multicast direction type setting of the vpn is '{vpn.MulticastVpnDirectionType.Name}' and therefore "
                                            + "the multicast vpn domain type of the attachment set must be either 'Sender-and-Receiver' or 'Sender-Only'.");
                                        }
                                    }
                                    else
                                    {
                                        if (_attachmentSet.MulticastVpnDomainType.MvpnDomainType != MvpnDomainTypeEnum.ReceiverOnly
                                            && _attachmentSet.MulticastVpnDomainType.MvpnDomainType != MvpnDomainTypeEnum.SenderAndReceiver)
                                        {
                                            throw new BuilderIllegalStateException($"The multicast vpn domain type selection of '{_attachmentSet.MulticastVpnDomainType.Name}' is not "
                                            + $"valid because attachment set '{_attachmentSet.Name}' is designated as a SPOKE for hub-and-spoke multicast vpn "
                                            + $"'{vpn.Name}'. The multicast direction type setting of the vpn is '{vpn.MulticastVpnDirectionType.Name}' and therefore "
                                            + "the multicast vpn domain type of the attachment set must be either 'Sender-and-Receiver' or 'Receiver-Only'.");
                                        }
                                    }
                                }
                            }
                        }

                        if (_attachmentSet.MulticastVpnDomainType.MvpnDomainType == MvpnDomainTypeEnum.ReceiverOnly)
                        {
                            if (_attachmentSet.VpnTenantMulticastGroups.Any())
                            {
                                throw new BuilderIllegalStateException("The multicast domain type cannot be 'Receiver-Only' for attachment set "
                                    + $"'{_attachmentSet.Name}' because multicast group ranges are associated with the attachment set. "
                                    + "Remove the multicast group ranges from the attachment set first.");
                            }
                        }
                    }
                });
        }
    }
}
