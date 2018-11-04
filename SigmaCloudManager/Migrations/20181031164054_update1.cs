using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Mind.Migrations
{
    public partial class update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantIpNetworkIn_TenantIpNetwork_TenantIpNetworkID1",
                table: "VpnTenantIpNetworkIn");

            migrationBuilder.DropIndex(
                name: "IX_VpnTenantIpNetworkIn_TenantIpNetworkID1",
                table: "VpnTenantIpNetworkIn");

            migrationBuilder.DropColumn(
                name: "TenantIpNetworkID1",
                table: "VpnTenantIpNetworkIn");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TenantIpNetworkID1",
                table: "VpnTenantIpNetworkIn",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkIn_TenantIpNetworkID1",
                table: "VpnTenantIpNetworkIn",
                column: "TenantIpNetworkID1");

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantIpNetworkIn_TenantIpNetwork_TenantIpNetworkID1",
                table: "VpnTenantIpNetworkIn",
                column: "TenantIpNetworkID1",
                principalTable: "TenantIpNetwork",
                principalColumn: "TenantIpNetworkID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
