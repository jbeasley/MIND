using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SigmaCloudManager.Migrations
{
    public partial class update3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantNetworkIn_TenantNetwork_TenantNetworkID",
                table: "VpnTenantNetworkIn");

            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantNetworkOut_TenantNetwork_TenantNetworkID",
                table: "VpnTenantNetworkOut");

            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantNetworkRoutingInstance_TenantNetwork_TenantNetworkID",
                table: "VpnTenantNetworkRoutingInstance");

            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantNetworkStaticRouteRoutingInstance_TenantNetwork_TenantNetworkID",
                table: "VpnTenantNetworkStaticRouteRoutingInstance");

            migrationBuilder.DropTable(
                name: "TenantNetwork");

            migrationBuilder.AddColumn<int>(
                name: "TenantIpNetworkID",
                table: "VpnTenantNetworkStaticRouteRoutingInstance",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantIpNetworkID",
                table: "VpnTenantNetworkRoutingInstance",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantIpNetworkID",
                table: "VpnTenantNetworkOut",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantIpNetworkID",
                table: "VpnTenantNetworkIn",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TenantIpNetwork",
                columns: table => new
                {
                    TenantIpNetworkID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AllowExtranet = table.Column<bool>(nullable: false),
                    Ipv4Length = table.Column<int>(nullable: false),
                    Ipv4LessThanOrEqualToLength = table.Column<int>(nullable: true),
                    Ipv4Prefix = table.Column<string>(maxLength: 15, nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TenantID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantIpNetwork", x => x.TenantIpNetworkID);
                    table.ForeignKey(
                        name: "FK_TenantIpNetwork_Tenant_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Tenant",
                        principalColumn: "TenantID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantNetworkStaticRouteRoutingInstance_TenantIpNetworkID",
                table: "VpnTenantNetworkStaticRouteRoutingInstance",
                column: "TenantIpNetworkID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantNetworkRoutingInstance_TenantIpNetworkID",
                table: "VpnTenantNetworkRoutingInstance",
                column: "TenantIpNetworkID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantNetworkOut_TenantIpNetworkID",
                table: "VpnTenantNetworkOut",
                column: "TenantIpNetworkID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantNetworkIn_TenantIpNetworkID",
                table: "VpnTenantNetworkIn",
                column: "TenantIpNetworkID");

            migrationBuilder.CreateIndex(
                name: "IX_TenantIpNetwork_TenantID",
                table: "TenantIpNetwork",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_TenantIpNetwork_Ipv4Prefix_Ipv4Length",
                table: "TenantIpNetwork",
                columns: new[] { "Ipv4Prefix", "Ipv4Length" },
                unique: true,
                filter: "[Ipv4Prefix] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantNetworkIn_TenantIpNetwork_TenantIpNetworkID",
                table: "VpnTenantNetworkIn",
                column: "TenantIpNetworkID",
                principalTable: "TenantIpNetwork",
                principalColumn: "TenantIpNetworkID",
                onDelete: ReferentialAction.Restrict);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantNetworkIn_TenantIpNetwork_TenantIpNetworkID",
                table: "VpnTenantNetworkIn");

            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantNetworkOut_TenantIpNetwork_TenantIpNetworkID",
                table: "VpnTenantNetworkOut");

            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantNetworkRoutingInstance_TenantIpNetwork_TenantIpNetworkID",
                table: "VpnTenantNetworkRoutingInstance");

            migrationBuilder.DropForeignKey(
                name: "FK_VpnTenantNetworkStaticRouteRoutingInstance_TenantIpNetwork_TenantIpNetworkID",
                table: "VpnTenantNetworkStaticRouteRoutingInstance");

            migrationBuilder.DropTable(
                name: "TenantIpNetwork");

            migrationBuilder.DropIndex(
                name: "IX_VpnTenantNetworkStaticRouteRoutingInstance_TenantIpNetworkID",
                table: "VpnTenantNetworkStaticRouteRoutingInstance");

            migrationBuilder.DropIndex(
                name: "IX_VpnTenantNetworkRoutingInstance_TenantIpNetworkID",
                table: "VpnTenantNetworkRoutingInstance");

            migrationBuilder.DropIndex(
                name: "IX_VpnTenantNetworkOut_TenantIpNetworkID",
                table: "VpnTenantNetworkOut");

            migrationBuilder.DropIndex(
                name: "IX_VpnTenantNetworkIn_TenantIpNetworkID",
                table: "VpnTenantNetworkIn");

            migrationBuilder.DropColumn(
                name: "TenantIpNetworkID",
                table: "VpnTenantNetworkStaticRouteRoutingInstance");

            migrationBuilder.DropColumn(
                name: "TenantIpNetworkID",
                table: "VpnTenantNetworkRoutingInstance");

            migrationBuilder.DropColumn(
                name: "TenantIpNetworkID",
                table: "VpnTenantNetworkOut");

            migrationBuilder.DropColumn(
                name: "TenantIpNetworkID",
                table: "VpnTenantNetworkIn");

            migrationBuilder.CreateTable(
                name: "TenantNetwork",
                columns: table => new
                {
                    TenantNetworkID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AllowExtranet = table.Column<bool>(nullable: false),
                    IpPrefix = table.Column<string>(maxLength: 15, nullable: true),
                    Length = table.Column<int>(nullable: false),
                    LessThanOrEqualToLength = table.Column<int>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TenantID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantNetwork", x => x.TenantNetworkID);
                    table.ForeignKey(
                        name: "FK_TenantNetwork_Tenant_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Tenant",
                        principalColumn: "TenantID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TenantNetwork_TenantID",
                table: "TenantNetwork",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_TenantNetwork_IpPrefix_Length",
                table: "TenantNetwork",
                columns: new[] { "IpPrefix", "Length" },
                unique: true,
                filter: "[IpPrefix] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantNetworkIn_TenantNetwork_TenantNetworkID",
                table: "VpnTenantNetworkIn",
                column: "TenantNetworkID",
                principalTable: "TenantNetwork",
                principalColumn: "TenantNetworkID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantNetworkOut_TenantNetwork_TenantNetworkID",
                table: "VpnTenantNetworkOut",
                column: "TenantNetworkID",
                principalTable: "TenantNetwork",
                principalColumn: "TenantNetworkID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantNetworkRoutingInstance_TenantNetwork_TenantNetworkID",
                table: "VpnTenantNetworkRoutingInstance",
                column: "TenantNetworkID",
                principalTable: "TenantNetwork",
                principalColumn: "TenantNetworkID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VpnTenantNetworkStaticRouteRoutingInstance_TenantNetwork_TenantNetworkID",
                table: "VpnTenantNetworkStaticRouteRoutingInstance",
                column: "TenantNetworkID",
                principalTable: "TenantNetwork",
                principalColumn: "TenantNetworkID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
