using Microsoft.EntityFrameworkCore;
using Mind.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using IO.Swagger.Model;

namespace SCM.Models
{
    /// <summary>
    /// Vif nova client dto extensions.
    /// </summary>
    public static class VifNovaClientDtoExtensions
    {
        /// <summary>
        /// Create an instance of the nova vif dto.
        /// </summary>
        /// <returns>The nova vif dto.</returns>
        /// <param name="vif">An instance of Vif</param>
        public static DataAttachmentAttachmentPePePeName ToNovaVifDto(this Vif vif)
        {
            var vifs = (from vlan in vif.Vlans
                        select new DataAttachmentAttachmentPePepenameTaggedattachmentinterfaceTaggedattachmentinterfaceinterfacetypeTaggedattachmentinterfaceinterfaceidVifVifvlanidAttachmentvif
                        {
                            VlanId = vif.VlanTag,
                            VrfName = vif.RoutingInstance.Name,
                            ContractBandwidthPoolName = vif.ContractBandwidthPool.Name,
                            EnableIpv4 = vif.RoutingInstance.RoutingInstanceType.IsLayer3.ToString().ToLower(),
                            Ipv4 = new DataAttachmentAttachmentPePepenameTaggedattachmentinterfaceTaggedattachmentinterfaceinterfacetypeTaggedattachmentinterfaceinterfaceidVifVifvlanidIpv4Attachmentipv4
                            {
                                Ipv4Address = vlan.IpAddress,
                                Ipv4SubnetMask = vlan.SubnetMask
                            }
                        }).ToList();

            var bgpPeers = (from bgpPeer in vif.RoutingInstance.BgpPeers
                            select new DataAttachmentAttachmentPePepenameVrfVrfvrfnameBgppeerBgppeerpeeripv4addressAttachmentbgppeer
                            {
                                PeerIpv4Address = bgpPeer.Ipv4PeerAddress,
                                PeerPassword = bgpPeer.PeerPassword,
                                PeerAutonomousSystem = bgpPeer.Peer2ByteAutonomousSystem,
                                IsBfdEnabled = bgpPeer.IsBfdEnabled.ToString().ToLower(),
                                IsMultiHop = bgpPeer.IsMultiHop.ToString().ToLower(),
                                MaxPeerRoutes = bgpPeer.MaximumRoutes
                            }).ToList();

            var data = new DataAttachmentAttachmentPePePeName
            {
                Attachmentpe = new List<DataAttachmentAttachmentPePepenameAttachmentpe>
                {
                    new DataAttachmentAttachmentPePepenameAttachmentpe
                    {
                        PeName = vif.Attachment.Device.Name,
                        Vrf = new List<DataAttachmentAttachmentPePepenameVrfVrfvrfnameAttachmentvrf>
                        {
                            new DataAttachmentAttachmentPePepenameVrfVrfvrfnameAttachmentvrf
                            {
                                VrfName = vif.RoutingInstance.Name,
                                RdAdministratorSubfield = vif.RoutingInstance.AdministratorSubField,
                                RdAssignedNumberSubfield = vif.RoutingInstance.AssignedNumberSubField,
                                BgpPeer = bgpPeers
                            }
                        },
                        TaggedAttachmentInterface = new List<DataAttachmentAttachmentPePepenameTaggedattachmentinterfaceTaggedattachmentinterfaceinterfacetypeTaggedattachmentinterfaceinterfaceidAttachmenttaggedattachmentinterface>
                        {
                            new DataAttachmentAttachmentPePepenameTaggedattachmentinterfaceTaggedattachmentinterfaceinterfacetypeTaggedattachmentinterfaceinterfaceidAttachmenttaggedattachmentinterface
                            {
                                InterfaceId = vif.Attachment.PortName,
                                InterfaceType = Enum.Parse<DataAttachmentAttachmentPePepenameTaggedattachmentinterfaceTaggedattachmentinterfaceinterfacetypeTaggedattachmentinterfaceinterfaceidAttachmenttaggedattachmentinterface
                                                    .InterfaceTypeEnum>(vif.Attachment.PortType),
                                AttachmentBandwidth = Enum.Parse<DataAttachmentAttachmentPePepenameTaggedattachmentinterfaceTaggedattachmentinterfaceinterfacetypeTaggedattachmentinterfaceinterfaceidAttachmenttaggedattachmentinterface
                                                          .AttachmentBandwidthEnum>(vif.Attachment.AttachmentBandwidth.BandwidthGbps.ToString()),
                                InterfaceMtu = Enum.Parse<DataAttachmentAttachmentPePepenameTaggedattachmentinterfaceTaggedattachmentinterfaceinterfacetypeTaggedattachmentinterfaceinterfaceidAttachmenttaggedattachmentinterface
                                                   .InterfaceMtuEnum>(vif.Attachment.Mtu.MtuValue.ToString()),

                                Vif = vifs,
                                ContractBandwidthPool = new List<DataAttachmentAttachmentPePepenameTaggedattachmentinterfaceTaggedattachmentinterfaceinterfacetypeTaggedattachmentinterfaceinterfaceidContractbandwidthpoolContractbandwidthpoolnameAttachmentcontractbandwidthpool>
                                {
                                    new DataAttachmentAttachmentPePepenameTaggedattachmentinterfaceTaggedattachmentinterfaceinterfacetypeTaggedattachmentinterfaceinterfaceidContractbandwidthpoolContractbandwidthpoolnameAttachmentcontractbandwidthpool
                                    {
                                        Name = vif.ContractBandwidthPool.Name,
                                        ContractBandwidth = Enum.Parse<DataAttachmentAttachmentPePepenameTaggedattachmentinterfaceTaggedattachmentinterfaceinterfacetypeTaggedattachmentinterfaceinterfaceidContractbandwidthpoolContractbandwidthpoolnameAttachmentcontractbandwidthpool
                                                                .ContractBandwidthEnum>(vif.ContractBandwidthPool.ContractBandwidth.BandwidthMbps.ToString()),
                                        TrustReceivedCosAndDscp = vif.ContractBandwidthPool.TrustReceivedCosAndDscp.ToString().ToLower(),
                                        
                                        // TO-DO - add service classes
                                    }
                                }
                            }
                        }
                    }
                }
            };

            return data;
        }
    }

