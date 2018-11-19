using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Mind.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AttachmentBandwidth",
                columns: table => new
                {
                    AttachmentBandwidthID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BandwidthGbps = table.Column<int>(nullable: false),
                    BundleOrMultiPortMemberBandwidthGbps = table.Column<int>(nullable: true),
                    MustBeBundleOrMultiPort = table.Column<bool>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SupportedByBundle = table.Column<bool>(nullable: false),
                    SupportedByMultiPort = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttachmentBandwidth", x => x.AttachmentBandwidthID);
                });

            migrationBuilder.CreateTable(
                name: "AttachmentRedundancy",
                columns: table => new
                {
                    AttachmentRedundancyID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AttachmentRedundancyType = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttachmentRedundancy", x => x.AttachmentRedundancyID);
                });

            migrationBuilder.CreateTable(
                name: "ContractBandwidth",
                columns: table => new
                {
                    ContractBandwidthID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BandwidthMbps = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractBandwidth", x => x.ContractBandwidthID);
                });

            migrationBuilder.CreateTable(
                name: "DeviceModel",
                columns: table => new
                {
                    DeviceModelID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceModel", x => x.DeviceModelID);
                });

            migrationBuilder.CreateTable(
                name: "DeviceRole",
                columns: table => new
                {
                    DeviceRoleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    IsProviderDomainRole = table.Column<bool>(nullable: false),
                    IsTenantDomainRole = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    RequireSyncToNetwork = table.Column<bool>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceRole", x => x.DeviceRoleID);
                });

            migrationBuilder.CreateTable(
                name: "DeviceStatus",
                columns: table => new
                {
                    DeviceStatusID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    DeviceStatusType = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceStatus", x => x.DeviceStatusID);
                });

            migrationBuilder.CreateTable(
                name: "Mtu",
                columns: table => new
                {
                    MtuID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsJumbo = table.Column<bool>(nullable: false),
                    MtuValue = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    ValueIncludesLayer2Overhead = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mtu", x => x.MtuID);
                });

            migrationBuilder.CreateTable(
                name: "MulticastGeographicalScope",
                columns: table => new
                {
                    MulticastGeographicalScopeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MulticastGeographicalScope", x => x.MulticastGeographicalScopeID);
                });

            migrationBuilder.CreateTable(
                name: "MulticastVpnDirectionType",
                columns: table => new
                {
                    MulticastVpnDirectionTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MvpnDirectionType = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MulticastVpnDirectionType", x => x.MulticastVpnDirectionTypeID);
                });

            migrationBuilder.CreateTable(
                name: "MulticastVpnDomainType",
                columns: table => new
                {
                    MulticastVpnDomainTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MvpnDomainType = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MulticastVpnDomainType", x => x.MulticastVpnDomainTypeID);
                });

            migrationBuilder.CreateTable(
                name: "MulticastVpnServiceType",
                columns: table => new
                {
                    MulticastVpnServiceTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MvpnServiceType = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MulticastVpnServiceType", x => x.MulticastVpnServiceTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Plane",
                columns: table => new
                {
                    PlaneID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plane", x => x.PlaneID);
                });

            migrationBuilder.CreateTable(
                name: "PortBandwidth",
                columns: table => new
                {
                    PortBandwidthID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BandwidthGbps = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortBandwidth", x => x.PortBandwidthID);
                });

            migrationBuilder.CreateTable(
                name: "PortConnector",
                columns: table => new
                {
                    PortConnectorID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortConnector", x => x.PortConnectorID);
                });

            migrationBuilder.CreateTable(
                name: "PortRole",
                columns: table => new
                {
                    PortRoleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    PortRoleType = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortRole", x => x.PortRoleID);
                });

            migrationBuilder.CreateTable(
                name: "PortSfp",
                columns: table => new
                {
                    PortSfpID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortSfp", x => x.PortSfpID);
                });

            migrationBuilder.CreateTable(
                name: "PortStatus",
                columns: table => new
                {
                    PortStatusID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    PortStatusType = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortStatus", x => x.PortStatusID);
                });

            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    RegionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AutonomousSystemNumber = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Number = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.RegionID);
                });

            migrationBuilder.CreateTable(
                name: "RouteDistinguisherRange",
                columns: table => new
                {
                    RouteDistinguisherRangeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdministratorSubField = table.Column<int>(nullable: false),
                    AssignedNumberSubFieldCount = table.Column<int>(nullable: false),
                    AssignedNumberSubFieldStart = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteDistinguisherRange", x => x.RouteDistinguisherRangeID);
                });

            migrationBuilder.CreateTable(
                name: "RouteTargetRange",
                columns: table => new
                {
                    RouteTargetRangeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdministratorSubField = table.Column<int>(nullable: false),
                    AssignedNumberSubFieldCount = table.Column<int>(nullable: false),
                    AssignedNumberSubFieldStart = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Range = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteTargetRange", x => x.RouteTargetRangeID);
                });

            migrationBuilder.CreateTable(
                name: "RoutingInstanceType",
                columns: table => new
                {
                    RoutingInstanceTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    IsDefault = table.Column<bool>(nullable: false),
                    IsInfrastructureVrf = table.Column<bool>(nullable: false),
                    IsLayer3 = table.Column<bool>(nullable: false),
                    IsTenantFacingVrf = table.Column<bool>(nullable: false),
                    IsVrf = table.Column<bool>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoutingInstanceType", x => x.RoutingInstanceTypeID);
                });

            migrationBuilder.CreateTable(
                name: "RoutingPolicyMatchOption",
                columns: table => new
                {
                    RoutingPolicyMatchOptionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoutingPolicyMatchOption", x => x.RoutingPolicyMatchOptionID);
                });

            migrationBuilder.CreateTable(
                name: "Tenant",
                columns: table => new
                {
                    TenantID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenant", x => x.TenantID);
                });

            migrationBuilder.CreateTable(
                name: "VlanTagRange",
                columns: table => new
                {
                    VlanTagRangeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    VlanTagRangeCount = table.Column<int>(nullable: false),
                    VlanTagRangeStart = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VlanTagRange", x => x.VlanTagRangeID);
                });

            migrationBuilder.CreateTable(
                name: "VpnProtocolType",
                columns: table => new
                {
                    VpnProtocolTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    ProtocolType = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VpnProtocolType", x => x.VpnProtocolTypeID);
                });

            migrationBuilder.CreateTable(
                name: "VpnTenancyType",
                columns: table => new
                {
                    VpnTenancyTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TenancyType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VpnTenancyType", x => x.VpnTenancyTypeID);
                });

            migrationBuilder.CreateTable(
                name: "DeviceRolePortRole",
                columns: table => new
                {
                    DeviceRolePortRoleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DeviceRoleID = table.Column<int>(nullable: false),
                    PortRoleID = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceRolePortRole", x => x.DeviceRolePortRoleID);
                    table.ForeignKey(
                        name: "FK_DeviceRolePortRole_DeviceRole_DeviceRoleID",
                        column: x => x.DeviceRoleID,
                        principalTable: "DeviceRole",
                        principalColumn: "DeviceRoleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceRolePortRole_PortRole_PortRoleID",
                        column: x => x.PortRoleID,
                        principalTable: "PortRole",
                        principalColumn: "PortRoleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PortPool",
                columns: table => new
                {
                    PortPoolID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    PortRoleID = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortPool", x => x.PortPoolID);
                    table.ForeignKey(
                        name: "FK_PortPool_PortRole_PortRoleID",
                        column: x => x.PortRoleID,
                        principalTable: "PortRole",
                        principalColumn: "PortRoleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubRegion",
                columns: table => new
                {
                    SubRegionID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AutonomousSystemNumber = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Number = table.Column<int>(nullable: false),
                    RegionID = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubRegion", x => x.SubRegionID);
                    table.ForeignKey(
                        name: "FK_SubRegion_Region_RegionID",
                        column: x => x.RegionID,
                        principalTable: "Region",
                        principalColumn: "RegionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractBandwidthPool",
                columns: table => new
                {
                    ContractBandwidthPoolID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContractBandwidthID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TenantID = table.Column<int>(nullable: true),
                    TrustReceivedCosAndDscp = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractBandwidthPool", x => x.ContractBandwidthPoolID);
                    table.ForeignKey(
                        name: "FK_ContractBandwidthPool_ContractBandwidth_ContractBandwidthID",
                        column: x => x.ContractBandwidthID,
                        principalTable: "ContractBandwidth",
                        principalColumn: "ContractBandwidthID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContractBandwidthPool_Tenant_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Tenant",
                        principalColumn: "TenantID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TenantCommunity",
                columns: table => new
                {
                    TenantCommunityID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AllowExtranet = table.Column<bool>(nullable: false),
                    AutonomousSystemNumber = table.Column<int>(nullable: false),
                    IpRoutingBehaviour = table.Column<int>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TenantID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantCommunity", x => x.TenantCommunityID);
                    table.ForeignKey(
                        name: "FK_TenantCommunity_Tenant_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Tenant",
                        principalColumn: "TenantID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TenantIpNetwork",
                columns: table => new
                {
                    TenantIpNetworkID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AllowExtranet = table.Column<bool>(nullable: false),
                    IpRoutingBehaviour = table.Column<int>(nullable: false),
                    Ipv4Length = table.Column<int>(nullable: false),
                    Ipv4LessThanOrEqualToLength = table.Column<int>(nullable: true),
                    Ipv4Prefix = table.Column<string>(maxLength: 15, nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TenantID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantIpNetwork", x => x.TenantIpNetworkID);
                    table.ForeignKey(
                        name: "FK_TenantIpNetwork_Tenant_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Tenant",
                        principalColumn: "TenantID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TenantMulticastGroup",
                columns: table => new
                {
                    TenantMulticastGroupID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AllowExtranet = table.Column<bool>(nullable: false),
                    GroupAddress = table.Column<string>(maxLength: 15, nullable: false),
                    GroupMask = table.Column<string>(maxLength: 15, nullable: true),
                    IsSsmGroup = table.Column<bool>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SourceAddress = table.Column<string>(maxLength: 15, nullable: true),
                    SourceMask = table.Column<string>(maxLength: 15, nullable: true),
                    TenantID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantMulticastGroup", x => x.TenantMulticastGroupID);
                    table.ForeignKey(
                        name: "FK_TenantMulticastGroup_Tenant_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Tenant",
                        principalColumn: "TenantID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AddressFamily",
                columns: table => new
                {
                    AddressFamilyID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    VpnProtocolTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressFamily", x => x.AddressFamilyID);
                    table.ForeignKey(
                        name: "FK_AddressFamily_VpnProtocolType_VpnProtocolTypeID",
                        column: x => x.VpnProtocolTypeID,
                        principalTable: "VpnProtocolType",
                        principalColumn: "VpnProtocolTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VpnTopologyType",
                columns: table => new
                {
                    VpnTopologyTypeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TopologyType = table.Column<int>(nullable: false),
                    VpnProtocolTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VpnTopologyType", x => x.VpnTopologyTypeID);
                    table.ForeignKey(
                        name: "FK_VpnTopologyType_VpnProtocolType_VpnProtocolTypeID",
                        column: x => x.VpnProtocolTypeID,
                        principalTable: "VpnProtocolType",
                        principalColumn: "VpnProtocolTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttachmentRole",
                columns: table => new
                {
                    AttachmentRoleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    IsLayer3Role = table.Column<bool>(nullable: false),
                    IsTaggedRole = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    PortPoolID = table.Column<int>(nullable: false),
                    RequireContractBandwidth = table.Column<bool>(nullable: false),
                    RequireSyncToNetwork = table.Column<bool>(nullable: false),
                    RoutingInstanceTypeID = table.Column<int>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false),
                    SupportedByBundle = table.Column<bool>(nullable: false),
                    SupportedByMultiPort = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttachmentRole", x => x.AttachmentRoleID);
                    table.ForeignKey(
                        name: "FK_AttachmentRole_PortPool_PortPoolID",
                        column: x => x.PortPoolID,
                        principalTable: "PortPool",
                        principalColumn: "PortPoolID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttachmentRole_RoutingInstanceType_RoutingInstanceTypeID",
                        column: x => x.RoutingInstanceTypeID,
                        principalTable: "RoutingInstanceType",
                        principalColumn: "RoutingInstanceTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AttachmentSet",
                columns: table => new
                {
                    AttachmentSetID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AttachmentRedundancyID = table.Column<int>(nullable: false),
                    IsLayer3 = table.Column<bool>(nullable: false),
                    MulticastVpnDomainTypeID = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    RegionID = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SubRegionID = table.Column<int>(nullable: true),
                    TenantID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttachmentSet", x => x.AttachmentSetID);
                    table.ForeignKey(
                        name: "FK_AttachmentSet_AttachmentRedundancy_AttachmentRedundancyID",
                        column: x => x.AttachmentRedundancyID,
                        principalTable: "AttachmentRedundancy",
                        principalColumn: "AttachmentRedundancyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AttachmentSet_MulticastVpnDomainType_MulticastVpnDomainTypeID",
                        column: x => x.MulticastVpnDomainTypeID,
                        principalTable: "MulticastVpnDomainType",
                        principalColumn: "MulticastVpnDomainTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AttachmentSet_Region_RegionID",
                        column: x => x.RegionID,
                        principalTable: "Region",
                        principalColumn: "RegionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AttachmentSet_SubRegion_SubRegionID",
                        column: x => x.SubRegionID,
                        principalTable: "SubRegion",
                        principalColumn: "SubRegionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AttachmentSet_Tenant_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Tenant",
                        principalColumn: "TenantID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    LocationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AutonomousSystemNumber = table.Column<int>(nullable: true),
                    LocationType = table.Column<int>(nullable: false),
                    Number = table.Column<int>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SiteName = table.Column<string>(maxLength: 50, nullable: false),
                    SubRegionID = table.Column<int>(nullable: false),
                    Tier = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.LocationID);
                    table.ForeignKey(
                        name: "FK_Location_SubRegion_SubRegionID",
                        column: x => x.SubRegionID,
                        principalTable: "SubRegion",
                        principalColumn: "SubRegionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TenantCommunitySet",
                columns: table => new
                {
                    TenantCommunitySetID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    RoutingPolicyMatchOptionID = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TenantCommunityID = table.Column<int>(nullable: true),
                    TenantID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantCommunitySet", x => x.TenantCommunitySetID);
                    table.ForeignKey(
                        name: "FK_TenantCommunitySet_RoutingPolicyMatchOption_RoutingPolicyMatchOptionID",
                        column: x => x.RoutingPolicyMatchOptionID,
                        principalTable: "RoutingPolicyMatchOption",
                        principalColumn: "RoutingPolicyMatchOptionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TenantCommunitySet_TenantCommunity_TenantCommunityID",
                        column: x => x.TenantCommunityID,
                        principalTable: "TenantCommunity",
                        principalColumn: "TenantCommunityID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TenantCommunitySet_Tenant_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Tenant",
                        principalColumn: "TenantID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vpn",
                columns: table => new
                {
                    VpnID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressFamilyID = table.Column<int>(nullable: true),
                    Created = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    IsExtranet = table.Column<bool>(nullable: false),
                    IsMulticastVpn = table.Column<bool>(nullable: false),
                    IsNovaVpn = table.Column<bool>(nullable: false),
                    MulticastVpnDirectionTypeID = table.Column<int>(nullable: true),
                    MulticastVpnServiceTypeID = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    PlaneID = table.Column<int>(nullable: true),
                    RegionID = table.Column<int>(nullable: true),
                    RequiresSync = table.Column<bool>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    ShowCreatedAlert = table.Column<bool>(nullable: false),
                    ShowRequiresSyncAlert = table.Column<bool>(nullable: false),
                    TenantID = table.Column<int>(nullable: false),
                    VpnTenancyTypeID = table.Column<int>(nullable: false),
                    VpnTopologyTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vpn", x => x.VpnID);
                    table.ForeignKey(
                        name: "FK_Vpn_AddressFamily_AddressFamilyID",
                        column: x => x.AddressFamilyID,
                        principalTable: "AddressFamily",
                        principalColumn: "AddressFamilyID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vpn_MulticastVpnDirectionType_MulticastVpnDirectionTypeID",
                        column: x => x.MulticastVpnDirectionTypeID,
                        principalTable: "MulticastVpnDirectionType",
                        principalColumn: "MulticastVpnDirectionTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vpn_MulticastVpnServiceType_MulticastVpnServiceTypeID",
                        column: x => x.MulticastVpnServiceTypeID,
                        principalTable: "MulticastVpnServiceType",
                        principalColumn: "MulticastVpnServiceTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vpn_Plane_PlaneID",
                        column: x => x.PlaneID,
                        principalTable: "Plane",
                        principalColumn: "PlaneID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vpn_Region_RegionID",
                        column: x => x.RegionID,
                        principalTable: "Region",
                        principalColumn: "RegionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vpn_Tenant_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Tenant",
                        principalColumn: "TenantID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vpn_VpnTenancyType_VpnTenancyTypeID",
                        column: x => x.VpnTenancyTypeID,
                        principalTable: "VpnTenancyType",
                        principalColumn: "VpnTenancyTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vpn_VpnTopologyType_VpnTopologyTypeID",
                        column: x => x.VpnTopologyTypeID,
                        principalTable: "VpnTopologyType",
                        principalColumn: "VpnTopologyTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceRoleAttachmentRole",
                columns: table => new
                {
                    DeviceRoleAttachmentRoleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AttachmentRoleID = table.Column<int>(nullable: false),
                    DeviceRoleID = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceRoleAttachmentRole", x => x.DeviceRoleAttachmentRoleID);
                    table.ForeignKey(
                        name: "FK_DeviceRoleAttachmentRole_AttachmentRole_AttachmentRoleID",
                        column: x => x.AttachmentRoleID,
                        principalTable: "AttachmentRole",
                        principalColumn: "AttachmentRoleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceRoleAttachmentRole_DeviceRole_DeviceRoleID",
                        column: x => x.DeviceRoleID,
                        principalTable: "DeviceRole",
                        principalColumn: "DeviceRoleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VifRole",
                columns: table => new
                {
                    VifRoleID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AttachmentRoleID = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    IsLayer3Role = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    RequireContractBandwidth = table.Column<bool>(nullable: false),
                    RequireSyncToNetwork = table.Column<bool>(nullable: false),
                    RoutingInstanceTypeID = table.Column<int>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VifRole", x => x.VifRoleID);
                    table.ForeignKey(
                        name: "FK_VifRole_AttachmentRole_AttachmentRoleID",
                        column: x => x.AttachmentRoleID,
                        principalTable: "AttachmentRole",
                        principalColumn: "AttachmentRoleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VifRole_RoutingInstanceType_RoutingInstanceTypeID",
                        column: x => x.RoutingInstanceTypeID,
                        principalTable: "RoutingInstanceType",
                        principalColumn: "RoutingInstanceTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MulticastVpnRp",
                columns: table => new
                {
                    MulticastVpnRpID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AttachmentSetID = table.Column<int>(nullable: false),
                    IpAddress = table.Column<string>(maxLength: 15, nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MulticastVpnRp", x => x.MulticastVpnRpID);
                    table.ForeignKey(
                        name: "FK_MulticastVpnRp_AttachmentSet_AttachmentSetID",
                        column: x => x.AttachmentSetID,
                        principalTable: "AttachmentSet",
                        principalColumn: "AttachmentSetID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Device",
                columns: table => new
                {
                    DeviceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Created = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    DeviceModelID = table.Column<int>(nullable: false),
                    DeviceRoleID = table.Column<int>(nullable: false),
                    DeviceStatusID = table.Column<int>(nullable: false),
                    LocationID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Notes = table.Column<string>(maxLength: 250, nullable: true),
                    PlaneID = table.Column<int>(nullable: true),
                    RequiresSync = table.Column<bool>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    ShowCreatedAlert = table.Column<bool>(nullable: false),
                    ShowRequiresSyncAlert = table.Column<bool>(nullable: false),
                    TenantID = table.Column<int>(nullable: true),
                    UseLayer2InterfaceMtu = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.DeviceID);
                    table.ForeignKey(
                        name: "FK_Device_DeviceModel_DeviceModelID",
                        column: x => x.DeviceModelID,
                        principalTable: "DeviceModel",
                        principalColumn: "DeviceModelID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Device_DeviceRole_DeviceRoleID",
                        column: x => x.DeviceRoleID,
                        principalTable: "DeviceRole",
                        principalColumn: "DeviceRoleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Device_DeviceStatus_DeviceStatusID",
                        column: x => x.DeviceStatusID,
                        principalTable: "DeviceStatus",
                        principalColumn: "DeviceStatusID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Device_Location_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Location",
                        principalColumn: "LocationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Device_Plane_PlaneID",
                        column: x => x.PlaneID,
                        principalTable: "Plane",
                        principalColumn: "PlaneID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Device_Tenant_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Tenant",
                        principalColumn: "TenantID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TenantCommunitySetCommunity",
                columns: table => new
                {
                    TenantCommunitySetCommunityID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TenantCommunityID = table.Column<int>(nullable: false),
                    TenantCommunitySetID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantCommunitySetCommunity", x => x.TenantCommunitySetCommunityID);
                    table.ForeignKey(
                        name: "FK_TenantCommunitySetCommunity_TenantCommunity_TenantCommunityID",
                        column: x => x.TenantCommunityID,
                        principalTable: "TenantCommunity",
                        principalColumn: "TenantCommunityID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TenantCommunitySetCommunity_TenantCommunitySet_TenantCommunitySetID",
                        column: x => x.TenantCommunitySetID,
                        principalTable: "TenantCommunitySet",
                        principalColumn: "TenantCommunitySetID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExtranetVpnMember",
                columns: table => new
                {
                    ExtranetVpnMemberID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExtranetVpnID = table.Column<int>(nullable: true),
                    MemberVpnID = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtranetVpnMember", x => x.ExtranetVpnMemberID);
                    table.ForeignKey(
                        name: "FK_ExtranetVpnMember_Vpn_ExtranetVpnID",
                        column: x => x.ExtranetVpnID,
                        principalTable: "Vpn",
                        principalColumn: "VpnID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExtranetVpnMember_Vpn_MemberVpnID",
                        column: x => x.MemberVpnID,
                        principalTable: "Vpn",
                        principalColumn: "VpnID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RouteTarget",
                columns: table => new
                {
                    RouteTargetID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AssignedNumberSubField = table.Column<int>(nullable: false),
                    IsHubExport = table.Column<bool>(nullable: false),
                    RouteTargetRangeID = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    VpnID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteTarget", x => x.RouteTargetID);
                    table.ForeignKey(
                        name: "FK_RouteTarget_RouteTargetRange_RouteTargetRangeID",
                        column: x => x.RouteTargetRangeID,
                        principalTable: "RouteTargetRange",
                        principalColumn: "RouteTargetRangeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RouteTarget_Vpn_VpnID",
                        column: x => x.VpnID,
                        principalTable: "Vpn",
                        principalColumn: "VpnID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VpnAttachmentSet",
                columns: table => new
                {
                    VpnAttachmentSetID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AttachmentSetID = table.Column<int>(nullable: false),
                    IsHub = table.Column<bool>(nullable: true),
                    IsMulticastDirectlyIntegrated = table.Column<bool>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    VpnID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VpnAttachmentSet", x => x.VpnAttachmentSetID);
                    table.ForeignKey(
                        name: "FK_VpnAttachmentSet_AttachmentSet_AttachmentSetID",
                        column: x => x.AttachmentSetID,
                        principalTable: "AttachmentSet",
                        principalColumn: "AttachmentSetID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VpnAttachmentSet_Vpn_VpnID",
                        column: x => x.VpnID,
                        principalTable: "Vpn",
                        principalColumn: "VpnID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VpnTenantMulticastGroup",
                columns: table => new
                {
                    VpnTenantMulticastGroupID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AttachmentSetID = table.Column<int>(nullable: false),
                    MulticastGeographicalScopeID = table.Column<int>(nullable: true),
                    MulticastVpnRpID = table.Column<int>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TenantMulticastGroupID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VpnTenantMulticastGroup", x => x.VpnTenantMulticastGroupID);
                    table.ForeignKey(
                        name: "FK_VpnTenantMulticastGroup_AttachmentSet_AttachmentSetID",
                        column: x => x.AttachmentSetID,
                        principalTable: "AttachmentSet",
                        principalColumn: "AttachmentSetID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VpnTenantMulticastGroup_MulticastGeographicalScope_MulticastGeographicalScopeID",
                        column: x => x.MulticastGeographicalScopeID,
                        principalTable: "MulticastGeographicalScope",
                        principalColumn: "MulticastGeographicalScopeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VpnTenantMulticastGroup_MulticastVpnRp_MulticastVpnRpID",
                        column: x => x.MulticastVpnRpID,
                        principalTable: "MulticastVpnRp",
                        principalColumn: "MulticastVpnRpID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VpnTenantMulticastGroup_TenantMulticastGroup_TenantMulticastGroupID",
                        column: x => x.TenantMulticastGroupID,
                        principalTable: "TenantMulticastGroup",
                        principalColumn: "TenantMulticastGroupID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoutingInstance",
                columns: table => new
                {
                    RoutingInstanceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdministratorSubField = table.Column<int>(nullable: true),
                    AssignedNumberSubField = table.Column<int>(nullable: true),
                    DeviceID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    RouteDistinguisherRangeID = table.Column<int>(nullable: true),
                    RoutingInstanceTypeID = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TenantID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoutingInstance", x => x.RoutingInstanceID);
                    table.ForeignKey(
                        name: "FK_RoutingInstance_Device_DeviceID",
                        column: x => x.DeviceID,
                        principalTable: "Device",
                        principalColumn: "DeviceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoutingInstance_RouteDistinguisherRange_RouteDistinguisherRangeID",
                        column: x => x.RouteDistinguisherRangeID,
                        principalTable: "RouteDistinguisherRange",
                        principalColumn: "RouteDistinguisherRangeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoutingInstance_RoutingInstanceType_RoutingInstanceTypeID",
                        column: x => x.RoutingInstanceTypeID,
                        principalTable: "RoutingInstanceType",
                        principalColumn: "RoutingInstanceTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoutingInstance_Tenant_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Tenant",
                        principalColumn: "TenantID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attachment",
                columns: table => new
                {
                    AttachmentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AttachmentBandwidthID = table.Column<int>(nullable: false),
                    AttachmentRoleID = table.Column<int>(nullable: false),
                    BundleMaxLinks = table.Column<int>(nullable: true),
                    BundleMinLinks = table.Column<int>(nullable: true),
                    ContractBandwidthPoolID = table.Column<int>(nullable: true),
                    Created = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    DeviceID = table.Column<int>(nullable: false),
                    ID = table.Column<int>(nullable: true),
                    IsBundle = table.Column<bool>(nullable: false),
                    IsLayer3 = table.Column<bool>(nullable: false),
                    IsMultiPort = table.Column<bool>(nullable: false),
                    IsTagged = table.Column<bool>(nullable: false),
                    MtuID = table.Column<int>(nullable: false),
                    Notes = table.Column<string>(maxLength: 250, nullable: true),
                    RequiresSync = table.Column<bool>(nullable: false),
                    RoutingInstanceID = table.Column<int>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    ShowCreatedAlert = table.Column<bool>(nullable: false),
                    ShowRequiresSyncAlert = table.Column<bool>(nullable: false),
                    TenantID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.AttachmentID);
                    table.ForeignKey(
                        name: "FK_Attachment_AttachmentBandwidth_AttachmentBandwidthID",
                        column: x => x.AttachmentBandwidthID,
                        principalTable: "AttachmentBandwidth",
                        principalColumn: "AttachmentBandwidthID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attachment_AttachmentRole_AttachmentRoleID",
                        column: x => x.AttachmentRoleID,
                        principalTable: "AttachmentRole",
                        principalColumn: "AttachmentRoleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attachment_ContractBandwidthPool_ContractBandwidthPoolID",
                        column: x => x.ContractBandwidthPoolID,
                        principalTable: "ContractBandwidthPool",
                        principalColumn: "ContractBandwidthPoolID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attachment_Device_DeviceID",
                        column: x => x.DeviceID,
                        principalTable: "Device",
                        principalColumn: "DeviceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attachment_Mtu_MtuID",
                        column: x => x.MtuID,
                        principalTable: "Mtu",
                        principalColumn: "MtuID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attachment_RoutingInstance_RoutingInstanceID",
                        column: x => x.RoutingInstanceID,
                        principalTable: "RoutingInstance",
                        principalColumn: "RoutingInstanceID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attachment_Tenant_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Tenant",
                        principalColumn: "TenantID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AttachmentSetRoutingInstance",
                columns: table => new
                {
                    AttachmentSetRoutingInstanceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdvertisedIpRoutingPreference = table.Column<int>(nullable: true),
                    AttachmentSetID = table.Column<int>(nullable: false),
                    LocalIpRoutingPreference = table.Column<int>(nullable: true),
                    MulticastDesignatedRouterPreference = table.Column<int>(nullable: true),
                    RoutingInstanceID = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttachmentSetRoutingInstance", x => x.AttachmentSetRoutingInstanceID);
                    table.ForeignKey(
                        name: "FK_AttachmentSetRoutingInstance_AttachmentSet_AttachmentSetID",
                        column: x => x.AttachmentSetID,
                        principalTable: "AttachmentSet",
                        principalColumn: "AttachmentSetID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttachmentSetRoutingInstance_RoutingInstance_RoutingInstanceID",
                        column: x => x.RoutingInstanceID,
                        principalTable: "RoutingInstance",
                        principalColumn: "RoutingInstanceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BgpPeer",
                columns: table => new
                {
                    BgpPeerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ipv4PeerAddress = table.Column<string>(maxLength: 15, nullable: true),
                    IsBfdEnabled = table.Column<bool>(nullable: false),
                    IsMultiHop = table.Column<bool>(nullable: false),
                    MaximumRoutes = table.Column<int>(nullable: true),
                    Peer2ByteAutonomousSystem = table.Column<int>(nullable: false),
                    PeerPassword = table.Column<string>(maxLength: 50, nullable: true),
                    RoutingInstanceID = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BgpPeer", x => x.BgpPeerID);
                    table.ForeignKey(
                        name: "FK_BgpPeer_RoutingInstance_RoutingInstanceID",
                        column: x => x.RoutingInstanceID,
                        principalTable: "RoutingInstance",
                        principalColumn: "RoutingInstanceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LogicalInterface",
                columns: table => new
                {
                    LogicalInterfaceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    ID = table.Column<int>(nullable: false),
                    IpAddress = table.Column<string>(maxLength: 15, nullable: true),
                    LogicalInterfaceType = table.Column<int>(nullable: false),
                    RoutingInstanceID = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SubnetMask = table.Column<string>(maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogicalInterface", x => x.LogicalInterfaceID);
                    table.ForeignKey(
                        name: "FK_LogicalInterface_RoutingInstance_RoutingInstanceID",
                        column: x => x.RoutingInstanceID,
                        principalTable: "RoutingInstance",
                        principalColumn: "RoutingInstanceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VpnTenantCommunityRoutingInstance",
                columns: table => new
                {
                    VpnTenantCommunityRoutingInstanceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AttachmentSetID = table.Column<int>(nullable: false),
                    LocalIpRoutingPreference = table.Column<int>(nullable: false),
                    RoutingInstanceID = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TenantCommunityID = table.Column<int>(nullable: true),
                    TenantCommunitySetID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VpnTenantCommunityRoutingInstance", x => x.VpnTenantCommunityRoutingInstanceID);
                    table.ForeignKey(
                        name: "FK_VpnTenantCommunityRoutingInstance_AttachmentSet_AttachmentSetID",
                        column: x => x.AttachmentSetID,
                        principalTable: "AttachmentSet",
                        principalColumn: "AttachmentSetID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VpnTenantCommunityRoutingInstance_RoutingInstance_RoutingInstanceID",
                        column: x => x.RoutingInstanceID,
                        principalTable: "RoutingInstance",
                        principalColumn: "RoutingInstanceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VpnTenantCommunityRoutingInstance_TenantCommunity_TenantCommunityID",
                        column: x => x.TenantCommunityID,
                        principalTable: "TenantCommunity",
                        principalColumn: "TenantCommunityID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VpnTenantCommunityRoutingInstance_TenantCommunitySet_TenantCommunitySetID",
                        column: x => x.TenantCommunitySetID,
                        principalTable: "TenantCommunitySet",
                        principalColumn: "TenantCommunitySetID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VpnTenantIpNetworkRoutingInstance",
                columns: table => new
                {
                    VpnTenantIpNetworkRoutingInstanceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AttachmentSetID = table.Column<int>(nullable: false),
                    LocalIpRoutingPreference = table.Column<int>(nullable: false),
                    RoutingInstanceID = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TenantIpNetworkID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VpnTenantIpNetworkRoutingInstance", x => x.VpnTenantIpNetworkRoutingInstanceID);
                    table.ForeignKey(
                        name: "FK_VpnTenantIpNetworkRoutingInstance_AttachmentSet_AttachmentSetID",
                        column: x => x.AttachmentSetID,
                        principalTable: "AttachmentSet",
                        principalColumn: "AttachmentSetID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VpnTenantIpNetworkRoutingInstance_RoutingInstance_RoutingInstanceID",
                        column: x => x.RoutingInstanceID,
                        principalTable: "RoutingInstance",
                        principalColumn: "RoutingInstanceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VpnTenantIpNetworkRoutingInstance_TenantIpNetwork_TenantIpNetworkID",
                        column: x => x.TenantIpNetworkID,
                        principalTable: "TenantIpNetwork",
                        principalColumn: "TenantIpNetworkID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VpnTenantIpNetworkRoutingInstanceStaticRoute",
                columns: table => new
                {
                    VpnTenantIpNetworkRoutingInstanceStaticRouteID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddToAllRoutingInstancesInAttachmentSet = table.Column<bool>(nullable: false),
                    AttachmentSetID = table.Column<int>(nullable: false),
                    Ipv4NextHopAddress = table.Column<string>(nullable: true),
                    IsBfdEnabled = table.Column<bool>(nullable: false),
                    RoutingInstanceID = table.Column<int>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TenantIpNetworkID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VpnTenantIpNetworkRoutingInstanceStaticRoute", x => x.VpnTenantIpNetworkRoutingInstanceStaticRouteID);
                    table.ForeignKey(
                        name: "FK_VpnTenantIpNetworkRoutingInstanceStaticRoute_AttachmentSet_AttachmentSetID",
                        column: x => x.AttachmentSetID,
                        principalTable: "AttachmentSet",
                        principalColumn: "AttachmentSetID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VpnTenantIpNetworkRoutingInstanceStaticRoute_RoutingInstance_RoutingInstanceID",
                        column: x => x.RoutingInstanceID,
                        principalTable: "RoutingInstance",
                        principalColumn: "RoutingInstanceID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VpnTenantIpNetworkRoutingInstanceStaticRoute_TenantIpNetwork_TenantIpNetworkID",
                        column: x => x.TenantIpNetworkID,
                        principalTable: "TenantIpNetwork",
                        principalColumn: "TenantIpNetworkID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Interface",
                columns: table => new
                {
                    InterfaceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AttachmentID = table.Column<int>(nullable: false),
                    DeviceID = table.Column<int>(nullable: true),
                    IpAddress = table.Column<string>(maxLength: 15, nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SubnetMask = table.Column<string>(maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interface", x => x.InterfaceID);
                    table.ForeignKey(
                        name: "FK_Interface_Attachment_AttachmentID",
                        column: x => x.AttachmentID,
                        principalTable: "Attachment",
                        principalColumn: "AttachmentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Interface_Device_DeviceID",
                        column: x => x.DeviceID,
                        principalTable: "Device",
                        principalColumn: "DeviceID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vif",
                columns: table => new
                {
                    VifID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AttachmentID = table.Column<int>(nullable: false),
                    ContractBandwidthPoolID = table.Column<int>(nullable: true),
                    Created = table.Column<bool>(nullable: false),
                    IsLayer3 = table.Column<bool>(nullable: false),
                    MtuID = table.Column<int>(nullable: false),
                    RequiresSync = table.Column<bool>(nullable: false),
                    RoutingInstanceID = table.Column<int>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    ShowCreatedAlert = table.Column<bool>(nullable: false),
                    ShowRequiresSyncAlert = table.Column<bool>(nullable: false),
                    TenantID = table.Column<int>(nullable: true),
                    VifRoleID = table.Column<int>(nullable: false),
                    VlanTag = table.Column<int>(nullable: false),
                    VlanTagRangeID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vif", x => x.VifID);
                    table.ForeignKey(
                        name: "FK_Vif_Attachment_AttachmentID",
                        column: x => x.AttachmentID,
                        principalTable: "Attachment",
                        principalColumn: "AttachmentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vif_ContractBandwidthPool_ContractBandwidthPoolID",
                        column: x => x.ContractBandwidthPoolID,
                        principalTable: "ContractBandwidthPool",
                        principalColumn: "ContractBandwidthPoolID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vif_Mtu_MtuID",
                        column: x => x.MtuID,
                        principalTable: "Mtu",
                        principalColumn: "MtuID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vif_RoutingInstance_RoutingInstanceID",
                        column: x => x.RoutingInstanceID,
                        principalTable: "RoutingInstance",
                        principalColumn: "RoutingInstanceID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vif_Tenant_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Tenant",
                        principalColumn: "TenantID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vif_VifRole_VifRoleID",
                        column: x => x.VifRoleID,
                        principalTable: "VifRole",
                        principalColumn: "VifRoleID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vif_VlanTagRange_VlanTagRangeID",
                        column: x => x.VlanTagRangeID,
                        principalTable: "VlanTagRange",
                        principalColumn: "VlanTagRangeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VpnTenantCommunityIn",
                columns: table => new
                {
                    VpnTenantCommunityInID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddToAllBgpPeersInAttachmentSet = table.Column<bool>(nullable: false),
                    AttachmentSetID = table.Column<int>(nullable: true),
                    BgpPeerID = table.Column<int>(nullable: true),
                    LocalIpRoutingPreference = table.Column<int>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TenantCommunityID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VpnTenantCommunityIn", x => x.VpnTenantCommunityInID);
                    table.ForeignKey(
                        name: "FK_VpnTenantCommunityIn_AttachmentSet_AttachmentSetID",
                        column: x => x.AttachmentSetID,
                        principalTable: "AttachmentSet",
                        principalColumn: "AttachmentSetID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VpnTenantCommunityIn_BgpPeer_BgpPeerID",
                        column: x => x.BgpPeerID,
                        principalTable: "BgpPeer",
                        principalColumn: "BgpPeerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VpnTenantCommunityIn_TenantCommunity_TenantCommunityID",
                        column: x => x.TenantCommunityID,
                        principalTable: "TenantCommunity",
                        principalColumn: "TenantCommunityID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VpnTenantCommunityOut",
                columns: table => new
                {
                    VpnTenantCommunityOutID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdvertisedIpRoutingPreference = table.Column<int>(nullable: false),
                    AttachmentSetID = table.Column<int>(nullable: true),
                    BgpPeerID = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TenantCommunityID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VpnTenantCommunityOut", x => x.VpnTenantCommunityOutID);
                    table.ForeignKey(
                        name: "FK_VpnTenantCommunityOut_AttachmentSet_AttachmentSetID",
                        column: x => x.AttachmentSetID,
                        principalTable: "AttachmentSet",
                        principalColumn: "AttachmentSetID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VpnTenantCommunityOut_BgpPeer_BgpPeerID",
                        column: x => x.BgpPeerID,
                        principalTable: "BgpPeer",
                        principalColumn: "BgpPeerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VpnTenantCommunityOut_TenantCommunity_TenantCommunityID",
                        column: x => x.TenantCommunityID,
                        principalTable: "TenantCommunity",
                        principalColumn: "TenantCommunityID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VpnTenantIpNetworkIn",
                columns: table => new
                {
                    VpnTenantIpNetworkInID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddToAllBgpPeersInAttachmentSet = table.Column<bool>(nullable: false),
                    AttachmentSetID = table.Column<int>(nullable: true),
                    BgpPeerID = table.Column<int>(nullable: true),
                    LocalIpRoutingPreference = table.Column<int>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TenantIpNetworkID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VpnTenantIpNetworkIn", x => x.VpnTenantIpNetworkInID);
                    table.ForeignKey(
                        name: "FK_VpnTenantIpNetworkIn_AttachmentSet_AttachmentSetID",
                        column: x => x.AttachmentSetID,
                        principalTable: "AttachmentSet",
                        principalColumn: "AttachmentSetID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VpnTenantIpNetworkIn_BgpPeer_BgpPeerID",
                        column: x => x.BgpPeerID,
                        principalTable: "BgpPeer",
                        principalColumn: "BgpPeerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VpnTenantIpNetworkIn_TenantIpNetwork_TenantIpNetworkID",
                        column: x => x.TenantIpNetworkID,
                        principalTable: "TenantIpNetwork",
                        principalColumn: "TenantIpNetworkID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VpnTenantIpNetworkOut",
                columns: table => new
                {
                    VpnTenantIpNetworkOutID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddToAllBgpPeersInAttachmentSet = table.Column<bool>(nullable: false),
                    AdvertisedIpRoutingPreference = table.Column<int>(nullable: false),
                    AttachmentSetID = table.Column<int>(nullable: true),
                    BgpPeerID = table.Column<int>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TenantIpNetworkID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VpnTenantIpNetworkOut", x => x.VpnTenantIpNetworkOutID);
                    table.ForeignKey(
                        name: "FK_VpnTenantIpNetworkOut_AttachmentSet_AttachmentSetID",
                        column: x => x.AttachmentSetID,
                        principalTable: "AttachmentSet",
                        principalColumn: "AttachmentSetID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VpnTenantIpNetworkOut_BgpPeer_BgpPeerID",
                        column: x => x.BgpPeerID,
                        principalTable: "BgpPeer",
                        principalColumn: "BgpPeerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VpnTenantIpNetworkOut_TenantIpNetwork_TenantIpNetworkID",
                        column: x => x.TenantIpNetworkID,
                        principalTable: "TenantIpNetwork",
                        principalColumn: "TenantIpNetworkID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Port",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DeviceID = table.Column<int>(nullable: false),
                    InterfaceID = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    PortBandwidthID = table.Column<int>(nullable: false),
                    PortConnectorID = table.Column<int>(nullable: false),
                    PortPoolID = table.Column<int>(nullable: false),
                    PortSfpID = table.Column<int>(nullable: false),
                    PortStatusID = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TenantID = table.Column<int>(nullable: true),
                    Type = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Port", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Port_Device_DeviceID",
                        column: x => x.DeviceID,
                        principalTable: "Device",
                        principalColumn: "DeviceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Port_Interface_InterfaceID",
                        column: x => x.InterfaceID,
                        principalTable: "Interface",
                        principalColumn: "InterfaceID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Port_PortBandwidth_PortBandwidthID",
                        column: x => x.PortBandwidthID,
                        principalTable: "PortBandwidth",
                        principalColumn: "PortBandwidthID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Port_PortConnector_PortConnectorID",
                        column: x => x.PortConnectorID,
                        principalTable: "PortConnector",
                        principalColumn: "PortConnectorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Port_PortPool_PortPoolID",
                        column: x => x.PortPoolID,
                        principalTable: "PortPool",
                        principalColumn: "PortPoolID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Port_PortSfp_PortSfpID",
                        column: x => x.PortSfpID,
                        principalTable: "PortSfp",
                        principalColumn: "PortSfpID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Port_PortStatus_PortStatusID",
                        column: x => x.PortStatusID,
                        principalTable: "PortStatus",
                        principalColumn: "PortStatusID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Port_Tenant_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Tenant",
                        principalColumn: "TenantID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vlan",
                columns: table => new
                {
                    VlanID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InterfaceID = table.Column<int>(nullable: true),
                    IpAddress = table.Column<string>(maxLength: 15, nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    SubnetMask = table.Column<string>(maxLength: 15, nullable: true),
                    VifID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vlan", x => x.VlanID);
                    table.ForeignKey(
                        name: "FK_Vlan_Interface_InterfaceID",
                        column: x => x.InterfaceID,
                        principalTable: "Interface",
                        principalColumn: "InterfaceID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vlan_Vif_VifID",
                        column: x => x.VifID,
                        principalTable: "Vif",
                        principalColumn: "VifID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExtranetVpnTenantCommunityIn",
                columns: table => new
                {
                    ExtranetVpnTenantCommunityInID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExtranetVpnMemberID = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    VpnTenantCommunityInID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtranetVpnTenantCommunityIn", x => x.ExtranetVpnTenantCommunityInID);
                    table.ForeignKey(
                        name: "FK_ExtranetVpnTenantCommunityIn_ExtranetVpnMember_ExtranetVpnMemberID",
                        column: x => x.ExtranetVpnMemberID,
                        principalTable: "ExtranetVpnMember",
                        principalColumn: "ExtranetVpnMemberID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExtranetVpnTenantCommunityIn_VpnTenantCommunityIn_VpnTenantCommunityInID",
                        column: x => x.VpnTenantCommunityInID,
                        principalTable: "VpnTenantCommunityIn",
                        principalColumn: "VpnTenantCommunityInID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExtranetVpnTenantNetworkIn",
                columns: table => new
                {
                    ExtranetVpnTenantNetworkInID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ExtranetVpnMemberID = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    VpnTenantIpNetworkInID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtranetVpnTenantNetworkIn", x => x.ExtranetVpnTenantNetworkInID);
                    table.ForeignKey(
                        name: "FK_ExtranetVpnTenantNetworkIn_ExtranetVpnMember_ExtranetVpnMemberID",
                        column: x => x.ExtranetVpnMemberID,
                        principalTable: "ExtranetVpnMember",
                        principalColumn: "ExtranetVpnMemberID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExtranetVpnTenantNetworkIn_VpnTenantIpNetworkIn_VpnTenantIpNetworkInID",
                        column: x => x.VpnTenantIpNetworkInID,
                        principalTable: "VpnTenantIpNetworkIn",
                        principalColumn: "VpnTenantIpNetworkInID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VpnTenantIpNetworkCommunityIn",
                columns: table => new
                {
                    VpnTenantIpNetworkCommunityInID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TenantCommunityID = table.Column<int>(nullable: false),
                    VpnTenantIpNetworkInID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VpnTenantIpNetworkCommunityIn", x => x.VpnTenantIpNetworkCommunityInID);
                    table.ForeignKey(
                        name: "FK_VpnTenantIpNetworkCommunityIn_TenantCommunity_TenantCommunityID",
                        column: x => x.TenantCommunityID,
                        principalTable: "TenantCommunity",
                        principalColumn: "TenantCommunityID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VpnTenantIpNetworkCommunityIn_VpnTenantIpNetworkIn_VpnTenantIpNetworkInID",
                        column: x => x.VpnTenantIpNetworkInID,
                        principalTable: "VpnTenantIpNetworkIn",
                        principalColumn: "VpnTenantIpNetworkInID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddressFamily_Name",
                table: "AddressFamily",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AddressFamily_VpnProtocolTypeID",
                table: "AddressFamily",
                column: "VpnProtocolTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_AttachmentBandwidthID",
                table: "Attachment",
                column: "AttachmentBandwidthID");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_AttachmentRoleID",
                table: "Attachment",
                column: "AttachmentRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_ContractBandwidthPoolID",
                table: "Attachment",
                column: "ContractBandwidthPoolID");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_DeviceID",
                table: "Attachment",
                column: "DeviceID");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_MtuID",
                table: "Attachment",
                column: "MtuID");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_RoutingInstanceID",
                table: "Attachment",
                column: "RoutingInstanceID");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_TenantID",
                table: "Attachment",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_AttachmentBandwidth_BandwidthGbps",
                table: "AttachmentBandwidth",
                column: "BandwidthGbps",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AttachmentRedundancy_Name",
                table: "AttachmentRedundancy",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AttachmentRole_PortPoolID",
                table: "AttachmentRole",
                column: "PortPoolID");

            migrationBuilder.CreateIndex(
                name: "IX_AttachmentRole_RoutingInstanceTypeID",
                table: "AttachmentRole",
                column: "RoutingInstanceTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_AttachmentSet_AttachmentRedundancyID",
                table: "AttachmentSet",
                column: "AttachmentRedundancyID");

            migrationBuilder.CreateIndex(
                name: "IX_AttachmentSet_MulticastVpnDomainTypeID",
                table: "AttachmentSet",
                column: "MulticastVpnDomainTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_AttachmentSet_RegionID",
                table: "AttachmentSet",
                column: "RegionID");

            migrationBuilder.CreateIndex(
                name: "IX_AttachmentSet_SubRegionID",
                table: "AttachmentSet",
                column: "SubRegionID");

            migrationBuilder.CreateIndex(
                name: "IX_AttachmentSet_TenantID",
                table: "AttachmentSet",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_AttachmentSetRoutingInstance_RoutingInstanceID",
                table: "AttachmentSetRoutingInstance",
                column: "RoutingInstanceID");

            migrationBuilder.CreateIndex(
                name: "IX_AttachmentSetRoutingInstance_AttachmentSetID_RoutingInstanceID",
                table: "AttachmentSetRoutingInstance",
                columns: new[] { "AttachmentSetID", "RoutingInstanceID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BgpPeer_RoutingInstanceID_Ipv4PeerAddress",
                table: "BgpPeer",
                columns: new[] { "RoutingInstanceID", "Ipv4PeerAddress" },
                unique: true,
                filter: "[Ipv4PeerAddress] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ContractBandwidth_BandwidthMbps",
                table: "ContractBandwidth",
                column: "BandwidthMbps",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContractBandwidthPool_ContractBandwidthID",
                table: "ContractBandwidthPool",
                column: "ContractBandwidthID");

            migrationBuilder.CreateIndex(
                name: "IX_ContractBandwidthPool_TenantID",
                table: "ContractBandwidthPool",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_Device_DeviceModelID",
                table: "Device",
                column: "DeviceModelID");

            migrationBuilder.CreateIndex(
                name: "IX_Device_DeviceRoleID",
                table: "Device",
                column: "DeviceRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Device_DeviceStatusID",
                table: "Device",
                column: "DeviceStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Device_LocationID",
                table: "Device",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Device_Name",
                table: "Device",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Device_PlaneID",
                table: "Device",
                column: "PlaneID");

            migrationBuilder.CreateIndex(
                name: "IX_Device_TenantID",
                table: "Device",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceModel_Name",
                table: "DeviceModel",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceRole_Name",
                table: "DeviceRole",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeviceRoleAttachmentRole_AttachmentRoleID",
                table: "DeviceRoleAttachmentRole",
                column: "AttachmentRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceRoleAttachmentRole_DeviceRoleID",
                table: "DeviceRoleAttachmentRole",
                column: "DeviceRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceRolePortRole_DeviceRoleID",
                table: "DeviceRolePortRole",
                column: "DeviceRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceRolePortRole_PortRoleID",
                table: "DeviceRolePortRole",
                column: "PortRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceStatus_Name",
                table: "DeviceStatus",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExtranetVpnMember_MemberVpnID",
                table: "ExtranetVpnMember",
                column: "MemberVpnID");

            migrationBuilder.CreateIndex(
                name: "IX_ExtranetVpnMember_ExtranetVpnID_MemberVpnID",
                table: "ExtranetVpnMember",
                columns: new[] { "ExtranetVpnID", "MemberVpnID" },
                unique: true,
                filter: "[ExtranetVpnID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ExtranetVpnTenantCommunityIn_VpnTenantCommunityInID",
                table: "ExtranetVpnTenantCommunityIn",
                column: "VpnTenantCommunityInID");

            migrationBuilder.CreateIndex(
                name: "IX_ExtranetVpnTenantCommunityIn_ExtranetVpnMemberID_VpnTenantCommunityInID",
                table: "ExtranetVpnTenantCommunityIn",
                columns: new[] { "ExtranetVpnMemberID", "VpnTenantCommunityInID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExtranetVpnTenantNetworkIn_VpnTenantIpNetworkInID",
                table: "ExtranetVpnTenantNetworkIn",
                column: "VpnTenantIpNetworkInID");

            migrationBuilder.CreateIndex(
                name: "IX_ExtranetVpnTenantNetworkIn_ExtranetVpnMemberID_VpnTenantIpNetworkInID",
                table: "ExtranetVpnTenantNetworkIn",
                columns: new[] { "ExtranetVpnMemberID", "VpnTenantIpNetworkInID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Interface_AttachmentID",
                table: "Interface",
                column: "AttachmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Interface_DeviceID",
                table: "Interface",
                column: "DeviceID");

            migrationBuilder.CreateIndex(
                name: "IX_Location_SiteName",
                table: "Location",
                column: "SiteName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Location_SubRegionID",
                table: "Location",
                column: "SubRegionID");

            migrationBuilder.CreateIndex(
                name: "IX_LogicalInterface_RoutingInstanceID",
                table: "LogicalInterface",
                column: "RoutingInstanceID");

            migrationBuilder.CreateIndex(
                name: "IX_Mtu_MtuValue",
                table: "Mtu",
                column: "MtuValue",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MulticastGeographicalScope_Name",
                table: "MulticastGeographicalScope",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MulticastVpnDirectionType_Name",
                table: "MulticastVpnDirectionType",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MulticastVpnDomainType_Name",
                table: "MulticastVpnDomainType",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MulticastVpnRp_AttachmentSetID",
                table: "MulticastVpnRp",
                column: "AttachmentSetID");

            migrationBuilder.CreateIndex(
                name: "IX_MulticastVpnServiceType_Name",
                table: "MulticastVpnServiceType",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Plane_Name",
                table: "Plane",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Port_DeviceID",
                table: "Port",
                column: "DeviceID");

            migrationBuilder.CreateIndex(
                name: "IX_Port_InterfaceID",
                table: "Port",
                column: "InterfaceID");

            migrationBuilder.CreateIndex(
                name: "IX_Port_PortBandwidthID",
                table: "Port",
                column: "PortBandwidthID");

            migrationBuilder.CreateIndex(
                name: "IX_Port_PortConnectorID",
                table: "Port",
                column: "PortConnectorID");

            migrationBuilder.CreateIndex(
                name: "IX_Port_PortPoolID",
                table: "Port",
                column: "PortPoolID");

            migrationBuilder.CreateIndex(
                name: "IX_Port_PortSfpID",
                table: "Port",
                column: "PortSfpID");

            migrationBuilder.CreateIndex(
                name: "IX_Port_PortStatusID",
                table: "Port",
                column: "PortStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Port_TenantID",
                table: "Port",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_Port_Type_Name_DeviceID",
                table: "Port",
                columns: new[] { "Type", "Name", "DeviceID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PortBandwidth_BandwidthGbps",
                table: "PortBandwidth",
                column: "BandwidthGbps",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PortConnector_Name",
                table: "PortConnector",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PortPool_Name",
                table: "PortPool",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PortPool_PortRoleID",
                table: "PortPool",
                column: "PortRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_PortRole_Name",
                table: "PortRole",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PortSfp_Name",
                table: "PortSfp",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PortStatus_Name",
                table: "PortStatus",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Region_Name",
                table: "Region",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RouteDistinguisherRange_Type",
                table: "RouteDistinguisherRange",
                column: "Type",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RouteTarget_VpnID",
                table: "RouteTarget",
                column: "VpnID");

            migrationBuilder.CreateIndex(
                name: "IX_RouteTarget_RouteTargetRangeID_AssignedNumberSubField",
                table: "RouteTarget",
                columns: new[] { "RouteTargetRangeID", "AssignedNumberSubField" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoutingInstance_DeviceID",
                table: "RoutingInstance",
                column: "DeviceID");

            migrationBuilder.CreateIndex(
                name: "IX_RoutingInstance_RouteDistinguisherRangeID",
                table: "RoutingInstance",
                column: "RouteDistinguisherRangeID");

            migrationBuilder.CreateIndex(
                name: "IX_RoutingInstance_RoutingInstanceTypeID",
                table: "RoutingInstance",
                column: "RoutingInstanceTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_RoutingInstance_TenantID",
                table: "RoutingInstance",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_RoutingInstance_Name_DeviceID",
                table: "RoutingInstance",
                columns: new[] { "Name", "DeviceID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoutingInstanceType_Type",
                table: "RoutingInstanceType",
                column: "Type",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoutingPolicyMatchOption_Name",
                table: "RoutingPolicyMatchOption",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubRegion_Name",
                table: "SubRegion",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubRegion_RegionID",
                table: "SubRegion",
                column: "RegionID");

            migrationBuilder.CreateIndex(
                name: "IX_Tenant_Name",
                table: "Tenant",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TenantCommunity_TenantID",
                table: "TenantCommunity",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_TenantCommunity_AutonomousSystemNumber_Number",
                table: "TenantCommunity",
                columns: new[] { "AutonomousSystemNumber", "Number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TenantCommunitySet_Name",
                table: "TenantCommunitySet",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TenantCommunitySet_RoutingPolicyMatchOptionID",
                table: "TenantCommunitySet",
                column: "RoutingPolicyMatchOptionID");

            migrationBuilder.CreateIndex(
                name: "IX_TenantCommunitySet_TenantCommunityID",
                table: "TenantCommunitySet",
                column: "TenantCommunityID");

            migrationBuilder.CreateIndex(
                name: "IX_TenantCommunitySet_TenantID",
                table: "TenantCommunitySet",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_TenantCommunitySetCommunity_TenantCommunitySetID",
                table: "TenantCommunitySetCommunity",
                column: "TenantCommunitySetID");

            migrationBuilder.CreateIndex(
                name: "IX_TenantCommunitySetCommunity_TenantCommunityID_TenantCommunitySetID",
                table: "TenantCommunitySetCommunity",
                columns: new[] { "TenantCommunityID", "TenantCommunitySetID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TenantIpNetwork_TenantID",
                table: "TenantIpNetwork",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_TenantMulticastGroup_TenantID",
                table: "TenantMulticastGroup",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_Vif_ContractBandwidthPoolID",
                table: "Vif",
                column: "ContractBandwidthPoolID");

            migrationBuilder.CreateIndex(
                name: "IX_Vif_MtuID",
                table: "Vif",
                column: "MtuID");

            migrationBuilder.CreateIndex(
                name: "IX_Vif_RoutingInstanceID",
                table: "Vif",
                column: "RoutingInstanceID");

            migrationBuilder.CreateIndex(
                name: "IX_Vif_TenantID",
                table: "Vif",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_Vif_VifRoleID",
                table: "Vif",
                column: "VifRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Vif_VlanTagRangeID",
                table: "Vif",
                column: "VlanTagRangeID");

            migrationBuilder.CreateIndex(
                name: "IX_Vif_AttachmentID_VlanTag",
                table: "Vif",
                columns: new[] { "AttachmentID", "VlanTag" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VifRole_AttachmentRoleID",
                table: "VifRole",
                column: "AttachmentRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_VifRole_RoutingInstanceTypeID",
                table: "VifRole",
                column: "RoutingInstanceTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Vlan_InterfaceID",
                table: "Vlan",
                column: "InterfaceID");

            migrationBuilder.CreateIndex(
                name: "IX_Vlan_VifID",
                table: "Vlan",
                column: "VifID");

            migrationBuilder.CreateIndex(
                name: "IX_Vpn_AddressFamilyID",
                table: "Vpn",
                column: "AddressFamilyID");

            migrationBuilder.CreateIndex(
                name: "IX_Vpn_MulticastVpnDirectionTypeID",
                table: "Vpn",
                column: "MulticastVpnDirectionTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Vpn_MulticastVpnServiceTypeID",
                table: "Vpn",
                column: "MulticastVpnServiceTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Vpn_Name",
                table: "Vpn",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vpn_PlaneID",
                table: "Vpn",
                column: "PlaneID");

            migrationBuilder.CreateIndex(
                name: "IX_Vpn_RegionID",
                table: "Vpn",
                column: "RegionID");

            migrationBuilder.CreateIndex(
                name: "IX_Vpn_TenantID",
                table: "Vpn",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_Vpn_VpnTenancyTypeID",
                table: "Vpn",
                column: "VpnTenancyTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Vpn_VpnTopologyTypeID",
                table: "Vpn",
                column: "VpnTopologyTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnAttachmentSet_VpnID",
                table: "VpnAttachmentSet",
                column: "VpnID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnAttachmentSet_AttachmentSetID_VpnID",
                table: "VpnAttachmentSet",
                columns: new[] { "AttachmentSetID", "VpnID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VpnProtocolType_ProtocolType",
                table: "VpnProtocolType",
                column: "ProtocolType",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenancyType_TenancyType",
                table: "VpnTenancyType",
                column: "TenancyType",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantCommunityIn_AttachmentSetID",
                table: "VpnTenantCommunityIn",
                column: "AttachmentSetID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantCommunityIn_BgpPeerID",
                table: "VpnTenantCommunityIn",
                column: "BgpPeerID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantCommunityIn_TenantCommunityID_AttachmentSetID_AddToAllBgpPeersInAttachmentSet_BgpPeerID",
                table: "VpnTenantCommunityIn",
                columns: new[] { "TenantCommunityID", "AttachmentSetID", "AddToAllBgpPeersInAttachmentSet", "BgpPeerID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantCommunityOut_AttachmentSetID",
                table: "VpnTenantCommunityOut",
                column: "AttachmentSetID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantCommunityOut_BgpPeerID",
                table: "VpnTenantCommunityOut",
                column: "BgpPeerID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantCommunityOut_TenantCommunityID_AttachmentSetID_BgpPeerID",
                table: "VpnTenantCommunityOut",
                columns: new[] { "TenantCommunityID", "AttachmentSetID", "BgpPeerID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantCommunityRoutingInstance_AttachmentSetID",
                table: "VpnTenantCommunityRoutingInstance",
                column: "AttachmentSetID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantCommunityRoutingInstance_RoutingInstanceID",
                table: "VpnTenantCommunityRoutingInstance",
                column: "RoutingInstanceID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantCommunityRoutingInstance_TenantCommunityID_AttachmentSetID",
                table: "VpnTenantCommunityRoutingInstance",
                columns: new[] { "TenantCommunityID", "AttachmentSetID" },
                unique: true,
                filter: "[TenantCommunityID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantCommunityRoutingInstance_TenantCommunitySetID_AttachmentSetID",
                table: "VpnTenantCommunityRoutingInstance",
                columns: new[] { "TenantCommunitySetID", "AttachmentSetID" },
                unique: true,
                filter: "[TenantCommunitySetID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkCommunityIn_TenantCommunityID",
                table: "VpnTenantIpNetworkCommunityIn",
                column: "TenantCommunityID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkCommunityIn_VpnTenantIpNetworkInID_TenantCommunityID",
                table: "VpnTenantIpNetworkCommunityIn",
                columns: new[] { "VpnTenantIpNetworkInID", "TenantCommunityID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkIn_AttachmentSetID",
                table: "VpnTenantIpNetworkIn",
                column: "AttachmentSetID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkIn_BgpPeerID",
                table: "VpnTenantIpNetworkIn",
                column: "BgpPeerID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkIn_TenantIpNetworkID_AttachmentSetID_AddToAllBgpPeersInAttachmentSet_BgpPeerID",
                table: "VpnTenantIpNetworkIn",
                columns: new[] { "TenantIpNetworkID", "AttachmentSetID", "AddToAllBgpPeersInAttachmentSet", "BgpPeerID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkOut_AttachmentSetID",
                table: "VpnTenantIpNetworkOut",
                column: "AttachmentSetID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkOut_BgpPeerID",
                table: "VpnTenantIpNetworkOut",
                column: "BgpPeerID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkOut_TenantIpNetworkID_AttachmentSetID_BgpPeerID",
                table: "VpnTenantIpNetworkOut",
                columns: new[] { "TenantIpNetworkID", "AttachmentSetID", "BgpPeerID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkRoutingInstance_AttachmentSetID",
                table: "VpnTenantIpNetworkRoutingInstance",
                column: "AttachmentSetID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkRoutingInstance_RoutingInstanceID",
                table: "VpnTenantIpNetworkRoutingInstance",
                column: "RoutingInstanceID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkRoutingInstance_TenantIpNetworkID_AttachmentSetID",
                table: "VpnTenantIpNetworkRoutingInstance",
                columns: new[] { "TenantIpNetworkID", "AttachmentSetID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkRoutingInstanceStaticRoute_AttachmentSetID",
                table: "VpnTenantIpNetworkRoutingInstanceStaticRoute",
                column: "AttachmentSetID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkRoutingInstanceStaticRoute_RoutingInstanceID",
                table: "VpnTenantIpNetworkRoutingInstanceStaticRoute",
                column: "RoutingInstanceID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkRoutingInstanceStaticRoute_TenantIpNetworkID_AttachmentSetID",
                table: "VpnTenantIpNetworkRoutingInstanceStaticRoute",
                columns: new[] { "TenantIpNetworkID", "AttachmentSetID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantMulticastGroup_AttachmentSetID",
                table: "VpnTenantMulticastGroup",
                column: "AttachmentSetID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantMulticastGroup_MulticastGeographicalScopeID",
                table: "VpnTenantMulticastGroup",
                column: "MulticastGeographicalScopeID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantMulticastGroup_MulticastVpnRpID",
                table: "VpnTenantMulticastGroup",
                column: "MulticastVpnRpID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantMulticastGroup_TenantMulticastGroupID_AttachmentSetID",
                table: "VpnTenantMulticastGroup",
                columns: new[] { "TenantMulticastGroupID", "AttachmentSetID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VpnTopologyType_VpnProtocolTypeID",
                table: "VpnTopologyType",
                column: "VpnProtocolTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTopologyType_TopologyType_VpnProtocolTypeID",
                table: "VpnTopologyType",
                columns: new[] { "TopologyType", "VpnProtocolTypeID" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttachmentSetRoutingInstance");

            migrationBuilder.DropTable(
                name: "DeviceRoleAttachmentRole");

            migrationBuilder.DropTable(
                name: "DeviceRolePortRole");

            migrationBuilder.DropTable(
                name: "ExtranetVpnTenantCommunityIn");

            migrationBuilder.DropTable(
                name: "ExtranetVpnTenantNetworkIn");

            migrationBuilder.DropTable(
                name: "LogicalInterface");

            migrationBuilder.DropTable(
                name: "Port");

            migrationBuilder.DropTable(
                name: "RouteTarget");

            migrationBuilder.DropTable(
                name: "TenantCommunitySetCommunity");

            migrationBuilder.DropTable(
                name: "Vlan");

            migrationBuilder.DropTable(
                name: "VpnAttachmentSet");

            migrationBuilder.DropTable(
                name: "VpnTenantCommunityOut");

            migrationBuilder.DropTable(
                name: "VpnTenantCommunityRoutingInstance");

            migrationBuilder.DropTable(
                name: "VpnTenantIpNetworkCommunityIn");

            migrationBuilder.DropTable(
                name: "VpnTenantIpNetworkOut");

            migrationBuilder.DropTable(
                name: "VpnTenantIpNetworkRoutingInstance");

            migrationBuilder.DropTable(
                name: "VpnTenantIpNetworkRoutingInstanceStaticRoute");

            migrationBuilder.DropTable(
                name: "VpnTenantMulticastGroup");

            migrationBuilder.DropTable(
                name: "VpnTenantCommunityIn");

            migrationBuilder.DropTable(
                name: "ExtranetVpnMember");

            migrationBuilder.DropTable(
                name: "PortBandwidth");

            migrationBuilder.DropTable(
                name: "PortConnector");

            migrationBuilder.DropTable(
                name: "PortSfp");

            migrationBuilder.DropTable(
                name: "PortStatus");

            migrationBuilder.DropTable(
                name: "RouteTargetRange");

            migrationBuilder.DropTable(
                name: "Interface");

            migrationBuilder.DropTable(
                name: "Vif");

            migrationBuilder.DropTable(
                name: "TenantCommunitySet");

            migrationBuilder.DropTable(
                name: "VpnTenantIpNetworkIn");

            migrationBuilder.DropTable(
                name: "MulticastGeographicalScope");

            migrationBuilder.DropTable(
                name: "MulticastVpnRp");

            migrationBuilder.DropTable(
                name: "TenantMulticastGroup");

            migrationBuilder.DropTable(
                name: "Vpn");

            migrationBuilder.DropTable(
                name: "Attachment");

            migrationBuilder.DropTable(
                name: "VifRole");

            migrationBuilder.DropTable(
                name: "VlanTagRange");

            migrationBuilder.DropTable(
                name: "RoutingPolicyMatchOption");

            migrationBuilder.DropTable(
                name: "TenantCommunity");

            migrationBuilder.DropTable(
                name: "BgpPeer");

            migrationBuilder.DropTable(
                name: "TenantIpNetwork");

            migrationBuilder.DropTable(
                name: "AttachmentSet");

            migrationBuilder.DropTable(
                name: "AddressFamily");

            migrationBuilder.DropTable(
                name: "MulticastVpnDirectionType");

            migrationBuilder.DropTable(
                name: "MulticastVpnServiceType");

            migrationBuilder.DropTable(
                name: "VpnTenancyType");

            migrationBuilder.DropTable(
                name: "VpnTopologyType");

            migrationBuilder.DropTable(
                name: "AttachmentBandwidth");

            migrationBuilder.DropTable(
                name: "ContractBandwidthPool");

            migrationBuilder.DropTable(
                name: "Mtu");

            migrationBuilder.DropTable(
                name: "AttachmentRole");

            migrationBuilder.DropTable(
                name: "RoutingInstance");

            migrationBuilder.DropTable(
                name: "AttachmentRedundancy");

            migrationBuilder.DropTable(
                name: "MulticastVpnDomainType");

            migrationBuilder.DropTable(
                name: "VpnProtocolType");

            migrationBuilder.DropTable(
                name: "ContractBandwidth");

            migrationBuilder.DropTable(
                name: "PortPool");

            migrationBuilder.DropTable(
                name: "Device");

            migrationBuilder.DropTable(
                name: "RouteDistinguisherRange");

            migrationBuilder.DropTable(
                name: "RoutingInstanceType");

            migrationBuilder.DropTable(
                name: "PortRole");

            migrationBuilder.DropTable(
                name: "DeviceModel");

            migrationBuilder.DropTable(
                name: "DeviceRole");

            migrationBuilder.DropTable(
                name: "DeviceStatus");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Plane");

            migrationBuilder.DropTable(
                name: "Tenant");

            migrationBuilder.DropTable(
                name: "SubRegion");

            migrationBuilder.DropTable(
                name: "Region");
        }
    }
}
