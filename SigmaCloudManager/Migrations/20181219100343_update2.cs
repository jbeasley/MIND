using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Mind.Migrations
{
    public partial class update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequiresSync",
                table: "Device");

            migrationBuilder.DropColumn(
                name: "ShowRequiresSyncAlert",
                table: "Device");

            migrationBuilder.AddColumn<int>(
                name: "TenantID",
                table: "Location",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Location_TenantID",
                table: "Location",
                column: "TenantID");

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Tenant_TenantID",
                table: "Location",
                column: "TenantID",
                principalTable: "Tenant",
                principalColumn: "TenantID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Location_Tenant_TenantID",
                table: "Location");

            migrationBuilder.DropIndex(
                name: "IX_Location_TenantID",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "TenantID",
                table: "Location");

            migrationBuilder.AddColumn<bool>(
                name: "RequiresSync",
                table: "Device",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ShowRequiresSyncAlert",
                table: "Device",
                nullable: false,
                defaultValue: false);
        }
    }
}
