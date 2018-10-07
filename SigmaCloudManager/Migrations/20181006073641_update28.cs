using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SigmaCloudManager.Migrations
{
    public partial class update28 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VpnTenantCommunityOut_TenantCommunityID_AttachmentSetID",
                table: "VpnTenantCommunityOut");

            migrationBuilder.DropIndex(
                name: "IX_VpnTenantCommunityIn_TenantCommunityID_AttachmentSetID",
                table: "VpnTenantCommunityIn");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantCommunityOut_TenantCommunityID_AttachmentSetID_BgpPeerID",
                table: "VpnTenantCommunityOut",
                columns: new[] { "TenantCommunityID", "AttachmentSetID", "BgpPeerID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantCommunityIn_TenantCommunityID_AttachmentSetID_AddToAllBgpPeersInAttachmentSet_BgpPeerID",
                table: "VpnTenantCommunityIn",
                columns: new[] { "TenantCommunityID", "AttachmentSetID", "AddToAllBgpPeersInAttachmentSet", "BgpPeerID" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VpnTenantCommunityOut_TenantCommunityID_AttachmentSetID_BgpPeerID",
                table: "VpnTenantCommunityOut");

            migrationBuilder.DropIndex(
                name: "IX_VpnTenantCommunityIn_TenantCommunityID_AttachmentSetID_AddToAllBgpPeersInAttachmentSet_BgpPeerID",
                table: "VpnTenantCommunityIn");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantCommunityOut_TenantCommunityID_AttachmentSetID",
                table: "VpnTenantCommunityOut",
                columns: new[] { "TenantCommunityID", "AttachmentSetID" },
                unique: true,
                filter: "[AttachmentSetID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantCommunityIn_TenantCommunityID_AttachmentSetID",
                table: "VpnTenantCommunityIn",
                columns: new[] { "TenantCommunityID", "AttachmentSetID" },
                unique: true,
                filter: "[AttachmentSetID] IS NOT NULL");
        }
    }
}
