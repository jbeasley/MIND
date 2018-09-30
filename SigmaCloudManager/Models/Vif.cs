using Microsoft.EntityFrameworkCore;
using Mind.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;

namespace SCM.Models
{
    public static class VifQueryableExtensions
    {
        public static IQueryable<Vif> IncludeValidationProperties(this IQueryable<Vif> query)
        {
            return query.Include(x => x.Attachment.Vifs)
                        .ThenInclude(x => x.ContractBandwidthPool.ContractBandwidth)
                        .Include(x => x.Attachment.AttachmentBandwidth)
                        .Include(x => x.Attachment.Device)
                        .Include(x => x.Vlans)
                        .Include(x => x.VifRole.AttachmentRole.PortPool.PortRole)
                        .Include(x => x.Tenant)
                        .Include(x => x.ContractBandwidthPool)
                        .Include(x => x.Mtu);
        }

        public static IQueryable<Vif> IncludeDeleteValidationProperties(this IQueryable<Vif> query)
        {
            return query.Include(x => x.RoutingInstance.Vifs)
                        .Include(x => x.RoutingInstance.Attachments)
                        .Include(x => x.ContractBandwidthPool)
                        .Include(x => x.Vlans)
                        .Include(x => x.RoutingInstance.RoutingInstanceType)
                        .Include(x => x.ContractBandwidthPool.Vifs)
                        .Include(x => x.ContractBandwidthPool.Attachments)
                        .Include(x => x.RoutingInstance.BgpPeers); 
        }

        public static IQueryable<Vif> IncludeDeepProperties(this IQueryable<Vif> query)
        {
            return query.Include(x => x.VifRole)
                        .Include(x => x.Attachment.Interfaces)
                        .ThenInclude(x => x.Ports)
                        .Include(x => x.ContractBandwidthPool)
                        .Include(x => x.RoutingInstance.BgpPeers)
                        .ThenInclude(x => x.VpnTenantIpNetworksIn)
                        .Include(x => x.RoutingInstance.BgpPeers)
                        .ThenInclude(x => x.VpnTenantIpNetworksOut)
                        .Include(x => x.Vlans)
                        .Include(x => x.Tenant);
        }
    }

