using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SigmaCloudManager.Migrations
{
    public partial class update16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IpRoutingBehaviour",
                table: "TenantIpNetwork",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IpRoutingBehaviour",
                table: "TenantIpNetwork");
        }
    }
}
