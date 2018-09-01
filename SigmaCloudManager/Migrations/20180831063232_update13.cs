using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SigmaCloudManager.Migrations
{
    public partial class update13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vlan_Interface_InterfaceID",
                table: "Vlan");

            migrationBuilder.DropForeignKey(
                name: "FK_Vlan_Vif_VifID",
                table: "Vlan");

            migrationBuilder.AlterColumn<int>(
                name: "VifID",
                table: "Vlan",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InterfaceID",
                table: "Vlan",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Vlan_Interface_InterfaceID",
                table: "Vlan",
                column: "InterfaceID",
                principalTable: "Interface",
                principalColumn: "InterfaceID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vlan_Vif_VifID",
                table: "Vlan",
                column: "VifID",
                principalTable: "Vif",
                principalColumn: "VifID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vlan_Interface_InterfaceID",
                table: "Vlan");

            migrationBuilder.DropForeignKey(
                name: "FK_Vlan_Vif_VifID",
                table: "Vlan");

            migrationBuilder.AlterColumn<int>(
                name: "VifID",
                table: "Vlan",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "InterfaceID",
                table: "Vlan",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vlan_Interface_InterfaceID",
                table: "Vlan",
                column: "InterfaceID",
                principalTable: "Interface",
                principalColumn: "InterfaceID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vlan_Vif_VifID",
                table: "Vlan",
                column: "VifID",
                principalTable: "Vif",
                principalColumn: "VifID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
