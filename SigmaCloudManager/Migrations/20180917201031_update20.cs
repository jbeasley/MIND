using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SigmaCloudManager.Migrations
{
    public partial class update20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TenantID1",
                table: "Vpn",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vpn_TenantID1",
                table: "Vpn",
                column: "TenantID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Vpn_Tenant_TenantID1",
                table: "Vpn",
                column: "TenantID1",
                principalTable: "Tenant",
                principalColumn: "TenantID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vpn_Tenant_TenantID1",
                table: "Vpn");

            migrationBuilder.DropIndex(
                name: "IX_Vpn_TenantID1",
                table: "Vpn");

            migrationBuilder.DropColumn(
                name: "TenantID1",
                table: "Vpn");
        }
    }
}
