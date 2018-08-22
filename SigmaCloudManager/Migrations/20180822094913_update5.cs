using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SigmaCloudManager.Migrations
{
    public partial class update5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttachmentSet_Tenant_TenantID",
                table: "AttachmentSet");

            migrationBuilder.DropForeignKey(
                name: "FK_AttachmentSet_Tenant_TenantID1",
                table: "AttachmentSet");

            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantCommunityIn_TenantCommunity_TenantCommunityID",
                table: "VpnTenantCommunityIn");

            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantCommunityOut_TenantCommunity_TenantCommunityID",
                table: "VpnTenantCommunityOut");

            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantCommunityRoutingInstance_TenantCommunity_TenantCommunityID",
                table: "VpnTenantCommunityRoutingInstance");

            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantIpNetworkIn_TenantIpNetwork_TenantIpNetworkID",
                table: "VpnTenantIpNetworkIn");

            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantMulticastGroup_TenantMulticastGroup_TenantMulticastGroupID",
                table: "VpnTenantMulticastGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantNetworkOut_TenantIpNetwork_TenantIpNetworkID",
                table: "VpnTenantNetworkOut");

            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantNetworkRoutingInstance_TenantIpNetwork_TenantIpNetworkID",
                table: "VpnTenantNetworkRoutingInstance");

            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantNetworkStaticRouteRoutingInstance_TenantIpNetwork_TenantIpNetworkID",
                table: "VpnTenantNetworkStaticRouteRoutingInstance");

            migrationBuilder.DropIndex(
                name: "IX_AttachmentSet_TenantID1",
                table: "AttachmentSet");

            migrationBuilder.DropColumn(
                name: "TenantID1",
                table: "AttachmentSet");

            migrationBuilder.AddColumn<int>(
                name: "TenantIpNetworkID1",
                table: "VpnTenantNetworkRoutingInstance",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantIpNetworkID1",
                table: "VpnTenantNetworkOut",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantMulticastGroupID1",
                table: "VpnTenantMulticastGroup",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantIpNetworkID1",
                table: "VpnTenantIpNetworkIn",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantCommunityID1",
                table: "VpnTenantCommunityRoutingInstance",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantCommunityID1",
                table: "VpnTenantCommunityOut",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantCommunityID1",
                table: "VpnTenantCommunityIn",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantNetworkRoutingInstance_TenantIpNetworkID1",
                table: "VpnTenantNetworkRoutingInstance",
                column: "TenantIpNetworkID1");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantNetworkOut_TenantIpNetworkID1",
                table: "VpnTenantNetworkOut",
                column: "TenantIpNetworkID1");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantMulticastGroup_TenantMulticastGroupID1",
                table: "VpnTenantMulticastGroup",
                column: "TenantMulticastGroupID1");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkIn_TenantIpNetworkID1",
                table: "VpnTenantIpNetworkIn",
                column: "TenantIpNetworkID1");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantCommunityRoutingInstance_TenantCommunityID1",
                table: "VpnTenantCommunityRoutingInstance",
                column: "TenantCommunityID1");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantCommunityOut_TenantCommunityID1",
                table: "VpnTenantCommunityOut",
                column: "TenantCommunityID1");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantCommunityIn_TenantCommunityID1",
                table: "VpnTenantCommunityIn",
                column: "TenantCommunityID1");

            migrationBuilder.AddForeignKey(
                name: "FK_AttachmentSet_Tenant_TenantID",
                table: "AttachmentSet",
                column: "TenantID",
                principalTable: "Tenant",
                principalColumn: "TenantID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantCommunityIn_TenantCommunity_TenantCommunityID",
                table: "VpnTenantCommunityIn",
                column: "TenantCommunityID",
                principalTable: "TenantCommunity",
                principalColumn: "TenantCommunityID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantCommunityIn_TenantCommunity_TenantCommunityID1",
                table: "VpnTenantCommunityIn",
                column: "TenantCommunityID1",
                principalTable: "TenantCommunity",
                principalColumn: "TenantCommunityID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantCommunityOut_TenantCommunity_TenantCommunityID",
                table: "VpnTenantCommunityOut",
                column: "TenantCommunityID",
                principalTable: "TenantCommunity",
                principalColumn: "TenantCommunityID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantCommunityOut_TenantCommunity_TenantCommunityID1",
                table: "VpnTenantCommunityOut",
                column: "TenantCommunityID1",
                principalTable: "TenantCommunity",
                principalColumn: "TenantCommunityID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantCommunityRoutingInstance_TenantCommunity_TenantCommunityID",
                table: "VpnTenantCommunityRoutingInstance",
                column: "TenantCommunityID",
                principalTable: "TenantCommunity",
                principalColumn: "TenantCommunityID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantCommunityRoutingInstance_TenantCommunity_TenantCommunityID1",
                table: "VpnTenantCommunityRoutingInstance",
                column: "TenantCommunityID1",
                principalTable: "TenantCommunity",
                principalColumn: "TenantCommunityID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantIpNetworkIn_TenantIpNetwork_TenantIpNetworkID",
                table: "VpnTenantIpNetworkIn",
                column: "TenantIpNetworkID",
                principalTable: "TenantIpNetwork",
                principalColumn: "TenantIpNetworkID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantIpNetworkIn_TenantIpNetwork_TenantIpNetworkID1",
                table: "VpnTenantIpNetworkIn",
                column: "TenantIpNetworkID1",
                principalTable: "TenantIpNetwork",
                principalColumn: "TenantIpNetworkID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantMulticastGroup_TenantMulticastGroup_TenantMulticastGroupID",
                table: "VpnTenantMulticastGroup",
                column: "TenantMulticastGroupID",
                principalTable: "TenantMulticastGroup",
                principalColumn: "TenantMulticastGroupID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantMulticastGroup_TenantMulticastGroup_TenantMulticastGroupID1",
                table: "VpnTenantMulticastGroup",
                column: "TenantMulticastGroupID1",
                principalTable: "TenantMulticastGroup",
                principalColumn: "TenantMulticastGroupID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantNetworkOut_TenantIpNetwork_TenantIpNetworkID",
                table: "VpnTenantNetworkOut",
                column: "TenantIpNetworkID",
                principalTable: "TenantIpNetwork",
                principalColumn: "TenantIpNetworkID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantNetworkOut_TenantIpNetwork_TenantIpNetworkID1",
                table: "VpnTenantNetworkOut",
                column: "TenantIpNetworkID1",
                principalTable: "TenantIpNetwork",
                principalColumn: "TenantIpNetworkID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantNetworkRoutingInstance_TenantIpNetwork_TenantIpNetworkID",
                table: "VpnTenantNetworkRoutingInstance",
                column: "TenantIpNetworkID",
                principalTable: "TenantIpNetwork",
                principalColumn: "TenantIpNetworkID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantNetworkRoutingInstance_TenantIpNetwork_TenantIpNetworkID1",
                table: "VpnTenantNetworkRoutingInstance",
                column: "TenantIpNetworkID1",
                principalTable: "TenantIpNetwork",
                principalColumn: "TenantIpNetworkID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantNetworkStaticRouteRoutingInstance_TenantIpNetwork_TenantIpNetworkID",
                table: "VpnTenantNetworkStaticRouteRoutingInstance",
                column: "TenantIpNetworkID",
                principalTable: "TenantIpNetwork",
                principalColumn: "TenantIpNetworkID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttachmentSet_Tenant_TenantID",
                table: "AttachmentSet");

            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantCommunityIn_TenantCommunity_TenantCommunityID",
                table: "VpnTenantCommunityIn");

            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantCommunityIn_TenantCommunity_TenantCommunityID1",
                table: "VpnTenantCommunityIn");

            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantCommunityOut_TenantCommunity_TenantCommunityID",
                table: "VpnTenantCommunityOut");

            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantCommunityOut_TenantCommunity_TenantCommunityID1",
                table: "VpnTenantCommunityOut");

            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantCommunityRoutingInstance_TenantCommunity_TenantCommunityID",
                table: "VpnTenantCommunityRoutingInstance");

            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantCommunityRoutingInstance_TenantCommunity_TenantCommunityID1",
                table: "VpnTenantCommunityRoutingInstance");

            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantIpNetworkIn_TenantIpNetwork_TenantIpNetworkID",
                table: "VpnTenantIpNetworkIn");

            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantIpNetworkIn_TenantIpNetwork_TenantIpNetworkID1",
                table: "VpnTenantIpNetworkIn");

            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantMulticastGroup_TenantMulticastGroup_TenantMulticastGroupID",
                table: "VpnTenantMulticastGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantMulticastGroup_TenantMulticastGroup_TenantMulticastGroupID1",
                table: "VpnTenantMulticastGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantNetworkOut_TenantIpNetwork_TenantIpNetworkID",
                table: "VpnTenantNetworkOut");

            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantNetworkOut_TenantIpNetwork_TenantIpNetworkID1",
                table: "VpnTenantNetworkOut");

            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantNetworkRoutingInstance_TenantIpNetwork_TenantIpNetworkID",
                table: "VpnTenantNetworkRoutingInstance");

            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantNetworkRoutingInstance_TenantIpNetwork_TenantIpNetworkID1",
                table: "VpnTenantNetworkRoutingInstance");

            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantNetworkStaticRouteRoutingInstance_TenantIpNetwork_TenantIpNetworkID",
                table: "VpnTenantNetworkStaticRouteRoutingInstance");

            migrationBuilder.DropIndex(
                name: "IX_VpnTenantNetworkRoutingInstance_TenantIpNetworkID1",
                table: "VpnTenantNetworkRoutingInstance");

            migrationBuilder.DropIndex(
                name: "IX_VpnTenantNetworkOut_TenantIpNetworkID1",
                table: "VpnTenantNetworkOut");

            migrationBuilder.DropIndex(
                name: "IX_VpnTenantMulticastGroup_TenantMulticastGroupID1",
                table: "VpnTenantMulticastGroup");

            migrationBuilder.DropIndex(
                name: "IX_VpnTenantIpNetworkIn_TenantIpNetworkID1",
                table: "VpnTenantIpNetworkIn");

            migrationBuilder.DropIndex(
                name: "IX_VpnTenantCommunityRoutingInstance_TenantCommunityID1",
                table: "VpnTenantCommunityRoutingInstance");

            migrationBuilder.DropIndex(
                name: "IX_VpnTenantCommunityOut_TenantCommunityID1",
                table: "VpnTenantCommunityOut");

            migrationBuilder.DropIndex(
                name: "IX_VpnTenantCommunityIn_TenantCommunityID1",
                table: "VpnTenantCommunityIn");

            migrationBuilder.DropColumn(
                name: "TenantIpNetworkID1",
                table: "VpnTenantNetworkRoutingInstance");

            migrationBuilder.DropColumn(
                name: "TenantIpNetworkID1",
                table: "VpnTenantNetworkOut");

            migrationBuilder.DropColumn(
                name: "TenantMulticastGroupID1",
                table: "VpnTenantMulticastGroup");

            migrationBuilder.DropColumn(
                name: "TenantIpNetworkID1",
                table: "VpnTenantIpNetworkIn");

            migrationBuilder.DropColumn(
                name: "TenantCommunityID1",
                table: "VpnTenantCommunityRoutingInstance");

            migrationBuilder.DropColumn(
                name: "TenantCommunityID1",
                table: "VpnTenantCommunityOut");

            migrationBuilder.DropColumn(
                name: "TenantCommunityID1",
                table: "VpnTenantCommunityIn");

            migrationBuilder.AddColumn<int>(
                name: "TenantID1",
                table: "AttachmentSet",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AttachmentSet_TenantID1",
                table: "AttachmentSet",
                column: "TenantID1");

            migrationBuilder.AddForeignKey(
                name: "FK_AttachmentSet_Tenant_TenantID",
                table: "AttachmentSet",
                column: "TenantID",
                principalTable: "Tenant",
                principalColumn: "TenantID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AttachmentSet_Tenant_TenantID1",
                table: "AttachmentSet",
                column: "TenantID1",
                principalTable: "Tenant",
                principalColumn: "TenantID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantCommunityIn_TenantCommunity_TenantCommunityID",
                table: "VpnTenantCommunityIn",
                column: "TenantCommunityID",
                principalTable: "TenantCommunity",
                principalColumn: "TenantCommunityID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantCommunityOut_TenantCommunity_TenantCommunityID",
                table: "VpnTenantCommunityOut",
                column: "TenantCommunityID",
                principalTable: "TenantCommunity",
                principalColumn: "TenantCommunityID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantCommunityRoutingInstance_TenantCommunity_TenantCommunityID",
                table: "VpnTenantCommunityRoutingInstance",
                column: "TenantCommunityID",
                principalTable: "TenantCommunity",
                principalColumn: "TenantCommunityID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantIpNetworkIn_TenantIpNetwork_TenantIpNetworkID",
                table: "VpnTenantIpNetworkIn",
                column: "TenantIpNetworkID",
                principalTable: "TenantIpNetwork",
                principalColumn: "TenantIpNetworkID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantMulticastGroup_TenantMulticastGroup_TenantMulticastGroupID",
                table: "VpnTenantMulticastGroup",
                column: "TenantMulticastGroupID",
                principalTable: "TenantMulticastGroup",
                principalColumn: "TenantMulticastGroupID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantNetworkOut_TenantIpNetwork_TenantIpNetworkID",
                table: "VpnTenantNetworkOut",
                column: "TenantIpNetworkID",
                principalTable: "TenantIpNetwork",
                principalColumn: "TenantIpNetworkID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantNetworkRoutingInstance_TenantIpNetwork_TenantIpNetworkID",
                table: "VpnTenantNetworkRoutingInstance",
                column: "TenantIpNetworkID",
                principalTable: "TenantIpNetwork",
                principalColumn: "TenantIpNetworkID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantNetworkStaticRouteRoutingInstance_TenantIpNetwork_TenantIpNetworkID",
                table: "VpnTenantNetworkStaticRouteRoutingInstance",
                column: "TenantIpNetworkID",
                principalTable: "TenantIpNetwork",
                principalColumn: "TenantIpNetworkID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
