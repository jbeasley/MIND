using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SigmaCloudManager.Migrations
{
    public partial class update14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_Mtu_MtuID",
                table: "Attachment");

            migrationBuilder.AddColumn<int>(
                name: "MtuID",
                table: "Vif",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MtuID1",
                table: "Vif",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MtuID1",
                table: "Attachment",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vif_MtuID",
                table: "Vif",
                column: "MtuID");

            migrationBuilder.CreateIndex(
                name: "IX_Vif_MtuID1",
                table: "Vif",
                column: "MtuID1");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_MtuID1",
                table: "Attachment",
                column: "MtuID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_Mtu_MtuID",
                table: "Attachment",
                column: "MtuID",
                principalTable: "Mtu",
                principalColumn: "MtuID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_Mtu_MtuID1",
                table: "Attachment",
                column: "MtuID1",
                principalTable: "Mtu",
                principalColumn: "MtuID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vif_Mtu_MtuID",
                table: "Vif",
                column: "MtuID",
                principalTable: "Mtu",
                principalColumn: "MtuID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vif_Mtu_MtuID1",
                table: "Vif",
                column: "MtuID1",
                principalTable: "Mtu",
                principalColumn: "MtuID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_Mtu_MtuID",
                table: "Attachment");

            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_Mtu_MtuID1",
                table: "Attachment");

            migrationBuilder.DropForeignKey(
                name: "FK_Vif_Mtu_MtuID",
                table: "Vif");

            migrationBuilder.DropForeignKey(
                name: "FK_Vif_Mtu_MtuID1",
                table: "Vif");

            migrationBuilder.DropIndex(
                name: "IX_Vif_MtuID",
                table: "Vif");

            migrationBuilder.DropIndex(
                name: "IX_Vif_MtuID1",
                table: "Vif");

            migrationBuilder.DropIndex(
                name: "IX_Attachment_MtuID1",
                table: "Attachment");

            migrationBuilder.DropColumn(
                name: "MtuID",
                table: "Vif");

            migrationBuilder.DropColumn(
                name: "MtuID1",
                table: "Vif");

            migrationBuilder.DropColumn(
                name: "MtuID1",
                table: "Attachment");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_Mtu_MtuID",
                table: "Attachment",
                column: "MtuID",
                principalTable: "Mtu",
                principalColumn: "MtuID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
