using Mind.Models.RequestModels;
using SCM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mind.Directors;

namespace Mind.Builders
{
    public class IpVpnDirector : IVpnDirector, INetworkSynchronizable<Vpn>, IDestroyable<Vpn>
    {
        private readonly IIpVpnBuilder _builder;

        public IpVpnDirector(IIpVpnBuilder builder)
        {
            _builder = builder;
        }

        public async Task<SCM.Models.Vpn> BuildAsync(int tenantId, VpnRequest request, bool syncToNetwork = false)
        {
            return await _builder.ForTenant(tenantId)
                                 .AsNovaVpn(request.IsNovaVpn)
                                 .WithAddressFamily(request.AddressFamily.ToString())
                                 .WithName(request.Name)
                                 .WithDescription(request.Description)
                                 .WithPlane(request.Plane.ToString())
                                 .WithRegion(request.Region.ToString())
                                 .WithRouteTargetRange(request.RouteTargetRange.ToString())
                                 .WithRouteTargets(request.RouteTargetRequests)
                                 .WithTenancyType(request.TenancyType.ToString())
                                 .WithTopologyType(request.TopologyType.ToString()) 
                                 .WithExtranet(request.IsExtranet)
                                 .WithMulticast(request.IsMulticastVpn)
                                 .WithMulticastVpnServiceType(request.MulticastVpnServiceType.ToString())
                                 .WithMulticastVpnDirectionType(request.MulticastVpnDirectionType.ToString())
                                 .WithAttachmentSets(request.VpnAttachmentSets)
                                 .SyncToNetworkPut(syncToNetwork)
                                 .BuildAsync();
        }

        public async Task<SCM.Models.Vpn> UpdateAsync(int vpnId, VpnUpdate update, bool syncToNetworkPut = false, bool syncToNetworkPatch = false)
        {
            return await _builder.ForVpn(vpnId)
                                 .WithDescription(update.Description)
                                 .WithRegion(update.Region.ToString())
                                 .WithTenancyType(update.TenancyType.ToString())
                                 .WithExtranet(update.IsExtranet)
                                 .WithMulticastVpnDirectionType(update.MulticastVpnDirectionType.ToString())
                                 .WithAttachmentSets(update.VpnAttachmentSets)
                                 .SyncToNetworkPut(syncToNetworkPut)
                                 .SyncToNetworkPatch(syncToNetworkPatch)
                                 .BuildAsync();
        }

        public async Task<Vpn> SyncToNetworkPutAsync(Vpn vpn)
        {
            return await _builder.ForVpn(vpn.VpnID)
                                 .SyncToNetworkPutAsync();
        }

        public async Task DestroyAsync(Vpn item, bool cleanUpNetwork = false)
        {
            await _builder.ForVpn(item.VpnID)
                          .CleanUpNetwork(cleanUpNetwork)
                          .DestroyAsync();
        }

        public async Task DestroyAsync(List<Vpn> items, bool cleanUpNetwork = false)
        {
            var tasks = items.Select(
               async vpn =>
            {
                await DestroyAsync(vpn, cleanUpNetwork);
            });

            await Task.WhenAll(tasks);
        }
    }
}