    public static class VifQueryableExtensions
    {
        public static IQueryable<Vif> IncludeValidationProperties(this IQueryable<Vif> query)
        {
            return query.Include(x => x.Attachment.Vifs)
                        .ThenInclude(x => x.ContractBandwidthPool.ContractBandwidth)
                        .Include(x => x.Attachment.AttachmentBandwidth)
                        .Include(x => x.Attachment.Device)
                        .Include(x => x.Attachment.Interfaces)
                        .Include(x => x.Attachment.Mtu)
                        .Include(x => x.Vlans)
                        .Include(x => x.VifRole.AttachmentRole.PortPool.PortRole)
                        .Include(x => x.VifRole.RoutingInstanceType)
                        .Include(x => x.Tenant)
                        .Include(x => x.ContractBandwidthPool)
                        .Include(x => x.Mtu)
                        .Include(x => x.RoutingInstance.BgpPeers);
        }

        public static IQueryable<Vif> IncludeDeleteValidationProperties(this IQueryable<Vif> query)
        {
            return query.Include(x => x.Attachment.Device)
                        .Include(x => x.Attachment.Interfaces)
                        .ThenInclude(x => x.Ports)
                        .Include(x => x.RoutingInstance.Vifs)
                        .Include(x => x.RoutingInstance.Attachments)
                        .Include(x => x.ContractBandwidthPool)
                        .Include(x => x.Vlans)
                        .Include(x => x.RoutingInstance.RoutingInstanceType)
                        .Include(x => x.RoutingInstance.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.AttachmentSet)
                        .Include(x => x.ContractBandwidthPool.Vifs)
                        .Include(x => x.ContractBandwidthPool.Attachments)
                        .Include(x => x.RoutingInstance.BgpPeers); 
        }

        public static IQueryable<Vif> IncludeDeepProperties(this IQueryable<Vif> query)
        {
            return query.Include(x => x.VifRole)
                        .Include(x => x.Attachment.Interfaces)
                        .ThenInclude(x => x.Ports)
                        .Include(x => x.ContractBandwidthPool.ContractBandwidth)
                        .Include(x => x.RoutingInstance.BgpPeers)
                        .ThenInclude(x => x.VpnTenantIpNetworksIn)
                        .ThenInclude(x => x.TenantIpNetwork)
                        .Include(x => x.RoutingInstance.BgpPeers)
                        .ThenInclude(x => x.VpnTenantIpNetworksOut)
                        .ThenInclude(x => x.TenantIpNetwork)
                        .Include(x => x.RoutingInstance.LogicalInterfaces)
                        .Include(x => x.Vlans)
                        .Include(x => x.Tenant)
                        .Include(x => x.Mtu);
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
                if (this.Vlans.Count(x => !string.IsNullOrEmpty(x.IpAddress) && !string.IsNullOrEmpty(x.SubnetMask)) != this.Vlans.Count)
                {
                    throw new IllegalStateException("The vif is enabled for layer 3 but insufficient IPv4 addresses have been requested.");
                }
            }
            else if (this.Vlans.Any(x => !string.IsNullOrEmpty(x.IpAddress) || !string.IsNullOrEmpty(x.SubnetMask)))
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

                // Bear in mind that if a new VIF has been created it will not appear in the VIFs collection of the Attachment entity
                // This is why we check to exclude any VIF with the same ID as this VIF, and then add the contract bandwidth of this VIF
                // to the aggregate bandwidth
                var aggContractBandwidthMbps = this.Attachment.Vifs
                                                              .Where(
                                                                vif => 
                                                                vif.VifID != this.VifID)
                                                              .Select(
                                                                vif =>
                                                                vif.ContractBandwidthPool.ContractBandwidth.BandwidthMbps)
                                                              .Aggregate(0, (x, y) => x + y) + this.ContractBandwidthPool.ContractBandwidth.BandwidthMbps;

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
            var sb = new StringBuilder();
            if (this.RoutingInstance != null)
            {
                (from attachmentSetRoutingInstances in this.RoutingInstance.AttachmentSetRoutingInstances
                 select attachmentSetRoutingInstances)
                 .ToList()
                    .ForEach(attachmentSetRoutingInstance =>
                    {
                        sb.Append($"Vif '{this.Name}' is belongs to attachment set '{attachmentSetRoutingInstance.AttachmentSet.Name}' " +
                        "and therefore cannot be deleted. Remove the routing instance from the attachment set first.");
                    });
                }

            if (sb.Length > 0) throw new IllegalDeleteAttemptException(sb.ToString());
        }
    }
}