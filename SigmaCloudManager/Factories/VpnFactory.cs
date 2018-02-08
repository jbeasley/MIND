using AutoMapper;
using SCM.Data;
using SCM.Models;
using SCM.Models.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SCM.Services;

namespace SCM.Factories
{
    /// <summary>
    /// Factory for creating VPNs
    /// </summary>
    public class VpnFactory : BaseFactory, IVpnFactory
    {
        public VpnFactory(IMapper mapper,
                          IRouteTargetRangeService routeTargetRangeService,
                          IRouteTargetFactory routeTargetFactory) : base(mapper)
        {
            RouteTargetFactory = routeTargetFactory;
            RouteTargetRangeService = routeTargetRangeService;
        }

        private IRouteTargetFactory RouteTargetFactory { get; }
        private IRouteTargetRangeService RouteTargetRangeService { get; }

        /// <summary>
        /// Create a new VPN
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<FactoryResult> NewAsync(VpnRequest vpnRequest)
        {
            var vpn = Mapper.Map<Vpn>(vpnRequest);

            var result = new FactoryResult
            {
                IsSuccess = true,
                Item = vpn
            };

            vpn.RequiresSync = true;
            vpn.Created = true;
            vpn.ShowCreatedAlert = true;
            vpn.ShowRequiresSyncAlert = true;

            // If the VPN request is for a 'Nova' type VPN then ensure that the Route Target Range is for the Default range.
            
            if (vpnRequest.IsNovaVpn)
            {
                var defaultRtRange = await RouteTargetRangeService.GetByNameAsync("Default");
                if (defaultRtRange == null)
                {
                    throw new FactoryFailureException("The default Route Target Range was not found.");
                }

                vpnRequest.RouteTargetRangeID = defaultRtRange.RouteTargetRangeID;
            }

            var routeTargetFactoryResult = await RouteTargetFactory.NewForVpnAsync(new RouteTargetRequest
            {
                VpnTopologyTypeID = vpn.VpnTopologyTypeID,
                RouteTargetRangeID = vpnRequest.RouteTargetRangeID.Value
            });

            var routeTargets = (List<RouteTarget>)routeTargetFactoryResult.Item;
            vpn.RouteTargets = routeTargets;
        
            return result;
        }
    }
}
