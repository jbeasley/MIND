using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SigmaCloudManager.Migrations
{
    public partial class Update22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantIpNetworkIn_AttachmentSet_AttachmentSetID",
                table: "VpnTenantIpNetworkIn");

            migrationBuilder.AlterColumn<int>(
                name: "AttachmentSetID",
                table: "VpnTenantIpNetworkIn",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantIpNetworkIn_AttachmentSet_AttachmentSetID",
                table: "VpnTenantIpNetworkIn",
                column: "AttachmentSetID",
                principalTable: "AttachmentSet",
                principalColumn: "AttachmentSetID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantIpNetworkIn_AttachmentSet_AttachmentSetID",
                table: "VpnTenantIpNetworkIn");

            migrationBuilder.AlterColumn<int>(
                name: "AttachmentSetID",
                table: "VpnTenantIpNetworkIn",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantIpNetworkIn_AttachmentSet_AttachmentSetID",
                table: "VpnTenantIpNetworkIn",
                column: "AttachmentSetID",
                principalTable: "AttachmentSet",
                principalColumn: "AttachmentSetID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
