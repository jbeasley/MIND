using Microsoft.EntityFrameworkCore;
using Mind.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;

namespace SCM.Models
{
    public static class VpnTenantIpNetworkRoutingInstanceStaticRouteQueryableExtensions
    {
        public static IQueryable<VpnTenantIpNetworkRoutingInstanceStaticRoute> IncludeValidationProperties(this IQueryable<VpnTenantIpNetworkRoutingInstanceStaticRoute> query)
        {
            return query.Include(x => x.AttachmentSet)
                        .Include(x => x.TenantIpNetwork)
                        .Include(x => x.RoutingInstance)
                        .Include(x => x.AttachmentSet.VpnAttachmentSets)
                        .Include(x => x.AttachmentSet.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.RoutingInstance.Vifs)
                        .ThenInclude(x => x.Vlans)
                        .Include(x => x.AttachmentSet.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.RoutingInstance.Attachments)
                        .ThenInclude(x => x.Interfaces);
        }

        public static IQueryable<VpnTenantIpNetworkRoutingInstanceStaticRoute> IncludeDeepProperties(this IQueryable<VpnTenantIpNetworkRoutingInstanceStaticRoute> query)
        {
            return query.Include(x => x.AttachmentSet.VpnAttachmentSets)
                        .Include(x => x.RoutingInstance)
                        .Include(x => x.TenantIpNetwork);
        }
    }

    public class VpnTenantIpNetworkRoutingInstanceStaticRoute : IModifiableResource
    {
        public int VpnTenantIpNetworkRoutingInstanceStaticRouteID { get; private set; }
        public int TenantIpNetworkID { get; set; }
        public int AttachmentSetID { get; set; }
        public bool AddToAllRoutingInstancesInAttachmentSet { get; set; }
        public string Ipv4NextHopAddress { get; set; }
        public bool IsBfdEnabled { get; set; }
        public int? RoutingInstanceID { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        [ForeignKey("TenantIpNetworkID")]
        public virtual TenantIpNetwork TenantIpNetwork { get; set; }
        public virtual AttachmentSet AttachmentSet { get; set; }
        public virtual RoutingInstance RoutingInstance { get; set; }
        string IModifiableResource.ConcurrencyToken => this.GetWeakETag();

        /// <summary>
        /// Validate the state of the vpn tenant IP network static route
        /// </summary>
        public void Validate()
        {
            if (this.AttachmentSet == null) throw new IllegalStateException("An attachment set association with the " +
                "tenant IP network static route is required but was not found.");

            if (this.TenantIpNetwork == null)
                throw new IllegalStateException("A tenant IP network is required but was not found.");

            if (this.AddToAllRoutingInstancesInAttachmentSet)
            {
                if (this.RoutingInstance != null)
                {
                    throw new IllegalStateException($"A routing instance association with the static route for tenant IP network " +
                        $"'{this.TenantIpNetwork.CidrName}' was found but is not required because the request is to add the static route " +
                        $"to all routing instances in attachment set '{this.AttachmentSet.Name}'.");
                }
            }
            else
            {
                if (this.RoutingInstance == null)
                {
                    throw new IllegalStateException($"A routing instance association with the static route for tenant IP network " +
                        $"'{this.TenantIpNetwork.CidrName}' is required because the request is to add the static route to a specific routing instance " +
                        $"in attachment set '{this.AttachmentSet.Name}'.");
                }
            }

            if (string.IsNullOrEmpty(this.Ipv4NextHopAddress)) throw new IllegalStateException("An IPv4 next-hop address is required but was not found.");

            if (!IPAddress.TryParse(this.Ipv4NextHopAddress, out IPAddress ipv4NextHopAddress))
                throw new IllegalStateException("The next-hop address is not a valid IPv4 address");

            if (this.RoutingInstance != null)
            {
                if (!this.AttachmentSet.AttachmentSetRoutingInstances.Any(x => x.RoutingInstance.Name == this.RoutingInstance.Name))
                {
                    throw new IllegalStateException($"Routing instance '{this.RoutingInstance.Name}' was not found or does not belong to attachment set " +
                        $"'{this.AttachmentSet.Name}'.");
                }

                // Check the IPv4 next-hop address is 'reachable' within the routing instance
                var vlan = this.RoutingInstance.Vifs.SelectMany(
                    x =>
                    x.Vlans)
                     .ToList()
                     .FirstOrDefault(
                        x =>
                        {
                            var network = IPNetwork.Parse(x.IpAddress, x.SubnetMask);
                            return network.Contains(ipv4NextHopAddress);
                        });

                var iface = this.RoutingInstance.Attachments.SelectMany(
                    x =>
                    x.Interfaces)
                     .ToList()
                     .FirstOrDefault(
                        x =>
                        {
                            var network = IPNetwork.Parse(x.IpAddress, x.SubnetMask);
                            return network.Contains(ipv4NextHopAddress);
                        });

                if (vlan == null && iface == null)
                    throw new IllegalStateException($"The IPv4 next-hop address '{this.Ipv4NextHopAddress}' is not contained by any network which is " +
                        $"directly reachable from routing instance '{this.RoutingInstance.Name}'. Check that the IP address for at least one vif or " +
                        $"attachment belonging to the routing instance is in the same IPv4 network as the next-hop address.");
            }
            else
            {
                //Check that the next-hop address is 'reachable' from within any routing instance in the attachment set
                var vlan = this.AttachmentSet.AttachmentSetRoutingInstances.Select(
                          q => q.RoutingInstance)
                          .Select(
                          q => 
                            q.Vifs.SelectMany(
                              x =>
                              x.Vlans)
                              .ToList()
                          .FirstOrDefault(
                          x =>
                            {
                                var network = IPNetwork.Parse(x.IpAddress, x.SubnetMask);
                                return network.Contains(ipv4NextHopAddress);
                            }))
                          ?.FirstOrDefault();

                var iface = this.AttachmentSet.AttachmentSetRoutingInstances.Select(
                          q => q.RoutingInstance)
                          .Select(
                          q =>
                            q.Attachments.SelectMany(
                              x =>
                              x.Interfaces)
                              .ToList()
                          .FirstOrDefault(
                          x =>
                          {
                              var network = IPNetwork.Parse(x.IpAddress, x.SubnetMask);
                              return network.Contains(ipv4NextHopAddress);
                          }))
                          ?.FirstOrDefault();

                if (vlan == null && iface == null)
                    throw new IllegalStateException($"The IPv4 next-hop address '{this.Ipv4NextHopAddress}' is not contained by any network which is " +
                        $"directly reachable from any routing instance in attachment set '{this.AttachmentSet.Name}'. Check that the IP address for " +
                        $"at least one vif or attachment belonging to the routing instances in the attachment set is in the same IPv4 network as " +
                        $"the next-hop address.");
            }

            var sb = new StringBuilder();
            (from result in this.AttachmentSet.VpnAttachmentSets.Where(
             q =>
             q.AttachmentSetID == this.AttachmentSetID && q.Vpn.IsExtranet)
             select result.Vpn)
             .ToList()
             .ForEach(
                x => 
                    sb.Append($"A static route for tenant IP network '{this.TenantIpNetwork.CidrName}' " +
                               $"cannot be added to attachment set '{this.AttachmentSet.Name}' because the attachment set is associated with extranet vpn " +
                               $"'{x.Name}' and the tenant IP network is not enabled for extranet. Update the tenant IP network " +
                               "to enable it for extranet services first.").Append("\r\n")
                     );

            if (sb.Length > 0) throw new IllegalStateException(sb.ToString());
        }
    }
}