using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SigmaCloudManager.Migrations
{
    public partial class update24 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VpnTenantIpNetworkOut_TenantIpNetworkID_AttachmentSetID_BgpPeerID",
                table: "VpnTenantIpNetworkOut");

            migrationBuilder.AlterColumn<int>(
                name: "AttachmentSetID",
                table: "VpnTenantIpNetworkOut",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "AttachmentSetID1",
                table: "VpnTenantIpNetworkOut",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkOut_AttachmentSetID1",
                table: "VpnTenantIpNetworkOut",
                column: "AttachmentSetID1");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkOut_TenantIpNetworkID_AttachmentSetID_BgpPeerID",
                table: "VpnTenantIpNetworkOut",
                columns: new[] { "TenantIpNetworkID", "AttachmentSetID", "BgpPeerID" },
                unique: true,
                filter: "[AttachmentSetID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantIpNetworkOut_AttachmentSet_AttachmentSetID1",
                table: "VpnTenantIpNetworkOut",
                column: "AttachmentSetID1",
                principalTable: "AttachmentSet",
                principalColumn: "AttachmentSetID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantIpNetworkOut_AttachmentSet_AttachmentSetID1",
                table: "VpnTenantIpNetworkOut");

            migrationBuilder.DropIndex(
                name: "IX_VpnTenantIpNetworkOut_AttachmentSetID1",
                table: "VpnTenantIpNetworkOut");

            migrationBuilder.DropIndex(
                name: "IX_VpnTenantIpNetworkOut_TenantIpNetworkID_AttachmentSetID_BgpPeerID",
                table: "VpnTenantIpNetworkOut");

            migrationBuilder.DropColumn(
                name: "AttachmentSetID1",
                table: "VpnTenantIpNetworkOut");

            migrationBuilder.AlterColumn<int>(
                name: "AttachmentSetID",
                table: "VpnTenantIpNetworkOut",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkOut_TenantIpNetworkID_AttachmentSetID_BgpPeerID",
                table: "VpnTenantIpNetworkOut",
                columns: new[] { "TenantIpNetworkID", "AttachmentSetID", "BgpPeerID" },
                unique: true);
        }
    }
}
