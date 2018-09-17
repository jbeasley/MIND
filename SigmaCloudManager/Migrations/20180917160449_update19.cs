using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SigmaCloudManager.Migrations
{
    public partial class update19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VpnTenantIpNetworkStaticRouteRoutingInstance");

            migrationBuilder.CreateTable(
                name: "VpnTenantIpNetworkRoutingInstanceStaticRoute",
                columns: table => new
                {
                    VpnTenantIpNetworkRoutingInstanceStaticRouteID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddToAllRoutingInstancesInAttachmentSet = table.Column<bool>(nullable: false),
                    AttachmentSetID = table.Column<int>(nullable: false),
                    Ipv4NextHopAddress = table.Column<string>(nullable: true),
                    IsBfdEnabled = table.Column<bool>(nullable: false),
                    RoutingInstanceID = table.Column<int>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TenantIpNetworkID = table.Column<int>(nullable: false),
                    TenantIpNetworkID1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VpnTenantIpNetworkRoutingInstanceStaticRoute", x => x.VpnTenantIpNetworkRoutingInstanceStaticRouteID);
                    table.ForeignKey(
                        name: "FK_VpnTenantIpNetworkRoutingInstanceStaticRoute_AttachmentSet_AttachmentSetID",
                        column: x => x.AttachmentSetID,
                        principalTable: "AttachmentSet",
                        principalColumn: "AttachmentSetID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VpnTenantIpNetworkRoutingInstanceStaticRoute_RoutingInstance_RoutingInstanceID",
                        column: x => x.RoutingInstanceID,
                        principalTable: "RoutingInstance",
                        principalColumn: "RoutingInstanceID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VpnTenantIpNetworkRoutingInstanceStaticRoute_TenantIpNetwork_TenantIpNetworkID",
                        column: x => x.TenantIpNetworkID,
                        principalTable: "TenantIpNetwork",
                        principalColumn: "TenantIpNetworkID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VpnTenantIpNetworkRoutingInstanceStaticRoute_TenantIpNetwork_TenantIpNetworkID1",
                        column: x => x.TenantIpNetworkID1,
                        principalTable: "TenantIpNetwork",
                        principalColumn: "TenantIpNetworkID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkRoutingInstanceStaticRoute_AttachmentSetID",
                table: "VpnTenantIpNetworkRoutingInstanceStaticRoute",
                column: "AttachmentSetID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkRoutingInstanceStaticRoute_RoutingInstanceID",
                table: "VpnTenantIpNetworkRoutingInstanceStaticRoute",
                column: "RoutingInstanceID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkRoutingInstanceStaticRoute_TenantIpNetworkID1",
                table: "VpnTenantIpNetworkRoutingInstanceStaticRoute",
                column: "TenantIpNetworkID1");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkRoutingInstanceStaticRoute_TenantIpNetworkID_AttachmentSetID",
                table: "VpnTenantIpNetworkRoutingInstanceStaticRoute",
                columns: new[] { "TenantIpNetworkID", "AttachmentSetID" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VpnTenantIpNetworkRoutingInstanceStaticRoute");

            migrationBuilder.CreateTable(
                name: "VpnTenantIpNetworkStaticRouteRoutingInstance",
                columns: table => new
                {
                    VpnTenantIpNetworkStaticRouteRoutingInstanceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddToAllRoutingInstancesInAttachmentSet = table.Column<bool>(nullable: false),
                    AttachmentSetID = table.Column<int>(nullable: false),
                    Ipv4NextHopAddress = table.Column<string>(nullable: true),
                    IsBfdEnabled = table.Column<bool>(nullable: false),
                    RoutingInstanceID = table.Column<int>(nullable: true),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    TenantIpNetworkID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VpnTenantIpNetworkStaticRouteRoutingInstance", x => x.VpnTenantIpNetworkStaticRouteRoutingInstanceID);
                    table.ForeignKey(
                        name: "FK_VpnTenantIpNetworkStaticRouteRoutingInstance_AttachmentSet_AttachmentSetID",
                        column: x => x.AttachmentSetID,
                        principalTable: "AttachmentSet",
                        principalColumn: "AttachmentSetID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VpnTenantIpNetworkStaticRouteRoutingInstance_RoutingInstance_RoutingInstanceID",
                        column: x => x.RoutingInstanceID,
                        principalTable: "RoutingInstance",
                        principalColumn: "RoutingInstanceID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VpnTenantIpNetworkStaticRouteRoutingInstance_TenantIpNetwork_TenantIpNetworkID",
                        column: x => x.TenantIpNetworkID,
                        principalTable: "TenantIpNetwork",
                        principalColumn: "TenantIpNetworkID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkStaticRouteRoutingInstance_AttachmentSetID",
                table: "VpnTenantIpNetworkStaticRouteRoutingInstance",
                column: "AttachmentSetID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkStaticRouteRoutingInstance_RoutingInstanceID",
                table: "VpnTenantIpNetworkStaticRouteRoutingInstance",
                column: "RoutingInstanceID");

            migrationBuilder.CreateIndex(
                name: "IX_VpnTenantIpNetworkStaticRouteRoutingInstance_TenantIpNetworkID_AttachmentSetID",
                table: "VpnTenantIpNetworkStaticRouteRoutingInstance",
                columns: new[] { "TenantIpNetworkID", "AttachmentSetID" },
                unique: true);
        }
    }
}
