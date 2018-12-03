using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Mind.Migrations
{
    public partial class update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequiresSync",
                table: "Vpn");

            migrationBuilder.DropColumn(
                name: "ShowRequiresSyncAlert",
                table: "Vpn");

            migrationBuilder.DropColumn(
                name: "RequiresSync",
                table: "Vif");

            migrationBuilder.DropColumn(
                name: "ShowRequiresSyncAlert",
                table: "Vif");

            migrationBuilder.DropColumn(
                name: "RequiresSync",
                table: "Attachment");

            migrationBuilder.DropColumn(
                name: "ShowRequiresSyncAlert",
                table: "Attachment");

            migrationBuilder.AddColumn<int>(
                name: "NetworkStatus",
                table: "Vpn",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NetworkStatus",
                table: "Vif",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NetworkStatus",
                table: "Attachment",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NetworkStatus",
                table: "Vpn");

            migrationBuilder.DropColumn(
                name: "NetworkStatus",
                table: "Vif");

            migrationBuilder.DropColumn(
                name: "NetworkStatus",
                table: "Attachment");

            migrationBuilder.AddColumn<bool>(
                name: "RequiresSync",
                table: "Vpn",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ShowRequiresSyncAlert",
                table: "Vpn",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RequiresSync",
                table: "Vif",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ShowRequiresSyncAlert",
                table: "Vif",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RequiresSync",
                table: "Attachment",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ShowRequiresSyncAlert",
                table: "Attachment",
                nullable: false,
                defaultValue: false);
        }
    }
}
