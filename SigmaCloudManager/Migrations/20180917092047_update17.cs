using Microsoft.EntityFrameworkCore.Migrations;

namespace SigmaCloudManager.Migrations
{
    public partial class update17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Location_Location_AlternateLocationLocationID",
                table: "Location");

            migrationBuilder.DropIndex(
                name: "IX_Location_AlternateLocationLocationID",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "AlternateLocationLocationID",
                table: "Location");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AlternateLocationLocationID",
                table: "Location",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Location_AlternateLocationLocationID",
                table: "Location",
                column: "AlternateLocationLocationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Location_AlternateLocationLocationID",
                table: "Location",
                column: "AlternateLocationLocationID",
                principalTable: "Location",
                principalColumn: "LocationID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
