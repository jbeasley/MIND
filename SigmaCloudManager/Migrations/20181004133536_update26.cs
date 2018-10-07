using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SigmaCloudManager.Migrations
{
    public partial class update26 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantCommunityIn_AttachmentSet_AttachmentSetID",
                table: "VpnTenantCommunityIn");

            migrationBuilder.DropIndex(
                name: "IX_VpnTenantCommunityIn_TenantCommunityID_AttachmentSetID",
                table: "VpnTenantCommunityIn");

            migrationBuilder.AlterColumn<int>(
                name: "AttachmentSetID",
                table: "VpnTenantCommunityIn",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "IpRoutingBehaviour",
                table: "TenantCommunity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantCommunityIn_TenantCommunityID_AttachmentSetID",
                table: "VpnTenantCommunityIn",
                columns: new[] { "TenantCommunityID", "AttachmentSetID" },
                unique: true,
                filter: "[AttachmentSetID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantCommunityIn_AttachmentSet_AttachmentSetID",
                table: "VpnTenantCommunityIn",
                column: "AttachmentSetID",
                principalTable: "AttachmentSet",
                principalColumn: "AttachmentSetID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantCommunityIn_AttachmentSet_AttachmentSetID",
                table: "VpnTenantCommunityIn");

            migrationBuilder.DropIndex(
                name: "IX_VpnTenantCommunityIn_TenantCommunityID_AttachmentSetID",
                table: "VpnTenantCommunityIn");

            migrationBuilder.DropColumn(
                name: "IpRoutingBehaviour",
                table: "TenantCommunity");

            migrationBuilder.AlterColumn<int>(
                name: "AttachmentSetID",
                table: "VpnTenantCommunityIn",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantCommunityIn_TenantCommunityID_AttachmentSetID",
                table: "VpnTenantCommunityIn",
                columns: new[] { "TenantCommunityID", "AttachmentSetID" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantCommunityIn_AttachmentSet_AttachmentSetID",
                table: "VpnTenantCommunityIn",
                column: "AttachmentSetID",
                principalTable: "AttachmentSet",
                principalColumn: "AttachmentSetID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
