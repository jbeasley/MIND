using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SigmaCloudManager.Migrations
{
    public partial class update8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VpnTenantIpNetworkIn_TenantIpNetworkID_AttachmentSetID_AddToAllBgpPeersInAttachmentSet_BgpPeerID",
                table: "VpnTenantIpNetworkIn");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkIn_TenantIpNetworkID_AttachmentSetID_AddToAllBgpPeersInAttachmentSet_BgpPeerID",
                table: "VpnTenantIpNetworkIn",
                columns: new[] { "TenantIpNetworkID", "AttachmentSetID", "AddToAllBgpPeersInAttachmentSet", "BgpPeerID" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VpnTenantIpNetworkIn_TenantIpNetworkID_AttachmentSetID_AddToAllBgpPeersInAttachmentSet_BgpPeerID",
                table: "VpnTenantIpNetworkIn");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkIn_TenantIpNetworkID_AttachmentSetID_AddToAllBgpPeersInAttachmentSet_BgpPeerID",
                table: "VpnTenantIpNetworkIn",
                columns: new[] { "TenantIpNetworkID", "AttachmentSetID", "AddToAllBgpPeersInAttachmentSet", "BgpPeerID" },
                unique: true,
                filter: "[BgpPeerID] IS NOT NULL");
        }
    }
}
