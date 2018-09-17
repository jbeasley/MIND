﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Mind.Models;

namespace SCM.Models
{
    public static class AttachmentQueryableExtensions
    {
        public static IQueryable<Attachment> IncludeValidationProperties(this IQueryable<Attachment> query)
        {
            return query.Include(x => x.Tenant)
                            .Include(x => x.Device)
                            .Include(x => x.RoutingInstance.Attachments)
                            .Include(x => x.RoutingInstance.Vifs)
                            .Include(x => x.ContractBandwidthPool)
                            .Include(x => x.AttachmentRole.PortPool.PortRole)
                            .Include(x => x.AttachmentBandwidth)
                            .Include(x => x.Interfaces)
                            .ThenInclude(x => x.Ports)
                            .Include(x => x.Vifs);
        }

        public static IQueryable<Attachment> IncludeDeleteValidationProperties(this IQueryable<Attachment> query)
        {
            return query.Include(x => x.ContractBandwidthPool.Attachments)
                                     .Include(x => x.ContractBandwidthPool.Vifs)
                                     .Include(x => x.Interfaces)
                                     .ThenInclude(x => x.Ports)
                                     .ThenInclude(x => x.PortStatus)
                                     .Include(x => x.Vifs)
                                     .ThenInclude(x => x.Vlans)
                                     .Include(x => x.Vifs)
                                     .ThenInclude(x => x.RoutingInstance.RoutingInstanceType)
                                     .Include(x => x.Vifs)
                                     .ThenInclude(x => x.RoutingInstance.Attachments)
                                     .Include(x => x.Vifs)
                                     .ThenInclude(x => x.RoutingInstance.Vifs)
                                     .Include(x => x.Vifs)
                                     .ThenInclude(x => x.ContractBandwidthPool.Vifs)
                                     .Include(x => x.Vifs)
                                     .ThenInclude(x => x.ContractBandwidthPool.Attachments)
                                     .Include(x => x.Vifs)
                                     .ThenInclude(x => x.RoutingInstance.AttachmentSetRoutingInstances)
                                     .Include(x => x.RoutingInstance.RoutingInstanceType)
                                     .Include(x => x.RoutingInstance.Vifs)
                                     .Include(x => x.RoutingInstance.Attachments)
                                     .Include(x => x.RoutingInstance.BgpPeers)
                                     .Include(x => x.RoutingInstance.AttachmentSetRoutingInstances);
        }

        public static IQueryable<Attachment> IncludeDeepProperties(this IQueryable<Attachment> query)
        {
            return query.Include(x => x.Device.Location.SubRegion.Region)
                        .Include(x => x.ContractBandwidthPool.ContractBandwidth)
                        .Include(x => x.Interfaces)
                        .ThenInclude(x => x.Ports)
                        .Include(x => x.Mtu)
                        .Include(x => x.RoutingInstance.BgpPeers)
                        .Include(x => x.Tenant);
        }
     }

