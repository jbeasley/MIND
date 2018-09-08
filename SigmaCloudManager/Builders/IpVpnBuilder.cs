﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Models.RequestModels;
using SCM.Data;
using SCM.Models;

namespace Mind.Builders
{
    /// <summary>
    /// Builder for IP vpns. The builder exposes a fluent API.
    /// </summary>
    public class IpVpnBuilder : VpnBuilder, IIpVpnBuilder
    {
        public IpVpnBuilder(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        IIpVpnBuilder IIpVpnBuilder.ForTenant(int tenantId)
        {
            base.ForTenant(tenantId);
            return this;
        }

        IIpVpnBuilder IIpVpnBuilder.WithName(string name)
        {
            base.WithName(name);
            return this;
        }

        IIpVpnBuilder IIpVpnBuilder.WithDescription(string description)
        {
            base.WithDescription(description);
            return this;
        }

        IIpVpnBuilder IIpVpnBuilder.WithRegion(string regionName)
        {
            base.WithRegion(regionName);
            return this;
        }

        IIpVpnBuilder IIpVpnBuilder.WithTopologyType(string topologyName)
        {
            base.WithTopologyType(topologyName);
            return this;
        }

        IIpVpnBuilder IIpVpnBuilder.WithPlane(string planeName)
        {
            base.WithPlane(planeName);
            return this;
        }

        IIpVpnBuilder IIpVpnBuilder.WithTenancyType(string tenancyName)
        {
            base.WithTenancyType(tenancyName);
            return this;
        }

        IIpVpnBuilder IIpVpnBuilder.AsNovaVpn(bool? isNovaVpn)
        {
            base.AsNovaVpn(isNovaVpn);
            return this;
        }

        IIpVpnBuilder IIpVpnBuilder.WithAddressFamily(string addressFamilyName)
        {
            base.WithAddressFamily(addressFamilyName);
            return this;
        }

        public IIpVpnBuilder WithRouteTargetRange(string rangeName)
        {
            if (!string.IsNullOrEmpty(rangeName)) _args.Add(nameof(WithRouteTargetRange), rangeName);
            return this;
        }

        public IIpVpnBuilder WithRouteTargets(List<RouteTargetRequest> routeTargetRequests)
        {
            if (routeTargetRequests != null && routeTargetRequests.Any()) _args.Add(nameof(WithRouteTargets), routeTargetRequests);
            return this;
        }

        public IIpVpnBuilder WithExtranet(bool? isExtranet)
        {
            if (isExtranet.HasValue) _args.Add(nameof(WithExtranet), isExtranet);
            return this;
        }

        public IIpVpnBuilder WithMulticast(bool? isMulticastVpn)
        {
            if (isMulticastVpn.HasValue) _args.Add(nameof(WithMulticast), isMulticastVpn);
            return this;
        }

        public IIpVpnBuilder WithMulticastVpnServiceType(string multicastVpnServiceType)
        {
            if (!string.IsNullOrEmpty(multicastVpnServiceType)) _args.Add(nameof(WithMulticastVpnServiceType), multicastVpnServiceType);
            return this;
        }

        public IIpVpnBuilder WithMulticastVpnDirectionType(string multicastVpnDirectionType)
        {
            if (!string.IsNullOrEmpty(multicastVpnDirectionType)) _args.Add(nameof(WithMulticastVpnDirectionType), multicastVpnDirectionType);
            return this;
        }

        public override async Task<Vpn> BuildAsync()
        {
            await base.BuildAsync();
            if (_args.ContainsKey(nameof(WithExtranet))) SetExtranet();
            if (_args.ContainsKey(nameof(WithMulticast))) SetIsMulticastVpn();
            if (_args.ContainsKey(nameof(WithMulticastVpnServiceType))) await SetMulticastVpnServiceTypeAsync();
            if (_args.ContainsKey(nameof(WithMulticastVpnDirectionType))) await SetMulticastVpnDirectionTypeAsync();
            if (_args.ContainsKey(nameof(WithRouteTargetRange)))
            {
                await AllocateRouteTargetsAsync();
            }
            else if (_args.ContainsKey(nameof(WithRouteTargets)))
            {
                await AssignRequestedRouteTargetsAsync();
            }
            return _vpn;
        }

        protected internal virtual void SetExtranet()
        {
            var isExtranet = (bool)_args[nameof(WithExtranet)];
            if (isExtranet && _vpn.VpnTenancyType.TenancyType != SCM.Models.TenancyTypeEnum.Multi)
                throw new BuilderBadArgumentsException("In order to enable a vpn for extranet the tenancy type must be 'multi'. " +
                    "Please change the tenancy type and again.");

            _vpn.IsExtranet = isExtranet;
        }

        protected internal virtual void SetIsMulticastVpn()
        {
            _vpn.IsMulticastVpn = (bool)_args[nameof(WithMulticast)];
        }

        protected virtual async Task SetMulticastVpnServiceTypeAsync()
        {
            var multicastVpnServiceTypeName = _args[nameof(WithMulticastVpnServiceType)].ToString();
            var multicastVpnServiceType = (from result in await _unitOfWork.MulticastVpnServiceTypeRepository.GetAsync(
                                    q =>
                                           q.MvpnServiceType.ToString() == multicastVpnServiceTypeName,
                                           AsTrackable: true)
                                           select result)
                                           .SingleOrDefault();

            _vpn.MulticastVpnServiceType = multicastVpnServiceType ?? throw new BuilderBadArgumentsException("The multicast vpn service type " +
                $"option '{multicastVpnServiceTypeName}' was not recognised.");
        }

        protected virtual async Task SetMulticastVpnDirectionTypeAsync()
        {
            var multicastVpnDirectionTypeName = _args[nameof(WithMulticastVpnDirectionType)].ToString();
            var multicastVpnDirectionType = (from result in await _unitOfWork.MulticastVpnDirectionTypeRepository.GetAsync(
                                        q =>
                                            q.MvpnDirectionType.ToString() == multicastVpnDirectionTypeName,
                                            AsTrackable: true)
                                            select result)
                                           .SingleOrDefault();

            _vpn.MulticastVpnDirectionType = multicastVpnDirectionType ?? throw new BuilderBadArgumentsException("The multicast vpn direction type " +
                $"option '{multicastVpnDirectionTypeName}' was not recognised.");
        }

        /// <summary>
        /// Allocate route targets to the vpn
        /// </summary>
        /// <returns></returns>
        protected internal virtual async Task AllocateRouteTargetsAsync()
        {
            var rtRangeName = _args[nameof(WithRouteTargetRange)].ToString();

            // Get used RTs from inventory - these will be eliminated when calculated free route targets
            var rtRange = (from result in await _unitOfWork.RouteTargetRangeRepository.GetAsync(
                    q =>
                           q.Name == rtRangeName)
                           select result)
                           .SingleOrDefault();

            if (rtRange == null) throw new BuilderBadArgumentsException("The specified route target range was not found. Check that the route target " +
                $"range argument '{rtRangeName}' is valid.");

            if (_vpn.IsNovaVpn)
            {
                if (rtRange.Range != SCM.Models.RouteTargetRangeEnum.Default) throw new BuilderBadArgumentsException("The route target range must " +
                    "be 'default' for requests for a Nova vpn");
            }

            if (!_vpn.IsNovaVpn && rtRange.Range != SCM.Models.RouteTargetRangeEnum.Default) 
                throw new BuilderBadArgumentsException("The default route target range cannot be used for non-Nova vpns.");

            var usedRTs = (from result in await _unitOfWork.RouteTargetRepository.GetAsync(
                    q =>
                          q.RouteTargetRange.Name == rtRangeName)
                           select result)
                          .ToList();

            // Allocate new unused RTs from the RT range
            IEnumerable<int> rtAssignedNumbers = null;

            if (_vpn.VpnTopologyType.TopologyType == SCM.Models.TopologyTypeEnum.Meshed)
            {
                // One RT required for an Any-to-Any vpn
                rtAssignedNumbers = Enumerable.Range(rtRange.AssignedNumberSubFieldStart, rtRange.AssignedNumberSubFieldCount)
                    .Except(usedRTs.Select(q => q.AssignedNumberSubField)).Take(1);

                if (!rtAssignedNumbers.Any())
                {
                    throw new BuilderUnableToCompleteException("Failed to allocate a route target for the vpn. "
                        + "Please contact your administrator to report this issue. Alternatively you can try another range.");
                }

                _vpn.RouteTargets.Add(
                    new RouteTarget
                    {
                        AssignedNumberSubField = rtAssignedNumbers.First(),
                        RouteTargetRangeID = rtRange.RouteTargetRangeID
                    }
                );
            }
            else if (_vpn.VpnTopologyType.TopologyType == SCM.Models.TopologyTypeEnum.HubandSpoke)
            {
                // Two RTs required for a Hub-and-Spoke vpn
                rtAssignedNumbers = Enumerable.Range(rtRange.AssignedNumberSubFieldStart, rtRange.AssignedNumberSubFieldCount)
                    .Except(usedRTs.Select(q => q.AssignedNumberSubField)).Take(2);

                if (rtAssignedNumbers.Count() != 2)
                {
                    throw new BuilderUnableToCompleteException("Failed to allocate route targets for the vpn. "
                        + "Please contact your administrator to report this issue. Alternatively you can try another range.");
                }

                _vpn.RouteTargets = new List<RouteTarget>
                {
                    new RouteTarget
                    {
                        AssignedNumberSubField = rtAssignedNumbers.ElementAt(0),
                        RouteTargetRangeID = rtRange.RouteTargetRangeID
                    },
                    new RouteTarget
                    {
                        AssignedNumberSubField = rtAssignedNumbers.ElementAt(1),
                        RouteTargetRangeID = rtRange.RouteTargetRangeID,
                        IsHubExport = true
                    }
                };
            }
        }

        protected virtual async Task AssignRequestedRouteTargetsAsync()
        {
            var requestedRouteTargets = (List<RouteTargetRequest>)_args[nameof(WithRouteTargets)];
            var tasks = requestedRouteTargets.Select(
               async request =>
                    {
                        if (request.IsHubExport.HasValue && request.IsHubExport.Value && _vpn.VpnTopologyType.TopologyType != SCM.Models.TopologyTypeEnum.HubandSpoke)
                        {
                            throw new BuilderBadArgumentsException("Cannot set 'IsHubExport' on the route target because the vpn topology is not " +
                                "hub-and-spoke.");
                        }

                        var rtRange = (from result in await _unitOfWork.RouteTargetRangeRepository.GetAsync(
                                    q =>
                                       q.Name == request.Range.ToString(),
                                       AsTrackable: false)
                                       select result)
                                       .SingleOrDefault();

                        if (rtRange == null) throw new BuilderBadArgumentsException($"The route target range '{request.Range.ToString()}' " +
                            $"was not found. " +
                            $"Check that the specified range is correct.");

                        if (_vpn.IsNovaVpn)
                        {
                            if (rtRange.Range != SCM.Models.RouteTargetRangeEnum.Default)
                                throw new BuilderBadArgumentsException("The route target range must " +
                                "be 'default' for requests for a Nova vpn");
                        }

                        var existingRT = (from result in await _unitOfWork.RouteTargetRepository.GetAsync(
                                    q =>
                                         q.RouteTargetRange.Name == request.Range.ToString()
                                         && q.AssignedNumberSubField == request.AssignedNumberSubField,
                                         AsTrackable: false)
                                         select result)
                                         .SingleOrDefault();

                        if (existingRT == null)
                        {
                            _vpn.RouteTargets.Add(new RouteTarget
                            {
                                AssignedNumberSubField = request.AssignedNumberSubField.Value,
                                RouteTargetRangeID = rtRange.RouteTargetRangeID,
                                IsHubExport = request.IsHubExport ?? false
                            });
                        }
                        else
                        {
                            throw new BuilderUnableToCompleteException($"The requested route target is already assigned. " +
                                                                        "Try again with a different route target.");
                        }
                    }
            );

            await Task.WhenAll(tasks);
        }
    }
}
