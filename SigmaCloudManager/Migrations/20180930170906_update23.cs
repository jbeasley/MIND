using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SigmaCloudManager.Migrations
{
    public partial class update23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantIpNetworkIn_AttachmentSet_AttachmentSetID",
                table: "VpnTenantIpNetworkIn");

            migrationBuilder.AddColumn<int>(
                name: "AttachmentSetID1",
                table: "VpnTenantIpNetworkIn",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkIn_AttachmentSetID1",
                table: "VpnTenantIpNetworkIn",
                column: "AttachmentSetID1");

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantIpNetworkIn_AttachmentSet_AttachmentSetID",
                table: "VpnTenantIpNetworkIn",
                column: "AttachmentSetID",
                principalTable: "AttachmentSet",
                principalColumn: "AttachmentSetID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantIpNetworkIn_AttachmentSet_AttachmentSetID1",
                table: "VpnTenantIpNetworkIn",
                column: "AttachmentSetID1",
                principalTable: "AttachmentSet",
                principalColumn: "AttachmentSetID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantIpNetworkIn_AttachmentSet_AttachmentSetID",
                table: "VpnTenantIpNetworkIn");

            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantIpNetworkIn_AttachmentSet_AttachmentSetID1",
                table: "VpnTenantIpNetworkIn");

            migrationBuilder.DropIndex(
                name: "IX_VpnTenantIpNetworkIn_AttachmentSetID1",
                table: "VpnTenantIpNetworkIn");

            migrationBuilder.DropColumn(
                name: "AttachmentSetID1",
                table: "VpnTenantIpNetworkIn");

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantIpNetworkIn_AttachmentSet_AttachmentSetID",
                table: "VpnTenantIpNetworkIn",
                column: "AttachmentSetID",
                principalTable: "AttachmentSet",
                principalColumn: "AttachmentSetID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