    public class Attachment : IModifiableResource
    {
        [Key]
        public int AttachmentID { get; private set; }
        [NotMapped]
        public string Name
        {
            get
            {
                if (IsBundle)
                {
                    return $"Bundle{ID}";
                }
                else if (IsMultiPort)
                {
                    return $"MultiPort{ID}";
                }
                else
                {
                    var port = Interfaces?.SingleOrDefault()?.Ports?.SingleOrDefault();
                    if (port != null) return $"{port.Type} {port.Name}";
                    return string.Empty;
                }
            }
        }
        [MaxLength(250)]
        public string Description { get; set; }
        [MaxLength(250)]
        public string Notes { get; set; }
        public bool IsTagged { get; set; }
        public bool IsLayer3 { get; set; }
        public bool IsBundle { get; set; }
        public int? BundleMinLinks { get; set; }
        public int? BundleMaxLinks { get; set; }
        public bool IsMultiPort { get; set; }
        public int? ID { get; set; }
        public int AttachmentBandwidthID { get; set; }
        public int? TenantID { get; set; }
        public int DeviceID { get; set; }
        public int? RoutingInstanceID { get; set; }
        public int? ContractBandwidthPoolID { get; set; }
        public int AttachmentRoleID { get; set; }
        public int MtuID { get; set; }
        public bool Created { get; set; }
        public bool RequiresSync { get; set; }
        public bool ShowCreatedAlert { get; set; }
        public bool ShowRequiresSyncAlert { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        [ForeignKey("TenantID")]
        public virtual Tenant Tenant { get; set; }
        public virtual Device Device { get; set; }
        public virtual RoutingInstance RoutingInstance { get; set; }
        public virtual AttachmentBandwidth AttachmentBandwidth { get; set; }
        [ForeignKey("ContractBandwidthPoolID")]
        public virtual ContractBandwidthPool ContractBandwidthPool { get; set; }
        [ForeignKey("MtuID")]
        public virtual Mtu Mtu { get; set; }
        public virtual AttachmentRole AttachmentRole { get; set; }
        public virtual ICollection<Interface> Interfaces { get; set; }
        public virtual ICollection<Vif> Vifs { get; set; }
        string IModifiableResource.ConcurrencyToken => this.GetWeakETag();

        /// <summary>
        /// Validate the state of the attachment.
        /// </summary>
        public virtual void Validate()
        {
            if (this.Mtu == null) throw new IllegalStateException("An MTU is required for the attachment.");
            if (this.AttachmentBandwidth == null) throw new IllegalStateException("An attachment bandwidth is required for the attachment.");
            if (this.AttachmentRole == null) throw new IllegalStateException("An attachment role is required for the attachment.");
            if (this.Device == null) throw new IllegalStateException("A device is required for the attachment.");
            if (!this.Interfaces.Any()) throw new IllegalStateException("At least one interface is required for the attachment.");
            if (this.AttachmentRole.PortPool.PortRole.PortRoleType == PortRoleTypeEnum.TenantFacing && this.Tenant == null)
            {
                throw new IllegalStateException("A tenant association is required for the attachment in accordance with the attachment role of " +
                    $"'{this.AttachmentRole.Name}'.");
            }
            else if (this.AttachmentRole.PortPool.PortRole.PortRoleType == PortRoleTypeEnum.TenantInfrastructure && this.Tenant == null)
            {
                throw new IllegalStateException("A tenant association is required for the attachment in accordance with the attachment role of " +
                    $"'{this.AttachmentRole.Name}'.");
            }
            else if (this.AttachmentRole.PortPool.PortRole.PortRoleType == PortRoleTypeEnum.ProviderInfrastructure && this.Tenant != null)
            {
                throw new IllegalStateException("A tenant association exists for the attachment but is NOT required in accordance with the " +
                    $"attachment role of '{this.AttachmentRole.Name}'.");
            }

            if (this.RoutingInstance == null && this.AttachmentRole.RoutingInstanceTypeID.HasValue)
                throw new IllegalStateException("Illegal routing instance state. A routing instance for the attachment is required in accordance " +
                    $"with the requested attachment role of '{this.AttachmentRole.Name}' but was not found.");

            if (this.RoutingInstance != null && !this.AttachmentRole.RoutingInstanceTypeID.HasValue)
                throw new IllegalStateException("Illegal routing instance state. A routing instance for the attachment has been assigned but is " +
                    $"not required for an attachment with attachment role of '{this.AttachmentRole.Name}'.");

            if (this.RoutingInstance != null && this.AttachmentRole.RoutingInstanceType != null)
            {
                if (this.RoutingInstance.RoutingInstanceType.RoutingInstanceTypeID != this.AttachmentRole.RoutingInstanceTypeID)
                {
                    throw new IllegalStateException("Illegal routing instance state. The routing instance type for the attachment is different to that " +
                        $"required by the attachment role. The routing instance type required is '{this.AttachmentRole.RoutingInstanceType.Type.ToString()}'. " +
                        $"The routing instance type assigned to the attachment is '{this.RoutingInstance.RoutingInstanceType.Type.ToString()}'.");
                }
            }

            if (this.IsLayer3)
            {
                if (this.Interfaces.Where(x => !string.IsNullOrEmpty(x.IpAddress) &&
                !string.IsNullOrEmpty(x.SubnetMask)).Count() != this.Interfaces.Count)
                {
                    throw new IllegalStateException("The attachment is enabled for layer 3 but insufficient IPv4 addresses have been requested.");
                }
            }
            else if (this.Interfaces.Where(x => !string.IsNullOrEmpty(x.IpAddress) || !string.IsNullOrEmpty(x.SubnetMask)).Any())
            {
                throw new IllegalStateException("The attachment is NOT enabled for layer 3 but IPv4 addresses have been requested.");
            }

            if (this.AttachmentRole.RequireContractBandwidth)
            {
                if (this.ContractBandwidthPool == null)
                {
                    throw new IllegalStateException("A contract bandwidth for the attachment is required in accordance with the attachment role " +
                        $"of '{this.AttachmentRole.Name}' but none is defined.");
                }

                if (this.ContractBandwidthPool.ContractBandwidth.BandwidthMbps > this.AttachmentBandwidth.BandwidthGbps * 1000)
                {
                    throw new IllegalStateException($"The requested contract bandwidth of " +
                        $"{this.ContractBandwidthPool.ContractBandwidth.BandwidthMbps} Mbps is greater " +
                        $"than the bandwidth of the attachment which is {this.AttachmentBandwidth.BandwidthGbps} Gbps.");
                }
            }
            else
            {
                if (this.ContractBandwidthPool != null)
                {
                    throw new IllegalStateException("A contract bandwidth for the attachment is defined but is NOT required for the attachment role " +
                        $"of '{this.AttachmentRole.Name}'.");
                }
            }

            if (this.AttachmentRole.IsTaggedRole)
            {
                if (!this.IsTagged)
                {
                    throw new IllegalStateException("The attachment must be enabled for tagging with the 'isTagged' property in accordance with the " +
                        $"attachment role of '{this.AttachmentRole.Name}'.");
                }
                if (this.IsLayer3) throw new IllegalStateException("Layer 3 cannot be enabled concurrently with a tagged attachment.");
            }
            else
            {
                if (this.Vifs.Any())
                {
                    throw new IllegalStateException("Vifs were found for the attachment but the attachment is not enabled for tagging with the 'isTagged' properrty.");
                }
            }

            if (this.IsBundle)
            {
                if (!this.AttachmentRole.SupportedByBundle) throw new IllegalStateException($"The requested attachment role " +
                $"'{this.AttachmentRole.Name}' is not supported with a bundle attachment.");

                if (!this.AttachmentBandwidth.SupportedByBundle) throw new IllegalStateException($"The requested attachment " +
                $"bandwidth '{this.AttachmentBandwidth.BandwidthGbps} Gbps' is not supported with a bundle attachment.");

                var numPorts = this.Interfaces.SelectMany(x => x.Ports).Count();
                if (this.BundleMinLinks > numPorts) throw new IllegalStateException($"The min links parameter for the bundle " +
                    $"({this.BundleMinLinks}) must be " +
                    $"less than or equal to the total number of ports required for the bundle ({numPorts}).");

                if (this.BundleMaxLinks > numPorts) throw new IllegalStateException($"The max links parameter for the bundle " +
                    $"({this.BundleMaxLinks}) must be " +
                    $"less than or equal to the total number of ports required for the bundle ({numPorts}).");

                if (this.BundleMinLinks > this.BundleMaxLinks) throw new IllegalStateException($"The min links parameter for the bundle " +
                    $"({this.BundleMinLinks}) must be less then " +
                    $"or equal to the max links parameter for the bundle ({this.BundleMaxLinks})");
            }

            if (this.IsMultiPort)
            {
                if (!this.AttachmentRole.SupportedByMultiPort) throw new IllegalStateException($"The requested attachment role " +
                    $"'{this.AttachmentRole.Name}' is not supported with a multiport attachment.");

                if (!this.AttachmentBandwidth.SupportedByMultiPort) throw new IllegalStateException($"The requested attachment " +
                    $"bandwidth '{this.AttachmentBandwidth.BandwidthGbps} Gbps' is not supported with a multiport attachment.");
            }
        }

        /// <summary>
        /// Validate the attachment can be deleted
        /// </summary>
        public virtual void ValidateDelete()
        {
            if (this.RoutingInstance != null)
            {
                if (this.RoutingInstance.AttachmentSetRoutingInstances.Any())
                {
                    throw new IllegalDeleteAttemptException("The attachment is a member belongs to one or more attachment sets " +
                        "and cannot be deleted. Remove the attachment from all attachment sets first.");
                }
            }

            // Validate each Vif associated with the Attachment can be deleted
            this.Vifs
                .ToList()
                .ForEach(
                    x =>
                    x.ValidateDelete()
                );
        }
    }
}