using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SigmaCloudManager.Migrations
{
    public partial class update21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VpnTenantIpNetworkOut_TenantIpNetworkID_AttachmentSetID",
                table: "VpnTenantIpNetworkOut");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkOut_TenantIpNetworkID_AttachmentSetID_BgpPeerID",
                table: "VpnTenantIpNetworkOut",
                columns: new[] { "TenantIpNetworkID", "AttachmentSetID", "BgpPeerID" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VpnTenantIpNetworkOut_TenantIpNetworkID_AttachmentSetID_BgpPeerID",
                table: "VpnTenantIpNetworkOut");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkOut_TenantIpNetworkID_AttachmentSetID",
                table: "VpnTenantIpNetworkOut",
                columns: new[] { "TenantIpNetworkID", "AttachmentSetID" },
                unique: true);
        }
    }
}
