using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SigmaCloudManager.Migrations
{
    public partial class update12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RoutingInstanceType_Type",
                table: "RoutingInstanceType",
                column: "Type",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RouteDistinguisherRange_Type",
                table: "RouteDistinguisherRange",
                column: "Type",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RoutingInstanceType_Type",
                table: "RoutingInstanceType");

            migrationBuilder.DropIndex(
                name: "IX_RouteDistinguisherRange_Type",
                table: "RouteDistinguisherRange");
        }
    }
}
