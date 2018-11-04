using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Mind.Migrations
{
    public partial class update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_Mtu_MtuID1",
                table: "Attachment");

            migrationBuilder.DropForeignKey(
                name: "FK_Vif_Mtu_MtuID1",
                table: "Vif");

            migrationBuilder.DropForeignKey(
                name: "FK_Vif_VifRole_VifRoleID1",
                table: "Vif");

            migrationBuilder.DropForeignKey(
                name: "FK_Vpn_Tenant_TenantID1",
                table: "Vpn");

            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantCommunityIn_TenantCommunity_TenantCommunityID1",
                table: "VpnTenantCommunityIn");

            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantCommunityOut_TenantCommunity_TenantCommunityID1",
                table: "VpnTenantCommunityOut");

            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantCommunityRoutingInstance_TenantCommunity_TenantCommunityID1",
                table: "VpnTenantCommunityRoutingInstance");

            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantIpNetworkCommunityIn_TenantCommunity_TenantCommunityID1",
                table: "VpnTenantIpNetworkCommunityIn");

            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantIpNetworkIn_AttachmentSet_AttachmentSetID1",
                table: "VpnTenantIpNetworkIn");

            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantIpNetworkOut_AttachmentSet_AttachmentSetID1",
                table: "VpnTenantIpNetworkOut");

            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantIpNetworkOut_TenantIpNetwork_TenantIpNetworkID1",
                table: "VpnTenantIpNetworkOut");

            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantIpNetworkRoutingInstance_TenantIpNetwork_TenantIpNetworkID1",
                table: "VpnTenantIpNetworkRoutingInstance");

            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantIpNetworkRoutingInstanceStaticRoute_TenantIpNetwork_TenantIpNetworkID1",
                table: "VpnTenantIpNetworkRoutingInstanceStaticRoute");

            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantMulticastGroup_TenantMulticastGroup_TenantMulticastGroupID1",
                table: "VpnTenantMulticastGroup");

            migrationBuilder.DropIndex(
                name: "IX_VpnTenantMulticastGroup_TenantMulticastGroupID1",
                table: "VpnTenantMulticastGroup");

            migrationBuilder.DropIndex(
                name: "IX_VpnTenantIpNetworkRoutingInstanceStaticRoute_TenantIpNetworkID1",
                table: "VpnTenantIpNetworkRoutingInstanceStaticRoute");

            migrationBuilder.DropIndex(
                name: "IX_VpnTenantIpNetworkRoutingInstance_TenantIpNetworkID1",
                table: "VpnTenantIpNetworkRoutingInstance");

            migrationBuilder.DropIndex(
                name: "IX_VpnTenantIpNetworkOut_AttachmentSetID1",
                table: "VpnTenantIpNetworkOut");

            migrationBuilder.DropIndex(
                name: "IX_VpnTenantIpNetworkOut_TenantIpNetworkID1",
                table: "VpnTenantIpNetworkOut");

            migrationBuilder.DropIndex(
                name: "IX_VpnTenantIpNetworkIn_AttachmentSetID1",
                table: "VpnTenantIpNetworkIn");

            migrationBuilder.DropIndex(
                name: "IX_VpnTenantIpNetworkCommunityIn_TenantCommunityID1",
                table: "VpnTenantIpNetworkCommunityIn");

            migrationBuilder.DropIndex(
                name: "IX_VpnTenantCommunityRoutingInstance_TenantCommunityID1",
                table: "VpnTenantCommunityRoutingInstance");

            migrationBuilder.DropIndex(
                name: "IX_VpnTenantCommunityOut_TenantCommunityID1",
                table: "VpnTenantCommunityOut");

            migrationBuilder.DropIndex(
                name: "IX_VpnTenantCommunityIn_TenantCommunityID1",
                table: "VpnTenantCommunityIn");

            migrationBuilder.DropIndex(
                name: "IX_Vpn_TenantID1",
                table: "Vpn");

            migrationBuilder.DropIndex(
                name: "IX_Vif_MtuID1",
                table: "Vif");

            migrationBuilder.DropIndex(
                name: "IX_Vif_VifRoleID1",
                table: "Vif");

            migrationBuilder.DropIndex(
                name: "IX_Attachment_MtuID1",
                table: "Attachment");

            migrationBuilder.DropColumn(
                name: "TenantMulticastGroupID1",
                table: "VpnTenantMulticastGroup");

            migrationBuilder.DropColumn(
                name: "TenantIpNetworkID1",
                table: "VpnTenantIpNetworkRoutingInstanceStaticRoute");

            migrationBuilder.DropColumn(
                name: "TenantIpNetworkID1",
                table: "VpnTenantIpNetworkRoutingInstance");

            migrationBuilder.DropColumn(
                name: "AttachmentSetID1",
                table: "VpnTenantIpNetworkOut");

            migrationBuilder.DropColumn(
                name: "TenantIpNetworkID1",
                table: "VpnTenantIpNetworkOut");

            migrationBuilder.DropColumn(
                name: "AttachmentSetID1",
                table: "VpnTenantIpNetworkIn");

            migrationBuilder.DropColumn(
                name: "TenantCommunityID1",
                table: "VpnTenantIpNetworkCommunityIn");

            migrationBuilder.DropColumn(
                name: "TenantCommunityID1",
                table: "VpnTenantCommunityRoutingInstance");

            migrationBuilder.DropColumn(
                name: "TenantCommunityID1",
                table: "VpnTenantCommunityOut");

            migrationBuilder.DropColumn(
                name: "TenantCommunityID1",
                table: "VpnTenantCommunityIn");

            migrationBuilder.DropColumn(
                name: "TenantID1",
                table: "Vpn");

            migrationBuilder.DropColumn(
                name: "MtuID1",
                table: "Vif");

            migrationBuilder.DropColumn(
                name: "VifRoleID1",
                table: "Vif");

            migrationBuilder.DropColumn(
                name: "MtuID1",
                table: "Attachment");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TenantMulticastGroupID1",
                table: "VpnTenantMulticastGroup",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantIpNetworkID1",
                table: "VpnTenantIpNetworkRoutingInstanceStaticRoute",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantIpNetworkID1",
                table: "VpnTenantIpNetworkRoutingInstance",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AttachmentSetID1",
                table: "VpnTenantIpNetworkOut",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantIpNetworkID1",
                table: "VpnTenantIpNetworkOut",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AttachmentSetID1",
                table: "VpnTenantIpNetworkIn",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantCommunityID1",
                table: "VpnTenantIpNetworkCommunityIn",
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

            migrationBuilder.AddColumn<int>(
                name: "TenantID1",
                table: "Vpn",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MtuID1",
                table: "Vif",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VifRoleID1",
                table: "Vif",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MtuID1",
                table: "Attachment",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantMulticastGroup_TenantMulticastGroupID1",
                table: "VpnTenantMulticastGroup",
                column: "TenantMulticastGroupID1");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkRoutingInstanceStaticRoute_TenantIpNetworkID1",
                table: "VpnTenantIpNetworkRoutingInstanceStaticRoute",
                column: "TenantIpNetworkID1");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkRoutingInstance_TenantIpNetworkID1",
                table: "VpnTenantIpNetworkRoutingInstance",
                column: "TenantIpNetworkID1");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkOut_AttachmentSetID1",
                table: "VpnTenantIpNetworkOut",
                column: "AttachmentSetID1");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkOut_TenantIpNetworkID1",
                table: "VpnTenantIpNetworkOut",
                column: "TenantIpNetworkID1");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkIn_AttachmentSetID1",
                table: "VpnTenantIpNetworkIn",
                column: "AttachmentSetID1");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkCommunityIn_TenantCommunityID1",
                table: "VpnTenantIpNetworkCommunityIn",
                column: "TenantCommunityID1");

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

            migrationBuilder.CreateIndex(
                name: "IX_Vpn_TenantID1",
                table: "Vpn",
                column: "TenantID1");

            migrationBuilder.CreateIndex(
                name: "IX_Vif_MtuID1",
                table: "Vif",
                column: "MtuID1");

            migrationBuilder.CreateIndex(
                name: "IX_Vif_VifRoleID1",
                table: "Vif",
                column: "VifRoleID1");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_MtuID1",
                table: "Attachment",
                column: "MtuID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_Mtu_MtuID1",
                table: "Attachment",
                column: "MtuID1",
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

            migrationBuilder.AddForeignKey(
                name: "FK_Vif_VifRole_VifRoleID1",
                table: "Vif",
                column: "VifRoleID1",
                principalTable: "VifRole",
                principalColumn: "VifRoleID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vpn_Tenant_TenantID1",
                table: "Vpn",
                column: "TenantID1",
                principalTable: "Tenant",
                principalColumn: "TenantID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantCommunityIn_TenantCommunity_TenantCommunityID1",
                table: "VpnTenantCommunityIn",
                column: "TenantCommunityID1",
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
                name: "FK_VpnTenantCommunityRoutingInstance_TenantCommunity_TenantCommunityID1",
                table: "VpnTenantCommunityRoutingInstance",
                column: "TenantCommunityID1",
                principalTable: "TenantCommunity",
                principalColumn: "TenantCommunityID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantIpNetworkCommunityIn_TenantCommunity_TenantCommunityID1",
                table: "VpnTenantIpNetworkCommunityIn",
                column: "TenantCommunityID1",
                principalTable: "TenantCommunity",
                principalColumn: "TenantCommunityID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantIpNetworkIn_AttachmentSet_AttachmentSetID1",
                table: "VpnTenantIpNetworkIn",
                column: "AttachmentSetID1",
                principalTable: "AttachmentSet",
                principalColumn: "AttachmentSetID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantIpNetworkOut_AttachmentSet_AttachmentSetID1",
                table: "VpnTenantIpNetworkOut",
                column: "AttachmentSetID1",
                principalTable: "AttachmentSet",
                principalColumn: "AttachmentSetID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantIpNetworkOut_TenantIpNetwork_TenantIpNetworkID1",
                table: "VpnTenantIpNetworkOut",
                column: "TenantIpNetworkID1",
                principalTable: "TenantIpNetwork",
                principalColumn: "TenantIpNetworkID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantIpNetworkRoutingInstance_TenantIpNetwork_TenantIpNetworkID1",
                table: "VpnTenantIpNetworkRoutingInstance",
                column: "TenantIpNetworkID1",
                principalTable: "TenantIpNetwork",
                principalColumn: "TenantIpNetworkID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantIpNetworkRoutingInstanceStaticRoute_TenantIpNetwork_TenantIpNetworkID1",
                table: "VpnTenantIpNetworkRoutingInstanceStaticRoute",
                column: "TenantIpNetworkID1",
                principalTable: "TenantIpNetwork",
                principalColumn: "TenantIpNetworkID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantMulticastGroup_TenantMulticastGroup_TenantMulticastGroupID1",
                table: "VpnTenantMulticastGroup",
                column: "TenantMulticastGroupID1",
                principalTable: "TenantMulticastGroup",
                principalColumn: "TenantMulticastGroupID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
