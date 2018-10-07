using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SigmaCloudManager.Migrations
{
    public partial class update27 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantCommunityOut_AttachmentSet_AttachmentSetID",
                table: "VpnTenantCommunityOut");

            migrationBuilder.DropIndex(
                name: "IX_VpnTenantCommunityOut_TenantCommunityID_AttachmentSetID",
                table: "VpnTenantCommunityOut");

            migrationBuilder.AlterColumn<int>(
                name: "AttachmentSetID",
                table: "VpnTenantCommunityOut",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantCommunityOut_TenantCommunityID_AttachmentSetID",
                table: "VpnTenantCommunityOut",
                columns: new[] { "TenantCommunityID", "AttachmentSetID" },
                unique: true,
                filter: "[AttachmentSetID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantCommunityOut_AttachmentSet_AttachmentSetID",
                table: "VpnTenantCommunityOut",
                column: "AttachmentSetID",
                principalTable: "AttachmentSet",
                principalColumn: "AttachmentSetID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantCommunityOut_AttachmentSet_AttachmentSetID",
                table: "VpnTenantCommunityOut");

            migrationBuilder.DropIndex(
                name: "IX_VpnTenantCommunityOut_TenantCommunityID_AttachmentSetID",
                table: "VpnTenantCommunityOut");

            migrationBuilder.AlterColumn<int>(
                name: "AttachmentSetID",
                table: "VpnTenantCommunityOut",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantCommunityOut_TenantCommunityID_AttachmentSetID",
                table: "VpnTenantCommunityOut",
                columns: new[] { "TenantCommunityID", "AttachmentSetID" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantCommunityOut_AttachmentSet_AttachmentSetID",
                table: "VpnTenantCommunityOut",
                column: "AttachmentSetID",
                principalTable: "AttachmentSet",
                principalColumn: "AttachmentSetID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
