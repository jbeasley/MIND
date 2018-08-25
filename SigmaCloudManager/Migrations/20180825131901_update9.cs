using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SigmaCloudManager.Migrations
{
    public partial class update9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TenantIpNetwork_Ipv4Prefix_Ipv4Length",
                table: "TenantIpNetwork");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TenantIpNetwork_Ipv4Prefix_Ipv4Length",
                table: "TenantIpNetwork",
                columns: new[] { "Ipv4Prefix", "Ipv4Length" },
                unique: true,
                filter: "[Ipv4Prefix] IS NOT NULL");
        }
    }
}
