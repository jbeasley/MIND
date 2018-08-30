using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SigmaCloudManager.Migrations
{
    public partial class update10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "RouteDistinguisherRange");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "RouteDistinguisherRange",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "RouteDistinguisherRange");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "RouteDistinguisherRange",
                nullable: false,
                defaultValue: "");
        }
    }
}
