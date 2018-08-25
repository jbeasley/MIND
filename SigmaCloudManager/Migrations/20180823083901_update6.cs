using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SigmaCloudManager.Migrations
{
    public partial class update6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VpnTenantNetworkOut");

            migrationBuilder.DropTable(
                name: "VpnTenantNetworkRoutingInstance");

            migrationBuilder.DropTable(
                name: "VpnTenantNetworkStaticRouteRoutingInstance");

            migrationBuilder.DropIndex(
                name: "IX_VpnTenantIpNetworkIn_TenantIpNetworkID_AttachmentSetID",
                table: "VpnTenantIpNetworkIn");

            migrationBuilder.CreateTable(
                name: "VpnTenantIpNetworkOut",
                columns: table => new
                {
                    VpnTenantIpNetworkOutID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdvertisedIpRoutingPreference = table.Column<int>(nullable: false),
                    AttachmentSetID = table.Column<int>(nullable: false),
                    BgpPeerID = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TenantIpNetworkID = table.Column<int>(nullable: false),
                    TenantIpNetworkID1 = table.Column<int>(nullable: true)
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VpnTenantIpNetworkOut_TenantIpNetwork_TenantIpNetworkID",
                        column: x => x.TenantIpNetworkID,
                        principalTable: "TenantIpNetwork",
                        principalColumn: "TenantIpNetworkID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VpnTenantIpNetworkOut_TenantIpNetwork_TenantIpNetworkID1",
                        column: x => x.TenantIpNetworkID1,
                        principalTable: "TenantIpNetwork",
                        principalColumn: "TenantIpNetworkID",
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
                    TenantIpNetworkID = table.Column<int>(nullable: false),
                    TenantIpNetworkID1 = table.Column<int>(nullable: true)
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
                    table.ForeignKey(
                        name: "FK_VpnTenantIpNetworkRoutingInstance_TenantIpNetwork_TenantIpNetworkID1",
                        column: x => x.TenantIpNetworkID1,
                        principalTable: "TenantIpNetwork",
                        principalColumn: "TenantIpNetworkID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VpnTenantIpNetworkStaticRouteRoutingInstance",
                columns: table => new
                {
                    VpnTenantIpNetworkStaticRouteRoutingInstanceID = table.Column<int>(nullable: false)
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
                    table.PrimaryKey("PK_VpnTenantIpNetworkStaticRouteRoutingInstance", x => x.VpnTenantIpNetworkStaticRouteRoutingInstanceID);
                    table.ForeignKey(
                        name: "FK_VpnTenantIpNetworkStaticRouteRoutingInstance_AttachmentSet_AttachmentSetID",
                        column: x => x.AttachmentSetID,
                        principalTable: "AttachmentSet",
                        principalColumn: "AttachmentSetID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VpnTenantIpNetworkStaticRouteRoutingInstance_RoutingInstance_RoutingInstanceID",
                        column: x => x.RoutingInstanceID,
                        principalTable: "RoutingInstance",
                        principalColumn: "RoutingInstanceID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VpnTenantIpNetworkStaticRouteRoutingInstance_TenantIpNetwork_TenantIpNetworkID",
                        column: x => x.TenantIpNetworkID,
                        principalTable: "TenantIpNetwork",
                        principalColumn: "TenantIpNetworkID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkIn_TenantIpNetworkID_AttachmentSetID_AddToAllBgpPeersInAttachmentSet_BgpPeerID",
                table: "VpnTenantIpNetworkIn",
                columns: new[] { "TenantIpNetworkID", "AttachmentSetID", "AddToAllBgpPeersInAttachmentSet", "BgpPeerID" },
                unique: true,
                filter: "[BgpPeerID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkOut_AttachmentSetID",
                table: "VpnTenantIpNetworkOut",
                column: "AttachmentSetID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkOut_BgpPeerID",
                table: "VpnTenantIpNetworkOut",
                column: "BgpPeerID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkOut_TenantIpNetworkID1",
                table: "VpnTenantIpNetworkOut",
                column: "TenantIpNetworkID1");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkOut_TenantIpNetworkID_AttachmentSetID",
                table: "VpnTenantIpNetworkOut",
                columns: new[] { "TenantIpNetworkID", "AttachmentSetID" },
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
                name: "IX_VpnTenantIpNetworkRoutingInstance_TenantIpNetworkID1",
                table: "VpnTenantIpNetworkRoutingInstance",
                column: "TenantIpNetworkID1");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkRoutingInstance_TenantIpNetworkID_AttachmentSetID",
                table: "VpnTenantIpNetworkRoutingInstance",
                columns: new[] { "TenantIpNetworkID", "AttachmentSetID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkStaticRouteRoutingInstance_AttachmentSetID",
                table: "VpnTenantIpNetworkStaticRouteRoutingInstance",
                column: "AttachmentSetID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkStaticRouteRoutingInstance_RoutingInstanceID",
                table: "VpnTenantIpNetworkStaticRouteRoutingInstance",
                column: "RoutingInstanceID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkStaticRouteRoutingInstance_TenantIpNetworkID_AttachmentSetID",
                table: "VpnTenantIpNetworkStaticRouteRoutingInstance",
                columns: new[] { "TenantIpNetworkID", "AttachmentSetID" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VpnTenantIpNetworkOut");

            migrationBuilder.DropTable(
                name: "VpnTenantIpNetworkRoutingInstance");

            migrationBuilder.DropTable(
                name: "VpnTenantIpNetworkStaticRouteRoutingInstance");

            migrationBuilder.DropIndex(
                name: "IX_VpnTenantIpNetworkIn_TenantIpNetworkID_AttachmentSetID_AddToAllBgpPeersInAttachmentSet_BgpPeerID",
                table: "VpnTenantIpNetworkIn");

            migrationBuilder.CreateTable(
                name: "VpnTenantNetworkOut",
                columns: table => new
                {
                    VpnTenantNetworkOutID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AdvertisedIpRoutingPreference = table.Column<int>(nullable: false),
                    AttachmentSetID = table.Column<int>(nullable: false),
                    BgpPeerID = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TenantIpNetworkID = table.Column<int>(nullable: true),
                    TenantIpNetworkID1 = table.Column<int>(nullable: true),
                    TenantNetworkID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VpnTenantNetworkOut", x => x.VpnTenantNetworkOutID);
                    table.ForeignKey(
                        name: "FK_VpnTenantNetworkOut_AttachmentSet_AttachmentSetID",
                        column: x => x.AttachmentSetID,
                        principalTable: "AttachmentSet",
                        principalColumn: "AttachmentSetID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VpnTenantNetworkOut_BgpPeer_BgpPeerID",
                        column: x => x.BgpPeerID,
                        principalTable: "BgpPeer",
                        principalColumn: "BgpPeerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VpnTenantNetworkOut_TenantIpNetwork_TenantIpNetworkID",
                        column: x => x.TenantIpNetworkID,
                        principalTable: "TenantIpNetwork",
                        principalColumn: "TenantIpNetworkID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VpnTenantNetworkOut_TenantIpNetwork_TenantIpNetworkID1",
                        column: x => x.TenantIpNetworkID1,
                        principalTable: "TenantIpNetwork",
                        principalColumn: "TenantIpNetworkID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VpnTenantNetworkRoutingInstance",
                columns: table => new
                {
                    VpnTenantNetworkRoutingInstanceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AttachmentSetID = table.Column<int>(nullable: false),
                    LocalIpRoutingPreference = table.Column<int>(nullable: false),
                    RoutingInstanceID = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TenantIpNetworkID = table.Column<int>(nullable: true),
                    TenantIpNetworkID1 = table.Column<int>(nullable: true),
                    TenantNetworkID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VpnTenantNetworkRoutingInstance", x => x.VpnTenantNetworkRoutingInstanceID);
                    table.ForeignKey(
                        name: "FK_VpnTenantNetworkRoutingInstance_AttachmentSet_AttachmentSetID",
                        column: x => x.AttachmentSetID,
                        principalTable: "AttachmentSet",
                        principalColumn: "AttachmentSetID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VpnTenantNetworkRoutingInstance_RoutingInstance_RoutingInstanceID",
                        column: x => x.RoutingInstanceID,
                        principalTable: "RoutingInstance",
                        principalColumn: "RoutingInstanceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VpnTenantNetworkRoutingInstance_TenantIpNetwork_TenantIpNetworkID",
                        column: x => x.TenantIpNetworkID,
                        principalTable: "TenantIpNetwork",
                        principalColumn: "TenantIpNetworkID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VpnTenantNetworkRoutingInstance_TenantIpNetwork_TenantIpNetworkID1",
                        column: x => x.TenantIpNetworkID1,
                        principalTable: "TenantIpNetwork",
                        principalColumn: "TenantIpNetworkID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VpnTenantNetworkStaticRouteRoutingInstance",
                columns: table => new
                {
                    VpnTenantNetworkStaticRouteRoutingInstanceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddToAllRoutingInstancesInAttachmentSet = table.Column<bool>(nullable: false),
                    AttachmentSetID = table.Column<int>(nullable: false),
                    IsBfdEnabled = table.Column<bool>(nullable: false),
                    NextHopAddress = table.Column<string>(nullable: true),
                    RoutingInstanceID = table.Column<int>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TenantIpNetworkID = table.Column<int>(nullable: true),
                    TenantNetworkID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VpnTenantNetworkStaticRouteRoutingInstance", x => x.VpnTenantNetworkStaticRouteRoutingInstanceID);
                    table.ForeignKey(
                        name: "FK_VpnTenantNetworkStaticRouteRoutingInstance_AttachmentSet_AttachmentSetID",
                        column: x => x.AttachmentSetID,
                        principalTable: "AttachmentSet",
                        principalColumn: "AttachmentSetID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VpnTenantNetworkStaticRouteRoutingInstance_RoutingInstance_RoutingInstanceID",
                        column: x => x.RoutingInstanceID,
                        principalTable: "RoutingInstance",
                        principalColumn: "RoutingInstanceID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VpnTenantNetworkStaticRouteRoutingInstance_TenantIpNetwork_TenantIpNetworkID",
                        column: x => x.TenantIpNetworkID,
                        principalTable: "TenantIpNetwork",
                        principalColumn: "TenantIpNetworkID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkIn_TenantIpNetworkID_AttachmentSetID",
                table: "VpnTenantIpNetworkIn",
                columns: new[] { "TenantIpNetworkID", "AttachmentSetID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantNetworkOut_AttachmentSetID",
                table: "VpnTenantNetworkOut",
                column: "AttachmentSetID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantNetworkOut_BgpPeerID",
                table: "VpnTenantNetworkOut",
                column: "BgpPeerID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantNetworkOut_TenantIpNetworkID",
                table: "VpnTenantNetworkOut",
                column: "TenantIpNetworkID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantNetworkOut_TenantIpNetworkID1",
                table: "VpnTenantNetworkOut",
                column: "TenantIpNetworkID1");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantNetworkOut_TenantNetworkID_AttachmentSetID",
                table: "VpnTenantNetworkOut",
                columns: new[] { "TenantNetworkID", "AttachmentSetID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantNetworkRoutingInstance_AttachmentSetID",
                table: "VpnTenantNetworkRoutingInstance",
                column: "AttachmentSetID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantNetworkRoutingInstance_RoutingInstanceID",
                table: "VpnTenantNetworkRoutingInstance",
                column: "RoutingInstanceID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantNetworkRoutingInstance_TenantIpNetworkID",
                table: "VpnTenantNetworkRoutingInstance",
                column: "TenantIpNetworkID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantNetworkRoutingInstance_TenantIpNetworkID1",
                table: "VpnTenantNetworkRoutingInstance",
                column: "TenantIpNetworkID1");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantNetworkRoutingInstance_TenantNetworkID_AttachmentSetID",
                table: "VpnTenantNetworkRoutingInstance",
                columns: new[] { "TenantNetworkID", "AttachmentSetID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantNetworkStaticRouteRoutingInstance_AttachmentSetID",
                table: "VpnTenantNetworkStaticRouteRoutingInstance",
                column: "AttachmentSetID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantNetworkStaticRouteRoutingInstance_RoutingInstanceID",
                table: "VpnTenantNetworkStaticRouteRoutingInstance",
                column: "RoutingInstanceID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantNetworkStaticRouteRoutingInstance_TenantIpNetworkID",
                table: "VpnTenantNetworkStaticRouteRoutingInstance",
                column: "TenantIpNetworkID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantNetworkStaticRouteRoutingInstance_TenantNetworkID_AttachmentSetID",
                table: "VpnTenantNetworkStaticRouteRoutingInstance",
                columns: new[] { "TenantNetworkID", "AttachmentSetID" },
                unique: true);
        }
    }
}
