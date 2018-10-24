using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SigmaCloudManager.Migrations
{
    public partial class update30 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TrustReceivedCosDscp",
                table: "ContractBandwidthPool",
                newName: "TrustReceivedCosAndDscp");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TrustReceivedCosAndDscp",
                table: "ContractBandwidthPool",
                newName: "TrustReceivedCosDscp");
        }
    }
}
