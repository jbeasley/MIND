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
                        .Include(x => x.RoutingInstance);
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

            if (this.RoutingInstance != null)
            {
                if (!this.AttachmentSet.AttachmentSetRoutingInstances.Any(x => x.RoutingInstance.Name == this.RoutingInstance.Name))
                {
                    throw new IllegalStateException($"Routing instance '{this.RoutingInstance.Name}' was not found or does not belong to attachment set " +
                        $"'{this.AttachmentSet.Name}'.");
                }
            }

            var sb = new StringBuilder();
            (from result in this.AttachmentSet.VpnAttachmentSets.Where(
             q =>
             q.AttachmentSetID == this.AttachmentSetID && q.Vpn.IsExtranet)
             select result.Vpn)
             .ToList()
             .ForEach(
                x => sb.Append($"A static route for tenant IP network '{this.TenantIpNetwork.CidrName}' " +
                               $"cannot be added to attachment set '{this.AttachmentSet.Name}' because the attachment set is associated with extranet vpn " +
                               $"'{x.Name}' and the tenant IP network is not enabled for extranet. Update the tenant IP network " +
                               "to enable it for extranet services first.\n"));

            if (sb.Length > 0) throw new IllegalStateException(sb.ToString());
        }
    }
}