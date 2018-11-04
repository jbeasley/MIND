using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Mind.Migrations
{
    public partial class update3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "AddressFamily",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VpnProtocolTypeID",
                table: "AddressFamily",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_AddressFamily_VpnProtocolTypeID",
                table: "AddressFamily",
                column: "VpnProtocolTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_AddressFamily_VpnProtocolType_VpnProtocolTypeID",
                table: "AddressFamily",
                column: "VpnProtocolTypeID",
                principalTable: "VpnProtocolType",
                principalColumn: "VpnProtocolTypeID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddressFamily_VpnProtocolType_VpnProtocolTypeID",
                table: "AddressFamily");

            migrationBuilder.DropIndex(
                name: "IX_AddressFamily_VpnProtocolTypeID",
                table: "AddressFamily");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "AddressFamily");

            migrationBuilder.DropColumn(
                name: "VpnProtocolTypeID",
                table: "AddressFamily");
        }
    }
}