    public class Vif : IModifiableResource
    {
        public int VifID { get; private set; }
        public bool IsLayer3 { get; set; }
        public int AttachmentID { get; set; }
        [NotMapped]
        public string Name
        {
            get
            {
                return $"{Attachment?.Name}.{VlanTag}";
            }
        }
        [Range(2, 4094)]
        public int VlanTag { get; set; }
        public int? RoutingInstanceID { get; set; }
        public int? TenantID { get; set; }
        public int? ContractBandwidthPoolID { get; set; }
        public int VifRoleID { get; set; }
        public int? VlanTagRangeID { get; set; }
        public bool Created { get; set; }
        public bool RequiresSync { get; set; }
        public bool ShowCreatedAlert { get; set; }
        public bool ShowRequiresSyncAlert { get; set; }
        public int MtuID { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual Attachment Attachment { get; set; }
        public virtual Tenant Tenant { get; set; }
        public virtual RoutingInstance RoutingInstance { get; set; }
        [ForeignKey("ContractBandwidthPoolID")]
        public virtual ContractBandwidthPool ContractBandwidthPool { get; set; }
        public virtual VlanTagRange VlanTagRange { get; set; }
        [ForeignKey("VifRoleID")]
        public virtual VifRole VifRole { get; set; }
        [ForeignKey("MtuID")]
        public virtual Mtu Mtu { get; set; }
        public virtual ICollection<Vlan> Vlans { get; set; }
        string IModifiableResource.ConcurrencyToken => this.GetWeakETag();

        /// <summary>
        /// Validate the state of the vif
        /// </summary>
        public virtual void Validate()
        {
            if (this.Attachment == null) throw new IllegalStateException("An attachment is required for the vif.");
            if (!this.Attachment.IsTagged) throw new IllegalStateException("The attachment under which the vif is to be created must be " +
                "enabled for tagging with the 'isTagged' property.");
            if (this.Mtu == null) throw new IllegalStateException("An MTU is required for the vif.");
            if (this.VifRole == null) throw new IllegalStateException("A vif role is required for the vif.");
            if (this.Vlans.Count != this.Attachment.Interfaces.Count) throw new IllegalStateException($"{this.Attachment.Interfaces.Count} vlans are required " +
                $"but only {this.Vlans.Count} were found. One vlan is required for each interface configured " +
                "for the attachment.");

            if (this.Attachment.AttachmentRole.PortPool.PortRole.PortRoleType == PortRoleTypeEnum.TenantFacing && this.Tenant == null)
            {
                throw new IllegalStateException("A tenant association is required for the vif in accordance with the vif role of " +
                    $"'{this.VifRole.Name}'.");
            }
            else if (this.VifRole.AttachmentRole.PortPool.PortRole.PortRoleType == PortRoleTypeEnum.ProviderInfrastructure && this.Tenant != null)
            {
                throw new IllegalStateException("A tenant association exists for the vif but is NOT required in accordance with the " +
                    $"vif role of '{this.VifRole.Name}'.");
            }

            if (this.IsLayer3)
            {
                if (this.Vlans.Where(x => !string.IsNullOrEmpty(x.IpAddress) && !string.IsNullOrEmpty(x.SubnetMask)).Count() != this.Vlans.Count)
                {
                    throw new IllegalStateException("The vif is enabled for layer 3 but insufficient IPv4 addresses have been requested.");
                }
            }
            else if (this.Vlans.Where(x => !string.IsNullOrEmpty(x.IpAddress) || !string.IsNullOrEmpty(x.SubnetMask)).Any())
            {
                throw new IllegalStateException("The vif is NOT enabled for layer 3 but IPv4 addresses have been requested.");
            }

            if (this.RoutingInstance == null && this.VifRole.RoutingInstanceTypeID.HasValue)
                throw new IllegalStateException("Illegal routing instance state. A routing instance for the vif is required in accordance " +
                    $"with the requested vif role of '{this.VifRole.Name}' but was not found.");

            if (this.RoutingInstance != null && !this.VifRole.RoutingInstanceTypeID.HasValue)
                throw new IllegalStateException("Illegal routing instance state. A routing instance for the vif has been assigned but is " +
                    $"not required for a vif with vif role of '{this.VifRole.Name}'.");

            if (this.RoutingInstance != null && this.VifRole.RoutingInstanceType != null)
            {
                if (this.RoutingInstance.RoutingInstanceType.RoutingInstanceTypeID != this.VifRole.RoutingInstanceTypeID)
                {
                    throw new IllegalStateException("Illegal routing instance state. The routing instance type for the vif is different to that " +
                        $"required by the vif role. The routing instance type required is '{this.VifRole.RoutingInstanceType.Type.ToString()}'. " +
                        $"The routing instance type assigned to the vif is '{this.RoutingInstance.RoutingInstanceType.Type.ToString()}'.");
                }
            }

            if (this.VifRole.RequireContractBandwidth)
            {
                if (this.ContractBandwidthPool == null)
                {
                    throw new IllegalStateException("A contract bandwidth for the vif is required in accordance with the vif role " +
                        $"of '{this.VifRole.Name}' but none is defined.");
                }

                var aggContractBandwidthMbps = this.Attachment.Vifs
                                                              .Select(
                                                               vif =>
                                                               vif.ContractBandwidthPool.ContractBandwidth.BandwidthMbps)
                                                              .Aggregate(0, (x, y) => x + y);

                var attachmentBandwidthMbps = this.Attachment.AttachmentBandwidth.BandwidthGbps * 1000;
                if (attachmentBandwidthMbps < aggContractBandwidthMbps)
                {
                    throw new IllegalStateException($"The vif contract bandwidth of " +
                        $"{this.ContractBandwidthPool.ContractBandwidth.BandwidthMbps} Mbps is greater " +
                        $"than the remaining available bandwidth of the attachment " +
                        $"({attachmentBandwidthMbps - aggContractBandwidthMbps + this.ContractBandwidthPool.ContractBandwidth.BandwidthMbps } Mbps).");
                }
            }
            else
            {
                if (this.ContractBandwidthPool != null)
                {
                    throw new IllegalStateException("A contract bandwidth for the vif is defined but is NOT required for the vif role " +
                        $"of '{this.VifRole.Name}'.");
                }
            }
        }

        public virtual void ValidateDelete()
        {
            if (this.RoutingInstance != null)
            {
                if (this.RoutingInstance.AttachmentSetRoutingInstances.Any())
                {
                    throw new IllegalDeleteAttemptException("The vif is a member belongs to one or more attachment sets " +
                        "and cannot be deleted. Remove the vif from all attachment sets first.");
                }
            }
        }
    }
}