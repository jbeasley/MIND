using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SigmaCloudManager.Migrations
{
    public partial class update7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Md5Password",
                table: "BgpPeer",
                newName: "PeerPassword");

            migrationBuilder.RenameColumn(
                name: "AutonomousSystem",
                table: "BgpPeer",
                newName: "Peer2ByteAutonomousSystem");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PeerPassword",
                table: "BgpPeer",
                newName: "Md5Password");

            migrationBuilder.RenameColumn(
                name: "Peer2ByteAutonomousSystem",
                table: "BgpPeer",
                newName: "AutonomousSystem");
        }
    }
}
