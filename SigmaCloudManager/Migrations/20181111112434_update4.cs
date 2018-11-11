using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Mind.Migrations
{
    public partial class update4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantIpNetworkOut_BgpPeer_BgpPeerID",
                table: "VpnTenantIpNetworkOut");

            migrationBuilder.AlterColumn<int>(
                name: "BgpPeerID",
                table: "VpnTenantIpNetworkOut",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<bool>(
                name: "AddToAllBgpPeersInAttachmentSet",
                table: "VpnTenantIpNetworkOut",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantIpNetworkOut_BgpPeer_BgpPeerID",
                table: "VpnTenantIpNetworkOut",
                column: "BgpPeerID",
                principalTable: "BgpPeer",
                principalColumn: "BgpPeerID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantIpNetworkOut_BgpPeer_BgpPeerID",
                table: "VpnTenantIpNetworkOut");

            migrationBuilder.DropColumn(
                name: "AddToAllBgpPeersInAttachmentSet",
                table: "VpnTenantIpNetworkOut");

            migrationBuilder.AlterColumn<int>(
                name: "BgpPeerID",
                table: "VpnTenantIpNetworkOut",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantIpNetworkOut_BgpPeer_BgpPeerID",
                table: "VpnTenantIpNetworkOut",
                column: "BgpPeerID",
                principalTable: "BgpPeer",
                principalColumn: "BgpPeerID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
