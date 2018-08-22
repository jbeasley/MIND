using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SigmaCloudManager.Migrations
{
    public partial class update4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExtranetVpnTenantNetworkIn_VpnTenantNetworkIn_VpnTenantNetworkInID",
                table: "ExtranetVpnTenantNetworkIn");

            migrationBuilder.DropTable(
                name: "VpnTenantNetworkCommunityIn");

            migrationBuilder.DropTable(
                name: "VpnTenantNetworkIn");

            migrationBuilder.DropIndex(
                name: "IX_BgpPeer_RoutingInstanceID_IpAddress",
                table: "BgpPeer");

            migrationBuilder.RenameColumn(
                name: "VpnTenantNetworkInID",
                table: "ExtranetVpnTenantNetworkIn",
                newName: "VpnTenantIpNetworkInID");

            migrationBuilder.RenameIndex(
                name: "IX_ExtranetVpnTenantNetworkIn_ExtranetVpnMemberID_VpnTenantNetworkInID",
                table: "ExtranetVpnTenantNetworkIn",
                newName: "IX_ExtranetVpnTenantNetworkIn_ExtranetVpnMemberID_VpnTenantIpNetworkInID");

            migrationBuilder.RenameIndex(
                name: "IX_ExtranetVpnTenantNetworkIn_VpnTenantNetworkInID",
                table: "ExtranetVpnTenantNetworkIn",
                newName: "IX_ExtranetVpnTenantNetworkIn_VpnTenantIpNetworkInID");

            migrationBuilder.RenameColumn(
                name: "IpAddress",
                table: "BgpPeer",
                newName: "Ipv4PeerAddress");

            migrationBuilder.AddColumn<int>(
                name: "TenantID1",
                table: "AttachmentSet",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VpnTenantIpNetworkIn",
                columns: table => new
                {
                    VpnTenantIpNetworkInID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddToAllBgpPeersInAttachmentSet = table.Column<bool>(nullable: false),
                    AttachmentSetID = table.Column<int>(nullable: false),
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
                    TenantCommunityID1 = table.Column<int>(nullable: true),
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
                        name: "FK_VpnTenantIpNetworkCommunityIn_TenantCommunity_TenantCommunityID1",
                        column: x => x.TenantCommunityID1,
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
                name: "IX_BgpPeer_RoutingInstanceID_Ipv4PeerAddress",
                table: "BgpPeer",
                columns: new[] { "RoutingInstanceID", "Ipv4PeerAddress" },
                unique: true,
                filter: "[Ipv4PeerAddress] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AttachmentSet_TenantID1",
                table: "AttachmentSet",
                column: "TenantID1");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkCommunityIn_TenantCommunityID",
                table: "VpnTenantIpNetworkCommunityIn",
                column: "TenantCommunityID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkCommunityIn_TenantCommunityID1",
                table: "VpnTenantIpNetworkCommunityIn",
                column: "TenantCommunityID1");

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
                name: "IX_VpnTenantIpNetworkIn_TenantIpNetworkID_AttachmentSetID",
                table: "VpnTenantIpNetworkIn",
                columns: new[] { "TenantIpNetworkID", "AttachmentSetID" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AttachmentSet_Tenant_TenantID1",
                table: "AttachmentSet",
                column: "TenantID1",
                principalTable: "Tenant",
                principalColumn: "TenantID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExtranetVpnTenantNetworkIn_VpnTenantIpNetworkIn_VpnTenantIpNetworkInID",
                table: "ExtranetVpnTenantNetworkIn",
                column: "VpnTenantIpNetworkInID",
                principalTable: "VpnTenantIpNetworkIn",
                principalColumn: "VpnTenantIpNetworkInID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttachmentSet_Tenant_TenantID1",
                table: "AttachmentSet");

            migrationBuilder.DropForeignKey(
                name: "FK_ExtranetVpnTenantNetworkIn_VpnTenantIpNetworkIn_VpnTenantIpNetworkInID",
                table: "ExtranetVpnTenantNetworkIn");

            migrationBuilder.DropTable(
                name: "VpnTenantIpNetworkCommunityIn");

            migrationBuilder.DropTable(
                name: "VpnTenantIpNetworkIn");

            migrationBuilder.DropIndex(
                name: "IX_BgpPeer_RoutingInstanceID_Ipv4PeerAddress",
                table: "BgpPeer");

            migrationBuilder.DropIndex(
                name: "IX_AttachmentSet_TenantID1",
                table: "AttachmentSet");

            migrationBuilder.DropColumn(
                name: "TenantID1",
                table: "AttachmentSet");

            migrationBuilder.RenameColumn(
                name: "VpnTenantIpNetworkInID",
                table: "ExtranetVpnTenantNetworkIn",
                newName: "VpnTenantNetworkInID");

            migrationBuilder.RenameIndex(
                name: "IX_ExtranetVpnTenantNetworkIn_ExtranetVpnMemberID_VpnTenantIpNetworkInID",
                table: "ExtranetVpnTenantNetworkIn",
                newName: "IX_ExtranetVpnTenantNetworkIn_ExtranetVpnMemberID_VpnTenantNetworkInID");

            migrationBuilder.RenameIndex(
                name: "IX_ExtranetVpnTenantNetworkIn_VpnTenantIpNetworkInID",
                table: "ExtranetVpnTenantNetworkIn",
                newName: "IX_ExtranetVpnTenantNetworkIn_VpnTenantNetworkInID");

            migrationBuilder.RenameColumn(
                name: "Ipv4PeerAddress",
                table: "BgpPeer",
                newName: "IpAddress");

            migrationBuilder.CreateTable(
                name: "VpnTenantNetworkIn",
                columns: table => new
                {
                    VpnTenantNetworkInID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddToAllBgpPeersInAttachmentSet = table.Column<bool>(nullable: false),
                    AttachmentSetID = table.Column<int>(nullable: false),
                    BgpPeerID = table.Column<int>(nullable: true),
                    LocalIpRoutingPreference = table.Column<int>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TenantIpNetworkID = table.Column<int>(nullable: true),
                    TenantNetworkID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VpnTenantNetworkIn", x => x.VpnTenantNetworkInID);
                    table.ForeignKey(
                        name: "FK_VpnTenantNetworkIn_AttachmentSet_AttachmentSetID",
                        column: x => x.AttachmentSetID,
                        principalTable: "AttachmentSet",
                        principalColumn: "AttachmentSetID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VpnTenantNetworkIn_BgpPeer_BgpPeerID",
                        column: x => x.BgpPeerID,
                        principalTable: "BgpPeer",
                        principalColumn: "BgpPeerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VpnTenantNetworkIn_TenantIpNetwork_TenantIpNetworkID",
                        column: x => x.TenantIpNetworkID,
                        principalTable: "TenantIpNetwork",
                        principalColumn: "TenantIpNetworkID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VpnTenantNetworkCommunityIn",
                columns: table => new
                {
                    VpnTenantNetworkCommunityInID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TenantCommunityID = table.Column<int>(nullable: false),
                    TenantCommunityID1 = table.Column<int>(nullable: true),
                    VpnTenantNetworkInID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VpnTenantNetworkCommunityIn", x => x.VpnTenantNetworkCommunityInID);
                    table.ForeignKey(
                        name: "FK_VpnTenantNetworkCommunityIn_TenantCommunity_TenantCommunityID",
                        column: x => x.TenantCommunityID,
                        principalTable: "TenantCommunity",
                        principalColumn: "TenantCommunityID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VpnTenantNetworkCommunityIn_TenantCommunity_TenantCommunityID1",
                        column: x => x.TenantCommunityID1,
                        principalTable: "TenantCommunity",
                        principalColumn: "TenantCommunityID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VpnTenantNetworkCommunityIn_VpnTenantNetworkIn_VpnTenantNetworkInID",
                        column: x => x.VpnTenantNetworkInID,
                        principalTable: "VpnTenantNetworkIn",
                        principalColumn: "VpnTenantNetworkInID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BgpPeer_RoutingInstanceID_IpAddress",
                table: "BgpPeer",
                columns: new[] { "RoutingInstanceID", "IpAddress" },
                unique: true,
                filter: "[IpAddress] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantNetworkCommunityIn_TenantCommunityID",
                table: "VpnTenantNetworkCommunityIn",
                column: "TenantCommunityID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantNetworkCommunityIn_TenantCommunityID1",
                table: "VpnTenantNetworkCommunityIn",
                column: "TenantCommunityID1");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantNetworkCommunityIn_VpnTenantNetworkInID_TenantCommunityID",
                table: "VpnTenantNetworkCommunityIn",
                columns: new[] { "VpnTenantNetworkInID", "TenantCommunityID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantNetworkIn_AttachmentSetID",
                table: "VpnTenantNetworkIn",
                column: "AttachmentSetID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantNetworkIn_BgpPeerID",
                table: "VpnTenantNetworkIn",
                column: "BgpPeerID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantNetworkIn_TenantIpNetworkID",
                table: "VpnTenantNetworkIn",
                column: "TenantIpNetworkID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantNetworkIn_TenantNetworkID_AttachmentSetID",
                table: "VpnTenantNetworkIn",
                columns: new[] { "TenantNetworkID", "AttachmentSetID" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ExtranetVpnTenantNetworkIn_VpnTenantNetworkIn_VpnTenantNetworkInID",
                table: "ExtranetVpnTenantNetworkIn",
                column: "VpnTenantNetworkInID",
                principalTable: "VpnTenantNetworkIn",
                principalColumn: "VpnTenantNetworkInID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
