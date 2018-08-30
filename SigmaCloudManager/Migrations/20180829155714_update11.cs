using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SigmaCloudManager.Migrations
{
    public partial class update11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RoutingInstanceType_Name",
                table: "RoutingInstanceType");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "RoutingInstanceType");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "RoutingInstanceType",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "RoutingInstanceType");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "RoutingInstanceType",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_RoutingInstanceType_Name",
                table: "RoutingInstanceType",
                column: "Name",
                unique: true);
        }
    }
}
