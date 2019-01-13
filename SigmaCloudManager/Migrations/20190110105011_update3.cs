using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Mind.Migrations
{
    public partial class update3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequireSyncToNetwork",
                table: "DeviceRole");

            migrationBuilder.AddColumn<int>(
                name: "TenantEnvironment",
                table: "TenantIpNetwork",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TenantEnvironment",
                table: "TenantCommunity",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantEnvironment",
                table: "TenantIpNetwork");

            migrationBuilder.DropColumn(
                name: "TenantEnvironment",
                table: "TenantCommunity");

            migrationBuilder.AddColumn<bool>(
                name: "RequireSyncToNetwork",
                table: "DeviceRole",
                nullable: false,
                defaultValue: false);
        }
    }
}
