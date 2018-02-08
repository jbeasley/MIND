using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Xml.Linq;
using SCM.Models;

namespace SCM.Data
{
    /// <summary>
    /// Initialise the database and populate tables with reference data.
    /// </summary>
    public static class DbInitializer
    {
        public static void Initialize(SigmaContext context)
        {

            // Apply pending migrations to the db

            context.Database.Migrate();

            // Check if the db is populated with any data by checking the VpnProtocolTypes entity.
            // If data is present, quit. Otherwise pre-populate certain tables with some default data.

            if (context.VpnProtocolTypes.ToList().Count > 0)
            {
                return;
            }

            var vpnProtocolTypes = new List<VpnProtocolType>
            {
                new VpnProtocolType { Name = "IP", ProtocolType = ProtocolType.IP },
                new VpnProtocolType { Name = "Ethernet", ProtocolType = ProtocolType.Ethernet }
            };

            foreach (VpnProtocolType v in vpnProtocolTypes)
            {
                context.VpnProtocolTypes.Add(v);
            }

            // Add support for IPv6 address family here when required to support
            // IPv6 VPN services

            var addressFamilies = new List<AddressFamily>
            {
                new AddressFamily {Name = "IPv4" }
            };

            foreach (var v in addressFamilies)
            {
                context.AddressFamilies.Add(v);
            }

            // Add Device Roles to the list below as needed

            var deviceRoles = new List<DeviceRole>
            {
                new DeviceRole { Name = "PE", Description = "Provider Edge Device", IsProviderDomainRole = true, RequireSyncToNetwork = true},
                new DeviceRole { Name = "P", Description = "Provider Device", IsProviderDomainRole = true },
                new DeviceRole { Name = "HBA", Description = "High Bandwidth Aggregation Client Distribution Device", IsProviderDomainRole = true },
                new DeviceRole { Name = "LBA", Description = "Low Bandwidth Aggregation Client Distribution Device", IsProviderDomainRole = true },
                new DeviceRole { Name = "SHC", Description = "Service Hub CPE Device", IsTenantDomainRole = true },
                new DeviceRole { Name = "CE", Description = "Generic CE Device", IsTenantDomainRole = true }
            };

            foreach (DeviceRole d in deviceRoles)
            {
                context.DeviceRoles.Add(d);
            }

            // Add Device Models to the list below as needed

            var deviceModels = new List<DeviceModel>
            {
                new DeviceModel { Name = "ASR-9001", Description = "Cisco ASR 9001 router" },
                new DeviceModel { Name = "ASR-9006", Description = "Cisco ASR 9006 router" },
                new DeviceModel { Name = "ASR-9010", Description = "Cisco ASR 9010 router" },
                new DeviceModel { Name = "EX-9208", Description = "Juniper EX router" },
                new DeviceModel { Name = "7208", Description = "Cisco 7208 router" }
            };

            foreach (DeviceModel d in deviceModels)
            {
                context.DeviceModels.Add(d);
            }

            // Add Device Status options to the list below as needed.
            // DO NOT change the value of the name property of the entries below because
            // business logic depends on the name.

            var deviceStatus = new List<DeviceStatus>
            {
                new DeviceStatus { Name = "Staging",
                    Description = "Device is being staged",
                    DeviceStatusType = DeviceStatusType.Staging },
                new DeviceStatus { Name = "Production",
                    Description = "Device is in production",
                    DeviceStatusType = DeviceStatusType.Production },
                new DeviceStatus { Name = "Retired",
                    Description = "Device is retired from production",
                    DeviceStatusType = DeviceStatusType.Retired }
            };

            foreach (DeviceStatus d in deviceStatus)
            {
                context.DeviceStatuses.Add(d);
            }

            // Add Port Roles to the list below as needed

            var portRoles = new List<PortRole>
            {
                new PortRole { Name = "Tenant-Facing",
                    Description = "Port Role for Provider Domain Attachment to Tenant Domains",
                    PortRoleType = PortRoleType.TenantFacing }, 
                new PortRole { Name = "Provider-Infrastructure",
                    Description = "Port Role for Provider Domain Infrastructure Attachment",
                    PortRoleType = PortRoleType.ProviderInfrastructure },
                new PortRole { Name = "Tenant-Infrastructure",
                    Description = "Port Role for Tenant Domain Infrastructure Attachment",
                    PortRoleType = PortRoleType.TenantInfrastructure}
            };

            foreach (PortRole p in portRoles)
            {
                context.PortRoles.Add(p);
            }

            context.SaveChanges();

            // Add additional mapping of Device Roles to Port Roles to the list below as needed
            // These mappings allow the required Port Roles to be associated to Ports of Devices which 
            // belong to the specified Device Role
         
            var deviceRolePortRoles = new List<DeviceRolePortRole>
            {
                new DeviceRolePortRole { DeviceRoleID = deviceRoles.Single(x => x.Name == "PE").DeviceRoleID,
                                         PortRoleID = portRoles.Single(x => x.PortRoleType == PortRoleType.TenantFacing).PortRoleID },
                new DeviceRolePortRole { DeviceRoleID = deviceRoles.Single(x => x.Name == "PE").DeviceRoleID,
                                         PortRoleID = portRoles.Single(x => x.PortRoleType == PortRoleType.ProviderInfrastructure).PortRoleID },
                new DeviceRolePortRole { DeviceRoleID = deviceRoles.Single(x => x.Name == "P").DeviceRoleID,
                                         PortRoleID = portRoles.Single(x => x.PortRoleType == PortRoleType.ProviderInfrastructure).PortRoleID },
                new DeviceRolePortRole { DeviceRoleID = deviceRoles.Single(x => x.Name == "HBA").DeviceRoleID,
                                         PortRoleID = portRoles.Single(x => x.PortRoleType == PortRoleType.TenantFacing).PortRoleID },
                new DeviceRolePortRole { DeviceRoleID = deviceRoles.Single(x => x.Name == "HBA").DeviceRoleID,
                                         PortRoleID = portRoles.Single(x => x.PortRoleType == PortRoleType.ProviderInfrastructure).PortRoleID },
                new DeviceRolePortRole { DeviceRoleID = deviceRoles.Single(x => x.Name == "LBA").DeviceRoleID,
                                         PortRoleID = portRoles.Single(x => x.PortRoleType == PortRoleType.TenantFacing).PortRoleID },
                new DeviceRolePortRole { DeviceRoleID = deviceRoles.Single(x => x.Name == "LBA").DeviceRoleID,
                                         PortRoleID = portRoles.Single(x => x.PortRoleType == PortRoleType.ProviderInfrastructure).PortRoleID },
                new DeviceRolePortRole { DeviceRoleID = deviceRoles.Single(x => x.Name == "SHC").DeviceRoleID,
                                         PortRoleID = portRoles.Single(x => x.PortRoleType == PortRoleType.TenantInfrastructure).PortRoleID },
                new DeviceRolePortRole { DeviceRoleID = deviceRoles.Single(x => x.Name == "CE").DeviceRoleID,
                                         PortRoleID = portRoles.Single(x => x.PortRoleType == PortRoleType.TenantInfrastructure).PortRoleID }
            };

            foreach (DeviceRolePortRole d in deviceRolePortRoles)
            {
                context.DeviceRolePortRoles.Add(d);
            }

            // Add Port Pools to the list below as needed

            var portPools = new List<PortPool>
            {
                new PortPool { Name = "Delivery-Direct",
                    Description = "Port Pool for port allocations exclusively for Delivery Direct tenant-facing port allocations",
                    PortRoleID = portRoles.Single(x => x.PortRoleType == PortRoleType.TenantFacing).PortRoleID },
                new PortPool { Name = "General",
                    Description = "Port Pool for general tenant-facing port allocations",
                    PortRoleID = portRoles.Single(x => x.PortRoleType == PortRoleType.TenantFacing).PortRoleID },
                new PortPool { Name = "Core",
                    Description = "Port Pool for core-facing infrastructure port allocations",
                    PortRoleID = portRoles.Single(x => x.PortRoleType == PortRoleType.ProviderInfrastructure).PortRoleID },
                new PortPool { Name = "Provider",
                    Description = "Port Pool for provider-facing port allocations",
                    PortRoleID = portRoles.Single(x => x.PortRoleType == PortRoleType.TenantInfrastructure).PortRoleID },
                new PortPool { Name = "Tenant-LAN",
                    Description = "Port Pool for tenant LAN port allocations",
                    PortRoleID = portRoles.Single(x => x.PortRoleType == PortRoleType.TenantInfrastructure).PortRoleID },
            };

            foreach (PortPool p in portPools)
            {
                context.PortPools.Add(p);
            }

            // Add Port SFPs to the list below as needed

            var portSfps = new List<PortSfp>
            {
                new PortSfp { Name = "GLC-T", Description = "1G Copper SFP" },
                new PortSfp { Name = "SFP-10G-SR", Description = "10G Short Range Fiber SFP" },
                new PortSfp { Name = "SFP-10G-LR", Description = "10G Long Range Fiber SFP" }
            };

            foreach (PortSfp p in portSfps)
            {
                context.PortSfps.Add(p);
            }

            // Add Port Connectors to the list below as needed

            var portConnectors = new List<PortConnector>
            {
                new PortConnector { Name = "LC", Description = "LC connector type" },
                new PortConnector { Name = "SC", Description = "SC connector type" },
                new PortConnector { Name = "RJ45", Description = "RJ45 connector type" }
            };

            foreach (PortConnector p in portConnectors)
            {
                context.PortConnectors.Add(p);
            }

            // Add Port Statuses to the list below as needed

            var portStatuses = new List<PortStatus>
            {
                new PortStatus { Name = "Free",
                    Description = "The port is free for allocation",
                    PortStatusType = PortStatusType.Free },
                new PortStatus { Name = "Assigned",
                    Description = "The port is assigned",
                    PortStatusType = PortStatusType.Assigned },
                new PortStatus { Name = "Reserved",
                    Description = "The port is reserved",
                    PortStatusType = PortStatusType.Reserved },
                new PortStatus { Name = "Locked",
                    Description = "The port is locked",
                    PortStatusType = PortStatusType.Locked },
                new PortStatus { Name = "Migration",
                    Description = "The port is reserved for migration purposes",
                    PortStatusType = PortStatusType.Migration },
            };

            foreach (PortStatus p in portStatuses)
            {
                context.PortStatuses.Add(p);
            }

            // Add Routing Instance Types to the list below as needed

            var routingInstanceTypes = new List<RoutingInstanceType>
            {
                new RoutingInstanceType { Name = "Layer3-Default",
                    Description = "Global Layer 3 Default Routing Instance Type",
                    IsLayer3 = true,
                    IsDefault = true},
                new RoutingInstanceType { Name = "Layer3-Tenant-VRF",
                    Description = "Layer 3 Virtual Routing and Forwarding Routing Instance Type for Tenants",
                    IsLayer3 = true,
                    IsVrf = true,
                    IsTenantFacingVrf = true},
                new RoutingInstanceType { Name = "Layer3-Infrastructure-VRF",
                    Description = "Layer 3 Virtual Routing and Forwarding Routing Instance Type for Infrastructure",
                    IsLayer3 = true,
                    IsVrf = true,
                    IsInfrastructureVrf = true}
            };

            foreach (RoutingInstanceType r in routingInstanceTypes)
            {
                context.RoutingInstanceTypes.Add(r);
            }

            context.SaveChanges();

            // Add Attachment Roles to the list below as needed

            var attachmentRoles = new List<AttachmentRole>
            {
                new AttachmentRole { Name = "CMC", Description = "Attachment Role for Delivery Direct Customer-Managed Comms circuit attachment",
                    PortPoolID = portPools.Single(x => x.Name == "Delivery-Direct").PortPoolID,
                    IsLayer3Role = true,
                    RequireContractBandwidth = true,
                    RequireSyncToNetwork = true,
                    RoutingInstanceTypeID = routingInstanceTypes.Single(x => x.IsLayer3 && x.IsVrf && x.IsTenantFacingVrf).RoutingInstanceTypeID },
                new AttachmentRole { Name = "TRMC-HUB", Description = "Attachment Role for Delivery Direct Thomson Reuters Managed Comms hub circuit attachment",
                    PortPoolID = portPools.Single(x => x.Name == "Delivery-Direct").PortPoolID,
                    IsTaggedRole = true,
                    RequireSyncToNetwork = true },
                new AttachmentRole { Name = "TRMC", Description = "Attachment Role for Delivery Direct Thomson Reuters Managed Comms circuit attachment",
                    PortPoolID = portPools.Single(x => x.Name == "Delivery-Direct").PortPoolID,
                    IsLayer3Role = true,
                    RequireContractBandwidth = true,
                    RequireSyncToNetwork = true,
                    RoutingInstanceTypeID = routingInstanceTypes.Single(x => x.IsLayer3 && x.IsVrf && x.IsTenantFacingVrf).RoutingInstanceTypeID },
                new AttachmentRole { Name = "TRME-NNI", Description = "Attachment Role for Delivery Direct Managed Extranet NNI attachment",
                    PortPoolID = portPools.Single(x => x.Name == "Delivery-Direct").PortPoolID,
                    IsTaggedRole = true,
                    SupportedByMultiPort = true,
                    RequireSyncToNetwork = true },
                new AttachmentRole { Name = "PE-P", Description = "Attachment Role for PE attachment to a P device",
                    PortPoolID = portPools.Single(x => x.Name == "Core").PortPoolID,
                    SupportedByBundle = true,
                    IsLayer3Role = true,
                    RoutingInstanceTypeID = routingInstanceTypes.Single(x => x.IsLayer3 && x.IsDefault).RoutingInstanceTypeID },
                 new AttachmentRole { Name = "P-PE", Description = "Attachment Role for P device attachment to a PE device",
                    PortPoolID = portPools.Single(x => x.Name == "Core").PortPoolID,
                    SupportedByBundle = true,
                    IsLayer3Role = true,
                    RoutingInstanceTypeID = routingInstanceTypes.Single(x => x.IsLayer3 && x.IsDefault).RoutingInstanceTypeID },
                new AttachmentRole { Name = "LBA-PE", Description = "Attachment Role for Low Bandwidth Aggregation device attachment to a PE device",
                    PortPoolID = portPools.Single(x => x.Name == "Core").PortPoolID,
                    SupportedByBundle = true,
                    IsTaggedRole = true },
                new AttachmentRole { Name = "PE-LBA", Description = "Attachment Role for PE device attachment to a Low Bandwidth Aggregation device",
                    PortPoolID = portPools.Single(x => x.Name == "Core").PortPoolID,
                    SupportedByBundle = true,
                    IsTaggedRole = true,
                    RequireSyncToNetwork = true },
                new AttachmentRole { Name = "HBA-PE", Description = "Attachment Role for High Bandwidth Aggregation device attachment to a PE device",
                    PortPoolID = portPools.Single(x => x.Name == "Core").PortPoolID,
                    SupportedByBundle = true,
                    IsTaggedRole = true },
                new AttachmentRole { Name = "PE-HBA", Description = "Attachment Role for PE device attachment to a High Bandwidth Aggregation device",
                    PortPoolID = portPools.Single(x => x.Name == "Core").PortPoolID,
                    SupportedByBundle = true,
                    IsTaggedRole = true,
                    RequireSyncToNetwork = true },
                new AttachmentRole { Name = "P-P", Description = "Attachment Role for P device attachment to a P device",
                    PortPoolID = portPools.Single(x => x.Name == "Core").PortPoolID,
                    SupportedByBundle = true,
                    IsLayer3Role = true,
                    RoutingInstanceTypeID = routingInstanceTypes.Single(x => x.IsLayer3 && x.IsDefault).RoutingInstanceTypeID },
                new AttachmentRole { Name = "DC-UNTAGGED", Description = "Attachment Role for Data-Center untagged attachment",
                    PortPoolID = portPools.Single(x => x.Name == "General").PortPoolID,
                    IsLayer3Role = true,
                    SupportedByBundle = true,
                    RequireContractBandwidth = true,
                    RequireSyncToNetwork = true,
                    RoutingInstanceTypeID = routingInstanceTypes.Single(x => x.IsLayer3 && x.IsVrf && x.IsTenantFacingVrf).RoutingInstanceTypeID },
                new AttachmentRole { Name = "DC-TAGGED", Description = "Attachment Role for Data-Center tagged attachment",
                    PortPoolID = portPools.Single(x => x.Name == "General").PortPoolID,
                    SupportedByBundle = true,
                    IsTaggedRole = true,
                    RequireSyncToNetwork = true },
                new AttachmentRole { Name = "PE-SHC-TAGGED", Description = "Attachment Role for PE device tagged attachment to a Service Hub CPE",
                    PortPoolID = portPools.Single(x => x.Name == "General").PortPoolID,
                    SupportedByMultiPort = true,
                    IsTaggedRole = true,
                    RequireSyncToNetwork = true },
                new AttachmentRole { Name = "SHC-PE-TAGGED", Description = "Attachment Role for Service Hub CPE tagged attachment to a PE device",
                    PortPoolID = portPools.Single(x => x.Name == "Provider").PortPoolID,
                    SupportedByMultiPort = true,
                    IsTaggedRole = true },
                new AttachmentRole { Name = "PE-CE-TAGGED", Description = "Attachment Role for PE device tagged attachment to a generic CE",
                    PortPoolID = portPools.Single(x => x.Name == "General").PortPoolID,
                    SupportedByMultiPort = true,
                    SupportedByBundle = true,
                    IsTaggedRole = true,
                    RequireSyncToNetwork = true },
                new AttachmentRole { Name = "CE-PE-TAGGED", Description = "Attachment Role for generic CE tagged attachment to a PE device",
                    PortPoolID = portPools.Single(x => x.Name == "Provider").PortPoolID,
                    SupportedByMultiPort = true,
                    SupportedByBundle = true,
                    IsTaggedRole = true },
                new AttachmentRole { Name = "PE-CE-UNTAGGED", Description = "Attachment Role for PE device untagged attachment to a generic CE",
                    PortPoolID = portPools.Single(x => x.Name == "General").PortPoolID,
                    SupportedByMultiPort = true,
                    SupportedByBundle = true,
                    IsLayer3Role = true,
                    RequireContractBandwidth = true,
                    RequireSyncToNetwork = true,
                    RoutingInstanceTypeID = routingInstanceTypes.Single(x => x.IsLayer3 && x.IsVrf && x.IsTenantFacingVrf).RoutingInstanceTypeID },
                new AttachmentRole { Name = "CE-PE-UNTAGGED", Description = "Attachment Role for generic CE untagged attachment to a PE device",
                    PortPoolID = portPools.Single(x => x.Name == "Provider").PortPoolID,
                    SupportedByMultiPort = true,
                    SupportedByBundle = true,
                    IsLayer3Role = true,
                    RequireContractBandwidth = true,
                    RoutingInstanceTypeID = routingInstanceTypes.Single(x => x.IsLayer3 && x.IsDefault).RoutingInstanceTypeID },
                new AttachmentRole { Name = "CE-LAN-UNTAGGED", Description = "Attachment Role for generic CE untagged attachment to a Tenant LAN",
                    PortPoolID = portPools.Single(x => x.Name == "Tenant-LAN").PortPoolID,
                    IsLayer3Role = true,
                    RoutingInstanceTypeID = routingInstanceTypes.Single(x => x.IsLayer3 && x.IsDefault).RoutingInstanceTypeID },
            };

            foreach (AttachmentRole a in attachmentRoles)
            {
                context.AttachmentRoles.Add(a);
            }

            context.SaveChanges();

            // Add additional mappings of Device Roles to Attachment Roles below as needed
            // Use these mappings to make Attachment Roles available to the user depending on the Device Role of the Device

            var deviceRoleAttachmentRoles = new List<DeviceRoleAttachmentRole>
            {
                new DeviceRoleAttachmentRole { DeviceRoleID = deviceRoles.Single(x => x.Name == "PE").DeviceRoleID,
                                         AttachmentRoleID = attachmentRoles.Single(x => x.Name == "TRMC-HUB").AttachmentRoleID },
                new DeviceRoleAttachmentRole { DeviceRoleID = deviceRoles.Single(x => x.Name == "PE").DeviceRoleID,
                                         AttachmentRoleID = attachmentRoles.Single(x => x.Name == "CMC").AttachmentRoleID },
                new DeviceRoleAttachmentRole { DeviceRoleID = deviceRoles.Single(x => x.Name == "PE").DeviceRoleID,
                                         AttachmentRoleID = attachmentRoles.Single(x => x.Name == "TRMC").AttachmentRoleID },
                new DeviceRoleAttachmentRole { DeviceRoleID = deviceRoles.Single(x => x.Name == "PE").DeviceRoleID,
                                         AttachmentRoleID = attachmentRoles.Single(x => x.Name == "TRME-NNI").AttachmentRoleID },
                new DeviceRoleAttachmentRole { DeviceRoleID = deviceRoles.Single(x => x.Name == "PE").DeviceRoleID,
                                         AttachmentRoleID = attachmentRoles.Single(x => x.Name == "PE-P").AttachmentRoleID },
                new DeviceRoleAttachmentRole { DeviceRoleID = deviceRoles.Single(x => x.Name == "LBA").DeviceRoleID,
                                         AttachmentRoleID = attachmentRoles.Single(x => x.Name == "TRMC-HUB").AttachmentRoleID },
                new DeviceRoleAttachmentRole { DeviceRoleID = deviceRoles.Single(x => x.Name == "HBA").DeviceRoleID,
                                         AttachmentRoleID = attachmentRoles.Single(x => x.Name == "CMC").AttachmentRoleID },
                new DeviceRoleAttachmentRole { DeviceRoleID = deviceRoles.Single(x => x.Name == "HBA").DeviceRoleID,
                                         AttachmentRoleID = attachmentRoles.Single(x => x.Name == "TRMC").AttachmentRoleID },
                new DeviceRoleAttachmentRole { DeviceRoleID = deviceRoles.Single(x => x.Name == "LBA").DeviceRoleID,
                                         AttachmentRoleID = attachmentRoles.Single(x => x.Name == "TRME-NNI").AttachmentRoleID },
                new DeviceRoleAttachmentRole { DeviceRoleID = deviceRoles.Single(x => x.Name == "P").DeviceRoleID,
                                         AttachmentRoleID = attachmentRoles.Single(x => x.Name == "P-PE").AttachmentRoleID },
                new DeviceRoleAttachmentRole { DeviceRoleID = deviceRoles.Single(x => x.Name == "P").DeviceRoleID,
                                         AttachmentRoleID = attachmentRoles.Single(x => x.Name == "P-P").AttachmentRoleID },
                new DeviceRoleAttachmentRole { DeviceRoleID = deviceRoles.Single(x => x.Name == "HBA").DeviceRoleID,
                                         AttachmentRoleID = attachmentRoles.Single(x => x.Name == "HBA-PE").AttachmentRoleID },
                new DeviceRoleAttachmentRole { DeviceRoleID = deviceRoles.Single(x => x.Name == "LBA").DeviceRoleID,
                                         AttachmentRoleID = attachmentRoles.Single(x => x.Name == "LBA-PE").AttachmentRoleID },
                new DeviceRoleAttachmentRole { DeviceRoleID = deviceRoles.Single(x => x.Name == "PE").DeviceRoleID,
                                         AttachmentRoleID = attachmentRoles.Single(x => x.Name == "PE-LBA").AttachmentRoleID },
                new DeviceRoleAttachmentRole { DeviceRoleID = deviceRoles.Single(x => x.Name == "PE").DeviceRoleID,
                                         AttachmentRoleID = attachmentRoles.Single(x => x.Name == "PE-HBA").AttachmentRoleID },
                new DeviceRoleAttachmentRole { DeviceRoleID = deviceRoles.Single(x => x.Name == "PE").DeviceRoleID,
                                         AttachmentRoleID = attachmentRoles.Single(x => x.Name == "DC-TAGGED").AttachmentRoleID },
                new DeviceRoleAttachmentRole { DeviceRoleID = deviceRoles.Single(x => x.Name == "PE").DeviceRoleID,
                                         AttachmentRoleID = attachmentRoles.Single(x => x.Name == "DC-UNTAGGED").AttachmentRoleID },
                new DeviceRoleAttachmentRole { DeviceRoleID = deviceRoles.Single(x => x.Name == "PE").DeviceRoleID,
                                         AttachmentRoleID = attachmentRoles.Single(x => x.Name == "PE-SHC-TAGGED").AttachmentRoleID },
                new DeviceRoleAttachmentRole { DeviceRoleID = deviceRoles.Single(x => x.Name == "PE").DeviceRoleID,
                                         AttachmentRoleID = attachmentRoles.Single(x => x.Name == "PE-CE-TAGGED").AttachmentRoleID },
                new DeviceRoleAttachmentRole { DeviceRoleID = deviceRoles.Single(x => x.Name == "PE").DeviceRoleID,
                                         AttachmentRoleID = attachmentRoles.Single(x => x.Name == "PE-CE-UNTAGGED").AttachmentRoleID },
                new DeviceRoleAttachmentRole { DeviceRoleID = deviceRoles.Single(x => x.Name == "LBA").DeviceRoleID,
                                         AttachmentRoleID = attachmentRoles.Single(x => x.Name == "DC-TAGGED").AttachmentRoleID },
                new DeviceRoleAttachmentRole { DeviceRoleID = deviceRoles.Single(x => x.Name == "LBA").DeviceRoleID,
                                         AttachmentRoleID = attachmentRoles.Single(x => x.Name == "DC-UNTAGGED").AttachmentRoleID },
                new DeviceRoleAttachmentRole { DeviceRoleID = deviceRoles.Single(x => x.Name == "HBA").DeviceRoleID,
                                         AttachmentRoleID = attachmentRoles.Single(x => x.Name == "DC-TAGGED").AttachmentRoleID },
                new DeviceRoleAttachmentRole { DeviceRoleID = deviceRoles.Single(x => x.Name == "HBA").DeviceRoleID,
                                         AttachmentRoleID = attachmentRoles.Single(x => x.Name == "DC-UNTAGGED").AttachmentRoleID },
                new DeviceRoleAttachmentRole { DeviceRoleID = deviceRoles.Single(x => x.Name == "SHC").DeviceRoleID,
                                         AttachmentRoleID = attachmentRoles.Single(x => x.Name == "SHC-PE-TAGGED").AttachmentRoleID },
                new DeviceRoleAttachmentRole { DeviceRoleID = deviceRoles.Single(x => x.Name == "CE").DeviceRoleID,
                                         AttachmentRoleID = attachmentRoles.Single(x => x.Name == "CE-PE-TAGGED").AttachmentRoleID },
                new DeviceRoleAttachmentRole { DeviceRoleID = deviceRoles.Single(x => x.Name == "CE").DeviceRoleID,
                                         AttachmentRoleID = attachmentRoles.Single(x => x.Name == "CE-PE-UNTAGGED").AttachmentRoleID },
                new DeviceRoleAttachmentRole { DeviceRoleID = deviceRoles.Single(x => x.Name == "CE").DeviceRoleID,
                                         AttachmentRoleID = attachmentRoles.Single(x => x.Name == "CE-LAN-UNTAGGED").AttachmentRoleID }
            };

            foreach (DeviceRoleAttachmentRole d in deviceRoleAttachmentRoles)
            {
                context.DeviceRoleAttachmentRoles.Add(d);
            }

            // Add VIF Roles to the list below as needed

            var vifRoles = new List<VifRole>
            {
                new VifRole { Name = "TRMC-SERVICE",
                    Description = "VIF Role for Delivery Direct TRMC Tenant logical connectivity",
                    AttachmentRoleID = attachmentRoles.Single(x => x.Name == "TRMC-HUB").AttachmentRoleID,
                    IsLayer3Role = true,
                    RequireContractBandwidth = true,
                    RequireSyncToNetwork = true,
                    RoutingInstanceTypeID = routingInstanceTypes.Single(x => x.IsLayer3 && x.IsVrf && x.IsTenantFacingVrf).RoutingInstanceTypeID },
                new VifRole { Name = "TRME-SERVICE",
                    Description = "VIF Role for Delivery Direct TRME NNI logical connectivity",
                    AttachmentRoleID = attachmentRoles.Single(x => x.Name == "TRME-NNI").AttachmentRoleID,
                    IsLayer3Role = true,
                    RequireContractBandwidth = true,
                    RequireSyncToNetwork = true,
                    RoutingInstanceTypeID = routingInstanceTypes.Single(x => x.IsLayer3 && x.IsVrf && x.IsTenantFacingVrf).RoutingInstanceTypeID },
                new VifRole { Name = "DC-SERVICE",
                    Description = "VIF Role for Data-Center logical connectivity",
                    AttachmentRoleID = attachmentRoles.Single(x => x.Name == "DC-TAGGED").AttachmentRoleID,
                    IsLayer3Role = true,
                    RequireContractBandwidth = true,
                    RequireSyncToNetwork = true,
                    RoutingInstanceTypeID = routingInstanceTypes.Single(x => x.IsLayer3 && x.IsVrf && x.IsTenantFacingVrf).RoutingInstanceTypeID },
                new VifRole { Name = "PE-LBA-SERVICE",
                    Description = "VIF Role for PE-LBA logical connectivity",
                    AttachmentRoleID = attachmentRoles.Single(x => x.Name == "PE-LBA").AttachmentRoleID,
                    IsLayer3Role = true,
                    RequireContractBandwidth = true,
                    RequireSyncToNetwork = true,
                    RoutingInstanceTypeID = routingInstanceTypes.Single(x => x.IsLayer3 && x.IsVrf && x.IsInfrastructureVrf).RoutingInstanceTypeID },
                new VifRole { Name = "LBA-PE-SERVICE",
                    Description = "VIF Role for LBA-PE logical connectivity",
                    AttachmentRoleID = attachmentRoles.Single(x => x.Name == "LBA-PE").AttachmentRoleID,
                    IsLayer3Role = true,
                    RequireContractBandwidth = true,
                    RoutingInstanceTypeID = routingInstanceTypes.Single(x => x.IsLayer3 && x.IsVrf && x.IsInfrastructureVrf).RoutingInstanceTypeID },
                new VifRole { Name = "PE-HBA-SERVICE",
                    Description = "VIF Role for PE-HBA logical connectivity",
                    AttachmentRoleID = attachmentRoles.Single(x => x.Name == "PE-HBA").AttachmentRoleID,
                    IsLayer3Role = true,
                    RequireContractBandwidth = true,
                    RequireSyncToNetwork = true,
                    RoutingInstanceTypeID = routingInstanceTypes.Single(x => x.IsLayer3 && x.IsVrf && x.IsInfrastructureVrf).RoutingInstanceTypeID },
                new VifRole { Name = "HBA-PE-SERVICE",
                    Description = "VIF Role for HBA-PE logical connectivity",
                    AttachmentRoleID = attachmentRoles.Single(x => x.Name == "HBA-PE").AttachmentRoleID,
                    IsLayer3Role = true,
                    RequireContractBandwidth = true,
                    RoutingInstanceTypeID = routingInstanceTypes.Single(x => x.IsLayer3 && x.IsDefault).RoutingInstanceTypeID },
                new VifRole { Name = "PE-SHC-SERVICE",
                    Description = "VIF Role for PE to Service Hub logical connectivity",
                    AttachmentRoleID = attachmentRoles.Single(x => x.Name == "PE-SHC-TAGGED").AttachmentRoleID,
                    IsLayer3Role = true,
                    RequireContractBandwidth = true,
                    RequireSyncToNetwork = true,
                    RoutingInstanceTypeID = routingInstanceTypes.Single(x => x.IsLayer3 && x.IsVrf && x.IsTenantFacingVrf).RoutingInstanceTypeID },
                new VifRole { Name = "SHC-PE-SERVICE",
                    Description = "VIF Role for Service Hub to PE logical connectivity",
                    AttachmentRoleID = attachmentRoles.Single(x => x.Name == "SHC-PE-TAGGED").AttachmentRoleID,
                    IsLayer3Role = true,
                    RequireContractBandwidth = true,
                    RoutingInstanceTypeID = routingInstanceTypes.Single(x => x.IsLayer3 && x.IsDefault).RoutingInstanceTypeID },
                 new VifRole { Name = "PE-CE-SERVICE",
                    Description = "VIF Role for PE to generic CE logical connectivity",
                    AttachmentRoleID = attachmentRoles.Single(x => x.Name == "PE-CE-TAGGED").AttachmentRoleID,
                    IsLayer3Role = true,
                    RequireContractBandwidth = true,
                    RequireSyncToNetwork = true,
                    RoutingInstanceTypeID = routingInstanceTypes.Single(x => x.IsLayer3 && x.IsVrf && x.IsTenantFacingVrf).RoutingInstanceTypeID },
                new VifRole { Name = "CE-PE-SERVICE",
                    Description = "VIF Role for generic CE to PE logical connectivity",
                    AttachmentRoleID = attachmentRoles.Single(x => x.Name == "CE-PE-TAGGED").AttachmentRoleID,
                    IsLayer3Role = true,
                    RequireContractBandwidth = true,
                    RoutingInstanceTypeID = routingInstanceTypes.Single(x => x.IsLayer3 && x.IsDefault).RoutingInstanceTypeID }
            };

            foreach (VifRole v in vifRoles)
            {
                context.VifRoles.Add(v);
            }

            // Routing Policy Match options for creating constructs such as Community Sets

            var routingPolicyMatchOptions = new[]
            {
                new RoutingPolicyMatchOption {Name = "match-all"},
                new RoutingPolicyMatchOption {Name = "match-any"}
            };

            foreach (var v in routingPolicyMatchOptions)
            {
                context.RoutingPolicyMatchOptions.Add(v);
            }

            // VPN Tenancy Type options
            // Do NOT change these options because application logic depends on the values

            var vpnTenancyTypes = new[]
            {
                new VpnTenancyType { Name = "Single", TenancyType = TenancyType.Single },
                new VpnTenancyType { Name = "Multi", TenancyType = TenancyType.Multi }
            };

            foreach (VpnTenancyType v in vpnTenancyTypes)
            {
                context.VpnTenancyTypes.Add(v);
            }
                
            context.SaveChanges();

            // VPN Topology Type options
            // Do NOT change the Default name property because business logic code depends upon the name.

            var vpnTopologyTypes = new[]
            {
                new VpnTopologyType { TopologyType = TopologyType.AnytoAny,
                    Name = "Any-to-Any",
                    VpnProtocolTypeID = vpnProtocolTypes.Single(v => v.ProtocolType == ProtocolType.IP).VpnProtocolTypeID },
                new VpnTopologyType { TopologyType = TopologyType.HubandSpoke,
                    Name = "Hub-and-Spoke",
                    VpnProtocolTypeID = vpnProtocolTypes.Single(v => v.ProtocolType == ProtocolType.IP).VpnProtocolTypeID },
                new VpnTopologyType { TopologyType = TopologyType.PointtoPoint,
                    Name = "Point-to-Point",
                    VpnProtocolTypeID = vpnProtocolTypes.Single(v => v.ProtocolType == ProtocolType.Ethernet).VpnProtocolTypeID },
                new VpnTopologyType { TopologyType = TopologyType.Multipoint,
                    Name = "Multipoint",
                    VpnProtocolTypeID = vpnProtocolTypes.Single(v => v.ProtocolType == ProtocolType.Ethernet).VpnProtocolTypeID }
            };

            foreach (VpnTopologyType v in vpnTopologyTypes)
            {
                context.VpnTopologyTypes.Add(v);
            }

            // Multicast VPN Service Type options.
            // Do NOT change the Default name property because client-side code depends upon the name.

            var multicastVpnServiceTypes = new List<MulticastVpnServiceType>
            {
                new MulticastVpnServiceType {Name = "ASM", MvpnServiceType = MvpnServiceType.ASM },
                new MulticastVpnServiceType {Name = "SSM", MvpnServiceType = MvpnServiceType.SSM }
            };

            foreach (MulticastVpnServiceType m in multicastVpnServiceTypes)
            {
                context.MulticastVpnServiceTypes.Add(m);
            }

            // Multicast VPN Domain Type options.Do NOT change the name property of these items
            // because client-side code references the names

            var multicastVpnDomainTypes = new List<MulticastVpnDomainType>
            {
                new MulticastVpnDomainType {Name = "Sender-Only", MvpnDomainType = MvpnDomainType.SenderOnly },
                new MulticastVpnDomainType {Name = "Receiver-Only", MvpnDomainType = MvpnDomainType.ReceiverOnly },
                new MulticastVpnDomainType {Name = "Sender-and-Receiver", MvpnDomainType = MvpnDomainType.SenderAndReceiver }
            };

            foreach (MulticastVpnDomainType m in multicastVpnDomainTypes)
            {
                context.MulticastVpnDomainTypes.Add(m);
            }

            // Multicast VPN Direction Type options. Do NOT change the name property of these
            // items because client-side code references the names.

            var multicastVpnDirectionTypes = new List<MulticastVpnDirectionType>
            {
                new MulticastVpnDirectionType {Name = "Unidirectional", MvpnDirectionType = MvpnDirectionType.Unidirectional },
                new MulticastVpnDirectionType {Name = "Bidirectional", MvpnDirectionType = MvpnDirectionType.Bidirectional }
            };

            foreach (MulticastVpnDirectionType m in multicastVpnDirectionTypes)
            {
                context.MulticastVpnDirectionTypes.Add(m);
            }

            // Multicast Geographical Scope Options.
            // Add additional Multicast Geographical Scopes to the list below as needed

            var multicastGeographicalScopes = new List<MulticastGeographicalScope>
            {
                new MulticastGeographicalScope {Name = "Local" },
                new MulticastGeographicalScope {Name = "Regional-Level-1" },
                new MulticastGeographicalScope {Name = "Regional-Level-1-and-2" }
            };

            foreach (MulticastGeographicalScope m in multicastGeographicalScopes)
            {
                context.MulticastGeographicalScopes.Add(m);
            }

            context.SaveChanges();

            // Provider Network Plane options

            var planes = new List<Plane>
            {
                new Plane {Name = "Red" },
                new Plane {Name = "Blue" }
            };

            foreach (Plane p in planes)
            {
                context.Planes.Add(p);
            }

            // Regions and Locale Community Values below are example - change values as needed

            var regions = new List<Region>
            {
                new Region {Name = "EMEA", AutonomousSystemNumber = 8718, Number = 1 },
                new Region {Name = "AMERS", AutonomousSystemNumber = 8718, Number = 2 },
                new Region {Name = "ASIAPAC", AutonomousSystemNumber = 8718, Number = 3 },
            };

            foreach (Region r in regions)
            {
                context.Regions.Add(r);
            }

            context.SaveChanges();

            // Add Sub-Regions to the list below as needed
            // Local community AS and number values are examples only - modify values as needed

            var subregions = new List<SubRegion>
            {
                new SubRegion {Name = "UK", RegionID = regions.Single(s => s.Name == "EMEA").RegionID,
                    AutonomousSystemNumber = 8718,
                    Number = 10 },
                new SubRegion {Name = "Frankfurt", RegionID = regions.Single(s => s.Name == "EMEA").RegionID,
                    AutonomousSystemNumber = 8718,
                    Number = 11 },
                new SubRegion {Name = "East Coast", RegionID = regions.Single(s => s.Name == "AMERS").RegionID,
                    AutonomousSystemNumber = 8718,
                    Number = 12 },
                new SubRegion {Name = "Mid West", RegionID = regions.Single(s => s.Name == "AMERS").RegionID,
                    AutonomousSystemNumber = 8718,
                    Number = 13 },
                new SubRegion {Name = "Hong Kong", RegionID = regions.Single(s => s.Name == "ASIAPAC").RegionID,
                    AutonomousSystemNumber = 8718,
                    Number = 14 },
                new SubRegion {Name = "Singapore", RegionID = regions.Single(s => s.Name == "ASIAPAC").RegionID,
                    AutonomousSystemNumber = 8718,
                    Number = 15 },
            };

            foreach (SubRegion s in subregions)
            {
                context.SubRegions.Add(s);
            }

            context.SaveChanges();

            // Add locations to the list below and modify the example Locale Community AS number values as needed

            var locations = new List<Location>
            {
                new Location {SiteName = "UK2",
                    SubRegionID = subregions.Single(s => s.Name == "UK").SubRegionID,
                    Tier = 1,
                    AutonomousSystemNumber = 8718,
                    Number = 100 },
                new Location {SiteName = "THW",
                    SubRegionID = subregions.Single(s => s.Name == "UK").SubRegionID,
                    Tier = 1,
                    AutonomousSystemNumber = 8718,
                    Number = 101 },
                new Location {SiteName = "FR4",
                    SubRegionID = subregions.Single(s => s.Name == "Frankfurt").SubRegionID,
                    Tier = 1,
                    AutonomousSystemNumber = 8718,
                    Number = 102},
                new Location {SiteName = "FR5",
                    SubRegionID = subregions.Single(s => s.Name == "Frankfurt").SubRegionID,
                    Tier = 1,
                    AutonomousSystemNumber = 8718,
                    Number = 103},
                new Location {SiteName = "NJ2",
                    SubRegionID = subregions.Single(s => s.Name == "East Coast").SubRegionID,
                    Tier = 1,
                    AutonomousSystemNumber = 8718,
                    Number = 104},
                new Location {SiteName = "NJH",
                    SubRegionID = subregions.Single(s => s.Name == "East Coast").SubRegionID,
                    Tier = 1,
                    AutonomousSystemNumber = 8718,
                    Number = 105}
            };

            foreach (Location l in locations)
            {
                context.Locations.Add(l);
            }

            // Port Bandwidth options

            var portBandwidths = new List<PortBandwidth>
            {
                new PortBandwidth {BandwidthGbps = 1 },
                new PortBandwidth {BandwidthGbps = 10 },
                new PortBandwidth {BandwidthGbps = 40 },
                new PortBandwidth {BandwidthGbps = 100}
            };

            foreach (PortBandwidth p in portBandwidths)
            {
                context.PortBandwidths.Add(p);
            }

            // Define the supported Attachment Bandwidths here. We also define how each Attachment Bandwidth may be provided
            // e.g. via a Bundle Attachment or Multiport.

            var attachmentBandwidths = new List<AttachmentBandwidth>
            {
                new AttachmentBandwidth {BandwidthGbps = 1 },
                new AttachmentBandwidth {BandwidthGbps = 10 },
                new AttachmentBandwidth
                {
                    BandwidthGbps = 20,
                    BundleOrMultiPortMemberBandwidthGbps = 10,
                    SupportedByBundle = true,
                    SupportedByMultiPort = true,
                    MustBeBundleOrMultiPort = true
                },
                new AttachmentBandwidth {
                    BandwidthGbps = 40,
                    BundleOrMultiPortMemberBandwidthGbps = 10,
                    SupportedByBundle = true,
                    SupportedByMultiPort = true,
                    MustBeBundleOrMultiPort = false
                },
                new AttachmentBandwidth {
                    BandwidthGbps = 80,
                    BundleOrMultiPortMemberBandwidthGbps = 10,
                    SupportedByBundle = true,
                    SupportedByMultiPort = false,
                    MustBeBundleOrMultiPort = true
                },
                new AttachmentBandwidth {BandwidthGbps = 100 }
            };

            foreach (var p in attachmentBandwidths)
            {
                context.AttachmentBandwidths.Add(p);
            }

            // Define the supported Contract Bandwidth values here. Add additional values or modify as needed

            var contractBandwidths = new List<ContractBandwidth>
            {
                new ContractBandwidth {BandwidthMbps = 10 },
                new ContractBandwidth {BandwidthMbps = 20 },
                new ContractBandwidth {BandwidthMbps = 30 },
                new ContractBandwidth {BandwidthMbps = 40 },
                new ContractBandwidth {BandwidthMbps = 50 },
                new ContractBandwidth {BandwidthMbps = 100 },
                new ContractBandwidth {BandwidthMbps = 500 },
                new ContractBandwidth {BandwidthMbps = 1000 },
                new ContractBandwidth {BandwidthMbps = 2000 },
                new ContractBandwidth {BandwidthMbps = 4000 },
                new ContractBandwidth {BandwidthMbps = 6000 },
                new ContractBandwidth {BandwidthMbps = 10000 },
                new ContractBandwidth {BandwidthMbps = 20000 },
                new ContractBandwidth {BandwidthMbps = 40000 },
                new ContractBandwidth {BandwidthMbps = 80000 },
                new ContractBandwidth {BandwidthMbps = 100000 },
            };

            foreach (ContractBandwidth p in contractBandwidths)
            {
                context.ContractBandwidths.Add(p);
            }

            // Define allowable Attachment MTU values here

            var mtus = new List<Mtu>
            {
                new Mtu {MtuValue = 1500 },
                new Mtu {MtuValue = 9000 },
                new Mtu {MtuValue = 1514, ValueIncludesLayer2Overhead = true},
                new Mtu {MtuValue = 9014, ValueIncludesLayer2Overhead = true}
            };

            foreach (var m in mtus)
            {
                context.Mtus.Add(m);
            }

            // Attachment Redundancy options.

            var attachmentRedundancies = new List<AttachmentRedundancy>
            {
                new AttachmentRedundancy {Name = "Bronze", AttachmentRedundancyType = AttachmentRedundancyType.Bronze },
                new AttachmentRedundancy {Name = "Silver", AttachmentRedundancyType = AttachmentRedundancyType.Silver },
                new AttachmentRedundancy {Name = "Gold", AttachmentRedundancyType = AttachmentRedundancyType.Gold },
                new AttachmentRedundancy {Name = "Custom", AttachmentRedundancyType = AttachmentRedundancyType.Custom },
            };
            foreach (AttachmentRedundancy p in attachmentRedundancies)
            {
                context.AttachmentRedundancies.Add(p);
            }

            // Define the default VLAN tag range here. Vlan IDs will be assigned from this range

            var defaultVlanTagRange = new VlanTagRange()
            {
                Name = "Default",
                VlanTagRangeStart = 2,
                VlanTagRangeCount = 4000
            };
            context.VlanTagRanges.Add(defaultVlanTagRange);

            // Define the default Route Distinguisher range here. Route Distinguishers will be assigned from this range.
            // Do NOT change the Default name property because business logic code depends upon the name.

            var defaultRdRange = new RouteDistinguisherRange()
            {
                Name = "Default",
                AdministratorSubField = 8718,
                AssignedNumberSubFieldStart = 1,
                AssignedNumberSubFieldCount = 1000000
            };
            context.RouteDistinguisherRanges.Add(defaultRdRange);

            // Define additional Route Distinguisher ranges and add them to the context here if needed
            // Ranges MUST NOT overlap and the name must be unique

            // Define the default Route Target range here. Route Targets will be assigned from this range.
            // Do NOT change the Default name property because business logic code depends upon the name.

            var defaultRtRange = new RouteTargetRange()
            {
                Name = "Default",
                AdministratorSubField = 8718,
                AssignedNumberSubFieldStart = 1,
                AssignedNumberSubFieldCount = 1000000
            };
            context.RouteTargetRanges.Add(defaultRtRange);

            // Define additional Route Target ranges and add them to the context here if needed
            // Ranges MUST NOT overlap

            // Here we add a separate range called 'Sigma'

            var sigmaRtRange = new RouteTargetRange()
            {
                Name = "Sigma",
                AdministratorSubField = 8718,
                AssignedNumberSubFieldStart = 1000001,
                AssignedNumberSubFieldCount = 1000000
            };
            context.RouteTargetRanges.Add(sigmaRtRange);

            context.SaveChanges();
        }
    }
}